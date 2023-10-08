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
        
        public Vector2 MovementDirection { get; private set; }

        private void OnEnable()
        {
            ResetPosition();
        }

        private void Update()
        {
            CalculateMovementDirection();
        }

        private void ResetPosition()
        {
            transform.position = cueBall.position;
        }

        private void CalculateMovementDirection()
        {
            MovementDirection = (MouseController.GetWorldPosition() - cueBall.position).normalized;
        }

        public void CalculateAngles()
        {
            _radians = Mathf.Atan2(MovementDirection.y, MovementDirection.x);
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

        public void Hit(Vector2 direction, float dragStrength)
        {
            var strength = dragStrength * strengthMultiplier;
            cueBall.AddForce(direction * strength, ForceMode2D.Impulse);
        }
    }
}