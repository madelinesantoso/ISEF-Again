using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.Spawning;
using MLAPI.NetworkVariable;
using UnityEngine.UI;
using System.Text;

public class Item : NetworkBehaviour
{
    public GameObject treeController;

    public GameObject hatPrefab;
    public GameObject hat;

    public NetworkVariableBool hatBool = new NetworkVariableBool();

    public GameObject moneyPrefab;
    public GameObject money;

    public GameObject foodPrefab;
    public GameObject food;

    public GameObject goodFoodPrefab;
    public GameObject goodfood;

    public GameObject woodPrefab;
    public GameObject wood;

    public GameObject fishPrefab;
    public GameObject fish;

    public GameObject orePrefab;
    public GameObject ore;

    public GameObject tree;

    public GameObject inventories;
    public Text TradeMessages;

    public NetworkVariableFloat moneyAmount = new NetworkVariableFloat();
    public NetworkVariableFloat ehfoodAmount = new NetworkVariableFloat();
    public NetworkVariableFloat goodFoodAmount = new NetworkVariableFloat();
    public NetworkVariableFloat fishAmount = new NetworkVariableFloat();
    public NetworkVariableFloat woodAmount = new NetworkVariableFloat();
    public NetworkVariableInt oreAmount = new NetworkVariableInt();

    public Text MessageText;
    public Text FoodText;
    public Text goodFoodText;
    public Text FishText;
    public Text HealthText;
    public Text woodText;
    public Text oreText;

    public NetworkVariableFloat Health = new NetworkVariableFloat();

    bool CanTrade = false;
    bool canFish = false;
    public GameObject TradingPerson;
    public GameObject heartContainer;

    bool CanChop = false;


    public void Start()
    {
        //moneyAmount.Value = 10;
        Health.Value = 25;
        InvokeRepeating("HealthDecrease", 20f, 20f);
        if (!IsLocalPlayer)
        {
            inventories.SetActive(false);
            heartContainer.SetActive(false);
        }
    }

    public void DecreaseMoney(float DecreaseMoneyByAmount)
    {
        print("time to decrease money. this function was called");
        moneyAmount.Value += -DecreaseMoneyByAmount;
        MessageText.text = "Money on Hand: " + moneyAmount.Value.ToString();
    }

    public void IncreaseMoney(float IncreaseMoneyByAmount)
    {
        moneyAmount.Value += IncreaseMoneyByAmount;
        MessageText.text = "Money on Hand: " + moneyAmount.Value.ToString();
    }

    public void increaseGoodFood(int amount)
    {
        goodFoodAmount.Value += amount;
        goodFoodText.text = "Good Food on Hand: " + goodFoodAmount.Value.ToString();
    }

    public void decreaseGoodFood(int amount)
    {
        goodFoodAmount.Value -= amount;
        goodFoodText.text = "Good Food on Hand: " + goodFoodAmount.Value.ToString();
    }

    public void IncreaseFood(int IncreaseFoodByAmount)
    {
        ehfoodAmount.Value += IncreaseFoodByAmount;
        FoodText.text = "Eh Food on Hand: " + ehfoodAmount.Value.ToString();
    }

    public void DecreaseFood(int DecreaseFoodByAmount)
    {
        ehfoodAmount.Value -= DecreaseFoodByAmount;
       FoodText.text = "Eh Food on Hand: " + ehfoodAmount.Value.ToString();
    }

    public void IncreaseOre(int num)
    {
        oreAmount.Value += num;
        oreText.text = "Ore: " + oreAmount.Value.ToString();
    }

    public void DecreaseOre(int num)
    {
        oreAmount.Value -= num;
        oreText.text = "Ore: " + oreAmount.Value.ToString();
    }

    public void EatGoodFood()
    {
        print("eat eat eat");
        Health.Value += 3;
        goodFoodAmount.Value += -1;
        goodFoodText.text = "Good Food on Hand: " + goodFoodAmount.Value.ToString();
        HealthText.text = Health.Value.ToString();
    }

