namespace Cue.Inputs
{
    public class DesktopCueInputController : CueInputController
    {
        protected override void Update()
        {
            base.Update();
            
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                AimDirection = MovementDirection;
                DragHandler.BeginDrag(MousePosition);
            }

            if (UnityEngine.Input.GetMouseButton(0))
                DragHandler.Drag(MousePosition);
            else
                MovementHandler.CalculateAngles(MovementDirection);

            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                DragHandler.EndDrag();
                Hit();
                Disable();
            }
            else
                MovementHandler.HandleMovement(DragHandler.DragStrength);
        }
    }
}