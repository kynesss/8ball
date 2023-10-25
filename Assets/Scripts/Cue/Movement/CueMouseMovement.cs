using Common;
using Cue.Dragging;
using Medicine;
using UnityEngine;

namespace Cue.Movement
{
    public class CueMouseMovement : MonoBehaviour, IMovementHandler
    {
        [Inject.Single] private MouseController Mouse { get; }
        [Inject] private IDragHandler DragHandler { get; }

        private float _radians;
        private float _degrees;
        
        public void HandleMovement(Vector2 center)
        {
            if (!DragHandler.IsDragging)
            {
                var direction = Mouse.GetWorldPosition() - center;
                direction.Normalize();
                
                _radians = Mathf.Atan2(direction.y, direction.x);
                _degrees = _radians * Mathf.Rad2Deg;
            }
            
            var horizontal = center.x - Mathf.Cos(_radians) * DragHandler.DragStrength;
            var vertical = center.y - Mathf.Sin(_radians) * DragHandler.DragStrength;

            transform.position = new Vector2(horizontal, vertical);
            transform.rotation = Quaternion.Euler(0f, 0f, _degrees);
        }
    }
}