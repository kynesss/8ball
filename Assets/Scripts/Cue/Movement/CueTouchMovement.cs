using Balls;
using Common.Utils;
using Elympics;
using Medicine;
using Players;
using UnityEngine;

namespace Cue.Movement
{
    public class CueTouchMovement : MonoBehaviour, IMovementHandler
    {   
        [SerializeField] private float xOffset;
        [SerializeField] private float speedMultiplier;
        [Inject.Single] private WhiteBall WhiteBall { get; }

        private readonly ElympicsFloat _degrees = new();
        private readonly ElympicsFloat _radians = new();
        private Vector2 Center => WhiteBall.transform.position;
        private PlayerBehaviour CurrentPlayer => PlayerManager.GetCurrentPlayer();
        
        private void OnEnable()
        {
            SetPositionAndRotation(xOffset);
        }
        
        public void HandleMovement(Vector2 position, float deltaTime)
        {
            var magnitude = position.magnitude;
            var directionMultiplier = CalculateDirectionMultiplier(position.GetDirection());

            if (!CurrentPlayer.IsDragging)
            {
                _degrees.Value = Mathf.Lerp(_degrees.Value, _degrees.Value + (directionMultiplier * magnitude),  speedMultiplier * deltaTime);
                _radians.Value = _degrees.Value * Mathf.Deg2Rad;
            }
            
            var radius = Mathf.Max(CurrentPlayer.Power, xOffset);
            SetPositionAndRotation(radius);
        }
        
        private void SetPositionAndRotation(float radius)
        {
            var horizontal = Center.x - Mathf.Cos(_radians.Value) * radius;
            var vertical = Center.y - Mathf.Sin(_radians.Value) * radius;

            transform.position = new Vector2(horizontal, vertical);
            transform.rotation = Quaternion.Euler(0f, 0f, _degrees.Value);
        }

        private static float CalculateDirectionMultiplier(Vector2 direction)
        {
            if (direction == Vector2.left || direction == Vector2.down)
                return -1f;

            return 1f;
        }
    }
}