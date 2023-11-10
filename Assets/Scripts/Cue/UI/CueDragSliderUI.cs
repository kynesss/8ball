using Balls;
using Cue.Core;
using Elympics;
using Medicine;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Cue.UI
{
    public class CueDragSliderUI : ElympicsMonoBehaviour, IUpdatable, IInputHandler
    {
        [Inject.Single] private WhiteBall WhiteBall { get; }
        [Inject.Single] private CueController CueController { get; }
        [Inject.Single] private BallController BallController { get; }
        [Inject.FromChildren] private Slider DragSlider { get; }
        [Inject.FromChildren] private SliderHitArea HitArea { get; }
        [Inject.FromChildren] private EventTrigger EventTrigger { get; }
        
        private readonly ElympicsFloat _dragValue = new();
        
        private bool _beginDrag;
        private bool _drag;
        private bool _endDrag;

        private float _sliderValue;

        private void OnEnable()
        {
            DragSlider.onValueChanged.AddListener(DragSlider_OnValueChanged);
            _dragValue.ValueChanged += DragValue_ValueChanged;
        }

        private void OnDisable()
        {
            DragSlider.onValueChanged.RemoveListener(DragSlider_OnValueChanged);
            _dragValue.ValueChanged -= DragValue_ValueChanged;
        }

        private void DragSlider_OnValueChanged(float value)
        {
            _sliderValue = value;
        }

        private void DragValue_ValueChanged(float lastValue, float newValue)
        {
            if (Elympics.Player == PredictableFor)
                return;
            
            DragSlider.value = newValue;
        }

        private void Update()
        {
            DragSlider.enabled = BallController.AllBallsAreStationary;
            EventTrigger.enabled = BallController.AllBallsAreStationary;
            
            if (Elympics.Player != PredictableFor)
                return;

            if (Input.touchCount < 1)
                return;
            
            var phase = Input.GetTouch(0).phase;
            _beginDrag = phase is TouchPhase.Began;
            _drag = phase is TouchPhase.Moved or TouchPhase.Stationary;
            _endDrag = phase is TouchPhase.Ended;
        }

        private void BeginDrag()
        {
            CueController.DragHandler.BeginDrag();
        }

        private void Drag(float dragValue)
        {
            _dragValue.Value = dragValue;
            CueController.DragHandler.Drag(dragValue);
        }

        private void EndDrag()
        {
            if (DragSlider.value > 0f)
                CueController.Physics.Hit();

            CueController.DragHandler.EndDrag();
            DragSlider.value = 0f;
        }

        public void ElympicsUpdate()
        {
            if (!ElympicsBehaviour.TryGetInput(PredictableFor, out var reader))
                return;

            reader.Read(out float dragValue);
            reader.Read(out bool beginDrag);
            reader.Read(out bool drag);
            reader.Read(out bool endDrag);
            
            if (beginDrag)
                BeginDrag();

            if (drag)
                Drag(dragValue);

            if (endDrag)
                EndDrag();
        }

        public void OnInputForClient(IInputWriter writer)
        {
            writer.Write(_sliderValue);
            writer.Write(_beginDrag);
            writer.Write(_drag);
            writer.Write(_endDrag);
        }

        public void OnInputForBot(IInputWriter inputSerializer)
        {
        }
    }
}