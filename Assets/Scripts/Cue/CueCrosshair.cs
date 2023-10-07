using UnityEngine;

namespace Cue
{
    [RequireComponent(typeof(LineRenderer))]
    public class CueCrosshair : MonoBehaviour
    {
        [Header("Renderers")]
        [SerializeField] private LineRenderer cueBallRenderer;
        [SerializeField] private LineRenderer touchedBallRenderer;
        [SerializeField] private LineRenderer circleRenderer;

        [Header("Reflection Settings")]
        [SerializeField] private float lineLenghtMultiplier;
        [SerializeField] private float minOffsetToStraightLine;
        [SerializeField] private LayerMask layerMask;

        [Header("References")] 
        [SerializeField] private CircleCollider2D cueBall;

        [Header("Circle render")]
        [SerializeField] private int edgesCount;

        private Vector3 _ballCenter;
        private float CircleRadius => cueBall.radius * cueBall.transform.lossyScale.x;
        
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
            var hit = PerformCircleCast();

            if (!hit.collider)
                return;
            
            Vector3 collisionPoint = hit.point;
            Vector3 collisionNormal = hit.normal;

            DrawCueBallRenderer(hit, collisionPoint, collisionNormal);
            DrawTouchedBallRenderer(hit, collisionPoint, collisionNormal);
        }

        private RaycastHit2D PerformCircleCast()
        {
            return Physics2D.CircleCast(_ballCenter, CircleRadius, transform.right, Mathf.Infinity, layerMask);
        }

        private void DrawCueBallRenderer(RaycastHit2D hit, Vector3 collisionPoint, Vector3 collisionNormal)
        {
            cueBallRenderer.positionCount = 2;
            cueBallRenderer.SetPosition(0, _ballCenter);
            
            var circleCenter = collisionPoint + collisionNormal * CircleRadius;
            cueBallRenderer.SetPosition(1, circleCenter);
            
            DrawCircleRenderer(circleCenter);
            DrawCueBallReflection(hit, collisionPoint, collisionNormal);
        }

        private void DrawTouchedBallRenderer(RaycastHit2D hit, Vector3 collisionPoint, Vector3 collisionNormal)
        {
            if (!IsStandardBall(hit.collider))
            {
                touchedBallRenderer.enabled = false;
                return;
            }

            var touchedBallPosition = hit.transform.position;
            
            touchedBallRenderer.enabled = true;
            touchedBallRenderer.SetPosition(0, touchedBallPosition);

            var touchedBallDirection = (collisionPoint - collisionNormal);
            var touchedBallReflection = (touchedBallDirection - touchedBallPosition).normalized;
            
            var offsetToStraightLine = Vector3.Dot(transform.right, touchedBallReflection.normalized);
            touchedBallReflection *= Mathf.Abs(offsetToStraightLine) * lineLenghtMultiplier;
            
            touchedBallRenderer.SetPosition(1, touchedBallPosition + touchedBallReflection);
        }

        private void DrawCueBallReflection(RaycastHit2D hit, Vector3 collisionPoint, Vector3 collisionNormal)
        {
            if (!IsStandardBall(hit.collider))
                return;

            cueBallRenderer.positionCount = 3;

            var velocity = (collisionPoint - _ballCenter).normalized;
            var cueBallReflection = Vector3.Reflect(velocity, collisionNormal).normalized;

            var offsetToStraightLine = Vector3.Dot(transform.up, cueBallReflection);
            cueBallReflection *= Mathf.Abs(offsetToStraightLine) * lineLenghtMultiplier;

            if (Mathf.Abs(offsetToStraightLine) < minOffsetToStraightLine)
            {
                cueBallRenderer.positionCount = 2;
                return;
            }

            cueBallRenderer.SetPosition(2, collisionPoint + cueBallReflection);
        }

        private static bool IsStandardBall(Collider2D collider)
        {
            return collider.gameObject.layer == LayerMask.NameToLayer("StandardBall");
        }

        private void DrawCircleRenderer(Vector3 circleCenter)
        {
            for (var i = 0; i < edgesCount; i++)
            {
                var progress = (float)i / (edgesCount - 1);
                var radians = progress * 2f * Mathf.PI;

                var cos = Mathf.Cos(radians) * CircleRadius;
                var sin = Mathf.Sin(radians) * CircleRadius;

                var edgePosition = new Vector3(cos, sin, 0f) + circleCenter;
                circleRenderer.SetPosition(i, edgePosition);
            }
        }

        private void InitializeRenderers()
        {
            cueBallRenderer.positionCount = 3;
            touchedBallRenderer.positionCount = 2;
            circleRenderer.positionCount = edgesCount;
        }

        private void SetupInitialValues()
        {
            _ballCenter = cueBall.transform.position;
            cueBallRenderer.enabled = true;
            circleRenderer.enabled = true;
            touchedBallRenderer.enabled = false;
        }

        private void DisableRenderers()
        {
            cueBallRenderer.enabled = false;
            circleRenderer.enabled = false;
            touchedBallRenderer.enabled = false;
        }
    }
}
