using System;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class InitialUserData
{
    public int InitialSoftCurrency;
    public bool TutorialEnabled;
}

[Serializable]
public class TutorialConfiguration
{
    public bool IsEnabled;
}

[Serializable]
public class IsInitializedConfiguration
{
    public bool IsInitialized;
}

public class PlayfabLogin : MonoBehaviour
{
    [SerializeField]
    private Image _image;

    public void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        LoginWithAndroidDeviceIDRequest androidRequest = new LoginWithAndroidDeviceIDRequest
        {
            AndroidDeviceId = SystemInfo.deviceUniqueIdentifier, CreateAccount = true
        };
        
        PlayFabClientAPI.LoginWithAndroidDeviceID(androidRequest, OnLoginSuccess, OnLoginFailure);
#else
        var request = new LoginWithCustomIDRequest { CustomId = "GettingStartedGuide2", CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
#endif
    }

    private void OnLoginSuccess(LoginResult result)
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest { Keys = new List<string> { "IsInitialized" } },
            dataResult =>
            {
                bool isInitialized = dataResult.Data.ContainsKey("IsInitialized");
                if (!isInitialized)
                {
                    Debug.Log("InitializePlayer");
                    InitializePlayer();
                }
            }, error => { });

        _image.color = Color.green;
    }

    private void InitializePlayer()
    {
        GetTitleDataRequest request = new GetTitleDataRequest
        {
            Keys = new List<string> { "InitialUserData" }
        };

        PlayFabClientAPI.GetTitleData(request, OnInitialUserDataSuccess, OnInitialUserDataFailure);
    }

    private void OnInitialUserDataSuccess(GetTitleDataResult dataResult)
    {
        string data = dataResult.Data["InitialUserData"];
        InitialUserData initialUserData = JsonUtility.FromJson<InitialUserData>(data);

        AddUserVirtualCurrencyRequest currencyRequest = new AddUserVirtualCurrencyRequest
        {
            Amount = initialUserData.InitialSoftCurrency,
            VirtualCurrency = "SC"
        };

        PlayFabClientAPI.AddUserVirtualCurrency(currencyRequest, result => { },
            error => { });

        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {
                    "Tutorial",
                    JsonUtility.ToJson(new TutorialConfiguration { IsEnabled = initialUserData.TutorialEnabled })
                },
                { "IsInitialized", JsonUtility.ToJson(new IsInitializedConfiguration { IsInitialized = true }) }
            }
        }, result => { }, error => { });
    }

    private void OnInitialUserDataFailure(PlayFabError obj)
    {
        throw new System.NotImplementedException();
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("Something went wrong with your first API call.  :(");
        Debug.LogError("Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
        _image.color = Color.red;
    }
}