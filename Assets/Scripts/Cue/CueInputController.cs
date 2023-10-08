using Balls;
using Cue.Dragging;
using Cue.Movement;
using Medicine;
using UnityEngine;

namespace Cue
{
    public class CueInputController : MonoBehaviour
    {
        [SerializeField] private GameObject sprite;
        [Inject] private CueCrosshair Crosshair { get; }
        [Inject] private CueDragHandler DragHandler { get; }
        [Inject] private CueMovementHandler MovementHandler { get; }

        private void Awake()
        {
            SetInteractable(true);
        }

        private void SetInteractable(bool interactable)
        {
            sprite.SetActive(interactable);
            Crosshair.enabled = interactable;
            DragHandler.enabled = interactable;
            MovementHandler.enabled = interactable;
        }

        private void Update()
        {
            if (BallController.AllBallsAreStationary)
                SetInteractable(true);
            
            var dragStrength = DragHandler.DragStrength;
            
            if (Input.GetMouseButtonDown(0))
                DragHandler.BeginDrag();

            if (Input.GetMouseButton(0))
                DragHandler.Drag();
            else
                MovementHandler.CalculateAngles();

            if (Input.GetMouseButtonUp(0))
            {
                DragHandler.EndDrag();
                MovementHandler.Hit(dragStrength);
                
                SetInteractable(false);
            }
            else
                MovementHandler.HandleMovement(dragStrength);
        }
    }
}