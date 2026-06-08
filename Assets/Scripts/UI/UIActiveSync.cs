using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class UIActiveSync : MonoBehaviour
{
    private PhotonView photonView;
    public GameObject canvas;

    void Start()
    {
        photonView = GetComponentInChildren<PhotonView>();
        if (!photonView.IsMine)
        {
            canvas.SetActive(false);
        }
    }
}