    public void EatEhFood()
    {
        print("eat eat eat eh");
        Health.Value += 1;
        ehfoodAmount.Value += -1;
        FoodText.text = "Eh Food on Hand: " + ehfoodAmount.Value.ToString();
        HealthText.text = Health.Value.ToString();
    }

    public void IncreaseFish(int IncreaseFishByAmount)
    {
        fishAmount.Value += IncreaseFishByAmount;
        FishText.text = "Fish on Hand: " + fishAmount.Value.ToString();
    }

    public void DecreaseFish(int DecreaseFishByAmount)
    {
        fishAmount.Value -= DecreaseFishByAmount;
        FishText.text = "Fish on Hand: " + fishAmount.Value.ToString();
    }

    public void IncreaseWood(int amount)
    {
        woodAmount.Value += amount;
        woodText.text = "Wood on Hand " + woodAmount.Value.ToString();
    }

    public void DecreaseWood(int amount)
    {
        woodAmount.Value -= amount;
        woodText.text = "Wood on Hand " + woodAmount.Value.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "money")
        {
            money = collision.gameObject;
            PickUpServerRpc();
            moneyAmount.Value += 1;
            MessageText.text = "Money on Hand: " + moneyAmount.Value.ToString();
        }

        if (collision.tag == "fish")
        {
            fish = collision.gameObject;
            PickUpFishServerRpc();
            fishAmount.Value += 1;
            FishText.text = "Fish on Hand: " + fishAmount.Value.ToString();
        }

        if (collision.tag == "food")
        {
            food = collision.gameObject;
            PickUpFoodServerRpc();
            ehfoodAmount.Value += 1;
            FoodText.text = "Eh Food on Hand: " + ehfoodAmount.Value.ToString();
        }

        if (collision.tag == "goodFood")
        {
            food = collision.gameObject;
            PickUpFoodServerRpc();
            increaseGoodFood(1);
        }

        if (collision.tag == "tree")
        {
            treeController = GameObject.Find("Trees");
            tree = collision.gameObject;
            if (treeController.GetComponent<TreeGrowing>().returnTreeBool(tree) == true)
            {
                CanChop = true;
            } else
            {
                CanChop = false;
            }
            
        }

        if (collision.tag == "wood")
        {
            wood = collision.gameObject;
            PickUpWoodServerRpc();
            IncreaseWood(1);
        }

        if (collision.tag == "ore")
        {
            ore = collision.gameObject;
            PickUpOreServerRpc();
            IncreaseOre(1);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CanChop = false;
    }

    void Trade()
    {
        TradeMessages.text = "You can trade with this player now! Press 'f' to give food to player, press 'm' to give money. If you want to ask for a material, type '/f' to ask for food and '/m' to ask for money";
    }

    public void dropCash()
    {
        if (moneyAmount.Value > 0)
        {
            DecreaseMoney(1);
            SpawnMoneyServerRpc();
        }
    }

    public void dropGoodFood()
    {
        if (goodFoodAmount.Value > 0)
        {
            decreaseGoodFood(1);
            SpawnGoodFoodServerRpc();
        }
    }

    public void dropFood()
    {
        if (ehfoodAmount.Value > 0)
        {
            DecreaseFood(1);
            SpawnFoodServerRpc();
        }
    }

    public void dropFish()
    {
        if(fishAmount.Value > 0)
        {
            DecreaseFish(1);
            SpawnFishServerRpc();
        }
    }

    public void dropOre()
    {
        print("dropped ore");
        if(oreAmount.Value > 0)
        {
            DecreaseOre(1);
            SpawnOreServerRpc();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            SpawnGoodFoodServerRpc();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            SpawnFoodServerRpc();
        }
        //drop stuff
        if (Input.GetKey(KeyCode.P))
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                dropCash();
            }
            
