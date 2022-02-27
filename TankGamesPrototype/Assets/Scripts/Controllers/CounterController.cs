using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class CounterController : MonoBehaviour, IPunObservable
{
    public static CounterController Instance { get; private set; }

    public int RedTeamCount { get; set; }
    public int BlueTeamCount { get; set; }

    [SerializeField]
    private TMP_Text redTeamCounter;
    [SerializeField]
    private TMP_Text blueTeamCounter;

    private void Start()
    {
        Instance = this;

        redTeamCounter.text = System.Convert.ToString(RedTeamCount);
        blueTeamCounter.text = System.Convert.ToString(BlueTeamCount);
    }

    public void TeamCountUpdate(Teams teams, Action action, int count)
    {
        if(teams == Teams.Red)
        {
            if(action == Action.Add)
            {
                RedTeamCount += count;
            }
            else if(action == Action.Remove)
            {
                RedTeamCount -= count;
            }
            redTeamCounter.text = System.Convert.ToString(RedTeamCount);
        }
        else if(teams == Teams.Blue)
        {
            if (action == Action.Add)
            {
                BlueTeamCount += count;
            }
            else if (action == Action.Remove)
            {
                BlueTeamCount -= count;
            }
            blueTeamCounter.text = System.Convert.ToString(BlueTeamCount);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(RedTeamCount);
            stream.SendNext(BlueTeamCount);
        }
        else
        {
            RedTeamCount = (int)stream.ReceiveNext();
            BlueTeamCount = (int)stream.ReceiveNext();

            redTeamCounter.text = System.Convert.ToString(RedTeamCount);
            blueTeamCounter.text = System.Convert.ToString(BlueTeamCount);
        }
    }

    public enum Teams
    {
        Red,
        Blue
    }
    public enum Action
    {
        Add,
        Remove
    }

}
