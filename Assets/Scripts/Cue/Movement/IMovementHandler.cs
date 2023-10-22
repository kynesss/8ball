using UnityEngine;

namespace Cue.Movement
{
    public interface IMovementHandler
    {
        void HandleMovement(Vector2 center);
    }
}