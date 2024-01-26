using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class IP_Entry : MonoBehaviour
{
    private UIDocument _uiDocument;
    private Button back;
    private TextField ip_entry;
    private Button enter;

    private NetworkManager manager;


    private void Start()
    {
        manager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        _uiDocument = GetComponent<UIDocument>();
        back = _uiDocument.rootVisualElement.Q<Button>("back_button");
        ip_entry = _uiDocument.rootVisualElement.Q<TextField>("IP_entry");
        ip_entry.label = new string("IP ADDRESS : ");
        enter = _uiDocument.rootVisualElement.Q<Button>("enter");
        back.clicked += BackToPrevScene;
        enter.clicked += CheckIPAddress;
    }

    private void BackToPrevScene()
    {
        SceneManager.LoadScene("Start", LoadSceneMode.Single);
    }

    private void CheckIPAddress()
    {
        if (ip_entry.text != null)
        {
            manager.networkAddress = ip_entry.text;
            manager.StartClient();
            if (NetworkClient.isConnected)
            {
                SceneManager.LoadScene("Client", LoadSceneMode.Single);
            }
            else
            { //rotes feld
            }
            //rotes feld
        }
    }
}
