using System;
using Balls;
using Cysharp.Threading.Tasks;
using Medicine;
using Players;
using UnityEngine;

namespace Common
{ 
    public class GameManager : MonoBehaviourSingleton<GameManager>
    {
        [field: SerializeField] private bool mobileModeOn;
        [Inject.Single] private BallController BallController { get; }
        public static event Action TurnChanged; 
        public static bool IsMyTurn => PlayerManager.CurrentPlayerId == PlayerManager.LocalPlayerId;
        public static bool IsMobileModeOn => Instance.mobileModeOn;

        private void Start()
        {
            PlayerManager.Instance.CurrentPlayerIdSynchronized.ValueChanged += OnTurnChanged;
            SetTurnForPlayer(0);
        }

        private void OnDisable()
        {
            PlayerManager.Instance.CurrentPlayerIdSynchronized.ValueChanged -= OnTurnChanged;
        }

        private void OnTurnChanged(int lastId, int newId)
        {
            TurnChanged?.Invoke();
        }
        
        private static void SetTurnForPlayer(int playerId)
        {
            PlayerManager.Instance.CurrentPlayerIdSynchronized.Value = playerId;
        }

        public static async UniTask SetNextTurnAsync()
        {
            var nextPlayer = PlayerManager.CurrentPlayerId == 0 ? 1 : 0;
            await UniTask.WaitUntil(() => Instance.BallController.AllBallsAreStationary);
            SetTurnForPlayer(nextPlayer);
        }
    }
}
