using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI.NetworkVariable;
using UnityEngine.UI;
using MLAPI;
using MLAPI.Messaging;


public class Banking : NetworkBehaviour
{
    public NetworkVariableFloat BankAdepositAmount = new NetworkVariableFloat();
    //float playerCashOnHand;
    public NetworkVariableFloat playerCashOnHand = new NetworkVariableFloat();
    public GameObject player;
    public GameObject bank;
    public GameObject square;
    public Text DepositAmountText;
    public NetworkVariableFloat BankAStock = new NetworkVariableFloat();
    float bankAInterest;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("updateInterest", 0f, 60f);
        InvokeRepeating("BankAInterest", 0f, 60f);
        player = gameObject;
        bank = GameObject.Find("bank");
        BankAdepositAmount.Value = 0;
        BankAStock.Value = 100;
        DepositAmountText.text = "Money Deposited: " + BankAdepositAmount.Value.ToString();
        //square = GameObject.Find("square of make everything work");
        square = GameObject.FindGameObjectWithTag("squarePlease");
    }

    // Update is called once per frame
    void Update()
    {
        DepositAmountText.text = "Money Deposited: " + BankAdepositAmount.Value.ToString();
        if (Input.GetKeyDown(KeyCode.B))
        {
            //Deposit();
            CheckPlayerMoney();
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            square = GameObject.FindGameObjectWithTag("squarePlease");
            square.GetComponent<serverVariables>().lessCurrencyServerRpc(3);
        }
        //CheckPlayerMoney();
    }

    void Deposit()
    {
        square = GameObject.FindGameObjectWithTag("squarePlease");
        CheckPlayerMoney();
        NetworkVariableFloat BankAplayerCashOnHand = player.GetComponent<Item>().moneyAmount;
        if (BankAplayerCashOnHand.Value > 0)
        {
            //print("you got cash to deposit! money on hand: " + playerCashOnHand.Value.ToString());
            player.GetComponent<Item>().DecreaseMoney(1);
            BankAdepositAmount.Value += 1;
            DepositAmountText.text = "Money Deposited: " + BankAdepositAmount.Value.ToString();
        }
        else
        {
            print("no money, can't deposit :(");
        }
    }
    
    public void canceledBankAAccount()
    {
        square = GameObject.FindGameObjectWithTag("squarePlease");
        player.GetComponent<Item>().IncreaseMoney(BankAdepositAmount.Value);
        print("in bank rn: " + BankAdepositAmount.Value);
        BankAdepositAmount.Value = 0;
    }

    public void cancelA()
    {
        BankAdepositAmount.Value = 0;
        print("cancel called.");
    }

    public void MultiDeposit(float DepositAmount)
    {
        square = GameObject.FindGameObjectWithTag("squarePlease");
        //print(DepositAmount + " added to account");
        // bank.GetComponent<bankVar>().roundAboutIncreaseServerRpc(DepositAmount);
        square.GetComponent<serverVariables>().moreCurrencyServerRpc(DepositAmount);
        BankAdepositAmount.Value += DepositAmount;
        DepositAmountText.text = "Money Deposited: " + BankAdepositAmount.Value.ToString();
        //print("money deposited " + BankAdepositAmount.Value);

    }

    public void MultiWithdraw(float WithdrawAmount)
    {
        square = GameObject.FindGameObjectWithTag("squarePlease");
        if (WithdrawAmount <= square.GetComponent<serverVariables>().bankCurrency.Value)
        {
            square.GetComponent<serverVariables>().lessCurrencyServerRpc(WithdrawAmount);
            BankAdepositAmount.Value += -WithdrawAmount;
            DepositAmountText.text = "Money Deposited: " + BankAdepositAmount.Value.ToString();
        } else
        {
            print("<b>bro there's literally no money here.</b>");
        }

    }
    
    void CheckPlayerMoney()
    {
        NetworkVariableFloat BankAplayerCashOnHand = player.GetComponent<Item>().moneyAmount;
    }

    void checkVar()
    {
        BankAStock.Value = bank.GetComponent<bankVar>().returnStock();
    }

    void updateInterest()
    {
        BankAStock.Value = square.GetComponent<serverVariables>().bankCurrency.Value;
        bankAInterest = 1.5f/(BankAStock.Value*10);
        bankAInterest =  simplify(bankAInterest);
        //print("<b> INTEREST RATE </b> : " + bankAInterest);
    }

    private float simplify(float cost)
    {
        //taken from https://answers.unity.com/questions/50391/how-to-round-a-float-to-2-dp.html by Mike 3
        float newValue = Mathf.Round(cost * 1000f) / 1000f;
        return newValue;
    }

    private float simplify2(float cost)
    {
        //taken from https://answers.unity.com/questions/50391/how-to-round-a-float-to-2-dp.html by Mike 3
        float newValue = Mathf.Round(cost * 100f) / 100f;
        return newValue;
    }

    void BankAInterest()
    {
        BankAdepositAmount.Value += bankAInterest * BankAdepositAmount.Value;
        simplify2(BankAdepositAmount.Value);
       //print("<b>INTEREST APPLIED. YOU NOW HAVE: </b>" + BankAdepositAmount.Value);
    }

    public float returnBankADepositAmount()
    {
        return BankAdepositAmount.Value;
    }

    /*public float returnBankAInterest()
    {
        float interest = bankAInterest;
        return interest;
    }*/

}