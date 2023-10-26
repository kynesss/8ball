using Balls;
using Common;
using Cue.Dragging;
using Medicine;
using UnityEngine;

namespace Cue.Movement
{
    public class CueMouseMovement : MonoBehaviour, IMovementHandler
    {
        [Inject.Single] private MouseController Mouse { get; }
        [Inject.Single] private WhiteBall WhiteBall { get; }
        
        [Inject] private IDragHandler DragHandler { get; }

        private float _radians;
        private float _degrees;

        private Vector2 Center => WhiteBall.transform.position;
        
        public void HandleMovement()
        {
            if (!DragHandler.IsDragging)
            {
                var direction = Mouse.GetWorldPosition() - Center;
                direction.Normalize();
                
                _radians = Mathf.Atan2(direction.y, direction.x);
                _degrees = _radians * Mathf.Rad2Deg;
            }
            
            var horizontal = Center.x - Mathf.Cos(_radians) * DragHandler.DragStrength;
            var vertical = Center.y - Mathf.Sin(_radians) * DragHandler.DragStrength;

            transform.position = new Vector2(horizontal, vertical);
            transform.rotation = Quaternion.Euler(0f, 0f, _degrees);
        }
    }
}