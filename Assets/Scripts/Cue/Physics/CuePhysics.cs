using Balls;
using Cue.Dragging;
using Medicine;
using UnityEngine;

namespace Cue.Physics
{
    public class CuePhysics : MonoBehaviour
    {
        [SerializeField] private float strengthMultiplier;

        [Inject] private CueDragHandler DragHandler { get; }
        [Inject.Single] private WhiteBall WhiteBall { get; }
        
        public void Hit()
        {
            var force = DragHandler.AimDirection * (DragHandler.DragStrength * strengthMultiplier);
            WhiteBall.Rb.AddForce(force, ForceMode2D.Impulse);
        }
    }
}