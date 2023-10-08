using Balls;
using Cue.Dragging;
using Cue.Movement;
using UnityEngine;

namespace Cue
{
    public class CueInputController : MonoBehaviour
    {
        [SerializeField] private GameObject sprite;

        private CueCrosshair _crosshair;
        private CueDragHandler _dragHandler;
        private CueMovementHandler _movementHandler;
        
        private Vector2 _aimDirection;

        private void Awake()
        {
            _crosshair = GetComponent<CueCrosshair>(); //medicine
            _dragHandler = GetComponent<CueDragHandler>();
            _movementHandler = GetComponent<CueMovementHandler>();
            
            SetInteractable(true);
        }

        private void SetInteractable(bool interactable)
        {
            sprite.SetActive(interactable);
            
            _crosshair.enabled = interactable;
            _dragHandler.enabled = interactable;
            _movementHandler.enabled = interactable;
        }

        private void Update()
        {
            if (BallController.AllBallsAreStationary)
                SetInteractable(true);
            
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
                
                SetInteractable(false);
            }
            else
                _movementHandler.HandleMovement(dragStrength);
        }
    }
}