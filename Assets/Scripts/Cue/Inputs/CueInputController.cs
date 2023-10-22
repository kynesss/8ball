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

        protected CueDragHandler DragHandler;
        protected CuePhysics Physics;
        protected IMovementHandler MovementHandler;

        protected abstract void Awake();
        protected abstract void Update();
    }
}