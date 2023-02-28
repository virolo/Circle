using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public void OnButtonPressed()
    {
        SceneManager.LoadScene("GameScene");
    }
}
