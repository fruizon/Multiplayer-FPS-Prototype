using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLauncher : MonoBehaviourPunCallbacks
{
    // public GameObject spawner;
    public GameObject playerSpawn;
    public Text statusText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // PhotonNetwork.ConnectUsingSettings();
        SceneManager.sceneLoaded += OnGameSceneLoaded;
        DontDestroyOnLoad(gameObject);
    }

    private void OnGameSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == "Game")
        {
            playerSpawn = GameObject.Find("TestPlayerSpawn");
            GameObject player = PhotonNetwork.Instantiate("PlayerSet", playerSpawn.transform.position, Quaternion.identity);
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnConnectedToMaster()
    {
        print("Подключились к мастеру");
        statusText.text = $"{statusText.text}\nConnectedToMaster";

        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        print(string.Format("OnJoinedRoom() called by PUN: {0}", PhotonNetwork.CurrentRoom.Name));
        statusText.text = $"{statusText.text}\nOnJoinedRoom() called by PUN: 0 {PhotonNetwork.CurrentRoom.Name}";
    }


    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        print("Не удалось подключиться к комнате: " + message);
        statusText.text = $"{statusText.text}\nНе удалось подключиться к комнате: {message}";

        PhotonNetwork.CreateRoom("Main Room");
    }
}
