using Common;
using Elympics;
using Medicine;
using Players;
using UnityEngine;

namespace Cue.Core
{
    public class CueSpawner : ElympicsMonoBehaviour, IUpdatable
    {
        [Inject.Single] private GameManager GameManager { get; }

        private int _playerId = -1;
        private bool CanSpawn => _playerId != -1;

        private void Awake()
        {
            PlayerManager.PlayerConnected += OnPlayerConnected;
        }

        private void OnDestroy()
        {
            PlayerManager.PlayerConnected -= OnPlayerConnected;
        }

        public void ElympicsUpdate()
        {
            if (!Elympics.IsServer)
                return;

            if (!CanSpawn)
                return;

            SpawnCue();
        }

        private void OnPlayerConnected(int newValue)
        {
            Debug.Log($"Player connected: {newValue}");
            _playerId = newValue;
        }

        private void SpawnCue()
        {
            if (Application.isEditor)
            {
                ElympicsInstantiate(GameManager.MobileModeOn ? "Cue (Mobile)" : "Cue (Desktop)",
                    ElympicsPlayer.FromIndex(_playerId));
            }
            else
            {
                ElympicsInstantiate(Application.isMobilePlatform ? "Cue (Mobile)" : "Cue (Desktop)",
                    ElympicsPlayer.FromIndex(_playerId));
            }
            
            _playerId = -1;
        }
    }
}