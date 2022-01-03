using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;
using UnityEngine.UI;

public class serverVariables : NetworkBehaviour
{
    public NetworkVariableFloat bankInterest = new NetworkVariableFloat();
    public NetworkVariableFloat bankCurrency = new NetworkVariableFloat();
    public NetworkVariableInt goodFood = new NetworkVariableInt();
    public NetworkVariableInt ehFood = new NetworkVariableInt();
    public NetworkVariableInt bait = new NetworkVariableInt();
    public NetworkVariableInt wood = new NetworkVariableInt();
    public NetworkVariableInt oreInMine = new NetworkVariableInt();
    public NetworkVariableInt rarity = new NetworkVariableInt();
    public NetworkVariableInt ore = new NetworkVariableInt();
    // Start is called before the first frame update
    void Start()
    {
        bankInterest.Value = 0;
        bankCurrency.Value = 10;
        goodFood.Value = 10;
        ehFood.Value = 10;
        wood.Value = 20;
        bait.Value = 10;
        oreInMine.Value = 25;
        rarity.Value = 30;
        ore.Value = 20;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    [ServerRpc (RequireOwnership = false)]
    public void updateVarServerRpc(float num)
    {
        updateBankInterestClientRpc(num);
        //print("update interest called by " + num);
    }

    [ServerRpc(RequireOwnership = false)]
    public void moreCurrencyServerRpc(float num)
    {
        increaseCurrencyClientRpc(num);
        //print("serverVar script called, increase by " + num);
    }

    [ServerRpc(RequireOwnership = false)]
    public void lessCurrencyServerRpc(float num)
    {
        decreaseCurrencyClientRpc(num);
        //print("serverVar script called, decrease by " + num);
    }

    [ServerRpc(RequireOwnership = false)]
    public void roundABoutFoodServerRpc(int num)
    {
        updateFoodStoreClientRpc(num);
    }

    [ServerRpc(RequireOwnership = false)]
    public void roundABoutGoodFoodServerRpc(int num)
    {
        updateGoodFoodStoreClientRpc(num);
    }

    [ServerRpc(RequireOwnership = false)]
    public void roundABoutBaitServerRpc(int num)
    {
        updateBaitStoreClientRpc(num);
    }

    [ServerRpc(RequireOwnership = false)]
    public void roundABoutWoodServerRpc(int num)
    {
        increaseWoodClientRpc(num);
    }

    [ServerRpc(RequireOwnership = false)]
    public void roundABoutLessWoodServerRpc(int num)
    {
        decreaseWoodClientRpc(num);
    }

    [ServerRpc(RequireOwnership = false)]
    public void roundABoutMineServerRpc()
    {
        MineClientRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    public void roundABoutOreServerRpc(int num)
    {
        increaseOreClientRpc(num);
    }

    /* 


CLIENT RPC STUFF


   */

    [ClientRpc]
    public void updateBankInterestClientRpc(float interest)
    {
        //print(bankInterest.Value);
        bankInterest.Value += interest;
       // print("interest: " + bankInterest.Value);
    }

    [ClientRpc]
    public void MineClientRpc()
    {
        oreInMine.Value--;
        rarity.Value += 3;
    }

    [ClientRpc]
    public void updateFoodStoreClientRpc(int food)
    {
        ehFood.Value = food;
        //print("eh food is now " + ehFood.Value);
        
    }

    [ClientRpc]
    public void updateGoodFoodStoreClientRpc(int food)
    {
        goodFood.Value = food;
        //print("eh food is now " + goodFood.Value);
    }

    [ClientRpc]
    public void updateBaitStoreClientRpc(int fish)
    {
        bait.Value = fish;
    }

    [ClientRpc]
    public void updateStoreClientRpc(int num)
    {
        //do stuff
    }

    [ClientRpc]
    public void increaseCurrencyClientRpc(float num)
    {
        bankCurrency.Value += num;
        print("Currency: " + bankCurrency.Value + ". Increased by " + num);
    }

    [ClientRpc]
    public void decreaseCurrencyClientRpc(float num)
    {
        bankCurrency.Value -= num;
        print("Currency: " + bankCurrency.Value + ". Decreased by " + num);
    }

    [ClientRpc]
    public void decreaseWoodClientRpc(int num)
    {
        wood.Value -= num;
    }

    [ClientRpc]
    public void increaseWoodClientRpc(int num)
    {
        wood.Value += num;
    }

    [ClientRpc]
    public void decreaseOreClientRpc(int num)
    {
        ore.Value -= num;
    }

    [ClientRpc]
    public void increaseOreClientRpc(int num)
    {
        ore.Value += num;
    }
}
