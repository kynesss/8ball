using Balls;
using Cue.Dragging;
using Cue.Movement;
using Cue.Physics;
using Elympics;
using Medicine;

namespace Cue.Inputs
{
    public abstract class CueInputController : ElympicsMonoBehaviour, IUpdatable, IInputHandler
    {
        [Inject.Single] protected WhiteBall CueBall { get; }

        [Inject] protected IDragHandler DragHandler { get; }
        [Inject] protected IMovementHandler MovementHandler { get; }
        [Inject] protected CuePhysics Physics { get; }
        
        public void OnInputForBot(IInputWriter writer) { }
        
        public abstract void OnInputForClient(IInputWriter writer);
        public abstract void Update();
        public abstract void ElympicsUpdate();
    }
}