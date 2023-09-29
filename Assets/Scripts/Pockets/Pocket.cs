using Balls;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Pockets
{
    public class Pocket : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            var ball = other.GetComponent<Ball>();

            if (ball)
                ball.DisableAsync().Forget();
        }
    }
}
