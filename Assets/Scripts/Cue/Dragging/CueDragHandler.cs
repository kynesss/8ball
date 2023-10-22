using Balls;
using Common;
using Medicine;
using UnityEngine;

namespace Cue.Dragging
{
    public class CueDragHandler : MonoBehaviour
    {
        [Header("Drag Settings")] 
        [SerializeField] private float minDrag;
        [SerializeField] private float maxDrag;
        [SerializeField] private float dragSpeed;

        [Inject.Single] private MouseController Mouse { get; }
        [Inject.Single] private WhiteBall WhiteBall { get; }
        
        private Vector2 _currentDragPosition;
        public float DragStrength { get; private set; }
        
        public bool IsDragging { get; private set; }
        
        public Vector2 AimDirection { get; private set; }

        private void OnEnable()
        {
            ResetDrag();
        }

        private void ResetDrag()
        {
            IsDragging = false;
            DragStrength = minDrag;
        }
        
        public void BeginDrag()
        {
            _currentDragPosition = Mouse.GetWorldPosition();
            IsDragging = true;
            AimDirection = (transform.right - WhiteBall.transform.position).normalized;
        }

        public void Drag()
        {
            var mousePosition = Mouse.GetWorldPosition();
            var dragDirection = (mousePosition - _currentDragPosition).normalized;
            var dragScalar = Vector2.Dot(dragDirection, transform.right);
            var drag = DragStrength - dragScalar * dragSpeed * Time.deltaTime;

            DragStrength = Mathf.Clamp(drag, minDrag, maxDrag);
            _currentDragPosition = mousePosition;
        }

        public void EndDrag()
        {
            if (DragStrength <= minDrag)
                DragStrength = minDrag;
            
            ResetDrag();
        }
    }
}