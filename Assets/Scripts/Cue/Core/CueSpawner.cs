using Common;
using Cue.Inputs;
using Medicine;
using UnityEngine;

namespace Cue.Core
{
    public class CueSpawner : MonoBehaviour
    {
        [Header("Cue prefabs")]
        [SerializeField] private CueMouseController mouseCue;
        [SerializeField] private CueTouchController touchCue;

        [Inject.Single] private GameManager GameManager { get; }
        
        private void Awake()
        {
            Spawn();
        }

        private void Spawn()
        {
            if (Application.isEditor)
            {
                Instantiate(GameManager.MobileModeOn ? touchCue : mouseCue);
                return;
            }
            
            if (Application.isMobilePlatform)
                Instantiate(touchCue);
            else
                Instantiate(mouseCue);
        }
    }
}