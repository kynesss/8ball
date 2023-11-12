using System;
using Medicine;
using Players;
using UnityEngine;

namespace Common
{
    [Register.Single]
    public class GameManager : MonoBehaviour
    {
        [field: SerializeField] public bool MobileModeOn { get; private set; }
        public static event Action TurnChanged; 
        public static bool IsMyTurn => PlayerManager.CurrentPlayerId == PlayerManager.LocalPlayerId;

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
        
        private void SetTurnForPlayer(int playerId)
        {
            PlayerManager.Instance.CurrentPlayerIdSynchronized.Value = playerId;
        }
    }
}
