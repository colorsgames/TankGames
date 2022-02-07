using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

using Photon.Pun;

namespace Com.COLORSGAMES.TANKGAMES
{
    public class InputFieldController : MonoBehaviour
    {
        const string playerNameKey = "PlayerName";

        private void Start()
        {
            string defaultName = string.Empty;
            TMP_InputField _inputField = this.GetComponent<TMP_InputField>();
            if(_inputField != null)
            {
                if (PlayerPrefs.HasKey(playerNameKey))
                {
                    defaultName = PlayerPrefs.GetString(playerNameKey);
                    _inputField.text = defaultName;
                }
            }

            PhotonNetwork.NickName = defaultName;
        }

        public void SetPlayerName(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                Debug.LogError("valuse is null");
                return;
            }

            PhotonNetwork.NickName = value;

            PlayerPrefs.SetString(playerNameKey, value);
        }
    }
}
