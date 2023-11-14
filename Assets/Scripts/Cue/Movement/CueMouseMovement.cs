using Balls;
using Common;
using Medicine;
using Players;
using UnityEngine;

namespace Cue.Movement
{
    public class CueMouseMovement : MonoBehaviour, IMovementHandler
    {
        [Inject.Single] private MouseController Mouse { get; }
        [Inject.Single] private WhiteBall WhiteBall { get; }

        private float _radians;
        private float _degrees;

        private Vector2 Center => WhiteBall.transform.position;
        private PlayerBehaviour CurrentPlayer => PlayerManager.GetCurrentPlayer();
        
        public void HandleMovement(Vector2 position, float deltaTime)
        {
            if (!CurrentPlayer.IsDragging)
            {
                var direction = Mouse.GetWorldPosition() - Center;
                direction.Normalize();
                
                _radians = Mathf.Atan2(direction.y, direction.x);
                _degrees = _radians * Mathf.Rad2Deg;
            }
            
            var horizontal = Center.x - Mathf.Cos(_radians) * CurrentPlayer.Power;
            var vertical = Center.y - Mathf.Sin(_radians) * CurrentPlayer.Power;

            transform.position = new Vector2(horizontal, vertical);
            transform.rotation = Quaternion.Euler(0f, 0f, _degrees);
        }
    }
}