using Elympics;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class MainMenuUI : MonoBehaviour
    {
        [Header("Half remote")]
        [SerializeField] private Button halfRemoteServerButton;
        [SerializeField] private Button halfRemoteClientButton;

        [Header("Matchmaking")] 
        [SerializeField] private Button matchmakingButton;

        private void Awake()
        {
            halfRemoteServerButton.onClick.AddListener(HalfRemoteServerButton_OnClick);
            halfRemoteClientButton.onClick.AddListener(HalfRemoteClientButton_OnClick);
            matchmakingButton.onClick.AddListener(MatchmakingButton_OnClick);
        }

        private void HalfRemoteServerButton_OnClick()
        {
            ElympicsLobbyClient.Instance.StartHalfRemoteServer();
        }

        private void HalfRemoteClientButton_OnClick()
        {
            ElympicsLobbyClient.Instance.PlayHalfRemote(1);
        }

        private void MatchmakingButton_OnClick()
        {
            ElympicsLobbyClient.Instance.PlayOnlineInRegion("warsaw", null, null, "Test");
        }
    }
}
