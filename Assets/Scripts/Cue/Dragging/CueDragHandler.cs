using UnityEngine;

namespace Cue.Dragging
{
    public class CueDragHandler : MonoBehaviour, IDragHandler
    {
        [Header("Drag Settings")] 
        [SerializeField] private float minDrag;
        [SerializeField] private float maxDrag;
        [SerializeField] private float dragSpeed;
        [SerializeField] private float strengthMultiplier;
        
        private Vector2 _currentDragPosition;
        public float DragStrength { get; private set; }
        
        public void BeginDrag(Vector2 mousePosition)
        {
            _currentDragPosition = mousePosition;
            DragStrength = minDrag;
        }

        public void Drag(Vector2 mousePosition)
        {
            var dragDirection = (mousePosition - _currentDragPosition).normalized;
            var dragScalar = Vector2.Dot(dragDirection, transform.right);
            var drag = DragStrength - dragScalar * dragSpeed * Time.deltaTime;

            DragStrength = Mathf.Clamp(drag, minDrag, maxDrag);
            _currentDragPosition = mousePosition;
        }

        public void EndDrag()
        {
            if (!(DragStrength <= minDrag)) 
                return;
            
            DragStrength = minDrag;
        }
    }
}