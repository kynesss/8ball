using System;
using UnityEngine;

namespace Balls
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class BallSpriteController : MonoBehaviour
    {
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private SpriteRenderer spriteRenderer;

        public void UpdateSprites(float angle, Vector2 direction)
        {
            var value = angle switch
            {
                < 60f => 0,
                < 120f => 1,
                < 180f => 2,
                < 240f => 3,
                < 300f => 4,
                < 360f => 0,
                _ => throw new ArgumentOutOfRangeException()
            };
            
            spriteRenderer.sprite = sprites[value];
            spriteRenderer.flipX = direction.x < 0f;
            spriteRenderer.flipY = direction.y < 0f;
        }
    }
}
