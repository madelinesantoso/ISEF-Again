using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class woodShop : MonoBehaviour
{
    public GameObject storeUI;
    public GameObject introScreen;
    public GameObject sellScreen;
    public GameObject buyScreen;

    public GameObject player;

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
        gameObject.GetComponent<woodShopController>().updateValue();
        introScreen.SetActive(true);
        sellScreen.SetActive(false);
        buyScreen.SetActive(false);
    }

    public void SellingMenu()
    {
        introScreen.SetActive(false);
        sellScreen.SetActive(true);
        buyScreen.SetActive(false);
    }

    public void BuyingMenu()
    {
        introScreen.SetActive(false);
        sellScreen.SetActive(false);
        buyScreen.SetActive(true);
    }

    public void Exit()
    {
        storeUI.SetActive(false);
    }

    public GameObject returnGameObject()
    {
        return player;
    }
}
