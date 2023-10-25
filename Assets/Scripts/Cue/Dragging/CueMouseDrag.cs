using Balls;
using Common;
using Medicine;
using UnityEngine;

namespace Cue.Dragging
{
    public class CueMouseDrag : MonoBehaviour, IDragHandler
    {
        [Header("Drag Settings")] 
        [SerializeField] private float minDrag = 0.25f;
        [SerializeField] private float maxDrag = 5f;
        [SerializeField] private float dragSpeed = 10f;
        
        [Inject.Single] private MouseController Mouse { get; }
        [Inject.Single] private WhiteBall WhiteBall { get; }

        private Vector2 _currentDragPosition;
        public float DragStrength { get; private set; }
        public bool IsDragging { get; private set; }
        public Vector2 DragDirection { get; private set; }
        
        private void OnEnable()
        {
            EndDrag();
        }
        
        public void BeginDrag()
        {
            _currentDragPosition = Mouse.GetWorldPosition();
            IsDragging = true;
            DragDirection = (_currentDragPosition - WhiteBall.Rb.position).normalized;
        }

        public void Drag(float value)
        {
            var mousePosition = Mouse.GetWorldPosition();
            var dragDirection = (mousePosition - _currentDragPosition).normalized;
            var dragScalar = Vector2.Dot(dragDirection, transform.right);
            var drag = DragStrength - dragScalar * dragSpeed * value;

            DragStrength = Mathf.Clamp(drag, minDrag, maxDrag);
            _currentDragPosition = mousePosition;
        }
        
        public void EndDrag()
        {
            IsDragging = false;
            DragStrength = minDrag;
        }
    }
}