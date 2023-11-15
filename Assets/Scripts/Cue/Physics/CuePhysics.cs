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

        private void Start()
        {
            CurrentPlayer.IsDraggingSynchronized.ValueChanged += IsDraggingSynchronized_OnValueChanged;
        }

        private void IsDraggingSynchronized_OnValueChanged(bool lastValue, bool newValue)
        {
            if (newValue)
                return;
            
            Hit();

            if (GameManager.IsMyTurn)
                PlayerManager.LocalPlayer.Power = 0f;
            
            GameManager.SetNextTurn();
        }
        
        private void Hit()
        {
            var force = transform.right * (CurrentPlayer.Power * strengthMultiplier);
            WhiteBall.Rb.AddForce(force, ForceMode2D.Impulse);
        }
    }
}