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

    public GameObject startUI;
    public GameObject clientUI;

    private void Awake()
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
        _uiDocument.rootVisualElement.style.display = DisplayStyle.None;
        startUI.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;
    }

    private void CheckIPAddress()
    {
        if (ip_entry.text != null)
        {
            manager.networkAddress = ip_entry.text;
            manager.StartClient();
        }
    }
    void Update()
    {
        if (_uiDocument.rootVisualElement.style.display != DisplayStyle.None)
            if (NetworkClient.isConnected)
            {
                //PassBetweenScenes.playerInstance.GetComponent<OnPlayerSpawn>().AddPlayer(PassBetweenScenes.playername);
                _uiDocument.rootVisualElement.style.display = DisplayStyle.None;
                clientUI.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;
            }
            else if (NetworkClient.active)
            {
                ip_entry.value = "connecting...";
                ip_entry.SetEnabled(false);
            }
            else
            {
                ip_entry.value = manager.networkAddress;
                ip_entry.SetEnabled(true);
            }
    }
}
