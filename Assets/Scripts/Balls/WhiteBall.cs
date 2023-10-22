using Medicine;
using UnityEngine;

namespace Balls
{ 
    [Register.Single]
    public class WhiteBall : Ball
    {
        [Inject] public CircleCollider2D CircleCollider { get; }
    }
}