using System.Linq;
using Medicine;
using UnityEngine;

namespace Balls
{
    [Register.Single]
    public class BallController : MonoBehaviour
    {
        [SerializeField] private Ball[] balls;
        public bool AllBallsAreStationary => balls.All(x => x.Rb.velocity == Vector2.zero || x.IsPotted);
    }
}