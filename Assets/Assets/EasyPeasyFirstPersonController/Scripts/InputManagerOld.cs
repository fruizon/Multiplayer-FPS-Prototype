namespace EasyPeasyFirstPersonController
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Photon.Pun;
    using UnityEngine;

    public class InputManagerOld : MonoBehaviourPun
    {
        public Vector2 moveInput;
        public Vector2 lookInput;
        public bool jump;
        public bool sprint;
        public bool crouch;
        public bool slide;
        void Update()
        {
            if (photonView.IsMine)
            {
                moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                lookInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
                jump = Input.GetKey(KeyCode.Space);
                sprint = Input.GetKey(KeyCode.LeftShift);
                crouch = Input.GetKey(KeyCode.LeftControl);
                slide = Input.GetKey(KeyCode.LeftControl);
            }
        }
    }
}