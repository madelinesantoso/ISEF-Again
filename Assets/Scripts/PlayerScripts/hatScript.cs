using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.Spawning;
using MLAPI.NetworkVariable;
using UnityEngine.UI;
using System.Text;

public class hatScript : NetworkBehaviour
{
    public NetworkVariableBool cowboyHat = new NetworkVariableBool();
    public GameObject hat;
    public GameObject cowboyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        cowboyHat.Value = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsLocalPlayer)
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                cowboyHat.Value = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            hatServerRpc();
        }
    }

    [ServerRpc (RequireOwnership = false)]
    public void hatServerRpc()
    {
        print("bro");
    }
}
