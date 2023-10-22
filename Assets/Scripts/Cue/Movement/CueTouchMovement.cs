using Common;
using Cue.Dragging;
using Medicine;
using UnityEngine;

namespace Cue.Movement
{
    public class CueTouchMovement : MonoBehaviour, IMovementHandler
    {
       [Inject] private CueDragHandler DragHandler { get; }
        
        private float _degrees;
        
        public void HandleMovement(Vector2 center)
        {
            var touch = Input.GetTouch(0);
            var deltaTime = touch.deltaTime;
            var magnitude = touch.deltaPosition.magnitude;
            
            var directionMultiplier = CalculateDirectionMultiplier(touch.GetDirection());
            _degrees = Mathf.Lerp(_degrees, _degrees + (directionMultiplier * magnitude), deltaTime);
            
            var radians = _degrees * Mathf.Deg2Rad;
            var horizontal = center.x - Mathf.Cos(radians) * DragHandler.DragStrength;
            var vertical = center.y - Mathf.Sin(radians) * DragHandler.DragStrength;

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