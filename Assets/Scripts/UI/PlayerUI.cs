// using System.Collections;
// using System.Collections.Generic;
// using Photon.Pun;
// using UnityEngine;
// using UnityEngine.EventSystems;
// using UnityEngine.UI;

// public class PlayerUI : MonoBehaviourPun
// {
//     public FirstPersonShooter firstPersonShooter;
//     public Text ammoText;
//     public GameObject eventSystem;

//     void Start()
//     {
//         ammoText = GetComponentInChildren<Text>();
//         // firstPersonShooter = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonShooter>();
//     }


//     void Update()
//     {
//         if (!photonView.IsMine)
//         {
//             gameObject.SetActive(false);
//             eventSystem.SetActive(false);
//         }
//         if (firstPersonShooter == null)
//         {
//             firstPersonShooter = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonShooter>();    
//         }
//         ammoText.text = $"{firstPersonShooter._magazine}/{firstPersonShooter._totalAmmo}";
//     }

// }
