using System;
using Balls;
using Cue.Core;
using Medicine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using IDragHandler = Cue.Dragging.IDragHandler;

namespace Cue.UI
{
    public class CueDragSliderUI : MonoBehaviour
    {
        [Inject.Single] private WhiteBall WhiteBall { get; }
        [Inject.Single] private CueController CueController { get; }
        [Inject.Single] private BallController BallController { get; }
        [Inject.FromChildren] private Slider DragSlider { get; }
        [Inject.FromChildren] private EventTrigger EventTrigger { get; }
        
        private void Update()
        {
            DragSlider.enabled = BallController.AllBallsAreStationary;
            EventTrigger.enabled = BallController.AllBallsAreStationary;
        }
        
        public void BeginDrag()
        {
            CueController.DragHandler.BeginDrag();
        }

        public void Drag()
        {
            CueController.DragHandler.Drag(DragSlider.value);
        }

        public void EndDrag()
        {
            if (DragSlider.value > 0f)
                CueController.Physics.Hit();
            
            CueController.DragHandler.EndDrag();
            DragSlider.value = 0f;
        }
    }
}
