using Balls;
using Cue.Dragging;
using Cue.Movement;
using Cue.Visuals;
using Medicine;
using UnityEngine;

namespace Cue.Core
{
    public class CueController : MonoBehaviour
    {
        [SerializeField] private bool touchEnabled;
        
        [Inject] private CueMouseMovement MouseMovement { get; }
        [Inject] private CueTouchMovement TouchMovement { get; }
        [Inject] private CueDragHandler DragHandler { get; }
        [Inject] protected CueCrosshair Crosshair { get; }
        [Inject.FromChildren] protected CueVisuals CueVisuals { get; }
        
        private void Awake()
        {
            if (Application.isEditor)
            {
                MouseMovement.enabled = !touchEnabled;
                TouchMovement.enabled = touchEnabled;
                return;
            }
            
            if (Application.isMobilePlatform)
            {
                MouseMovement.enabled = false;
                TouchMovement.enabled = true;
            }
            else
            {
                MouseMovement.enabled = true;
                TouchMovement.enabled = false;
            }
        }

        private void Start()
        {
            SetInteractable(true);
        }

        private void Update()
        {
            if (BallController.AllBallsAreStationary)
                SetInteractable(true);
        }
        
        public void SetInteractable(bool interactable)
        {
            Crosshair.enabled = interactable;
            DragHandler.enabled = interactable;
            CueVisuals.enabled = interactable;
        }
    }
}