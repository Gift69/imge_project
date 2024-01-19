using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;



public class Ingame_UI : MonoBehaviour
{
    private UIDocument _Doc;

    private Button action_1;
    private Button action_2;
    private Button action_3;
    private Button action_4;
    private Button action_5;
    private Button ordered_Action_1;
    private Button ordered_Action_2;
    private Button ordered_Action_3;
    private Button ordered_Action_4;
    private Button ordered_Action_5;

    private Button _homeButton;
    private Button _resumeButton;
    private Label time;

    // Start is called before the first frame update
    void Start()
    {
        _Doc = GetComponent<UIDocument>();
        action_1 = _Doc.rootVisualElement.Q<Button>("Action_1");
        action_2 = _Doc.rootVisualElement.Q<Button>("Action_2");
        action_3 = _Doc.rootVisualElement.Q<Button>("Action_3");
        action_4 = _Doc.rootVisualElement.Q<Button>("Action_4");
        action_5 = _Doc.rootVisualElement.Q<Button>("Action_5");

        ordered_Action_1 = _Doc.rootVisualElement.Q<Button>("Ordered_Action_1");
        ordered_Action_2 = _Doc.rootVisualElement.Q<Button>("Ordered_Action_2");
        ordered_Action_3 = _Doc.rootVisualElement.Q<Button>("Ordered_Action_3");
        ordered_Action_4 = _Doc.rootVisualElement.Q<Button>("Ordered_Action_4");
        ordered_Action_5 = _Doc.rootVisualElement.Q<Button>("Ordered_Action_5");
        time = _Doc.rootVisualElement.Q<Label>("Time_Label");

        action_1.clicked += ActionButton1OnClicked;
        action_2.clicked += ActionButton2OnClicked;
        action_3.clicked += ActionButton3OnClicked;
        action_4.clicked += ActionButton4OnClicked;
        action_5.clicked += ActionButton5OnClicked;

        ordered_Action_1.clicked += OrderdActionButton1OnClicked;
        ordered_Action_2.clicked += OrderdActionButton2OnClicked;
        ordered_Action_3.clicked += OrderdActionButton3OnClicked;
        ordered_Action_4.clicked += OrderdActionButton4OnClicked;
        ordered_Action_5.clicked += OrderdActionButton5OnClicked;



        action_1.Focus();

        action_1.RegisterCallback<MouseOverEvent>((type) =>
        {
            _homeButton.Focus();
        });

        /* _Doc.rootVisualElement.RegisterCallback<GeometryChangedEvent>(ev =>
        {
            if (ev.oldRect.width != ev.newRect.width && ev.oldRect.height != ev.newRect.height)
            {
                _retryButton.style.fontSize = _retryButton.resolvedStyle.height;
                _homeButton.style.fontSize = _homeButton.resolvedStyle.height;
                _resumeButton.style.fontSize = _resumeButton.resolvedStyle.height;
                _text.style.fontSize = _text.resolvedStyle.height * 6 / 10;
            }
        }

        ); */
    }


    private void ActionButton1OnClicked()
    {

    }
    private void ActionButton2OnClicked()
    {

    }
    private void ActionButton3OnClicked()
    {

    }
    private void ActionButton4OnClicked()
    {

    }
    private void ActionButton5OnClicked()
    {

    }
    private void OrderdActionButton1OnClicked()
    {

    }private void OrderdActionButton2OnClicked()
    {

    }private void OrderdActionButton3OnClicked()
    {

    }private void OrderdActionButton4OnClicked()
    {

    }private void OrderdActionButton5OnClicked()
    {

    }
}
