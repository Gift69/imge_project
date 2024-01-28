using UnityEngine;
using UnityEngine.UIElements;

public class Ingame_Select_Actions_UI : MonoBehaviour
{

    private UIDocument _Doc;
    public NetworkLogic netLogic;

    private Button walk;
    private Button collect;
    private Button do_nothing;
    private Button special_action_1;
    private Button special_action_2;

    private Button action_1;
    private Button action_2;
    private Button action_3;
    private Button action_4;
    private Button action_5;

    private VisualElement player_info;
    private VisualElement right_Side_Time;
    private VisualElement left_Side;

    private Label timer;

    public float f = 0.5f;

    private Character character;

    private Action[] selected_Actions = new Action[5];

    public GameObject nextUI;



    void Awake()
    {
        character = new TestCharacter();
        _Doc = GetComponent<UIDocument>();
        walk = _Doc.rootVisualElement.Q<Button>("Walk");
        collect = _Doc.rootVisualElement.Q<Button>("Collect");
        do_nothing = _Doc.rootVisualElement.Q<Button>("Do_Nothing");
        special_action_1 = _Doc.rootVisualElement.Q<Button>("Special_Action_1");
        special_action_2 = _Doc.rootVisualElement.Q<Button>("Special_Action_2");
        action_1 = _Doc.rootVisualElement.Q<Button>("Action_1");
        action_2 = _Doc.rootVisualElement.Q<Button>("Action_2");
        action_3 = _Doc.rootVisualElement.Q<Button>("Action_3");
        action_4 = _Doc.rootVisualElement.Q<Button>("Action_4");
        action_5 = _Doc.rootVisualElement.Q<Button>("Action_5");

        timer = _Doc.rootVisualElement.Q<Label>("Time_Label");



        player_info = _Doc.rootVisualElement.Q<VisualElement>("Player_Info");

        right_Side_Time = _Doc.rootVisualElement.Q<VisualElement>("Time");

        left_Side = _Doc.rootVisualElement.Q<VisualElement>("Left_Side");



        walk.style.backgroundImage = new StyleBackground(character.GetMoveAction().getIcon());
        collect.style.backgroundImage = new StyleBackground(character.GetCoinAction().getIcon());
        do_nothing.style.backgroundImage = new StyleBackground(character.GetDoNothingAction().getIcon());
        special_action_1.style.backgroundImage = new StyleBackground(character.GetSpecialAction1().getIcon());
        special_action_2.style.backgroundImage = new StyleBackground(character.GetSpecialAction2().getIcon());



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


        walk.clicked += WalkButtonOnClicked;
        collect.clicked += CollectButtonOnClicked;
        do_nothing.clicked += DoNothingButtonOnClicked;
        special_action_1.clicked += SpecialActionButton1OnClicked;
        special_action_2.clicked += SpecialActionButton2OnClicked;

        action_1.clicked += Action1ButtonOnClicked;
        action_2.clicked += Action2ButtonOnClicked;
        action_3.clicked += Action3ButtonOnClicked;
        action_4.clicked += Action4ButtonOnClicked;
        action_5.clicked += Action5ButtonOnClicked;
    }


    void Start()
    {
        Debug.Log(nextUI.GetComponent<UIDocument>());
        nextUI.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.None;
    }

    private void WalkButtonOnClicked()
    {
        InsertActionIntoArray(character.GetMoveAction());
    }
    private void CollectButtonOnClicked()
    {
        InsertActionIntoArray(character.GetCoinAction());
    }
    private void DoNothingButtonOnClicked()
    {
        InsertActionIntoArray(character.GetDoNothingAction());
    }
    private void SpecialActionButton1OnClicked()
    {
        InsertActionIntoArray(character.GetSpecialAction1());
    }
    private void SpecialActionButton2OnClicked()
    {
        InsertActionIntoArray(character.GetSpecialAction2());
    }
    private void Action1ButtonOnClicked()
    {
        action_1.style.backgroundImage = null;
        selected_Actions[0] = null;
        walk.SetEnabled(true);
        collect.SetEnabled(true);
        do_nothing.SetEnabled(true);
        special_action_1.SetEnabled(true);
        special_action_2.SetEnabled(true);
    }
    private void Action2ButtonOnClicked()
    {
        action_2.style.backgroundImage = null;
        selected_Actions[1] = null;
        walk.SetEnabled(true);
        collect.SetEnabled(true);
        do_nothing.SetEnabled(true);
        special_action_1.SetEnabled(true);
        special_action_2.SetEnabled(true);
    }
    private void Action3ButtonOnClicked()
    {
        action_3.style.backgroundImage = null;
        selected_Actions[2] = null;
        walk.SetEnabled(true);
        collect.SetEnabled(true);
        do_nothing.SetEnabled(true);
        special_action_1.SetEnabled(true);
        special_action_2.SetEnabled(true);
    }
    private void Action4ButtonOnClicked()
    {
        action_4.style.backgroundImage = null;
        selected_Actions[3] = null;
        walk.SetEnabled(true);
        collect.SetEnabled(true);
        do_nothing.SetEnabled(true);
        special_action_1.SetEnabled(true);
        special_action_2.SetEnabled(true);
    }
    private void Action5ButtonOnClicked()
    {
        action_5.style.backgroundImage = null;
        selected_Actions[4] = null;
        walk.SetEnabled(true);
        collect.SetEnabled(true);
        do_nothing.SetEnabled(true);
        special_action_1.SetEnabled(true);
        special_action_2.SetEnabled(true);
    }


    private void InsertActionIntoArray(Action action)
    {
        for (int i = 0; i < selected_Actions.Length; i++)
        {
            if (selected_Actions[i] == null)
            {
                selected_Actions[i] = action;
                switch (i)
                {
                    case 0:
                        action_1.style.backgroundImage = new StyleBackground(action.getIcon());
                        break;
                    case 1:
                        action_2.style.backgroundImage = new StyleBackground(action.getIcon());
                        break;
                    case 2:
                        action_3.style.backgroundImage = new StyleBackground(action.getIcon());
                        break;
                    case 3:
                        action_4.style.backgroundImage = new StyleBackground(action.getIcon());
                        break;
                    case 4:
                        action_5.style.backgroundImage = new StyleBackground(action.getIcon());
                        break;
                    default:
                        break;
                }
                int number_of_elements = 0;
                foreach (Action a in selected_Actions)
                    if (a != null)
                        number_of_elements++;
                if (number_of_elements == selected_Actions.Length)
                {
                    walk.SetEnabled(false);
                    collect.SetEnabled(false);
                    do_nothing.SetEnabled(false);
                    special_action_1.SetEnabled(false);
                    special_action_2.SetEnabled(false);
                }
                return;
            }
        }
    }


    private void FinishSelecting()
    {
        _Doc.rootVisualElement.style.display = DisplayStyle.None;
        nextUI.GetComponent<Ingame_UI>().SetActions(selected_Actions);
        nextUI.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Play the animation once
            FinishSelecting();
        }
        timer.text = ((int)Mathf.Ceil(netLogic.timer)).ToString();
    }
}