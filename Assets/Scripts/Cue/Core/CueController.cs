using Balls;
using Common;
using Cue.Movement;
using Cue.Physics;
using Cue.Visuals;
using Elympics;
using Medicine;
using Players;
using UnityEngine;

namespace Cue.Core
{
    public class CueController : ElympicsMonoBehaviour
    {
        [Inject.Single] private BallController BallController { get; }

        [Inject] private IMovementHandler MovementHandler { get; }
        [Inject] private CueCrosshair Crosshair { get; }
        [Inject] public CuePhysics Physics { get; }
        [Inject.FromChildren] private MonoBehaviour[] Handlers { get; }
        [Inject.FromChildren] private CueVisuals CueVisuals { get; }
        private bool IsCurrentPlayer => (int)PredictableFor == PlayerManager.CurrentPlayerId;
        
        private void Start()
        {
            Physics.BallHit += () => SetHandlersEnabled(false);
            SetHandlersEnabled(IsCurrentPlayer);
        }

        private void OnEnable()
        {
            GameManager.TurnChanged += GameManager_OnTurnChanged;
        }

        private void OnDisable()
        {
            GameManager.TurnChanged -= GameManager_OnTurnChanged;
        }

        private void GameManager_OnTurnChanged()
        {
            SetHandlersEnabled(IsCurrentPlayer);
        }

        private void SetHandlersEnabled(bool enable)
        {
            foreach (var handler in Handlers)
            {
                if (handler == this)
                    continue;

                handler.enabled = enable;
            }

            CueVisuals.enabled = enable;
        }
    }
}