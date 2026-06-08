using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class WeaponLayersController : MonoBehaviour
{

    private PhotonView photonView;

    void Start()
    {
        photonView = GetComponentInParent<PhotonView>();

        if (photonView.IsMine)
        {
            SetLayerRecursively(gameObject, LayerMask.NameToLayer("Weapon"));
        }
        else
        {
            SetLayerRecursively(gameObject, LayerMask.NameToLayer("RemoteWeapon"));
        }
    }

    void SetLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer;

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, layer);
        }
    }
}
