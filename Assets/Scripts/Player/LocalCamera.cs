using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class LocalCamera : MonoBehaviourPun
{
    private Camera _camera;
    private AudioListener _audioListener;

    void Start()
    {
        _camera = GetComponent<Camera>();
        _audioListener = GetComponent<AudioListener>();
        if (photonView.IsMine is false)
        {
            _camera.enabled = false;
            _audioListener.enabled = false;
        }
    }
}
