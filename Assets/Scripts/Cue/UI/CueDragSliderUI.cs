using Balls;
using Common;
using Medicine;
using Players;
using UnityEngine;
using UnityEngine.UI;

namespace Cue.UI
{
    public class CueDragSliderUI : MonoBehaviour
    {
        [Inject.Single] private WhiteBall WhiteBall { get; }
        [Inject.Single] private BallController BallController { get; }
        [Inject.FromChildren] private Slider DragSlider { get; }
        
        private void OnEnable()
        {
            DragSlider.onValueChanged.AddListener(DragSlider_ValueChanged);
        }

        private void OnDisable()
        {
            DragSlider.onValueChanged.RemoveListener(DragSlider_ValueChanged);
        }

        private void Update()
        {
            DragSlider.enabled = BallController.AllBallsAreStationary;
            
            if (GameManager.IsMyTurn)
                OnMyTurn();
            else
                OnOtherPlayerTurn();
        }

        private void OnMyTurn()
        {
            DragSlider.interactable = true;
        }

        private void OnOtherPlayerTurn()
        {
            DragSlider.interactable = false;
            DragSlider.SetValueWithoutNotify((PlayerManager.GetCurrentPlayer().Power));
        }

        private void DragSlider_ValueChanged(float value)
        {
            if (GameManager.IsMyTurn)
                PlayerManager.LocalPlayerBehaviour.Power = value;
        }
    }
}