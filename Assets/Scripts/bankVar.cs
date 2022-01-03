using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI.Messaging;
using MLAPI.Spawning;
using MLAPI.NetworkVariable;
using UnityEngine.UI;
using System.Text;
using MLAPI;

public class bankVar : NetworkBehaviour
{
    public NetworkVariableFloat BankAStock = new NetworkVariableFloat();
    // Start is called before the first frame update
    void Start()
    {
        BankAStock.Value = 10;
    }

    public float returnStock()
    {
        return BankAStock.Value;
    }

    [ClientRpc]
    public void decreaseMoneyStockClientRpc(float cash)
    {
        BankAStock.Value -= cash;
        print("money stock went down by " + cash + " and now the bank has " + BankAStock.Value);
    }

    [ClientRpc]
    public void IncreaseMoneyStockClientRpc(float cash)
    {
        BankAStock.Value += cash;
        print("money stock went up by " + cash + " and now the bank has " + BankAStock.Value);
    }

    [ServerRpc(RequireOwnership = false)]
    public void roundAboutDecreaseServerRpc(float cash)
    {
        decreaseMoneyStockClientRpc(cash);
    }

    [ServerRpc(RequireOwnership = false)]
    public void roundAboutIncreaseServerRpc(float cash)
    {
        IncreaseMoneyStockClientRpc(cash);
    }
}
