using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;

using TMPro;

namespace Com.COLORSGAMES.TANKGAMES
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        public byte MaxPlayers;
        public int sceneIndex;

        [SerializeField]
        private GameObject controlPanel;
        [SerializeField]
        private GameObject connectText;
        [SerializeField]
        private DebugTextController debugText;

        private string GameVersion = "1";

        bool isConnect;

        private void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            connectText.SetActive(false);
        }

        public void Connect()
        {
            controlPanel.SetActive(false);
            connectText.SetActive(true);

            isConnect = true;

            if (PhotonNetwork.IsConnected)
            {
                debugText.Log("Join random Room");
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                debugText.Log("Connecting");
                PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = this.GameVersion;
            }
        }

        public override void OnConnectedToMaster()
        {
            if (isConnect)
            {
                debugText.Log("Connect to master");
                PhotonNetwork.JoinRandomRoom();
            }
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = this.MaxPlayers });
        }

        public override void OnJoinedRoom()
        {
            debugText.Log("Join room");
            SceneManager.LoadScene(sceneIndex);
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            debugText.Log("Dictonnected: " + cause);
            isConnect = false;
            controlPanel.SetActive(true);
            connectText.SetActive(false);
        }

    }
}
