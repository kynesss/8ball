using Balls;
using Cue.Dragging;
using Medicine;
using UnityEngine;
using System;
using Common;

namespace Cue.Physics
{
    public class CuePhysics : MonoBehaviour
    {
        [SerializeField] private float strengthMultiplier;

        [Inject] private IDragHandler DragHandler { get; }
        [Inject.Single] private WhiteBall WhiteBall { get; }
        [Inject.Single] private MouseController Mouse { get; }
        public event Action OnHit;

        public void Hit()
        {
            var force = DragHandler.DragDirection * (DragHandler.DragStrength * strengthMultiplier);
            WhiteBall.Rb.AddForce(force, ForceMode2D.Impulse);
            
            OnHit?.Invoke();
        }
    }
}