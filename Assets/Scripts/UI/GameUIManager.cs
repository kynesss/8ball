using Common;
using Cue.UI;
using Medicine;
using UnityEngine;

namespace UI
{
    public class GameUIManager : MonoBehaviour
    {
        [Inject.FromChildren] private CueDragSliderUI CueDragSlider { get; }

        private void Awake()
        {
            if (!Application.isMobilePlatform && !GameManager.IsMobileModeOn)
                CueDragSlider.gameObject.SetActive(false);
            else
                CueDragSlider.gameObject.SetActive(true);
        }
    }
}
