using System;
using Cue.Dragging;
using Cue.Movement;
using Cue.Physics;
using UnityEngine;

namespace Cue.Inputs
{
    public class CueTouchController : CueInputController
    {
        protected override void Awake()
        {
            MovementHandler = GetComponent<CueTouchMovement>();
            DragHandler = GetComponent<CueDragHandler>();
            Physics = GetComponent<CuePhysics>();
        }

        protected override void Update()
        {
            if (Input.touchCount < 1)
                return;

            var touchPhase = Input.GetTouch(0).phase;

            switch (touchPhase)
            {
                case TouchPhase.Began:
                {
                    //Debug.Log($"Began");
                    break;
                }
                case TouchPhase.Moved:
                {
                    MovementHandler.HandleMovement(CueBall.transform.position);
                    break;
                }
                case TouchPhase.Stationary:
                {
                    //Debug.Log($"Stationary");
                    break;
                }
                case TouchPhase.Ended:
                {
                    //Debug.Log($"Ended");
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}