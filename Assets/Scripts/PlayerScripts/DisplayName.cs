using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MLAPI;
using MLAPI.NetworkVariable;
using MLAPI.Messaging;
using MLAPI;
using UnityEngine;
using UnityEngine.UI;


public class DisplayName : MonoBehaviour
{
    public Text displayName;
    string EnteredName;
    public NetworkVariableString name = new NetworkVariableString();
    public InputField textBox;
    public GameObject texttext;
    // Start is called before the first frame update
    void Start()
    {
        //displayName = transform.Find("Name").Text;
        OnEnteredText();
    }

    public void OnEnteredText()
    {
        texttext = GameObject.Find("InputField");
        //textBox = texttext.GetComponent<intialInput>().sendNameField();
        EnteredName = textBox.text;
        if (name.Value == "" && EnteredName != "")
        {
            EnteredName = textBox.text;
            name.Value = EnteredName;
            roundAboutServerRpc();
        }
        EnteredName = textBox.text;
    }

    [ServerRpc(RequireOwnership = false)]
    public void roundAboutServerRpc()
    {
        updateNameClientRpc(displayName);
    }

    [ClientRpc]
    public void updateNameClientRpc(Text nameDisplay)
    {
        print("update name called");
        nameDisplay.text = name.Value.ToString();
    }
}
