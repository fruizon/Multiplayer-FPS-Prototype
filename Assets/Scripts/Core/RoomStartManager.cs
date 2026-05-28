using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class RoomStartManager : MonoBehaviourPunCallbacks
{
    public int PlayersInRoom;
    public Text CurrentAmountPlayers;
    public GameObject startButton;

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        CheckPlayers();
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        CheckPlayers();
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        CheckPlayers();
    }

    private void CheckPlayers()
    {
        int playersAmount = PhotonNetwork.CurrentRoom.PlayerCount;
        CurrentAmountPlayers.text = $"{playersAmount}/2";

        if (playersAmount <= 0)
        {
            startButton.SetActive(false);
        }

        if (playersAmount == 1)
        {
            if(PhotonNetwork.IsMasterClient) startButton.SetActive(true);
        }
    }

    public void LoadLevel1()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Game");
            
        }
    }
}
