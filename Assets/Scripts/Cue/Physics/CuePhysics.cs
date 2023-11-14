using Balls;
using Medicine;
using UnityEngine;
using Common;
using Players;

namespace Cue.Physics
{
    public class CuePhysics : MonoBehaviour
    {
        [SerializeField] private float strengthMultiplier;
        [Inject.Single] private WhiteBall WhiteBall { get; }
        [Inject.Single] private MouseController Mouse { get; }
        private PlayerBehaviour CurrentPlayer => PlayerManager.GetCurrentPlayer();
        public void Hit()
        {
            var force = transform.right * (CurrentPlayer.Power * strengthMultiplier);
            WhiteBall.Rb.AddForce(force, ForceMode2D.Impulse);
        }
    }
}