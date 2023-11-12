using Medicine;
using Players;
using UnityEngine;

namespace Common
{
    [Register.Single]
    public class GameManager : MonoBehaviour
    {
        [field: SerializeField] public bool MobileModeOn { get; private set; }
        public static bool IsMyTurn => PlayerManager.CurrentPlayerId.Value == PlayerManager.LocalPlayerId;

        private void Awake()
        {
            PlayerManager.CurrentPlayerId.ValueChanged += OnTurnChanged;
        }

        private void Start()
        {
            SetTurnForPlayer(0);
        }

        private void OnTurnChanged(int lastId, int newId)
        {
            
        }

        public void SetTurnForPlayer(int playerId)
        {
            PlayerManager.CurrentPlayerId.Value = playerId;
        }
    }
}
