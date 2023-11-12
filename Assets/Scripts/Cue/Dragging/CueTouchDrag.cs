using Balls;
using Cue.UI;
using Elympics;
using Medicine;
using UnityEngine;

namespace Cue.Dragging
{
    public class CueTouchDrag : ElympicsMonoBehaviour, IDragHandler
    {
        [Header("Drag Settings")] 
        [SerializeField] private float minDrag = 0.25f;
        
        [Inject.Single] private WhiteBall WhiteBall { get; }
        public ElympicsFloat DragStrength { get; } = new();
        public ElympicsBool IsDragging { get; } = new();
        public Vector2 DragDirection { get; private set; }

        private void OnEnable()
        {
            EndDrag();
        }

        private void OnBeginDrag(bool lastValue, bool newValue)
        {
            if (!newValue)
                return;
            
            BeginDrag();
        }

        private void OnDrag(bool lastValue, bool newValue)
        {
            if (!newValue)
                return;
            
            //Drag();
        }

        private void OnEndDrag(bool lastValue, bool newValue)
        {
            if (!newValue)
                return;
            
            EndDrag();
        }

        public void BeginDrag()
        {
            IsDragging.Value = true;
            DragDirection = transform.right;
        }

        public void Drag(float value)
        {
            DragStrength.Value = value;
        }

        public void EndDrag()
        {
            Drag(minDrag);
            IsDragging.Value = false;
            DragStrength.Value = minDrag;
        }
    }
}