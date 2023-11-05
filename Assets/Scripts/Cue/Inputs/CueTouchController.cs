using UnityEngine;

namespace Cue.Inputs
{
    public class CueTouchController : CueInputController
    {
        protected override void Update()
        {
            if (Input.touchCount < 1)
                return;

            var touchPhase = Input.GetTouch(0).phase;
            if (touchPhase != TouchPhase.Moved)
                return;
            
            MovementHandler.HandleMovement();
        }
    }
}