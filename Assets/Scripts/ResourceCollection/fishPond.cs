using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.Spawning;
using MLAPI.NetworkVariable;
using UnityEngine.UI;
using System.Text;

public class fishPond : NetworkBehaviour
{
    public NetworkVariableInt fishAmount = new NetworkVariableInt();
    public NetworkVariable<float> fishStuff = new NetworkVariable<float>(new NetworkVariableSettings { WritePermission = NetworkVariablePermission.Everyone}, 20);

    void Start()
    {
        fishStuff.Value = 20;
        fishAmount.Value = 20;
    }

    public int getFishAmount() //just learned what return stuff does, super awesome :D
    {
        return fishAmount.Value;
    }

    /*void increaseFish()
    {
        fishAmount.Value++;
    }*/

    [ServerRpc(RequireOwnership = false)]
    public void depleteFishServerRpc(int amountTaken)
    {
        fishStuff.Value -= amountTaken;
        print(fishStuff.Value);
        fishAmount.Value -= amountTaken;
    }
}
