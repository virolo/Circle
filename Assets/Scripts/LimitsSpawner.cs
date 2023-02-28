using DefaultNamespace.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class LimitsSpawner : MonoBehaviour
    {
        [SerializeField]
        private Camera _mainCamera;

        [SerializeField]
        [Range(0, 1)]
        private float _topOffset;

        [SerializeField]
        [Range(0, 1)]
        private float _bottomOffset;

        [SerializeField]
        [Range(0, 1)]
        private float _leftOffset;

        [SerializeField]
        [Range(0, 1)]
        private float _rightOffset;

        [SerializeField]
        private Transform _topLeftHandle;

        [SerializeField]
        private Transform _topRightHandle;

        [SerializeField]
        private Transform _bottomLeftHandle;

        [SerializeField]
        private Transform _bottomRightHandle;

        [SerializeField]
        private bool _showLimits;

        private void Awake()
        {
            if (_showLimits)
            {
                ShowLimits();
            }
        }

        private void OnValidate()
        {
            if (_showLimits)
            {
                ShowLimits();
            }
            else
            {
                HideLimits();
            }
        }

        private void ShowLimits()
        {
            _topLeftHandle.gameObject.Enable();
            _topRightHandle.gameObject.Enable();
            _bottomLeftHandle.gameObject.Enable();
            _bottomRightHandle.gameObject.Enable();
            
            RepositionLimits();
        }

        private void RepositionLimits()
        {
            Vector3 topLeft = GetHandlePosition(GetLeftLimit(), GetTopLimit());
            Vector3 topRight = GetHandlePosition(GetRightLimit(), GetTopLimit());
            Vector3 bottomLeft = GetHandlePosition(GetLeftLimit(), GetBottomLimit());
            Vector3 bottomRight = GetHandlePosition(GetRightLimit(), GetBottomLimit());

            _topLeftHandle.position = topLeft;
            _topRightHandle.position = topRight;
            _bottomLeftHandle.position = bottomLeft;
            _bottomRightHandle.position = bottomRight;
        }

        private Vector3 GetHandlePosition(float x, float y)
        {
            Vector3 position = new Vector3(x, y, _mainCamera.farClipPlane / 2);
            return _mainCamera.ScreenToWorldPoint(position);
        }

        private void HideLimits()
        {
            _topLeftHandle.gameObject.Disable();
            _topRightHandle.gameObject.Disable();
            _bottomLeftHandle.gameObject.Disable();
            _bottomRightHandle.gameObject.Disable();
        }
        
        public Vector3 GetRandomPosition()
        {
            float x = Random.Range(GetLeftLimit(), GetRightLimit());
            float y = Random.Range(GetBottomLimit(), GetTopLimit());

            float z = _mainCamera.farClipPlane / 2;

            return _mainCamera.ScreenToWorldPoint(new Vector3(x, y, z));
        }

        private float GetTopLimit()
        {
            return _topOffset * Screen.safeArea.height;
        }

        private float GetBottomLimit()
        {
            return (1 - _bottomOffset) * Screen.safeArea.height;
        }

        private float GetRightLimit()
        {
            return _rightOffset * Screen.safeArea.width;
        }

        private float GetLeftLimit()
        {
            return (1 - _leftOffset) * Screen.safeArea.width;
        }
    }
}