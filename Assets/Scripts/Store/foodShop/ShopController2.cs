using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MLAPI.NetworkVariable;
using MLAPI;

public class ShopController2 : MonoBehaviour
{
    public GameObject square;
    //Sell Food Input
    public InputField sellGoodFoodField;
    public InputField sellFineFoodField;
    public InputField sellEhFoodField;
    //Buy Food Input
    public InputField buyGoodFoodField;
    public InputField buyFineFoodField;
    public InputField buyEhFoodField;

    public GameObject store;

    public NetworkVariableInt goodFoodAmount = new NetworkVariableInt();
    public NetworkVariableInt fineFoodAmount = new NetworkVariableInt();
    public NetworkVariableInt ehFoodAmount = new NetworkVariableInt();

    public NetworkVariableFloat goodAmountValue = new NetworkVariableFloat();
    public NetworkVariableFloat fineAmountValue = new NetworkVariableFloat();
    public NetworkVariableFloat ehAmountValue = new NetworkVariableFloat();

    //sell values
    public NetworkVariableFloat sellGoodAmountValue = new NetworkVariableFloat();
    public NetworkVariableFloat sellEhAmountValue = new NetworkVariableFloat();

    //sell panel
    public Text showGoodFoodValue;
    public Text showEhFoodValue;
    //buy panel
    public Text showGoodFoodValue2;
    public Text showEhFoodValue2;

    //Player values
    public float playerMoney;
    public float playerFood;
    public float playerEhFood;
    //add stuff for other types of food
    
    public GameObject player;

    void Start()
    {
        square = GameObject.Find("square of make everything work");
        square = GameObject.FindGameObjectWithTag("squarePlease");
        if (square == null)
        {
            //square = GameObject.Find("square of make everything work(Clone)");
            square = GameObject.FindGameObjectWithTag("squarePlease");
        }
        goodFoodAmount.Value = 10;
        ehFoodAmount.Value = 10;
        UpdateText();
        InvokeRepeating("restock", 20f, 60f);
    }

    public void restock()
    {
        square = GameObject.Find("square of make everything work");
        goodFoodAmount.Value = square.GetComponent<serverVariables>().goodFood.Value;
        ehFoodAmount.Value = square.GetComponent<serverVariables>().ehFood.Value;
        if (goodFoodAmount.Value <= 15)
        {
            square.GetComponent<serverVariables>().roundABoutGoodFoodServerRpc(goodFoodAmount.Value + 1);
        }

        if (ehFoodAmount.Value <= 30)
        {
            square.GetComponent<serverVariables>().roundABoutFoodServerRpc(ehFoodAmount.Value + 3);
        }

        goodFoodAmount.Value = square.GetComponent<serverVariables>().goodFood.Value;
        ehFoodAmount.Value = square.GetComponent<serverVariables>().ehFood.Value;
    }

    public void buyGoodFood()
    {
        //NOTE: YA MIXED UP BUYING AND SELLING FOOD!! FIX THIS TOMORROW!!
        GetValues();
        updateValues();
        int buyFood = int.Parse(buyGoodFoodField.text);
        
        float cost = buyFood*goodAmountValue.Value;

        if (playerMoney >= cost && buyFood <= goodFoodAmount.Value)
        {
            goodFoodAmount.Value -= buyFood;
            player.GetComponent<Item>().increaseGoodFood(buyFood);
            player.GetComponent<Item>().DecreaseMoney(cost);
            square.GetComponent<serverVariables>().roundABoutGoodFoodServerRpc(goodFoodAmount.Value);
        } else
        {
            print("you don't have enough money to buy this!! OR there isn't enough food to buy!");
        }
    }

