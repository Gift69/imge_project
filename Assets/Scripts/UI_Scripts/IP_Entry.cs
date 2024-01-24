using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class IP_Entry : MonoBehaviour
{
    private UIDocument _uiDocument;
    private Button back;

    private void Start()
    {
        _uiDocument = GetComponent<UIDocument>();
        back = _uiDocument.rootVisualElement.Q<Button>("back_button");
     
        back.clicked += BackToPrevScene;
    }
    
    private void BackToPrevScene()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
