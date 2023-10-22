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

        [Inject] protected CueDragHandler DragHandler { get; }
        [Inject] protected CuePhysics Physics { get; }
        [Inject] protected IMovementHandler MovementHandler { get; }
        
        protected abstract void Update();
    }
}