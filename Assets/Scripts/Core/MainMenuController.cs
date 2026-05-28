using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        PhotonNetwork.ConnectUsingSettings();
        SceneManager.LoadScene("Lobby");
    }
}
