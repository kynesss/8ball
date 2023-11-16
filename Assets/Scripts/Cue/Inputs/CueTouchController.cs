using Elympics;
using UnityEngine;

namespace Cue.Inputs
{
    public class CueTouchController : CueInputController
    {
        private Vector2 _touchDeltaPositionInput;
        private int _touchCountInput;

        public override void OnInputForClient(IInputWriter writer)
        {
            writer.Write(_touchDeltaPositionInput.x);
            writer.Write(_touchDeltaPositionInput.y);
            writer.Write(_touchCountInput);
        }

        public override void Update()
        {
            if (Elympics.Player != PredictableFor)
                return;

            _touchCountInput = Input.touchCount;
            
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
            reader.Read(out int touchCount);

            if (touchCount == 0)
            {
                Debug.Log("Touch count 0");
                return;
            }
            
            Debug.Log($"Touch position: {new Vector2(horizontal, vertical)}");
            
            MovementHandler.HandleMovement(new Vector2(horizontal, vertical), Elympics.TickDuration);
        }
    }
}