using UnityEngine;

namespace Common.Utils
{
    public static class Vector2Extension
    {
        public static Vector2 GetDirection(this Vector2 vector)
        {
            if (Mathf.Abs(vector.x) > Mathf.Abs(vector.y))
                return vector.x > 0f ? Vector2.right : Vector2.left;

            return vector.y > 0f ? Vector2.up : Vector2.down;
        }
    }
}