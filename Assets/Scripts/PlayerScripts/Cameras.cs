using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class Cameras : NetworkBehaviour
{
    public Camera cam;
    public Camera defaultCam;
    //public Camera defaultCam;
    //public GameObject cool;
    // Start is called before the first frame update
    private void Awake()
    {
        defaultCam = Camera.main;
    }
    void Start()
    {
        if (IsLocalPlayer) return;

        cam.enabled = false;
        defaultCam.enabled = false;
        //defaultCam.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
