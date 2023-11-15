using Elympics;
using UnityEngine;

namespace Cue.Inputs
{
    public class CueTouchController : CueInputController
    {
        private Vector2 _touchDeltaPositionInput;

        public override void OnInputForClient(IInputWriter writer)
        {
            writer.Write(_touchDeltaPositionInput.x);
            writer.Write(_touchDeltaPositionInput.y);
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

            _touchDeltaPositionInput = touch.deltaPosition;
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