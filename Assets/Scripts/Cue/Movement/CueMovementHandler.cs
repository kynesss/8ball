using Common;
using UnityEngine;

namespace Cue.Movement
{
    public class CueMovementHandler : MonoBehaviour
    {
        [SerializeField] private float strengthMultiplier;
        [SerializeField] private Rigidbody2D cueBall;

        private float _radians;
        private float _degrees;

        private Vector2 _movementDirection;
        private Vector2 _hitDirection;

        private void OnEnable()
        {
            ResetPosition();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                _hitDirection = _movementDirection;
                
            CalculateMovementDirection();
        }

        private void ResetPosition()
        {
            transform.position = cueBall.position;
        }

        private void CalculateMovementDirection()
        {
            _movementDirection = (MouseController.GetWorldPosition() - cueBall.position).normalized;
        }

        public void CalculateAngles()
        {
            _radians = Mathf.Atan2(_movementDirection.y, _movementDirection.x);
            _degrees = _radians * Mathf.Rad2Deg;
        }

        public void HandleMovement(float dragStrength)
        {
            var cueBallPosition = cueBall.position;

            var horizontal = cueBallPosition.x - Mathf.Cos(_radians) * dragStrength;
            var vertical = cueBallPosition.y - Mathf.Sin(_radians) * dragStrength;

            transform.position = new Vector2(horizontal, vertical);
            transform.rotation = Quaternion.Euler(0f, 0f, _degrees);
        }

        public void Hit(float dragStrength)
        {
            var strength = dragStrength * strengthMultiplier;
            cueBall.AddForce(_hitDirection * strength, ForceMode2D.Impulse);
        }
    }
}