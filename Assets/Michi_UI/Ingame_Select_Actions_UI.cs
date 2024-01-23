using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Ingame_Select_Actions_UI : MonoBehaviour
{

    private UIDocument _Doc;

    private Button walk;
    private Button collect;
    private Button do_nothing;
    private Button special_action_1;
    private Button special_action_2;

    private VisualElement player_info;
    private VisualElement right_Side_Time;
    private VisualElement left_Side;

    public float f = 0.5f;


    void Start()
    {
        _Doc = GetComponent<UIDocument>();
        walk = _Doc.rootVisualElement.Q<Button>("Walk");
        collect = _Doc.rootVisualElement.Q<Button>("Collect");
        do_nothing = _Doc.rootVisualElement.Q<Button>("Do_Nothing");
        special_action_1 = _Doc.rootVisualElement.Q<Button>("Special_Action_1");
        special_action_2 = _Doc.rootVisualElement.Q<Button>("Special_Action_2");

        player_info = _Doc.rootVisualElement.Q<VisualElement>("Player_Info");

        right_Side_Time = _Doc.rootVisualElement.Q<VisualElement>("Time");

        left_Side = _Doc.rootVisualElement.Q<VisualElement>("Left_Side");


        walk.clicked += WalkButtonOnClicked;
        collect.clicked += CollectButtonOnClicked;
        do_nothing.clicked += DoNothingButtonOnClicked;
        special_action_1.clicked += SpecialActionButton1OnClicked;
        special_action_2.clicked += SpecialActionButton2OnClicked;

        _Doc.rootVisualElement.RegisterCallback<GeometryChangedEvent>(ev =>
       {
           player_info.style.width = left_Side.resolvedStyle.height * 0.5f;
           right_Side_Time.style.width = player_info.style.width;
           _Doc.rootVisualElement.Q<VisualElement>("Normal_Actions").style.width = player_info.style.width;
           _Doc.rootVisualElement.Q<VisualElement>("Special_Actions").style.width = player_info.style.width;

           left_Side.style.marginLeft = left_Side.resolvedStyle.height * 0.04f;
           _Doc.rootVisualElement.Q<VisualElement>("Right_Side").style.marginRight = left_Side.resolvedStyle.height * 0.04f;


           special_action_1.style.height = special_action_1.resolvedStyle.width;
           special_action_2.style.height = special_action_2.resolvedStyle.width;
           collect.style.height = collect.resolvedStyle.width;
           walk.style.height = walk.resolvedStyle.width;
           do_nothing.style.height = do_nothing.resolvedStyle.width;



       }

       );



    }

    private void WalkButtonOnClicked()
    {

    }
    private void CollectButtonOnClicked()
    {

    }
    private void DoNothingButtonOnClicked()
    {

    }
    private void SpecialActionButton1OnClicked()
    {

    }
    private void SpecialActionButton2OnClicked()
    {

    }
}