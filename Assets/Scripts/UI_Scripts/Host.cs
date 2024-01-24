using System.Collections;
using System.Collections.Generic;
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
    private Label IP;
    private Label host;
    private Label player1;
    private Label player2;
    private Label player3;
    private VisualElement player1_char;
    private  VisualElement player2_char;
    private  VisualElement player3_char;
    private  VisualElement host_char;
    
    
    void Start()
    {
        _uiDocument = GetComponent<UIDocument>();
        back = _uiDocument.rootVisualElement.Q<Button>("back_button");
        start = _uiDocument.rootVisualElement.Q<Button>("start_button");
        character1 = _uiDocument.rootVisualElement.Q<Button>("Char1");
        character2 = _uiDocument.rootVisualElement.Q<Button>("Char2");
        character3 = _uiDocument.rootVisualElement.Q<Button>("Char3");
        character4 = _uiDocument.rootVisualElement.Q<Button>("Char4");

        host = _uiDocument.rootVisualElement.Q<Label>("host_name");
        player1 = _uiDocument.rootVisualElement.Q<Label>("player1_name");
        player2 = _uiDocument.rootVisualElement.Q<Label>("player2_name");
        player3 = _uiDocument.rootVisualElement.Q<Label>("player3_name");

        host_char = _uiDocument.rootVisualElement.Q<VisualElement>("Character_icon_1");
     player1_char = _uiDocument.rootVisualElement.Q<VisualElement>("Character_icon_2");
     player2_char = _uiDocument.rootVisualElement.Q<VisualElement>("Character_icon_3");
     player3_char = _uiDocument.rootVisualElement.Q<VisualElement>("Character_icon_4");
        
        
        back.clicked += BackToPrevScene;
        start.clicked += StartScene;
    }

    private void BackToPrevScene()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    private void StartScene()
    {
    }

}
