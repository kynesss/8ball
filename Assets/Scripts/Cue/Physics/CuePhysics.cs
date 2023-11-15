using System;
using Balls;
using Common;
using Medicine;
using UnityEngine;
using Players;

namespace Cue.Physics
{
    public class CuePhysics : MonoBehaviour
    {
        [SerializeField] private float strengthMultiplier;
        [Inject.Single] private WhiteBall WhiteBall { get; }
        private PlayerBehaviour CurrentPlayer => PlayerManager.GetCurrentPlayer();

        public event Action BallHit;

        private void Start()
        {
            CurrentPlayer.IsDraggingSynchronized.ValueChanged += IsDraggingSynchronized_OnValueChanged;
        }

        private async void IsDraggingSynchronized_OnValueChanged(bool lastValue, bool newValue)
        {
            if (newValue)
                return;

            if (CurrentPlayer.Power < 0.25f)
                return;
            
            Hit();

            if (GameManager.IsMyTurn)
                PlayerManager.LocalPlayer.Power = 0f;
            
            await GameManager.SetNextTurnAsync();
        }
        
        private void Hit()
        {
            var force = transform.right * (CurrentPlayer.Power * strengthMultiplier);
            WhiteBall.Rb.AddForce(force, ForceMode2D.Impulse);
            BallHit?.Invoke();
        }
    }
}