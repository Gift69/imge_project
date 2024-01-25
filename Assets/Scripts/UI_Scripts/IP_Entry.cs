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
    private TextField ip_entry;
    private Button enter;

    private void Start()
    {
        _uiDocument = GetComponent<UIDocument>();
        back = _uiDocument.rootVisualElement.Q<Button>("back_button");
        ip_entry= _uiDocument.rootVisualElement.Q<TextField>("IP_entry");
        ip_entry.label = new string("IP ADDRESS : ");
        enter = _uiDocument.rootVisualElement.Q<Button>("enter");
        back.clicked += BackToPrevScene;
        enter.clicked += CheckIPAddress;
    }
    
    private void BackToPrevScene()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    private void CheckIPAddress()
    {
        if (ip_entry.text != null)
        {
            if (Host.IP_address.Equals(ip_entry.text))
            {
                SceneManager.LoadScene(3, LoadSceneMode.Single);
            }
            else
            { //rotes feld
            }
            //rotes feld
        }
    }
}
