using System;
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

            switch (touchPhase)
            {
                case TouchPhase.Began:
                {
                    break;
                }
                case TouchPhase.Moved:
                {
                    MovementHandler.HandleMovement(CueBall.transform.position);
                    break;
                }
                case TouchPhase.Stationary:
                {
                    break;
                }
                case TouchPhase.Ended:
                {
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}