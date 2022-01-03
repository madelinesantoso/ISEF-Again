using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MLAPI.NetworkVariable;
using MLAPI;

public class rockShopController : MonoBehaviour
{
    public GameObject square;
    public GameObject player;

    public InputField sellField;

    public NetworkVariableInt oreAmount = new NetworkVariableInt();
    public float orePrice;

    public Text sellText;

    public float playerMoney;
    public float playerOre;

    private void Start()
    {
        InvokeRepeating("restock", 20f, 60f);
    }

    void GetValues()
    {
        square = GameObject.FindGameObjectWithTag("squarePlease");
        player = gameObject.GetComponent<rockShop>().returnGameObject();
        playerMoney = player.GetComponent<Item>().moneyAmount.Value;
        playerOre = player.GetComponent<Item>().oreAmount.Value;
        oreAmount.Value = square.GetComponentInChildren<serverVariables>().ore.Value;
    }

    public void restock()
    {
        square = GameObject.Find("square of make everything work");
        oreAmount.Value = square.GetComponent<serverVariables>().ore.Value;
        if (oreAmount.Value > 0)
        {
            square.GetComponent<serverVariables>().roundABoutOreServerRpc(-1);
        }
        oreAmount.Value = square.GetComponent<serverVariables>().ore.Value;
    }

    public void sell()
    {
        GetValues();
        updateValue();
        int sell = int.Parse(sellField.text);
        float cost = sell * orePrice;
        cost = simplify(cost);

        if (sell <= playerOre)
        {
            oreAmount.Value += sell;
            player.GetComponent<Item>().IncreaseMoney(cost);
            player.GetComponent<Item>().DecreaseOre(sell);
            print("want to sell " + sell);
            //square.GetComponent<serverVariables>().roundABoutOreServerRpc(sell);
        } //idk if i need an else here...
    }

    //update Text
    void updateText()
    {
        sellText.text = orePrice.ToString() + " per piece";
    }
    //update Prices

    public void updateValue()
    {
        GetValues();
        orePrice = 300 / (oreAmount.Value+1);
        orePrice = simplify(orePrice);
        updateText();
    }

    private float simplify(float cost)
    {
        //taken from https://answers.unity.com/questions/50391/how-to-round-a-float-to-2-dp.html by Mike 3
        float newValue = Mathf.Round(cost * 100f) / 100f;
        return newValue;
    }
}
