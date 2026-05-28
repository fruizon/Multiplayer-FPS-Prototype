using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PlayerManager : MonoBehaviourPun
{
    public List<Transform> Players = new List<Transform>();
    public static PlayerManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    public void Register(Transform player)
    {
        Instance.Players.Add(player);
        StartCoroutine(Instance.CreatePlayerText(player));
    }
    public IEnumerator CreatePlayerText(Transform player){
        yield return new WaitForSeconds(1);

        if(PlayerListUI.Instance != null){
            PlayerListUI.Instance.photonView.RPC(
            "CreatePlayerText",
            RpcTarget.AllBuffered,
            player.name
            );
        }
    }

    public void UnRegister(Transform player)
    {
        Instance.Players.Remove(player);

        PlayerListUI.Instance.photonView.RPC(
            "DeletePlayerText",
            RpcTarget.AllBuffered,
            player.name
        );
    }
}