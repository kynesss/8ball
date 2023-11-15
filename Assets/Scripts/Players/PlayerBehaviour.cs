using Elympics;

namespace Players
{
    public class PlayerBehaviour : ElympicsMonoBehaviour, IInputHandler, IUpdatable
    {
        private readonly ElympicsFloat _powerSynchronized = new();
        public readonly ElympicsBool IsDraggingSynchronized = new();
        
        private float _powerInput;
        private bool _dragInput;
        
        public float Power
        {
            get => _powerSynchronized.Value;
            set => _powerInput = value;
        }
        public bool IsDragging
        {
            get => IsDraggingSynchronized.Value;
            set => _dragInput = value;
        }
        
        public void OnInputForClient(IInputWriter writer)
        {
            writer.Write(_powerInput);
            writer.Write(_dragInput);
        }

        public void OnInputForBot(IInputWriter inputSerializer) { }
        
        public void ElympicsUpdate()
        {
            if (!ElympicsBehaviour.TryGetInput(PredictableFor, out var reader))
                return;
            
            reader.Read(out float power);
            reader.Read(out bool drag);
            
            _powerSynchronized.Value = power;
            IsDraggingSynchronized.Value = drag;
        }
    }
}
