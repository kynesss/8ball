using UnityEngine;

namespace Common
{
    [RequireComponent(typeof(Camera))]
    public class MouseController : MonoBehaviour
    {
        private static MouseController _instance;
        
        private Camera _mainCamera;

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            _mainCamera = Camera.main;
        }

        public static Vector2 GetWorldPosition()
        {
            return _instance._mainCamera.ScreenToWorldPoint(Input.mousePosition);
        }

        public static Ray GetRay()
        {
            return _instance._mainCamera.ScreenPointToRay(Input.mousePosition);
        }
    }
}