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

    private VisualElement left_Side_o;
    private VisualElement left_Side_u;
    private VisualElement right_Side_Time;
    private VisualElement right_Side_Enemy_Actions;

    public Action[] selected_Actions;

    public Sprite test;


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


        left_Side_o = _Doc.rootVisualElement.Q<VisualElement>("Actionorder");
        left_Side_u = _Doc.rootVisualElement.Q<VisualElement>("Actions");
        right_Side_Enemy_Actions = _Doc.rootVisualElement.Q<VisualElement>("Enemy_Actions");
        right_Side_Time = _Doc.rootVisualElement.Q<VisualElement>("Time");


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


        action_1.style.backgroundImage = new StyleBackground(test);

        _Doc.rootVisualElement.RegisterCallback<GeometryChangedEvent>(ev =>
       {
           left_Side_o.style.width = left_Side_o.resolvedStyle.height;
           left_Side_u.style.width = left_Side_o.resolvedStyle.height;
           right_Side_Enemy_Actions.style.width = left_Side_o.resolvedStyle.height;
           right_Side_Time.style.width = left_Side_o.resolvedStyle.height;
           _Doc.rootVisualElement.Q<VisualElement>("Right_Side").style.width = left_Side_o.resolvedStyle.height;
           _Doc.rootVisualElement.Q<VisualElement>("Time_Label").style.width = left_Side_o.resolvedStyle.height;


           action_1.style.height = action_1.resolvedStyle.width;
           action_2.style.height = action_2.resolvedStyle.width;
           action_3.style.height = action_3.resolvedStyle.width;
           action_4.style.height = action_4.resolvedStyle.width;
           action_5.style.height = action_5.resolvedStyle.width;

           ordered_Action_1.style.height = ordered_Action_1.resolvedStyle.width;
           ordered_Action_2.style.height = ordered_Action_2.resolvedStyle.width;
           ordered_Action_3.style.height = ordered_Action_3.resolvedStyle.width;
           ordered_Action_4.style.height = ordered_Action_4.resolvedStyle.width;
           ordered_Action_5.style.height = ordered_Action_5.resolvedStyle.width;

           _Doc.rootVisualElement.Q<VisualElement>("Left_Side").style.marginLeft = action_1.resolvedStyle.height * 0.2f;
           _Doc.rootVisualElement.Q<VisualElement>("Right_Side").style.marginRight = action_1.resolvedStyle.height * 0.2f;
       }

       );
    }


    private void ActionButton1OnClicked()
    {
        action_1.SetEnabled(false);
        ordered_Action_1.style.backgroundImage = new StyleBackground(test);
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

    }
    private void OrderdActionButton2OnClicked()
    {

    }
    private void OrderdActionButton3OnClicked()
    {

    }
    private void OrderdActionButton4OnClicked()
    {

    }
    private void OrderdActionButton5OnClicked()
    {

    }

    public Button GetSelectableActionButtons(int button)
    {
        switch (button)
        {
            case 0:
                return action_1;
            case 1:
                return action_2;
            case 2:
                return action_3;
            case 3:
                return action_4;
            case 4:
                return action_5;
            default:
                return null;
        }
    }

    public Button GetExecutableActionButtons(int button)
    {
        switch (button)
        {
            case 0:
                return ordered_Action_1;
            case 1:
                return ordered_Action_2;
            case 2:
                return ordered_Action_3;
            case 3:
                return ordered_Action_4;
            case 4:
                return ordered_Action_5;
            default:
                return null;
        }
    }
    public void SetActions(Action[] selected_Actions){
        this.selected_Actions = selected_Actions;
        action_1.style.backgroundImage = new StyleBackground(selected_Actions[0].getIcon());
        action_2.style.backgroundImage = new StyleBackground(selected_Actions[1].getIcon());
        action_3.style.backgroundImage = new StyleBackground(selected_Actions[2].getIcon());
        action_4.style.backgroundImage = new StyleBackground(selected_Actions[3].getIcon());
        action_5.style.backgroundImage = new StyleBackground(selected_Actions[4].getIcon());
    }
}
