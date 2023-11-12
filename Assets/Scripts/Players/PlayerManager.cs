using System;
using Elympics;
using UnityEngine;

namespace Players
{
    public class PlayerManager : ElympicsMonoBehaviour, IServerHandlerGuid
    {
        [SerializeField] private PlayerBehaviour[] players;
        
        private static PlayerManager _instance;

        public static Action<int> PlayerConnected;
        
        public static readonly ElympicsInt CurrentPlayerId = new();
        public static PlayerBehaviour LocalPlayerBehaviour => _instance.players[LocalPlayerId];
        public static int LocalPlayerId => (int)_instance.Elympics.Player;
        
        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
        }

        public void OnServerInit(InitialMatchPlayerDatasGuid initialMatchPlayerDatas)
        {
        
        }
        
        public void OnPlayerConnected(ElympicsPlayer player)
        {
            PlayerConnected?.Invoke((int)player);
        }

        public void OnPlayerDisconnected(ElympicsPlayer player)
        {
            
        }

        public static PlayerBehaviour GetPlayerById(int id)
        {
            return _instance.players[id];
        }

        public static PlayerBehaviour GetCurrentPlayer()
        {
            return _instance.players[CurrentPlayerId.Value];
        }
    }
}