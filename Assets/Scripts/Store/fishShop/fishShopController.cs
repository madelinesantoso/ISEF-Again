using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MLAPI;
using MLAPI.NetworkVariable;
using MLAPI.Spawning;
using MLAPI.Messaging;

public class fishShopController : MonoBehaviour
{
    public GameObject storeUI;
    public GameObject introScreen;
    public GameObject sellScreen;
    public GameObject buyScreen;

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        storeUI.SetActive(false);
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = collision.gameObject;
            storeUI.SetActive(true);
            StartingMenu();
            print("Entered store!!");
        }
    }

    public void StartingMenu()
    {
        gameObject.GetComponent<fishController>().updateValues();
        introScreen.SetActive(true);
        sellScreen.SetActive(false);
        buyScreen.SetActive(false);
    }

    public void SellingMenu()
    {
        gameObject.GetComponent<fishController>().updateValues();
        introScreen.SetActive(false);
        sellScreen.SetActive(true);
        buyScreen.SetActive(false);
    }

    public void BuyingMenu()
    {
        gameObject.GetComponent<fishController>().updateValues();
        introScreen.SetActive(false);
        sellScreen.SetActive(false);
        buyScreen.SetActive(true);
    }

    public void Exit()
    {
        gameObject.GetComponent<fishController>().updateValues();
        storeUI.SetActive(false);
    }
}
