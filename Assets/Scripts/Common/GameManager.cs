using System;
using Players;
using UnityEngine;

namespace Common
{ 
    public class GameManager : MonoBehaviourSingleton<GameManager>
    {
        [field: SerializeField] private bool mobileModeOn;
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

        public static void SetNextTurn()
        {
            var nextPlayer = PlayerManager.CurrentPlayerId == 0 ? 1 : 0;
            SetTurnForPlayer(nextPlayer);
        }
    }
}
