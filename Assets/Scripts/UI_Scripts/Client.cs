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

    public GameObject startUI;
    public ConnectedPlayers networkLogic;

    public CustumNetworkManager manager;

    void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
        back = _uiDocument.rootVisualElement.Q<Button>("back_button");

        player4 = _uiDocument.rootVisualElement.Q<Label>("player4_name");
        player1 = _uiDocument.rootVisualElement.Q<Label>("player1_name");
        player2 = _uiDocument.rootVisualElement.Q<Label>("player2_name");
        player3 = _uiDocument.rootVisualElement.Q<Label>("player3_name");

        back.clicked += BackToPrevScene;

    }

    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i < networkLogic.playernames.Count)
                SetPlayerName(i, networkLogic.playernames[i]);
            else
                SetPlayerName(i, "notconnected");
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

}
