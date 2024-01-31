using System;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Start_Menu : MonoBehaviour
{
    private UIDocument _uiDocument;

    private Button create;
    private Button join;
    private TextField playername;

    private List<String> random_names = new List<string>
    {
        "Veteran Bison", "Classy Antelope", "Nick Nock", "Showmaker Playmaker", "T1 Faker", "xxGameMasterxx",
        "Gift96", "Atomseuche3", "schmiedl", "CPP modern cpp", "x86_assembly_pro", "i_lov_coc(clash_of_clans)", 
        "hooooogriiiida", "gp main",
    };

    public String player_name;

    //private String randomName;
    private NetworkManager manager;

    public GameObject hostUI;

    public GameObject ipEntryUI;

    public GameObject clientUI;

    public GameObject ingame1;

    public GameObject ingame2;

    void Awake()
    {
        manager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();

        _uiDocument = GetComponent<UIDocument>();
        create = _uiDocument.rootVisualElement.Q<Button>("host_create");
        join = _uiDocument.rootVisualElement.Q<Button>("client_join");
        playername = _uiDocument.rootVisualElement.Q<TextField>("player_name");
        playername.label = "Name : ";
        playername.maxLength = 30;
        //playername.value = randomName;
        create.clicked += CreateButtonClicked;
        join.clicked += JoinButtonClicked;

        _uiDocument.rootVisualElement.Q<Button>("RandomName").clicked += RandomName;
    }

    void Start()
    {
        hostUI.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.None;
        ipEntryUI.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.None;
        clientUI.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.None;
        ingame1.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.None;
        ingame2.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.None;
    }

    private void CreateButtonClicked()
    {
        Debug.Log(SaveName());
        if (SaveName())
        {
            if (Application.platform != RuntimePlatform.WebGLPlayer)
            {
                manager.StartHost();
            }

            GameObject.Find("ConnectedPlayers").GetComponent<ConnectedPlayers>().playernames
                .Add(PassBetweenScenes.playername);
            _uiDocument.rootVisualElement.style.display = DisplayStyle.None;
            hostUI.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;
        }
    }

    private void JoinButtonClicked()
    {
        if (SaveName())
        {
            _uiDocument.rootVisualElement.style.display = DisplayStyle.None;
            ipEntryUI.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;
        }
        else
        {
            //textfeld soll rot werden und wackeln
        }
    }

    public bool SaveName()
    {
        if (playername.value == null)
        {
            //textfeld soll rot werden und wackeln
            Debug.Log("No name entered");
            return false;
        }

        PassBetweenScenes.playername = playername.value;
        return true;
    }

    private void RandomName()
    {
        playername.value = random_names[Random.Range(0, random_names.Count)];
    }
}