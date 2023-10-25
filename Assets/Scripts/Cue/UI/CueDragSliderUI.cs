using Balls;
using Cue.Core;
using Cue.Dragging;
using Medicine;
using UnityEngine;
using UnityEngine.UI;

namespace Cue.UI
{
    public class CueDragSliderUI : MonoBehaviour
    {
        [Inject.Single] private WhiteBall WhiteBall { get; }
        [Inject.Single] private CueController CueController { get; }
        [Inject.FromChildren] private Slider DragSlider { get; }

        private IDragHandler _dragHandler;

        private void Start()
        {
            _dragHandler = CueController.DragHandler;
        }

        public void BeginDrag()
        {
            _dragHandler.BeginDrag();
        }

        public void Drag()
        {
            _dragHandler.Drag(DragSlider.value);
        }

        public void EndDrag()
        {
            CueController.Physics.Hit();
            _dragHandler.EndDrag();
            DragSlider.value = 0f;
        }
    }
}
