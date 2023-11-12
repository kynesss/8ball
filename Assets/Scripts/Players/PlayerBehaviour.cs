using Elympics;

namespace Players
{
    public class PlayerBehaviour : ElympicsMonoBehaviour, IInputHandler, IUpdatable
    {
        private readonly ElympicsFloat _synchronizedPower = new();
        private float _powerInput;
        public float Power
        {
            get => _synchronizedPower.Value;
            set => _powerInput = value;
        }

        public ElympicsBool Connected { get; } = new();

        public void OnInputForClient(IInputWriter writer)
        {
            writer.Write(_powerInput);
        }

        public void OnInputForBot(IInputWriter inputSerializer) { }
        
        public void ElympicsUpdate()
        {
            if (!ElympicsBehaviour.TryGetInput(PredictableFor, out var reader))
                return;
            
            reader.Read(out float power);
            _synchronizedPower.Value = power;
        }
    }
}
