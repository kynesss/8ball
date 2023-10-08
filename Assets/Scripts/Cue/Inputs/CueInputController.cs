using Cue.Dragging;
using Cue.Movement;
using Cue.Visuals;
using Medicine;
using UnityEngine;

namespace Cue.Inputs
{
    public abstract class CueInputController : MonoBehaviour
    {
        [Inject] protected CueCrosshair Crosshair { get; }
        [Inject] protected CueDragHandler DragHandler { get; }
        [Inject] protected CueMovementHandler MovementHandler { get; }
        [Inject.FromChildren] protected CueVisuals CueVisuals { get; }

        protected void Awake()
        {
            SetInteractable(true);
        }

        protected void SetInteractable(bool interactable)
        {
            Crosshair.enabled = interactable;
            DragHandler.enabled = interactable;
            MovementHandler.enabled = interactable;
            CueVisuals.enabled = interactable;
        }

        protected abstract void Update();
    }
}