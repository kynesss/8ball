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
        [Inject.Single] private BallController BallController { get; }
        [Inject.FromChildren] private Slider DragSlider { get; }

        private bool _test;

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
            {
                if (value > 0f)
                    PlayerManager.LocalPlayer.Power = value;
            }
        }

        public void PointerDown()
        {
            if (GameManager.IsMyTurn)
                PlayerManager.LocalPlayer.IsDragging = true;
        }

        public void PointerUp()
        { 
            if (!GameManager.IsMyTurn) 
                return;
            
            DragSlider.value = 0f;
            PlayerManager.LocalPlayer.IsDragging = false;
        }
    }
}