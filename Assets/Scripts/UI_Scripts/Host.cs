using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Host : MonoBehaviour
{
    private UIDocument _uiDocument;
    private Button back;
    private Button start;
    private Button character1;
    private Button character2;
    private Button character3;
    private Button character4;
    public Sprite chosenChar1;
    public Sprite chosenChar2;
    public Sprite chosenChar3;
    public Sprite chosenChar4;
    public static Label IP_address;
    private Label host;
    private Label player1;
    private Label player2;
    private Label player3;
    private VisualElement player1_char;
    private VisualElement player2_char;
    private VisualElement player3_char;
    private VisualElement host_char;

    public ConnectedPlayers networkLogic;

    public GameObject startUI;

    public CustumNetworkManager manager;

    public GameObject ingameUI;
    public NetworkLogic networkLogicIngame;

    public Camera camera1;
    public Camera camera2;



    void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
        back = _uiDocument.rootVisualElement.Q<Button>("back_button");
        start = _uiDocument.rootVisualElement.Q<Button>("start_button");
        character1 = _uiDocument.rootVisualElement.Q<Button>("Char1");
        character2 = _uiDocument.rootVisualElement.Q<Button>("Char2");
        character3 = _uiDocument.rootVisualElement.Q<Button>("Char3");
        character4 = _uiDocument.rootVisualElement.Q<Button>("Char4");

        IP_address = _uiDocument.rootVisualElement.Q<Label>("IP_adress");

        host = _uiDocument.rootVisualElement.Q<Label>("host_name");
        player1 = _uiDocument.rootVisualElement.Q<Label>("player1_name");
        player2 = _uiDocument.rootVisualElement.Q<Label>("player2_name");
        player3 = _uiDocument.rootVisualElement.Q<Label>("player3_name");



        host_char = _uiDocument.rootVisualElement.Q<VisualElement>("Character_icon_1");
        player1_char = _uiDocument.rootVisualElement.Q<VisualElement>("Character_icon_2");
        player2_char = _uiDocument.rootVisualElement.Q<VisualElement>("Character_icon_3");
        player3_char = _uiDocument.rootVisualElement.Q<VisualElement>("Character_icon_4");
        IP_address.text = GetLocalIP();

        back.clicked += BackToPrevScene;
        start.clicked += StartInGameScene;
        character1.clicked += Character1Selected;
        character2.clicked += Character2Selected;
        character3.clicked += Character3Selected;
        character4.clicked += Character4Selected;
    }

    void Update()
    {
        if (_uiDocument.rootVisualElement.style.display != DisplayStyle.None)
        {
            PassBetweenScenes.id = networkLogic.playernames.IndexOf(PassBetweenScenes.playername);

            for (int i = 0; i < 4; i++)
            {
                if (i < networkLogic.playernames.Count)
                    SetPlayerName(i, networkLogic.playernames[i]);
                else
                    SetPlayerName(i, "notconnected");
            }
        }
    }

    private void BackToPrevScene()
    {
        PassBetweenScenes.playerInstance.GetComponent<OnPlayerSpawn>().StopServer();
        _uiDocument.rootVisualElement.style.display = DisplayStyle.None;
        startUI.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;
    }

    private void StartInGameScene()
    {
        switch (PassBetweenScenes.selctedCharacter)
        {
            case 0:
                ingameUI.GetComponent<Ingame_Select_Actions_UI>().character = new Mage();
                break;
            case 1:
                ingameUI.GetComponent<Ingame_Select_Actions_UI>().character = new Gentleman();
                break;
            case 2:
                ingameUI.GetComponent<Ingame_Select_Actions_UI>().character = new Knight();
                break;
            case 3:
                ingameUI.GetComponent<Ingame_Select_Actions_UI>().character = new Miner();
                break;
        }
        //GameObject.FindGameObjectWithTag("useless").SetActive(false);
        PassBetweenScenes.playercount = networkLogic.playernames.Count;
        networkLogic.started = true;
        _uiDocument.rootVisualElement.style.display = DisplayStyle.None;
        ingameUI.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;
        camera1.enabled = false;
        camera2.enabled = true;
        networkLogicIngame.StartReal();
    }

    public void Character1Selected()
    {
        character1.AddToClassList("highlighted");
        character2.RemoveFromClassList("highlighted");
        character3.RemoveFromClassList("highlighted");
        character4.RemoveFromClassList("highlighted");
        PassBetweenScenes.selctedCharacter = 0;
        PassBetweenScenes.playerInstance.GetComponent<OnPlayerSpawn>().setPickedCharacter(PassBetweenScenes.id, PassBetweenScenes.selctedCharacter);

        //SetPlayerCharacter().style.backgroundImage = new StyleBackground(chosenChar1);
    }

    public void Character2Selected()
    {
        character2.AddToClassList("highlighted");
        character1.RemoveFromClassList("highlighted");
        character3.RemoveFromClassList("highlighted");
        character4.RemoveFromClassList("highlighted");
        PassBetweenScenes.selctedCharacter = 1;
        PassBetweenScenes.playerInstance.GetComponent<OnPlayerSpawn>().setPickedCharacter(PassBetweenScenes.id, PassBetweenScenes.selctedCharacter);


    }

    public void Character3Selected()
    {
        character3.AddToClassList("highlighted");
        character2.RemoveFromClassList("highlighted");
        character1.RemoveFromClassList("highlighted");
        character4.RemoveFromClassList("highlighted");
        PassBetweenScenes.selctedCharacter = 2;
        PassBetweenScenes.playerInstance.GetComponent<OnPlayerSpawn>().setPickedCharacter(PassBetweenScenes.id, PassBetweenScenes.selctedCharacter);


    }

    public void Character4Selected()
    {
        character4.AddToClassList("highlighted");
        character2.RemoveFromClassList("highlighted");
        character3.RemoveFromClassList("highlighted");
        character1.RemoveFromClassList("highlighted");
        PassBetweenScenes.selctedCharacter = 3;
        PassBetweenScenes.playerInstance.GetComponent<OnPlayerSpawn>().setPickedCharacter(PassBetweenScenes.id, PassBetweenScenes.selctedCharacter);

    }

    public bool SetPlayerName(int nr, string playername)
    {
        switch (nr)
        {
            case 0:
                {
                    host.text = playername;
                    return true;
                }
            case 1:
                {
                    player1.text = playername;
                    return true;
                }
            case 2:
                {
                    player2.text = playername;
                    return true;
                }
            case 3:
                {
                    player3.text = playername;
                    return true;
                }
            default:
                {
                    return false;
                }
        }
    }

    /*public VisualElement SetPlayerCharacter()
    {
        return host_char;
    }*/

    public bool SetIcon(int nr, Sprite character)
    {
        switch (nr)
        {
            case 1:
                {
                    host_char.style.backgroundImage = new StyleBackground(chosenChar1);
                    return true;
                }
            case 2:
                {
                    player1_char.style.backgroundImage = new StyleBackground(chosenChar2);
                    return true;
                }
            case 3:
                {
                    player3_char.style.backgroundImage = new StyleBackground(chosenChar3);
                    return true;
                }
            case 4:
                {
                    player3_char.style.backgroundImage = new StyleBackground(chosenChar4);
                    return true;
                }
            default:
                {
                    return false;
                }
        }
    }

    public static string GetLocalIP()
    {
        if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
        {
            IPAddress[] hostIP = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress IP in hostIP)
            {
                if (IP.AddressFamily == AddressFamily.InterNetwork)
                {
                    return IP.ToString();
                }
            }
        }
        else
        {
            throw new Exception("NO Network");
        }

        return null;
    }
}
