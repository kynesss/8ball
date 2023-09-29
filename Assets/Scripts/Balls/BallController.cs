using System.Linq;
using UnityEngine;

namespace Balls
{
    public class BallController : MonoBehaviour
    {
        private static BallController _instance;
        
        [SerializeField] private Ball[] allBalls;
        
        public static bool AllBallsAreStationary => _instance.allBalls.All(x => x.Rb.velocity == Vector2.zero || x.IsPotted);

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
        }
    }
}