    public void buyEhFood()
    {
        GetValues();

        int buyFood = int.Parse(sellEhFoodField.text);
        
        float cost = buyFood * ehAmountValue.Value;

        if (playerMoney >= cost && buyFood <= ehFoodAmount.Value)
        {
            ehFoodAmount.Value -= buyFood;
            player.GetComponent<Item>().IncreaseFood(buyFood);
            player.GetComponent<Item>().DecreaseMoney(cost);
            square.GetComponent<serverVariables>().roundABoutFoodServerRpc(ehFoodAmount.Value);
        }
        else
        {
            print("you don't have enough money to buy this!! EH FOOD OR there isn't enough food to buy!");
        }
    }

    public void sellGoodFood()
    {
        GetValues();

        int sellFood = int.Parse(sellGoodFoodField.text);
        
        float cost = sellFood * goodAmountValue.Value;
        print("player food is " + playerFood + " and they want to sell " + sellFood);
        if (playerFood >= sellFood)
        {
            goodFoodAmount.Value += sellFood;
            player.GetComponent<Item>().decreaseGoodFood(sellFood);
            player.GetComponent<Item>().IncreaseMoney(cost);
            square.GetComponent<serverVariables>().roundABoutGoodFoodServerRpc(goodFoodAmount.Value);
        }
        else
        {
            print("you don't have enough food sell!");
        }
    }

    public void sellEhFood()
    {
        GetValues();

        int sellFood = int.Parse(sellFineFoodField.text);
        
        float cost = sellFood * sellEhAmountValue.Value;
        if (playerEhFood >= sellFood)
        {
            ehFoodAmount.Value += sellFood;
            player.GetComponent<Item>().DecreaseFood(sellFood);
            player.GetComponent<Item>().IncreaseMoney(cost);
            square.GetComponent<serverVariables>().roundABoutFoodServerRpc(ehFoodAmount.Value);
        }
        else
        {
            print("you don't have enough food sell!");
        }
    }

    public void GetValues()
    {
        square = GameObject.FindGameObjectWithTag("squarePlease");
       // goodFoodAmount = square.GetComponent<serverVariables>().goodFood.Value;
        player = store.GetComponent<ShopController>().player;
        playerMoney = player.GetComponent<Item>().moneyAmount.Value;
        playerFood = player.GetComponent<Item>().goodFoodAmount.Value;
        playerEhFood = player.GetComponent<Item>().ehfoodAmount.Value;
        ehFoodAmount.Value = square.GetComponent<serverVariables>().ehFood.Value;
        goodFoodAmount.Value = square.GetComponent<serverVariables>().goodFood.Value;
    }

    public void UpdateText()
    {
        //sell panel
        showEhFoodValue.text = sellEhAmountValue.Value.ToString() + " dollars";
        showGoodFoodValue.text = sellGoodAmountValue.Value.ToString() + " dollars";
        //buy panel
        showEhFoodValue2.text = ehAmountValue.Value.ToString() + " dollars";
        showGoodFoodValue2.text = goodAmountValue.Value.ToString() + " dollars";
    }

    public void updateValues()
    {
        GetValues();
        //cool print function i spent too long on. For future reference: https://docs.unity3d.com/Packages/com.unity.ugui@1.0/manual/StyledText.html
        goodAmountValue.Value = 70 / (float)(goodFoodAmount.Value);
        ehAmountValue.Value = 77 / (float)(ehFoodAmount.Value)-(goodAmountValue.Value/3);

        //simplify because its going to come out with a very gross string of numbers
        goodAmountValue.Value = simplify(goodAmountValue.Value);
        ehAmountValue.Value = simplify(ehAmountValue.Value);

        sellEhAmountValue.Value = ehAmountValue.Value*.75f;
        sellGoodAmountValue.Value = goodAmountValue.Value*.60f;
        sellGoodAmountValue.Value = simplify(sellGoodAmountValue.Value);
        sellEhAmountValue.Value = simplify(sellEhAmountValue.Value);

        UpdateText();
    }

    private float simplify(float cost)
    {
        //taken from https://answers.unity.com/questions/50391/how-to-round-a-float-to-2-dp.html by Mike 3
        float newValue = Mathf.Round(cost * 100f) / 100f;
        return newValue;
    }
}
