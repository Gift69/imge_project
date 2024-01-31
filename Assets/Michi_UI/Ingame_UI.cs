using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;



public class Ingame_UI : MonoBehaviour
{

    public NetworkLogic netLogic;

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
    private Label timer;

    public Action[] selected_Actions;

    public Sprite test;

    public HexField hexField;

    public Sprite empty;

    public GameObject selectUI;

    public bool styled = false;
    public int numberofstyles = 0;

    // Start is called before the first frame update
    void Awake()
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
        timer = _Doc.rootVisualElement.Q<Label>("Time_Label");



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
        ordered_Action_1.SetEnabled(false);
        ordered_Action_2.SetEnabled(false);
        ordered_Action_3.SetEnabled(false);
        ordered_Action_4.SetEnabled(false);
        ordered_Action_5.SetEnabled(false);

        styled = true;
    }
    void Start()
    {
        //netLogic = GameObject.FindGameObjectWithTag("NetworkLogic").GetComponent<NetworkLogic>();

    }

    void Update()
    {
        if (_Doc.rootVisualElement.style.display != DisplayStyle.None)
        {
            if (styled && numberofstyles < 3)
            {
                if (numberofstyles == 2)
                    styled = false;
                numberofstyles++;
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

            if (netLogic.mode == NetworkLogic.Mode.ACTION_SELECTION)
            {
                numberofstyles = 0;
                _Doc.rootVisualElement.style.display = DisplayStyle.None;
                _Doc.rootVisualElement.Q<VisualElement>("Left_Side").style.display = DisplayStyle.Flex;
                selectUI.GetComponent<Ingame_Select_Actions_UI>().ClearUI();
                selectUI.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;
            }
            else if (netLogic.mode == NetworkLogic.Mode.ACTION_EXECUTION)
            {
                _Doc.rootVisualElement.Q<VisualElement>("Left_Side").style.display = DisplayStyle.None;
                if (hexField.currentPlayer.vPlayer != null)
                    hexField.currentPlayer.removeAllActions();
            }

            for (int i = 0; i < netLogic.otherplayerActions.Count; i++)
            {
                SetPlayer(i, netLogic.otherplayerActions[i]);
            }
            timer.text = ((int)Mathf.Ceil(netLogic.timer)).ToString();

        }
    }


    private void ActionButton1OnClicked()
    {
        if (selected_Actions[0].isValid())
            hexField.currentPlayer.selectAction(selected_Actions[0]);
        else
            hexField.startSelection(selected_Actions[0]);
    }
    private void ActionButton2OnClicked()
    {
        if (selected_Actions[1].isValid())
            hexField.currentPlayer.selectAction(selected_Actions[1]);
        else
            hexField.startSelection(selected_Actions[1]);
    }
    private void ActionButton3OnClicked()
    {
        if (selected_Actions[2].isValid())
            hexField.currentPlayer.selectAction(selected_Actions[2]);
        else
            hexField.startSelection(selected_Actions[2]);
    }
    private void ActionButton4OnClicked()
    {
        if (selected_Actions[3].isValid())
            hexField.currentPlayer.selectAction(selected_Actions[3]);
        else
            hexField.startSelection(selected_Actions[3]);
    }
    private void ActionButton5OnClicked()
    {
        if (selected_Actions[4].isValid())
            hexField.currentPlayer.selectAction(selected_Actions[4]);
        else
            hexField.startSelection(selected_Actions[4]);
    }
    private void OrderdActionButton1OnClicked()
    {
        hexField.currentPlayer.removeActionAt(0);
    }
    private void OrderdActionButton2OnClicked()
    {
        hexField.currentPlayer.removeActionAt(1);

    }
    private void OrderdActionButton3OnClicked()
    {
        hexField.currentPlayer.removeActionAt(2);

    }
    private void OrderdActionButton4OnClicked()
    {
        hexField.currentPlayer.removeActionAt(3);

    }
    private void OrderdActionButton5OnClicked()
    {
        hexField.currentPlayer.removeActionAt(4);
    }

    public Button GetSelectableActionButton(int button)
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

    public Button GetExecutableActionButton(int button)
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
    public void SetActions(Action[] selected_Actions)
    {
        this.selected_Actions = selected_Actions;
        action_1.style.backgroundImage = new StyleBackground(selected_Actions[0].getIcon());
        action_2.style.backgroundImage = new StyleBackground(selected_Actions[1].getIcon());
        action_3.style.backgroundImage = new StyleBackground(selected_Actions[2].getIcon());
        action_4.style.backgroundImage = new StyleBackground(selected_Actions[3].getIcon());
        action_5.style.backgroundImage = new StyleBackground(selected_Actions[4].getIcon());
        selected_Actions[0].selectableButton = action_1;
        selected_Actions[1].selectableButton = action_2;
        selected_Actions[2].selectableButton = action_3;
        selected_Actions[3].selectableButton = action_4;
        selected_Actions[4].selectableButton = action_5;
    }

    public void SetPlayer(int i, PlayerActions playerActions)
    {
        VisualElement root = _Doc.rootVisualElement.Q<VisualElement>("Enemy_" + (i + 1));
        Label name = (Label)root.Children().ElementAt<VisualElement>(0).Children().ElementAt<VisualElement>(0);
        name.text = playerActions.playername;
        VisualElement a1 = root.Children().ElementAt<VisualElement>(1).Children().ElementAt<VisualElement>(0);
        a1.style.backgroundImage = new StyleBackground(GetIcon(playerActions.a1));

        VisualElement a2 = root.Children().ElementAt<VisualElement>(1).Children().ElementAt<VisualElement>(1);
        a2.style.backgroundImage = new StyleBackground(GetIcon(playerActions.a2));

        VisualElement a3 = root.Children().ElementAt<VisualElement>(1).Children().ElementAt<VisualElement>(2);
        a3.style.backgroundImage = new StyleBackground(GetIcon(playerActions.a3));

        VisualElement a4 = root.Children().ElementAt<VisualElement>(1).Children().ElementAt<VisualElement>(3);
        a4.style.backgroundImage = new StyleBackground(GetIcon(playerActions.a4));
        VisualElement a5 = root.Children().ElementAt<VisualElement>(1).Children().ElementAt<VisualElement>(4);
        a5.style.backgroundImage = new StyleBackground(GetIcon(playerActions.a5));
    }
    public Sprite[] actionIcons = new Sprite[8];

    public Sprite GetIcon(Action.Type type)
    {
        return actionIcons[(int)type];
    }
    public void ClearUI()
    {
        styled = true;
        ordered_Action_1.SetEnabled(false);
        ordered_Action_2.SetEnabled(false);
        ordered_Action_3.SetEnabled(false);
        ordered_Action_4.SetEnabled(false);
        ordered_Action_5.SetEnabled(false);

        ordered_Action_1.style.backgroundImage = new StyleBackground(empty);
        ordered_Action_2.style.backgroundImage = new StyleBackground(empty);
        ordered_Action_3.style.backgroundImage = new StyleBackground(empty);
        ordered_Action_4.style.backgroundImage = new StyleBackground(empty);
        ordered_Action_5.style.backgroundImage = new StyleBackground(empty);


        action_1.SetEnabled(true);
        action_2.SetEnabled(true);
        action_3.SetEnabled(true);
        action_4.SetEnabled(true);
        action_5.SetEnabled(true);

    }
}
