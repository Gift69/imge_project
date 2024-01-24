using System.Collections;
using System.Collections.Generic;
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
    
    
    void Start()
    {
        _uiDocument = GetComponent<UIDocument>();
        back = _uiDocument.rootVisualElement.Q<Button>("back_button");
     
        back.clicked += BackToPrevScene;

    }

    private void BackToPrevScene()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    private void StartScene()
    {
    }
}
