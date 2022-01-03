using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class intialInput : MonoBehaviour
{
    public InputField nameField;
    //public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public InputField sendNameField()
    {
        //player.GetComponent<DisplayName>().OnEnteredText(nameField);
        return nameField;
    }
}
