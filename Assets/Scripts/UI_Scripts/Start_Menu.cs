using System;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Random = System.Random;

public class Start_Menu : MonoBehaviour
{
    private UIDocument _uiDocument;

    private Button create;
    private Button join;
    private TextField playername;
    private List<String> random_names = new List<string> { "q", "w", "e", "r", "t", "z", "u", "i" };
    public String player_name;
    //private String randomName;
    private NetworkManager manager;

    public GameObject hostUI;

    public GameObject ipEntryUI;

    public GameObject clientUI;


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
        playername.value = RandomName(random_names);
        create.clicked += CreateButtonClicked;
        join.clicked += JoinButtonClicked;


    }

    void Start()
    {
        hostUI.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.None;
        ipEntryUI.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.None;
        clientUI.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.None;
    }

    private void CreateButtonClicked()
    {
        if (SaveName())
        {
            if (Application.platform != RuntimePlatform.WebGLPlayer)
            {
                manager.StartHost();
            }
            GameObject.Find("NetworkLogic").GetComponent<NetworkLogic>().playernames.Add(PassBetweenScenes.playername);
            _uiDocument.rootVisualElement.style.display = DisplayStyle.None;
            hostUI.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;
            Debug.Log("playername:" + player_name);
        }
        else
        {//textfeld soll rot werden und wackeln
        }
    }

    private void JoinButtonClicked()
    {
        if (SaveName())
        {
            _uiDocument.rootVisualElement.style.display = DisplayStyle.None;
            ipEntryUI.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex; Debug.Log("playername:" + player_name);
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

    private string RandomName(List<String> rn)
    {
        if (rn == null || rn.Count == 0)
        {
            throw new ArgumentException("List Empty");
        }

        Random random = new Random();
        int randomidx = random.Next(0, rn.Count);
        return rn[randomidx];
    }



}
