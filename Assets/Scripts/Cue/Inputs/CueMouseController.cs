using Balls;
using UnityEngine;

namespace Cue.Inputs
{
    public class CueMouseController : CueInputController
    {
        protected override void Update()
        {
            if (BallController.AllBallsAreStationary)
                SetInteractable(true);
            
            var dragStrength = DragHandler.DragStrength;
            
            if (Input.GetMouseButtonDown(0))
                DragHandler.BeginDrag();

            if (Input.GetMouseButton(0))
                DragHandler.Drag();
            else
                MovementHandler.CalculateAngles();

            if (Input.GetMouseButtonUp(0))
            {
                DragHandler.EndDrag();
                MovementHandler.Hit(dragStrength);
                
                SetInteractable(false);
            }
            else
                MovementHandler.HandleMovement(dragStrength);
        }
    }
}