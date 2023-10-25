using Balls;
using Cue.Dragging;
using Cue.Movement;
using Cue.Physics;
using Cue.Visuals;
using Medicine;
using UnityEngine;

namespace Cue.Core
{
    [Register.Single]
    public class CueController : MonoBehaviour
    {
        [Inject.Single] private BallController BallController { get; }
        
        [Inject] private IMovementHandler MovementHandler { get; }
        [Inject] public IDragHandler DragHandler { get; }
        [Inject] private CueCrosshair Crosshair { get; }
        [Inject] public CuePhysics Physics { get; }
        [Inject.FromChildren] private MonoBehaviour[] Handlers { get; }
        [Inject.FromChildren] private CueVisuals CueVisuals { get; }

        private void OnEnable()
        {
            Physics.OnHit += Disable;
        }

        private void OnDisable()
        {
            Physics.OnHit -= Disable;
        }

        private void Start()
        {
            Enable();
        }

        private void Update()
        {
            if (BallController.AllBallsAreStationary)
                Enable();
        }

        private void Enable()
        {
            SetHandlersEnabled(true);
        }

        private void Disable()
        {
            SetHandlersEnabled(false);
        }

        private void SetHandlersEnabled(bool enable)
        {
            foreach (var handler in Handlers)
            {
                if (handler == this)
                    continue;
                
                handler.enabled = enable;
            }
        }
    }
}