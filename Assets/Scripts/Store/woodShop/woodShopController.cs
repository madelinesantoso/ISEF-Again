using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MLAPI.NetworkVariable;
using MLAPI;


public class woodShopController : MonoBehaviour
{
    public GameObject square;
    public GameObject player;

    public InputField sellWoodField;
    public InputField buyWoodField;

    public NetworkVariableInt woodAmount = new NetworkVariableInt();
    public float sellWood;
    public float woodPrice;

    public Text sellWoodText;
    public Text buyWoodText;

    public float playerMoney;
    public float playerWood;

    void Start()
    {
        //updateValue();
        square = GameObject.FindGameObjectWithTag("squarePlease");
        InvokeRepeating("restock", 20f, 60f);
        updateText();
    }

    public void restock()
    {
        square = GameObject.FindGameObjectWithTag("squarePlease");
        woodAmount.Value = square.GetComponent<serverVariables>().wood.Value;
        
        if (woodAmount.Value > 0)
        {
            woodAmount.Value--;
            square.GetComponent<serverVariables>().roundABoutLessWoodServerRpc(1);
            print("wood shop restocked, wood is " + woodAmount.Value);
        }
    }

    public void sell()
    {
        GetValues();
        updateValue();
        int sellWood = int.Parse(sellWoodField.text);
        float cost = sellWood * woodPrice;
        cost = simplify(cost);

        if (sellWood <= playerWood)
        {
            woodAmount.Value += sellWood;
            player.GetComponent<Item>().IncreaseMoney(cost);
            player.GetComponent<Item>().DecreaseWood(sellWood);
            square.GetComponent<serverVariables>().roundABoutWoodServerRpc(sellWood);
        } //idk if i need an else here...
    }

    public void buy()
    {
        GetValues();
        updateValue();
        int buyWood = int.Parse(buyWoodField.text);
        float cost = buyWood * woodPrice;
        cost = simplify(cost);

        if (playerMoney >= cost && buyWood < woodAmount.Value)
        {
            woodAmount.Value -= buyWood;
            player.GetComponent<Item>().DecreaseMoney(cost);
            player.GetComponent<Item>().IncreaseWood(buyWood);
            square.GetComponent<serverVariables>().roundABoutLessWoodServerRpc(buyWood);
        } //idk if i need an else here...
    }

    void GetValues()
    {
        square = GameObject.FindGameObjectWithTag("squarePlease");
        player = gameObject.GetComponent<woodShop>().returnGameObject();
        playerMoney = player.GetComponent<Item>().moneyAmount.Value;
        playerWood = player.GetComponent<Item>().woodAmount.Value;
        woodAmount.Value = square.GetComponentInChildren<serverVariables>().wood.Value;
    }

    //update Text
    void updateText()
    {
        sellWoodText.text = sellWood.ToString()+" per piece";
        buyWoodText.text = woodPrice.ToString() + " per piece";
    }
    //update Pricesg

    public void updateValue()
    {
        print("<b>WOOD SHOP VALUES UPDATED</b>");
        GetValues();
        woodPrice = 130 / (woodAmount.Value+1);
        sellWood = woodPrice * .85f;
        sellWood = simplify(sellWood);
        woodPrice = simplify(woodPrice);
        updateText();
    }

    private float simplify(float cost)
    {
        //taken from https://answers.unity.com/questions/50391/how-to-round-a-float-to-2-dp.html by Mike 3
        float newValue = Mathf.Round(cost * 100f) / 100f;
        return newValue;
    }
}
