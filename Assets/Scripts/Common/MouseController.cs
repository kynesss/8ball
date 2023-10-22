using Medicine;
using UnityEngine;

namespace Common
{
    [Register.Single]
    [RequireComponent(typeof(Camera))]
    public class MouseController : MonoBehaviour
    {
        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        public Vector2 GetWorldPosition()
        {
            return _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}