using Balls;
using Common;
using Cue.Dragging;
using Medicine;
using UnityEngine;

namespace Cue.Movement
{
    public class CueTouchMovement : MonoBehaviour, IMovementHandler
    {
        [SerializeField] private float xOffset;
        [SerializeField] private float speedMultiplier;
        
        [Inject.Single] private WhiteBall WhiteBall { get; }
        [Inject] private IDragHandler DragHandler { get; }

        private float _degrees;
        private float _radians;
        private Vector2 Center => WhiteBall.transform.position;
        
        private void OnEnable()
        {
            SetPositionAndRotation(xOffset);
        }

        public void HandleMovement()
        {
            var touch = Input.GetTouch(0);
            var deltaTime = touch.deltaTime;
            var magnitude = touch.deltaPosition.magnitude;
            var directionMultiplier = CalculateDirectionMultiplier(touch.GetDirection());

            if (!DragHandler.IsDragging)
            {
                _degrees = Mathf.Lerp(_degrees, _degrees + (directionMultiplier * magnitude),  speedMultiplier * deltaTime);
                _radians = _degrees * Mathf.Deg2Rad;
            }

            var radius = Mathf.Max(DragHandler.DragStrength, xOffset);
            SetPositionAndRotation(radius);
        }
        
        private void SetPositionAndRotation(float radius)
        {
            var horizontal = Center.x - Mathf.Cos(_radians) * radius;
            var vertical = Center.y - Mathf.Sin(_radians) * radius;

            transform.position = new Vector2(horizontal, vertical);
            transform.rotation = Quaternion.Euler(0f, 0f, _degrees);
        }

        private static float CalculateDirectionMultiplier(Vector2 direction)
        {
            if (direction == Vector2.left || direction == Vector2.down)
                return -1f;

            return 1f;
        }
    }
}