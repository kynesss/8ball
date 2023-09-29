using UnityEngine;

namespace Cue
{
    [RequireComponent(typeof(LineRenderer))]
    public class CueCrosshair : MonoBehaviour
    {
        private const float ZOffsetMultiplier = 0.001f;
        private const float VelocityMultiplier = 999f;
        private static Vector3 ZOffset => Vector3.forward * ZOffsetMultiplier;

        [Header("Renderers")]
        [SerializeField] private LineRenderer cueBallRenderer;
        [SerializeField] private LineRenderer touchedBallRenderer;

        [Header("Reflection Settings")]
        [SerializeField] private float cueBallPredictionMultiplier;
        [SerializeField] private LayerMask layerMask;

        [Header("References")]
        [SerializeField] private Collider2D cueBall;

        private Vector3 _center;
        private float CircleRadius => (cueBall.bounds.extents.x * 2f) * cueBall.transform.localScale.x;
        
        private void Awake()
        {
            InitializeRenderers();
        }

        private void OnEnable()
        {
            SetupInitialValues();
        }

        private void OnDisable()
        {
            DisableRenderers();
        }

        private void Update()
        {
            var hit = Physics2D.CircleCast(_center, CircleRadius, transform.right, Mathf.Infinity, layerMask);

            if (!hit.collider) 
                return;

            Vector3 collisionPoint = hit.point;
            Vector3 collisionNormal = hit.normal;
            
            UpdateCueBallRenderer(collisionPoint, collisionNormal);
            UpdateTouchedBallRenderer(hit, collisionPoint, collisionNormal);
        }

        private void UpdateCueBallRenderer(Vector3 collisionPoint, Vector3 collisionNormal)
        {
            cueBallRenderer.SetPosition(0, _center - ZOffset);
            cueBallRenderer.SetPosition(1, collisionPoint - ZOffset);

            var velocity = (collisionPoint - cueBall.transform.position) * VelocityMultiplier;

            Vector3 cueBallReflection = Vector2.Reflect(velocity, collisionNormal).normalized;
            cueBallReflection *= cueBallPredictionMultiplier;

            cueBallRenderer.SetPosition(2, collisionPoint + cueBallReflection);
        }

        private void UpdateTouchedBallRenderer(RaycastHit2D hit, Vector3 collisionPoint, Vector3 collisionNormal)
        {
            if (hit.collider.gameObject.layer != LayerMask.NameToLayer("StandardBall"))
            {
                touchedBallRenderer.enabled = false;
                return;
            }

            var touchedBallPosition = hit.transform.position;
            
            touchedBallRenderer.enabled = true;
            touchedBallRenderer.SetPosition(0, touchedBallPosition - ZOffset);
            touchedBallRenderer.SetPosition(1, (collisionPoint - collisionNormal) - ZOffset);
        }

        private void InitializeRenderers()
        {
            cueBallRenderer.positionCount = 3;
            touchedBallRenderer.positionCount = 2;
        }

        private void SetupInitialValues()
        {
            _center = cueBall.transform.position;
            cueBallRenderer.enabled = true;
            touchedBallRenderer.enabled = false;
        }

        private void DisableRenderers()
        {
            cueBallRenderer.enabled = false;
            touchedBallRenderer.enabled = false;
        }
    }
}
