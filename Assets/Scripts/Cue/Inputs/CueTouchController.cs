using Elympics;
using UnityEngine;

namespace Cue.Inputs
{
    public class CueTouchController : CueInputController
    {
        private int _touchCount;
        private Vector2 _touchDeltaPosition;

        public override void OnInputForClient(IInputWriter writer)
        {
            writer.Write(_touchCount);
            writer.Write(_touchDeltaPosition.x);
            writer.Write(_touchDeltaPosition.y);
        }

        public override void Update()
        {
            if (Elympics.Player != PredictableFor)
                return;
            
            _touchCount = Input.touchCount;
            
            if (Input.touchCount < 1)
                return;
            
            var touch = Input.GetTouch(0);
            if (touch.phase != TouchPhase.Moved)
                return;
            
            _touchDeltaPosition = touch.deltaPosition;
        }

        public override void ElympicsUpdate()
        {
            if (!ElympicsBehaviour.TryGetInput(PredictableFor, out var reader))
                return;
            
            reader.Read(out int touchCount);
            reader.Read(out float horizontal);
            reader.Read(out float vertical);

            if (touchCount < 1)
                return;
            
            MovementHandler.HandleMovement(new Vector2(horizontal, vertical), Elympics.TickDuration);
        }
    }
}