using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.Collections;
using MLAPI.NetworkVariable;

public class VariableStuff : NetworkBehaviour
{
    public NetworkVariable<float> amount = new NetworkVariable<float>(new NetworkVariableSettings { WritePermission = NetworkVariablePermission.Everyone }, 20);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [ServerRpc (RequireOwnership = false)]
    public void changeValueServerRpc(int num)
    {
        amount.Value += num;
        print("value: " + amount.Value);
    }

    
}
