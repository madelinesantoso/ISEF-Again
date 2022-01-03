using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MLAPI;
using MLAPI.NetworkVariable;
using MLAPI.Spawning;
using MLAPI.Messaging;

public class ShopController : MonoBehaviour
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = collision.gameObject;
            storeUI.SetActive(true);
            StartingMenu();
        } 
    }

    public void StartingMenu()
    {
        gameObject.GetComponent<ShopController2>().updateValues();
        introScreen.SetActive(true);
        sellScreen.SetActive(false);
        buyScreen.SetActive(false);
    } 

    public void ShoppingMenu()
    {
        gameObject.GetComponent<ShopController2>().updateValues();
        introScreen.SetActive(false);
        buyScreen.SetActive(true);
        sellScreen.SetActive(false);
    }

    public void BuyingMenu()
    {
        gameObject.GetComponent<ShopController2>().updateValues();
        introScreen.SetActive(false);
        sellScreen.SetActive(true);
        buyScreen.SetActive(false);
    }

    public void Exit()
    {
        gameObject.GetComponent<ShopController2>().updateValues();
        storeUI.SetActive(false);
    }
}
