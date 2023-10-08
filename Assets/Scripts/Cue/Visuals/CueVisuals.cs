using Medicine;
using UnityEngine;

namespace Cue.Visuals
{
    public class CueVisuals : MonoBehaviour
    {
        [Inject] private SpriteRenderer Renderer { get; }
        private void OnEnable() => Renderer.enabled = true;
        private void OnDisable() => Renderer.enabled = false;
    }
}