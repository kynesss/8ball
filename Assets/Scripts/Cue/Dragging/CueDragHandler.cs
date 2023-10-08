using Common;
using UnityEngine;

namespace Cue.Dragging
{
    public class CueDragHandler : MonoBehaviour
    {
        [Header("Drag Settings")] 
        [SerializeField] private float minDrag;
        [SerializeField] private float maxDrag;
        [SerializeField] private float dragSpeed;
        
        private Vector2 _currentDragPosition;
        public float DragStrength { get; private set; }

        private void OnEnable()
        {
            DragStrength = minDrag;
        }
        
        public void BeginDrag()
        {
            _currentDragPosition = MouseController.GetWorldPosition();
        }

        public void Drag()
        {
            var mousePosition = MouseController.GetWorldPosition();
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
        }
    }
}