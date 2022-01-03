
using MLAPI;
using UnityEngine;
using UnityEngine.UI;

namespace HelloWorld
{
    public class HelloWorldManager : NetworkBehaviour
    {
       //public GameObject hostButton;
        //public GameObject clientButton;
        public GameObject Stuff;
        public InputField textBox;
        string EnteredName;
        public Text PlayerDisplayName;
        public GameObject player;

        private void Start()
        {
            /*if (IsLocalPlayer)
            {
                Text PlayerDisplayName = player.transform.Find("Name").GetComponent<Text>();
            }*/
            //player = NetworkManager.Singleton.ConnectedClients[NetworkManager.Singleton.LocalClientId].PlayerObject;

            //PlayerDisplayName = player.GetComponentInChildren
            Stuff.SetActive(true);
            //mainInputField.onEndEdit.AddListener(delegate(mainI))
        }

        void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10, 10, 300, 300));
            if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
            {
                //StartButtons();
            }
            else
            {
                StatusLabels();
            }

            GUILayout.EndArea();
        }

        /*public void OnEndEditTextBox()
        {
            if (textBox.text != "")
            {
                EnteredName = textBox.text;
                print(EnteredName);
            }
            else
            {
                EnteredName = textBox.text;
                print("Enter a name!!");
            }
            EnteredName = textBox.text;
        }*/

        public void StartHost()
        {
            NetworkManager.Singleton.StartHost();
            Stuff.SetActive(false);
            PlayerDisplayName = player.GetComponentInChildren<Text>();
            /*if (IsLocalPlayer)
            {
                PlayerDisplayName.text = EnteredName;
            }*/
            EnteredName = textBox.text;

            print(PlayerDisplayName.text);
            //PlayerDisplayName = player.transform.Find("Name").GetComponent<Text>();
            /*if (IsLocalPlayer)
            {
                Text PlayerDisplayName = player.transform.Find("Name").GetComponent<Text>();
            }*/
        }

        public void StartClient()
        {
            NetworkManager.Singleton.StartClient();
            Stuff.SetActive(false);
            EnteredName = textBox.text;

            print(PlayerDisplayName.text);
            /*if (IsLocalPlayer)
            {
                Text PlayerDisplayName = player.transform.Find("Name").GetComponent<Text>();
            }*/
        }

        /*static void StartButtons()
        {
            if (GUILayout.Button("Host")) NetworkManager.Singleton.StartHost();
            if (GUILayout.Button("Client")) NetworkManager.Singleton.StartClient();
            if (GUILayout.Button("Server")) NetworkManager.Singleton.StartServer();
        }*/

        static void StatusLabels()
        {
            var mode = NetworkManager.Singleton.IsHost ?
                "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client";

            GUILayout.Label("Transport: " +
                NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name);
            GUILayout.Label("Mode: " + mode);
        }

        /*static void SubmitNewPosition()
        {
            if (GUILayout.Button(NetworkManager.Singleton.IsServer ? "Move" : "Request Position Change"))
            {
                if (NetworkManager.Singleton.ConnectedClients.TryGetValue(NetworkManager.Singleton.LocalClientId,
                    out var networkedClient))
                {
                    var player = networkedClient.PlayerObject.GetComponent<HelloWorldPlayer>();
                    if (player)
                    {
                        player.Move();
                    }
                }
            }
        }*/
    }
}
