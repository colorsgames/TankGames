using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

using TMPro;

using Photon.Pun;
using Photon.Realtime;

namespace Com.COLORSGAMES.TANKGAMES
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        public static bool isBlueTeam;
        public static bool isRedTeam;

        [SerializeField]
        private GameObject playerPrefab;
        [SerializeField]
        private DebugTextController debugText;
        [SerializeField]
        private GameObject teamSelection;
        [SerializeField]
        private GameObject uiControllers;
        [SerializeField]
        private GameObject mobileUIControllers;
        [SerializeField]
        private GameObject[] redTeamSpawners;
        [SerializeField]
        private GameObject[] blueTeamSpawners;

        Scene curretScene;

        int randomSpawn;

        private void Awake()
        {
            if(Application.platform == RuntimePlatform.Android)
            {
                mobileUIControllers.SetActive(true);
            }

            curretScene = SceneManager.GetActiveScene();
            teamSelection.SetActive(true);
            uiControllers.SetActive(false);
            //PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(Random.Range(-8, 8), 3, 0), Quaternion.identity, 0);
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

        public void RedTeam()
        {
            isRedTeam = true;
            randomSpawn = Random.Range(0, redTeamSpawners.Length);
            PhotonNetwork.Instantiate(playerPrefab.name, redTeamSpawners[randomSpawn].transform.position, redTeamSpawners[randomSpawn].transform.rotation, 0);
            NormalUI();
        }

        public void BlueTeam()
        {
            isBlueTeam = true;
            randomSpawn = Random.Range(0, blueTeamSpawners.Length);
            PhotonNetwork.Instantiate(playerPrefab.name, blueTeamSpawners[randomSpawn].transform.position, blueTeamSpawners[randomSpawn].transform.rotation, 0);
            NormalUI();
        }

        void NormalUI()
        {
            teamSelection.SetActive(false);
            uiControllers.SetActive(true);
        }
    }
}
