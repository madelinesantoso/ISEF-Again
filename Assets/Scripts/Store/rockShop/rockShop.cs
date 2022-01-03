using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockShop : MonoBehaviour
{
    public GameObject storeUI;
    public GameObject introScreen;
    public GameObject sellScreen;

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
        gameObject.GetComponent<rockShopController>().updateValue();
        introScreen.SetActive(true);
        sellScreen.SetActive(false);
    }

    public void SellingMenu()
    {
        gameObject.GetComponent<rockShopController>().updateValue();
        introScreen.SetActive(false);
        sellScreen.SetActive(true);
    }

    public void Exit()
    {
        gameObject.GetComponent<rockShopController>().updateValue();
        storeUI.SetActive(false);
    }

    public GameObject returnGameObject()
    {
        return player;
    }
}
