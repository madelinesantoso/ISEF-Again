using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI.NetworkVariable;
using MLAPI;
using UnityEngine.UI;

public class fishController : MonoBehaviour
{
    public GameObject square;
    public GameObject shop;
    public InputField sell;
    public InputField buy;

    public NetworkVariableInt fish = new NetworkVariableInt();
    public NetworkVariableFloat fishValue = new NetworkVariableFloat();

    public Text sellText;
    public Text buyText;

    public GameObject player;

    public float playerMoney;
    public float playerFish;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("restock", 20f, 60f);
    }

    void restock()
    {
        square = GameObject.FindGameObjectWithTag("squarePlease");
        fish.Value = square.GetComponent<serverVariables>().bait.Value;
        
        if (fish.Value <= 30)
        {
            fish.Value++;
            square.GetComponent<serverVariables>().roundABoutBaitServerRpc(fish.Value);
        }
    }
    // Update is called once per frame
    public void buyFish()
    {
        square = GameObject.FindGameObjectWithTag("squarePlease");
        GetValues();
        int buyFish = int.Parse(buy.text);
        
        float cost = buyFish * fishValue.Value;
        print("the cost is: " + cost);

        if (playerMoney >= cost && (fish.Value-buyFish)>=0)
        {
            fish.Value -= buyFish;
            print("buy fish is " + buyFish + " and now it has " + fish.Value);
            player.GetComponent<Item>().IncreaseFish(buyFish);
            player.GetComponent<Item>().DecreaseMoney(cost);
            square.GetComponent<serverVariables>().roundABoutBaitServerRpc(fish.Value);
        }
        else
        {
            print("you don't have enough money to buy this!!");
        }
    }

    public void sellFish()
    {
        square = GameObject.FindGameObjectWithTag("squarePlease");
        GetValues();
        int sellFish = int.Parse(sell.text);
        float cost = sellFish * (fishValue.Value*85);
        print("you want to sell: " + sellFish);

        if (playerFish >= sellFish)
        {
            fish.Value += sellFish;
            player.GetComponent<Item>().DecreaseFish(sellFish);
            player.GetComponent<Item>().IncreaseMoney(cost);
            square.GetComponent<serverVariables>().roundABoutBaitServerRpc(fish.Value);
        }
        else
        {
            print("you don't have enough fish to sell!");
        }
    }

    public void GetValues()
    {
        square = GameObject.FindGameObjectWithTag("squarePlease");
        player = shop.GetComponent<fishShopController>().player;
        playerMoney = player.GetComponent<Item>().moneyAmount.Value;
        playerFish = player.GetComponent<Item>().fishAmount.Value;
        fish.Value = square.GetComponent<serverVariables>().bait.Value;
    }

    public void updateValues()
    {
        GetValues();
        fishValue.Value = 60 / fish.Value;
        UpdateText();
    }

    void UpdateText()
    {
        buyText.text = fishValue.Value.ToString() + " dollars";
        sellText.text = (fishValue.Value * .85).ToString() + " dollars";
    }
}
