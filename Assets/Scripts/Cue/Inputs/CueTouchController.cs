using Balls;
using UnityEngine;

namespace Cue.Inputs
{
    public class CueTouchController : CueInputController
    {
        protected override void Update()
        {
            //TODO: Move to abstract class and make this method virtual
            if (BallController.AllBallsAreStationary)
                SetInteractable(true);
            
            var dragStrength = DragHandler.DragStrength;

            var touch = Input.GetTouch(0);
        }
    }
}