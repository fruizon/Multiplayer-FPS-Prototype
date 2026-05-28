using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListUI : MonoBehaviourPun
{
    public Transform panel;
    public GameObject textPrefab;

    private Dictionary<string, GameObject> playerTexts = new();

    public static PlayerListUI Instance;

    private void Awake()
    {
        Instance = this;
    }

    [PunRPC]
    public void CreatePlayerText(string playerName)
    {
        if (playerTexts.ContainsKey(playerName)) return;

        var obj = Instantiate(textPrefab, panel);
        obj.GetComponent<Text>().text = playerName;

        playerTexts[playerName] = obj;
    }

    [PunRPC]
    public void DeletePlayerText(string playerName)
    {
        if (playerTexts.TryGetValue(playerName, out var obj))
        {
            Destroy(obj);
            playerTexts.Remove(playerName);
        }
    }
}