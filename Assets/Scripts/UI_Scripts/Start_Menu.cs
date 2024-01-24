using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Start_Menu : MonoBehaviour
{
    private UIDocument _uiDocument;

    private Button create;
    private Button join;

    void Start()
    {
        _uiDocument = GetComponent<UIDocument>();
        create = _uiDocument.rootVisualElement.Q<Button>("host_create");
        join = _uiDocument.rootVisualElement.Q<Button>("client_join");

        create.clicked += CreateButtonClicked;
        join.clicked += JoinButtonClicked;
    }

    private void CreateButtonClicked()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    private void JoinButtonClicked()
    {
        SceneManager.LoadScene(4, LoadSceneMode.Single);
    }

}
