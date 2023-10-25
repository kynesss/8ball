using Balls;
using Cue.Dragging;
using Cue.Movement;
using Cue.Physics;
using Medicine;
using UnityEngine;

namespace Cue.Inputs
{
    public abstract class CueInputController : MonoBehaviour
    {
        [Inject.Single] protected WhiteBall CueBall { get; }

        [Inject] protected IDragHandler DragHandler { get; }
        [Inject] protected IMovementHandler MovementHandler { get; }
        [Inject] protected CuePhysics Physics { get; }

        protected abstract void Update();
    }
}