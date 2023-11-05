using Balls;
using Common;
using Cue.Dragging;
using Elympics;
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

        private readonly ElympicsFloat _degrees = new();
        private readonly ElympicsFloat _radians = new();

        private Vector2 _lastDeltaPosition;
        
        private Vector2 Center => WhiteBall.transform.position;
        
        private void OnEnable()
        {
            SetPositionAndRotation(xOffset);
        }
        
        public void HandleMovement(Vector2 position, float deltaTime)
        {
            if (position == _lastDeltaPosition)
                return;
             
            var magnitude = position.magnitude;
            var directionMultiplier = CalculateDirectionMultiplier(position.GetDirection());

            if (!DragHandler.IsDragging)
            {
                _degrees.Value = Mathf.Lerp(_degrees, _degrees + (directionMultiplier * magnitude),  speedMultiplier * deltaTime);
                _radians.Value = _degrees * Mathf.Deg2Rad;
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