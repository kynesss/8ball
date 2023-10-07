using UnityEngine;

namespace Cue.Movement
{
    public class CueMovementHandler : MonoBehaviour
    {
        [SerializeField] private Transform cueBall;
        
        private float _radians;
        private float _degrees;
        
        public void CalculateAngles(Vector2 mouseDirection)
        {
            _radians = Mathf.Atan2(mouseDirection.y, mouseDirection.x);
            _degrees = _radians * Mathf.Rad2Deg;
        }

        public void HandleMovement(float dragStrength)
        {
            var position = cueBall.position;
            var horizontal = position.x - Mathf.Cos(_radians) * dragStrength;
            var vertical = position.y - Mathf.Sin(_radians) * dragStrength;

            transform.position = new Vector2(horizontal, vertical);
            transform.rotation = Quaternion.Euler(0f, 0f, _degrees);
        }
    }
}