using Common;
using Elympics;
using Medicine;
using UnityEngine;

namespace Cue.Core
{
    public class CueSpawner : ElympicsMonoBehaviour, IUpdatable
    {
        [Inject.Single] private GameManager GameManager { get; }

        private bool _spawned;
        
        public void ElympicsUpdate()
        {
            if (_spawned)
                return;

            if (Application.isEditor)
                ElympicsInstantiate(GameManager.MobileModeOn ? "Cue (Mobile)" : "Cue (Desktop)", ElympicsPlayer.FromIndex(0));
            else
                ElympicsInstantiate(Application.isMobilePlatform ? "Cue (Mobile)" : "Cue (Desktop)", ElympicsPlayer.FromIndex(0));
            
            _spawned = true;
        }
    }
}