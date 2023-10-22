using Cue.Inputs;
using UnityEngine;

namespace Cue.Core
{
    public class CueSpawner : MonoBehaviour
    {
        [Header("Cue prefabs")]
        [SerializeField] private CueMouseController mouseCue;
        [SerializeField] private CueTouchController touchCue;

        [Header("Editor settings")] 
        [SerializeField] private bool touchEnabled;
        
        private void Awake()
        {
            Spawn();
        }

        private void Spawn()
        {
            if (Application.isEditor)
            {
                Instantiate(touchEnabled ? touchCue : mouseCue);
                return;
            }
            
            if (Application.isMobilePlatform)
            {
                Instantiate(touchCue);
            }
            else
            {
                Instantiate(mouseCue);
            }
        }
    }
}