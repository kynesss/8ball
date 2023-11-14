using System;
using Elympics;
using UnityEngine;

namespace Players
{
    public class PlayerManager : ElympicsMonoBehaviour, IServerHandlerGuid
    {
        [SerializeField] private PlayerBehaviour[] players;
        public static PlayerManager Instance { get; private set; }

        public static Action<int> PlayerConnected;
        public static PlayerBehaviour LocalPlayer => Instance.players[LocalPlayerId];
        public static int LocalPlayerId => (int)Instance.Elympics.Player;
        public static int CurrentPlayerId => Instance.CurrentPlayerIdSynchronized.Value;
        
        public readonly ElympicsInt CurrentPlayerIdSynchronized = new(-1);

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }
        
        public void OnServerInit(InitialMatchPlayerDatasGuid initialMatchPlayerDatas)
        {
        }
        
        public void OnPlayerConnected(ElympicsPlayer player)
        {
            var playerId = (int)player;
            PlayerConnected?.Invoke(playerId);
        }

        public void OnPlayerDisconnected(ElympicsPlayer player)
        {
        }

        public static PlayerBehaviour GetPlayerById(int id)
        {
            return Instance.players[id];
        }

        public static PlayerBehaviour GetCurrentPlayer()
        {
            return Instance.players[CurrentPlayerId];
        }
    }
}