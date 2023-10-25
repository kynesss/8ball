using System;
using Common;
using Cue.Dragging;
using Medicine;
using UnityEngine;

namespace Cue.Movement
{
    public class CueTouchMovement : MonoBehaviour, IMovementHandler
    {
       [Inject] private IDragHandler DragHandler { get; }
        
        private float _degrees;
        private float _radians;
        
        public void HandleMovement(Vector2 center)
        {
            var touch = Input.GetTouch(0);
            var deltaTime = touch.deltaTime;
            var magnitude = touch.deltaPosition.magnitude;
            var directionMultiplier = CalculateDirectionMultiplier(touch.GetDirection());
            
            if (!DragHandler.IsDragging)
            {
                _degrees = Mathf.Lerp(_degrees, _degrees + (directionMultiplier * magnitude), deltaTime);
                _radians = _degrees * Mathf.Deg2Rad;
            }

            var horizontal = center.x - Mathf.Cos(_radians) * DragHandler.DragStrength;
            var vertical = center.y - Mathf.Sin(_radians) * DragHandler.DragStrength;

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