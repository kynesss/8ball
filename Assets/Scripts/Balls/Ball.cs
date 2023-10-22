using Cysharp.Threading.Tasks;
using Medicine;
using UnityEngine;

namespace Balls
{
    public class Ball : MonoBehaviour
    {
        private const float VelocityThreshold = 0.1f;
        private const float RotationAngle = 60f;
        [field: SerializeField] public int Number { get; private set; }
        [Inject] public Rigidbody2D Rb { get; private set; }
        
        private BallSpriteController _spriteController;
        private Vector3 _startPosition;
        private Collider2D _collider;
        private SpriteRenderer _spriteRenderer;

        public BallType Type
        {
            get => (BallType)Number;
            set
            {
                value = Number switch
                {
                    0 => BallType.White,
                    < 8 => BallType.Solid,
                    8 => BallType.Black,
                    _ => BallType.Stripe
                };
            }
        }
        public bool IsPotted { get; private set; }
        
        private void Awake()
        {
            _startPosition = transform.position;
            _spriteController = GetComponent<BallSpriteController>();
            _collider = GetComponent<Collider2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            var velocity = Rb.velocity;
            
            if (velocity.magnitude <= VelocityThreshold)
                velocity = Vector2.zero;
            
            if (velocity.magnitude <= 0)
                return;

            Rotate(velocity);
        }

        private void Rotate(Vector2 velocity)
        {
            transform.Rotate(Vector3.forward, RotationAngle * velocity.magnitude * Time.deltaTime);
            _spriteController.UpdateSprites(Mathf.Abs(transform.eulerAngles.z), velocity);
        }

        public async UniTask DisableAsync()
        {
            _collider.enabled = false;
            await UniTask.Delay(Mathf.FloorToInt(100f / Rb.velocity.magnitude));
            _spriteRenderer.enabled = false;
        }
    }
}
