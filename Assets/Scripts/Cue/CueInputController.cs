using Balls;
using Cue.Dragging;
using Cue.Movement;
using UnityEngine;

namespace Cue
{
    public class CueController : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private GameObject sprite;

        private CueCrosshair _crosshair;
        private CueDragHandler _dragHandler;
        private CueMovementHandler _movementHandler;
        
        private Vector2 _aimDirection;
        private bool _enabled;

        private void Awake()
        {
            _crosshair = GetComponent<CueCrosshair>();
            _dragHandler = GetComponent<CueDragHandler>();
            Enable();
        }

        private void Enable()
        {
            sprite.SetActive(true);
            
            _enabled = true;
            _crosshair.enabled = true;
            _dragHandler.enabled = true;
            _movementHandler.enabled = true;
        }

        private void Disable()
        {
            sprite.SetActive(false);
            
            _enabled = false;
            _crosshair.enabled = false;
            _dragHandler.enabled = false;
            _movementHandler.enabled = false;
        }

        private void Update()
        {
            if (BallController.AllBallsAreStationary && !_enabled)
                Enable();

            if (!_enabled)
                return;

            var dragStrength = _dragHandler.DragStrength;
            
            if (Input.GetMouseButtonDown(0))
            {
                _aimDirection = _movementHandler.MovementDirection;   
                _dragHandler.BeginDrag();
            }

            if (Input.GetMouseButton(0))
                _dragHandler.Drag();
            else
                _movementHandler.CalculateAngles();

            if (Input.GetMouseButtonUp(0))
            {
                _dragHandler.EndDrag();
                _movementHandler.Hit(_aimDirection, dragStrength);
                
                Disable();
            }
            else
                _movementHandler.HandleMovement(dragStrength);
        }
    }
}