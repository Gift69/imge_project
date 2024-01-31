using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Client : MonoBehaviour
{
    private UIDocument _uiDocument;
    private Button back;
    private Button character1;
    private Button character2;
    private Button character3;
    private Button character4;
    private Label player1;
    private Label player2;
    private Label player3;
    private Label player4;
    private Label player1_char;
    private Label player2_char;
    private Label player3_char;
    private Label player4_char;

    public CustumNetworkManager manager;
    public GameObject startUI;
    public ConnectedPlayers networkLogic;

    public GameObject ingameUI;
    public NetworkLogic networkLogicIngame;

    public Camera camera1;
    public Camera camera2;

    void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
        back = _uiDocument.rootVisualElement.Q<Button>("back_button");

        player4 = _uiDocument.rootVisualElement.Q<Label>("player4_name");
        player1 = _uiDocument.rootVisualElement.Q<Label>("player1_name");
        player2 = _uiDocument.rootVisualElement.Q<Label>("player2_name");
        player3 = _uiDocument.rootVisualElement.Q<Label>("player3_name");

        character1 = _uiDocument.rootVisualElement.Q<Button>("Char1");
        character2 = _uiDocument.rootVisualElement.Q<Button>("Char2");
        character3 = _uiDocument.rootVisualElement.Q<Button>("Char3");
        character4 = _uiDocument.rootVisualElement.Q<Button>("Char4");


        back.clicked += BackToPrevScene;
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
            if (networkLogic.started)
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
                PassBetweenScenes.playercount = networkLogic.playernames.Count;
                _uiDocument.rootVisualElement.style.display = DisplayStyle.None;
                ingameUI.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;
                //GameObject.FindGameObjectWithTag("useless").SetActive(false);
                camera1.enabled = false;
                camera2.enabled = true;
                //networkLogicIngame.StartReal();
            }
            for (int i = 0; i < 4; i++)
            {
                if (i < networkLogic.playernames.Count)
                    SetPlayerName(i, networkLogic.playernames[i]);
                else
                    SetPlayerName(i, "notconnected");
            }
        }
    }
    public bool SetPlayerName(int nr, string playername)
    {
        switch (nr)
        {
            case 0:
                {
                    player1.text = playername;
                    return true;
                }
            case 1:
                {
                    player2.text = playername;
                    return true;
                }
            case 2:
                {
                    player3.text = playername;
                    return true;
                }
            case 3:
                {
                    player4.text = playername;
                    return true;
                }
            default:
                {
                    return false;
                }
        }
    }

    private void BackToPrevScene()
    {
        manager.StopClient();
        _uiDocument.rootVisualElement.style.display = DisplayStyle.None;
        startUI.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;
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
}
