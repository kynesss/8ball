using System;
using Elympics;
using UnityEngine;

namespace Players
{
    public class PlayerBehaviour : ElympicsMonoBehaviour, IInputHandler, IUpdatable
    {
        private readonly ElympicsFloat _synchronizedPower = new();
        private readonly ElympicsBool _synchronizedDrag = new();
        
        private float _powerInput;
        private bool _dragInput;

        public event Action<bool> DragStateChanged;
        public event Action<float> PowerValueChanged;
        
        public float Power
        {
            get => _synchronizedPower.Value;
            set
            {
                _powerInput = value;
                PowerValueChanged?.Invoke(value);
            }
        }

        public bool IsDragging
        {
            get => _synchronizedDrag.Value;
            set
            {
                _dragInput = value;
                DragStateChanged?.Invoke(value);
            }
        }

        private void Update()
        {
            //Debug.Log($"Player {(int)PredictableFor} Dragging: {IsDragging}");
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
            
            _synchronizedPower.Value = power;
            _synchronizedDrag.Value = drag;
        }
    }
}
