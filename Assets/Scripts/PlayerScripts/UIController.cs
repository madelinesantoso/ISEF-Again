using System.Collections;
using System.Collections.Generic;
using UnityEngine; //because UI rn is a nightmare
using MLAPI;
using MLAPI.NetworkVariable;

public class UIController : MonoBehaviour
{
    public NetworkVariableBool inBank = new NetworkVariableBool();
    public NetworkVariableBool inFoodShop = new NetworkVariableBool();
    public NetworkVariableBool inWoodShop = new NetworkVariableBool();
    public NetworkVariableBool inMineralShop = new NetworkVariableBool();

    public GameObject bankUI;
    public GameObject bankRadius;
    // Start is called before the first frame update
    void Start()
    {
        inBank.Value = false;
        bankUI = GameObject.FindGameObjectWithTag("bankUI");
        bankRadius = GameObject.FindGameObjectWithTag("bankingRadius");
        bankUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!inBank.Value)
        {
            bankUI.SetActive(false);
        } else
        {
            bankUI.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "bankingRadius")
        {
            inBank.Value = true;
            bankRadius.GetComponent<BankController>().StartScreen(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inBank.Value = false;
    }
}
