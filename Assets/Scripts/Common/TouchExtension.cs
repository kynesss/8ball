using UnityEngine;

namespace Common
{
    public static class TouchExtension
    {
        public static Vector2 GetDirection(this Touch touch)
        {
            if (Mathf.Abs(touch.deltaPosition.x) > Mathf.Abs(touch.deltaPosition.y))
                return touch.deltaPosition.x > 0f ? Vector2.right : Vector2.left;
            
            return touch.deltaPosition.y > 0f ? Vector2.up : Vector2.down;
        }
    }
}