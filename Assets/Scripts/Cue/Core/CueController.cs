using Balls;
using Cue.Dragging;
using Cue.Movement;
using Cue.Physics;
using Cue.Visuals;
using Medicine;
using UnityEngine;

namespace Cue.Core
{
    public class CueController : MonoBehaviour
    {
        [Inject] private IMovementHandler MovementHandler { get; }
        [Inject] private CueDragHandler DragHandler { get; }
        [Inject] private CueCrosshair Crosshair { get; }
        [Inject] private CuePhysics Physics { get; }
        [Inject.FromChildren] private CueVisuals CueVisuals { get; }
        [Inject.Single] private BallController BallController { get; }
        
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
            SetInteractable(true);
        }

        private void Disable()
        {
            SetInteractable(false);
        }

        private void SetInteractable(bool interactable)
        {
            Crosshair.enabled = interactable;
            DragHandler.enabled = interactable;
            CueVisuals.enabled = interactable;
            Physics.enabled = interactable;
        }
    }
}