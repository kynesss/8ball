using Balls;
using Common;
using Cue.Dragging;
using Cue.Movement;
using UnityEngine;

namespace Cue.Inputs
{
    public abstract class CueInputController : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private Rigidbody2D whiteBall;
        [SerializeField] private GameObject sprite;
        
        private CueCrosshair _crosshair;

        protected CueMovementHandler MovementHandler;
        protected IDragHandler DragHandler;
        
        protected Vector2 AimDirection;
        protected Vector2 MovementDirection;
        protected Vector2 MousePosition;
        
        private bool _enabled;

        private void Awake()
        {
            _crosshair = GetComponent<CueCrosshair>();
            DragHandler = GetComponent<IDragHandler>();
            MovementHandler = GetComponent<CueMovementHandler>();
            
            Enable();
        }

        private void Enable()
        {
            transform.position = whiteBall.position;
            //DragStrength = minDrag;
            sprite.SetActive(true);
            _enabled = true;

            _crosshair.enabled = true;
        }

        protected void Disable()
        {
            sprite.SetActive(false);
            _enabled = false;

            _crosshair.enabled = false;
        }

        protected virtual void Update()
        {
            if (BallController.AllBallsAreStationary && !_enabled)
                Enable();

            if (!_enabled)
                return;

            MousePosition = MouseController.GetWorldPosition();
            MovementDirection = (MousePosition - whiteBall.position).normalized;
        }
        
        protected void Hit()
        {
            /*var strength = DragStrength * strengthMultiplier;
            whiteBall.AddForce(AimDirection * strength, ForceMode2D.Impulse);*/
        }
    }
}