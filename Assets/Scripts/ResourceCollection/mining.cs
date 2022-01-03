using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.Spawning;
using MLAPI.NetworkVariable;
using UnityEngine.UI;
using System.Text;

public class mining : MonoBehaviour
{
    public bool canMine = false;
    public int rarity = 30;
    public GameObject square;
    public int inMine;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            mine();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "mine")
        {
            canMine = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canMine = false;
    }

    public void mine()
    {
        square = GameObject.FindGameObjectWithTag("squarePlease");
        inMine = square.GetComponent<serverVariables>().oreInMine.Value;
        rarity = square.GetComponent<serverVariables>().rarity.Value;
        if (canMine == true)
        {
            int randomNum;
            int num;

            randomNum = Random.Range(0, 100);
            num = Random.Range(2, rarity);
            //print("first num: " + randomNum + " | second num: " + num + ". " + (randomNum % num));
            if (randomNum % (num*3) == 0)
            {
                if (inMine > 0)
                {
                    print("ore time B)");

                    square.GetComponent<serverVariables>().roundABoutMineServerRpc();
                    gameObject.GetComponent<Item>().IncreaseOre(1);
                }
            }
        }

    }
}
