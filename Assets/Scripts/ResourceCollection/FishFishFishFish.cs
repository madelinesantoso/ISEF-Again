using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI.Messaging;
using MLAPI.Spawning;
using MLAPI.NetworkVariable;
using UnityEngine.UI;
using System.Text;
using MLAPI;

public class FishFishFishFish : NetworkBehaviour
{
    int amountOfTimesFished;
    public GameObject Pond;
    public NetworkVariableInt fish = new NetworkVariableInt();

    bool canFish = false;
    
    // Start is called before the first frame update
    void Start()
    {
        Pond = GameObject.Find("Lake(Clone)");
        fish.Value = 20;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == "fishRadius")
        {
            canFish = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "fishRadius")
        {
            canFish = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(canFish == true)
            {
                print("fishing now");
                fishNow();
            }
        }
    }

    public void fishNow()
    {
        if (amountOfTimesFished < 3)
        {
            amountOfTimesFished++;
        } else
        {
            if (fish.Value > 0)
            {
                gameObject.GetComponent<Item>().IncreaseFish(1);
                roundaboutServerRpc();
            } else
            {
                print("no fish");
            }
            amountOfTimesFished = 0;
            //add a fish to inventory
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void roundaboutServerRpc()
    {
        fish.Value -= 1;
        updateFishClientRpc(fish.Value);
    }

    [ClientRpc]
    public void updateFishClientRpc(int fishes)
    {
        print("client rpc called");
        fish.Value = fishes;
        print("fish in pond: " + fish.Value);
    }
}
