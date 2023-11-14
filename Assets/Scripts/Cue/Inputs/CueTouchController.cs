using Elympics;
using UnityEngine;

namespace Cue.Inputs
{
    public class CueTouchController : CueInputController
    {
        private Vector2 _touchDeltaPosition;
        private bool _dragging;

        public override void OnInputForClient(IInputWriter writer)
        {
            writer.Write(_touchDeltaPosition.x);
            writer.Write(_touchDeltaPosition.y);
        }

        public override void Update()
        {
            if (Elympics.Player != PredictableFor)
                return;

            if (Input.touchCount < 1)
                return;
            
            var touch = Input.GetTouch(0);
            if (touch.phase is not (TouchPhase.Moved or TouchPhase.Ended))
                return;
            
            _touchDeltaPosition = touch.deltaPosition;
        }

        public override void ElympicsUpdate()
        {
            if (!ElympicsBehaviour.TryGetInput(PredictableFor, out var reader))
                return;
            
            reader.Read(out float horizontal);
            reader.Read(out float vertical);

            MovementHandler.HandleMovement(new Vector2(horizontal, vertical), Elympics.TickDuration);
        }
    }
}