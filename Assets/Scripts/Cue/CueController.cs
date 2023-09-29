using Balls;
using Common;
using UnityEngine;

namespace Cue
{
    public class CueController : MonoBehaviour
    {
        [Header("Drag Settings")] 
        [SerializeField] private float minDrag;
        [SerializeField] private float maxDrag;
        [SerializeField] private float dragSpeed;
        [SerializeField] private float strengthMultiplier;

        [Header("References")] 
        [SerializeField] private Rigidbody2D whiteBall;
        [SerializeField] private GameObject sprite;

        private CueCrosshair _crosshair;
        
        private Vector2 _currentDragPosition;
        private Vector2 _aimDirection;
        private Vector2 _movementDirection;

        private float _radians;
        private float _degrees;

        private bool _enabled;

        public float DragStrength { get; private set; }

        private void Awake()
        {
            _crosshair = GetComponent<CueCrosshair>();
            Enable();
        }

        private void Enable()
        {
            transform.position = whiteBall.position;
            DragStrength = minDrag;
            sprite.SetActive(true);
            _enabled = true;

            _crosshair.enabled = true;
        }

        private void Disable()
        {
            sprite.SetActive(false);
            _enabled = false;

            _crosshair.enabled = false;
        }

        private void Update()
        {
            if (BallController.AllBallsAreStationary && !_enabled)
                Enable();

            if (!_enabled)
                return;

            var mousePosition = MouseController.GetWorldPosition();
            _movementDirection = (mousePosition - whiteBall.position).normalized;

            if (Input.GetMouseButtonDown(0))
                BeginDrag(mousePosition);

            if (Input.GetMouseButton(0))
                Drag(mousePosition);
            else
                CalculateAngles(_movementDirection);

            if (Input.GetMouseButtonUp(0))
                EndDrag();
            else
                HandleMovement();
        }

        private void CalculateAngles(Vector2 mouseDirection)
        {
            _radians = Mathf.Atan2(mouseDirection.y, mouseDirection.x);
            _degrees = _radians * Mathf.Rad2Deg;
        }

        private void HandleMovement()
        {
            var horizontal = whiteBall.position.x - Mathf.Cos(_radians) * DragStrength;
            var vertical = whiteBall.position.y - Mathf.Sin(_radians) * DragStrength;

            transform.position = new Vector2(horizontal, vertical);
            transform.rotation = Quaternion.Euler(0f, 0f, _degrees);
        }

        private void BeginDrag(Vector2 mousePosition)
        {
            _currentDragPosition = mousePosition;
            _aimDirection = _movementDirection;
        }

        private void Drag(Vector2 mousePosition)
        {
            var dragDirection = (mousePosition - _currentDragPosition).normalized;
            var dragScalar = Vector2.Dot(dragDirection, transform.right);
            var drag = DragStrength - dragScalar * dragSpeed * Time.deltaTime;

            DragStrength = Mathf.Clamp(drag, minDrag, maxDrag);
            _currentDragPosition = mousePosition;
        }

        private void EndDrag()
        {
            if (DragStrength <= minDrag)
            {
                DragStrength = minDrag;
                return;
            }

            Hit();
            Disable();
        }

        private void Hit()
        {
            var strength = DragStrength * strengthMultiplier;
            whiteBall.AddForce(_aimDirection * strength, ForceMode2D.Impulse);
        }
    }
}