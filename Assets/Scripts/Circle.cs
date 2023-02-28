using System;
using DefaultNamespace;
using DefaultNamespace.Extensions;
using UnityEngine;

public class Circle : MonoBehaviour
{
    public event Action OnPressed;
    
    [SerializeField]
    private GameObject _gameObject;

    [SerializeField]
    private Transform _transform;

    [SerializeField]
    private LimitsSpawner _limitsSpawner;
    
    private void Awake()
    {
        Disable();
    }

    public void Enable()
    {
        _gameObject.Enable();
    }

    public void Disable()
    {
        _gameObject.Disable();
    }
    
    public void Process()
    {
        Disable();
        OnPressed?.Invoke();
    }

    public void SetPosition(Vector3 getRandomPosition)
    {
        _transform.position = getRandomPosition;
    }

    public void SetRandomPosition()
    {
        _transform.position = _limitsSpawner.GetRandomPosition();
    }
}
