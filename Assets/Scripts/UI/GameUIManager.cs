using Common;
using Cue.UI;
using Medicine;
using UnityEngine;

namespace UI
{
    public class GameUIManager : MonoBehaviour
    {
        [Inject.Single] private GameManager GameManager { get; }
        [Inject.FromChildren] private CueDragSliderUI CueDragSlider { get; }

        private void Awake()
        {
            if (!Application.isMobilePlatform && !GameManager.MobileModeOn)
                CueDragSlider.gameObject.SetActive(false);
            else
                CueDragSlider.gameObject.SetActive(true);
        }
    }
}