            if (Input.GetKeyDown(KeyCode.G))
            {
                dropGoodFood();
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                dropFood();
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                if (woodAmount.Value > 0)
                {
                    SpawnWoodServerRpc();
                    DecreaseWood(1);
                }
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                dropFish();
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                dropOre();
                print("called drop ore");
          }
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            SpawnOreServerRpc();
        }

        if (Input.GetKey(KeyCode.E))
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                if (goodFoodAmount.Value > 0 && Health.Value < 22)
                {
                    EatGoodFood();
                }
                else
                {
                    print("can't eat :'(");
                }
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                if (ehfoodAmount.Value > 0 && Health.Value < 25)
                {
                    EatEhFood();
                }
                else
                {
                    print("can't eat :'(");
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (CanChop == true)
            {
                treeController = GameObject.Find("Trees");
                treeController.GetComponent<TreeGrowing>().destroyTreeServerRpc(tree);
                IncreaseWood(3);
            }
        }
    }

    void HealthDecrease()
    {
        Health.Value += -1;
        HealthText.text = Health.Value.ToString();
    }


    [ServerRpc(RequireOwnership = false)]
    private void PickUpServerRpc()
    {
        Destroy(money);
    }

    [ServerRpc(RequireOwnership = false)]
    private void PickUpFoodServerRpc()
    {
        //MessageText.text = "deleteObject";
        //print(OwnerClientId + ": " + ehfoodAmount.Value + " in food");
        Destroy(food);
    }

    [ServerRpc(RequireOwnership = false)]
    private void PickUpFishServerRpc()
    {
        //MessageText.text = "deleteObject";
        //print(OwnerClientId + ": " + fishAmount.Value + " in fish");
        Destroy(fish);
    }

    [ServerRpc(RequireOwnership = false)]
    private void PickUpWoodServerRpc()
    {
        Destroy(wood);
        woodAmount.Value++;
    }

    [ServerRpc(RequireOwnership = false)]
    private void PickUpOreServerRpc()
    {
        Destroy(ore);

    }

    [ServerRpc(RequireOwnership = false)]
    void SpawnMoneyServerRpc()
    {
        GameObject money = Instantiate(moneyPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y-2, gameObject.transform.position.z), Quaternion.identity);
        money.GetComponent<NetworkObject>().Spawn();
    }

    [ServerRpc(RequireOwnership = false)]
    void SpawnHatServerRpc()
    {
        if (hatBool.Value == true)
        {
            GameObject hat = Instantiate(hatPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + .75f, gameObject.transform.position.z), Quaternion.identity);
            hat.GetComponent<NetworkObject>().Spawn();
            hat.transform.SetParent(gameObject.transform);
            hatBool.Value = false;
            print("cool cool cool");
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void SpawnWoodServerRpc()
    {
        GameObject wood = Instantiate(woodPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 2, gameObject.transform.position.z), Quaternion.identity);
        wood.GetComponent<NetworkObject>().Spawn();
    }

    [ServerRpc(RequireOwnership = false)]
    void SpawnFoodServerRpc()
    {
        print("cool cool cool");
        GameObject food = Instantiate(foodPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y-2, gameObject.transform.position.z), Quaternion.identity);
        food.GetComponent<NetworkObject>().Spawn();
    }

    [ServerRpc(RequireOwnership = false)]
    void SpawnGoodFoodServerRpc()
    {
        GameObject goodFood = Instantiate(goodFoodPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 2, gameObject.transform.position.z), Quaternion.identity);
        goodFood.GetComponent<NetworkObject>().Spawn();
    }

    [ServerRpc(RequireOwnership = false)]
    public void SpawnFishServerRpc()
    {
        GameObject fish = Instantiate(fishPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y-2, gameObject.transform.position.z), Quaternion.identity);
        fish.GetComponent<NetworkObject>().Spawn();
    }

    [ServerRpc(RequireOwnership = false)]
    public void SpawnOreServerRpc()
    {
        GameObject ore = Instantiate(orePrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 3, gameObject.transform.position.z), Quaternion.identity);
        ore.GetComponent<NetworkObject>().Spawn();
    }

    /*private void simplifyMoney()
    {
        //taken from https://answers.unity.com/questions/50391/how-to-round-a-float-to-2-dp.html by Mike 3
        moneyAmount.Value = Mathf.Round(moneyAmount.Value * 100f) / 100f;
    }*/
}
