using Cue.Dragging;
using Cue.Movement;
using Cue.Physics;
using UnityEngine;

namespace Cue.Inputs
{
    public class CueMouseController : CueInputController
    {
        protected override void Awake()
        {
            MovementHandler = GetComponent<CueMouseMovement>();
            DragHandler = GetComponent<CueDragHandler>();
            Physics = GetComponent<CuePhysics>();
        }

        protected override void Update()
        {
            if (Input.GetMouseButtonDown(0))
                DragHandler.BeginDrag();

            if (Input.GetMouseButton(0))
                DragHandler.Drag();

            if (Input.GetMouseButtonUp(0))
            {
                DragHandler.EndDrag();
                Physics.Hit();
            }

            MovementHandler.HandleMovement(CueBall.transform.position);
        }
    }
}