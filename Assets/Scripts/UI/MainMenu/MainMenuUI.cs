using System;
using Elympics;
using GDT.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Button halfRemoteButton;

        private void Awake()
        {
            halfRemoteButton.onClick.AddListener(HalfRemoteButton_OnClick);
        }
        
        private void HalfRemoteButton_OnClick()
        {
            var halfRemoteMode = ElympicsGameConfig.GetHalfRemoteMode(ElympicsGameConfig.HalfRemoteModeEnum.Server);

            switch (halfRemoteMode)
            {
                case ElympicsGameConfig.HalfRemoteModeEnum.Client:
                    DebugX.Log("Starting HalfRemote Client");
                    ElympicsLobbyClient.Instance.PlayHalfRemote(ElympicsGameConfig.GetHalfRemotePlayerIndex(0));
                    break;
                case ElympicsGameConfig.HalfRemoteModeEnum.Server:
                    DebugX.Log("Starting HalfRemote Server");
                    ElympicsLobbyClient.Instance.StartHalfRemoteServer();
                    break;
                case ElympicsGameConfig.HalfRemoteModeEnum.Bot:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
