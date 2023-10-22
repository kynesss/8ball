using UnityEngine;

namespace Cue.Inputs
{
    public class CueMouseController : CueInputController
    {
        protected override void Update()
        {
            if (Input.GetMouseButtonDown(0))
                DragHandler.BeginDrag();

            if (Input.GetMouseButton(0))
                DragHandler.Drag();

            if (Input.GetMouseButtonUp(0))
            {
                Physics.Hit();
                DragHandler.EndDrag();
            }

            MovementHandler.HandleMovement(CueBall.transform.position);
        }
    }
}