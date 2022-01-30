using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

using TMPro;

using Photon.Pun;
using Photon.Realtime;

namespace Com.COLORSGAMES.TANKGAMES
{
    public class PlayManager : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private GameObject playerPrefab;
        [SerializeField]
        private DebugTextController debugText;

        Scene curretScene;

        private void Start()
        {
            curretScene = SceneManager.GetActiveScene();
            PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(Random.Range(-8, 8), 3, 0), Quaternion.identity, 0);
        }
        public void RestartScene()
        {
            SceneManager.LoadScene(curretScene.name);
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            debugText.Log(newPlayer.NickName + " join");
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            debugText.Log(otherPlayer.NickName + " left");
        }
    }
}
