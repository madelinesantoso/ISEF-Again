using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.Spawning;
using MLAPI.NetworkVariable;
using UnityEngine.UI;
using System.Text;

public class TreeGrowing : MonoBehaviour
{
    public GameObject treePrefab;
    public GameObject[] treeSpace;
    public bool[] filled;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("growServerRpc", 0f, 60f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            growServerRpc();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void growServerRpc()
    {
        for(int i = 0; i < treeSpace.Length; i++)
        {
            if (filled[i] == false) //from inventory system tutorial
            {
                GameObject tree = Instantiate(treePrefab, new Vector3(treeSpace[i].transform.position.x, treeSpace[i].transform.position.y+4, treeSpace[i].transform.position.z), Quaternion.identity, treeSpace[i].transform);
                tree.transform.localScale = new Vector3(1, 3, 3);
                filled[i] = true;
                break;
            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void destroyTreeServerRpc(GameObject tree)
    {
        int num = -1;
        for (int i=0;i<treeSpace.Length; i++) //from https://answers.unity.com/questions/324428/how-to-find-an-object-in-an-array.html
        {
            if (treeSpace[i] == tree)
            {
                num = i;
                break;
            }
        }
        GameObject child = tree.transform.GetChild(0).gameObject;
        GameObject.Destroy(child);
        filled[num] = false;
    }

    public bool returnTreeBool(GameObject tree)
    {
        int num = -1;
        for (int i = 0; i < treeSpace.Length; i++) //from https://answers.unity.com/questions/324428/how-to-find-an-object-in-an-array.html
        {
            if (treeSpace[i] == tree)
            {
                num = i;
                break;
            }
        }
        return filled[num];

    }

    void getInfo()
    {

    }
}
