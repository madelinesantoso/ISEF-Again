using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MLAPI.NetworkVariable;
using MLAPI;

public class BankAController2 : MonoBehaviour
{
    public InputField DepositField;
    public InputField WithdrawField;
    public GameObject bankingRadius;
    public GameObject player;

    float playerAmountDeposit;
    float playerAmountCurrency;

    float DepositAmountFloat;
    float WithdrawAmountFloat;

    public NetworkVariableFloat currency = new NetworkVariableFloat();

    public Text info;
    public Text info2;

    float[] interestRates = {.03f, .05f};

    // Start is called before the first frame update
    void Start()
    {
        //bankingRadius.GetComponent<BankController>().
        //InvokeRepeating("updateText", 0f, 60f);
        currency.Value = 500;
        FindStuff();
        updateText();
    }

    
    public void DepositFinished()
    {
        updateText();
        FindStuff();
        DepositAmountFloat = float.Parse(DepositField.text);
        
        if (DepositAmountFloat <= playerAmountCurrency)
        {
            //print("deposit " + DepositAmountFloat);
            player.GetComponent<Banking>().MultiDeposit(DepositAmountFloat);
            player.GetComponent<Item>().DecreaseMoney(DepositAmountFloat);
        }
        else
        {
            print("no money :(");
        }
        updateText();
    }

    public void WithdrawFinished()
    {
        updateText();
        FindStuff();
        WithdrawAmountFloat = float.Parse(WithdrawField.text);

        if (WithdrawAmountFloat <= playerAmountDeposit)
        {
            //print("deposit " + DepositAmountFloat);
            player.GetComponent<Banking>().MultiWithdraw(WithdrawAmountFloat);
            player.GetComponent<Item>().IncreaseMoney(WithdrawAmountFloat);
        }
        else
        {
            print("no money in account :(");
        }
        updateText();
    }

    void FindStuff()
    {
        bankingRadius.GetComponent<BankController>().ReturnHome();
        player = bankingRadius.GetComponent<BankController>().player;
        NetworkVariableFloat tempplayersMoney = player.GetComponent<Item>().moneyAmount;
        NetworkVariableFloat tempplayerdeposit = player.GetComponent<Banking>().BankAdepositAmount;
        playerAmountCurrency = tempplayersMoney.Value;
        playerAmountDeposit = tempplayerdeposit.Value;
    }

    public void updateText()
    {
        //print("updating text");
        FindStuff();
        info.text = "You currently have "+ playerAmountDeposit.ToString() +" in your bank account and " + playerAmountCurrency.ToString() + " on hand. How much would you like to withdraw?";
        info2.text = "You currently have " + playerAmountDeposit.ToString() + " in your bank account and " + playerAmountCurrency.ToString() + " on hand. How much would you like to withdraw?";
    }
}
