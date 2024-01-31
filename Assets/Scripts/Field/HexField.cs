using Mirror;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static HexField;

public class HexField : MonoBehaviour
{
    public const int radius = 6;
    public GameObject cellPrefab;
    public GameObject playerPrefab;
    public GameObject vPlayerPrefab;

    private GameObject[][] cells;

    //[SyncVar]
    private Player player0, player1, player2, player3;
    private Player[] players;

    public NetworkLogic netlogic;
    public Player currentPlayer
    {
        get
        {
            return netlogic.playerObjects[PassBetweenScenes.id].GetComponent<Player>();
        }
    }

    public class Selection
    {
        public Action action;
        public ActionSelection actionSelection;

        public Selection(Action action, ActionSelection actionSelection)
        {
            this.action = action;
            this.actionSelection = actionSelection;
        }
    }

    private Selection selection = null;

    public struct Coord
    {
        public int x, y;

        public static readonly Coord[] BASE_COORDS =
        {
            new(1, 0, 0),
            new(0, -1, 0),
            new(0, 0, -1),
            new(-1, 0, 0),
            new(0, 1, 0),
            new(0, 0, 1)
        };

        public static readonly Vector3 DELTA_X = new Vector3(1, 0, 0);
        public static readonly Vector3 DELTA_Y = new Vector3(Mathf.Cos(2 * Mathf.PI / 3), 0, Mathf.Sin(2 * Mathf.PI / 3));

        public Coord(int x, int y = 0, int z = 0)
        {
            this.x = x + z;
            this.y = y + z;
        }

        public static Coord operator +(Coord c1, Coord c2)
        {
            return new(c1.x + c2.x, c1.y + c2.y);
        }

        public static Coord operator -(Coord c1, Coord c2)
        {
            return new(c1.x - c2.x, c1.y - c2.y);
        }

        public static bool operator ==(Coord c1, Coord c2)
        {
            return c1.x == c2.x && c1.y == c2.y;
            ;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(Coord))
                return this == (Coord)obj;
            return false;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator !=(Coord c1, Coord c2)
        {
            return !(c1 == c2);
        }

        public Vector3 toCartesian()
        {
            return DELTA_X * x + DELTA_Y * y;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        cells = new GameObject[radius * 2 + 1][];
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i] = new GameObject[radius * 2 + 1];
        }


        Coord coord = new Coord(0);

        float cellRadius = Mathf.Cos(Mathf.PI / 6);

        GameObject centerObj = Instantiate(cellPrefab, transform);
        set(coord, centerObj);
        centerObj.transform.position = (coord.x * Coord.DELTA_X + coord.y * Coord.DELTA_Y) * cellRadius;
        centerObj.GetComponent<Cell>().isEdgePiece = false;
        centerObj.GetComponent<Cell>().setupPiece();

        for (int i = 1; i <= radius; i++)
        {
            coord.y++;

            for (int k = 0; k < Coord.BASE_COORDS.Length; k++)
            {
                for (int j = 0; j < i; j++)
                {
                    GameObject obj = Instantiate(cellPrefab, transform);

                    set(coord, obj);
                    obj.transform.position = (coord.x * Coord.DELTA_X + coord.y * Coord.DELTA_Y) * cellRadius;
                    Cell cellScript = obj.GetComponent<Cell>();
                    cellScript.setCoord(coord);
                    cellScript.isEdgePiece = i == radius;
                    cellScript.setupPiece();
                    coord += Coord.BASE_COORDS[k];
                }
            }
        }
    }

    public GameObject at(int x, int y = 0, int z = 0)
    {
        return at(new Coord(x, y, z));
    }

    public GameObject at(Coord coord)
    {
        return cells[coord.x + radius][coord.y + radius];
    }

    public void set(Coord coord, GameObject cell)
    {
        cells[coord.x + radius][coord.y + radius] = cell;
    }

    public void set(int x, int y, int z, GameObject cell)
    {
        cells[x + z + radius][y + z + radius] = cell;
    }

    public Cell cellAt(int x, int y, int z = 0)
    {
        return at(x, y, z).GetComponent<Cell>();
    }

    public Cell cellAt(Coord coord)
    {
        return at(coord).GetComponent<Cell>();
    }

    public Cell[] getSequence(Coord start, Coord delta, int maxCount = -1)
    {
        if (delta.x == 0 && delta.y == 0)
            if (isValidCoord(start))
                return new Cell[] { cellAt(start) };
            else
                return new Cell[] { };
        List<Cell> sequence = new List<Cell>();
        if (maxCount < 0)
            while (isValidCoord(start))
            {
                sequence.Add(cellAt(start));
                start += delta;
            }
        else
            while (sequence.Count <= maxCount && isValidCoord(start))
            {
                sequence.Add(cellAt(start));
                start += delta;
            }

        return sequence.ToArray();
    }

    public Cell[] getArea(Coord center, int radius)
    {
        List<Cell> sequence = new List<Cell>();

        if (isValidCoord(center))
            sequence.Add(cellAt(center));
        for (int i = 1; i <= radius; i++)
        {
            center.y++;
            for (int k = 0; k < Coord.BASE_COORDS.Length; k++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (isValidCoord(center))
                        sequence.Add(cellAt(center));
                    center += Coord.BASE_COORDS[k];
                }
            }
        }

        return sequence.ToArray();
    }

    public Cell[] getCircle(Coord center, int outerRadius, int innerRadius = 1)
    {
        List<Cell> sequence = new List<Cell>();

        center.y += innerRadius;
        for (int i = innerRadius; i <= outerRadius; i++)
        {
            for (int k = 0; k < Coord.BASE_COORDS.Length; k++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (isValidCoord(center))
                        sequence.Add(cellAt(center));
                    center += Coord.BASE_COORDS[k];
                }
            }

            center.y++;
        }

        return sequence.ToArray();
    }

    public bool isValidCoord(Coord coord)
    {
        return Math.Abs(coord.x) <= radius && Math.Abs(coord.y) <= radius && Math.Abs(coord.x - coord.y) <= radius;
    }

    public void removeOuterIndicators()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            for (int j = 0; j < cells[i].Length; j++)
                if (cells[i][j] != null)
                    cells[i][j].GetComponent<Cell>().removeOuterIndicator();
        }
    }

    public void removeInnerIndicators()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            for (int j = 0; j < cells[i].Length; j++)
                if (cells[i][j] != null)
                    cells[i][j].GetComponent<Cell>().removeInnerIndicator();
        }
    }

    public void removeIndicators()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            for (int j = 0; j < cells[i].Length; j++)
                if (cells[i][j] != null)
                {
                    cells[i][j].GetComponent<Cell>().removeOuterIndicator();
                    cells[i][j].GetComponent<Cell>().removeInnerIndicator();
                }
        }
    }

    public void startSelection(Action action)
    {
        removeIndicators();
        this.selection = new Selection(action, action.getActionSelection(currentPlayer.VPlayer));
        Cell.indicateOuter(selection.actionSelection.getOuterIndicatorCells());
    }

    public void cancelSelection()
    {
        removeIndicators();
        this.selection = null;
    }

    public void selectionHover(Cell cell)
    {
        Cell.indicateInner(
            selection.actionSelection.getInnerIndicatorCells(cell.getCoord() - currentPlayer.VPlayer.cell.getCoord()));
    }

    public void select(Cell cell)
    {
        selection.action.setValue(cell.getCoord() - currentPlayer.VPlayer.cell.getCoord());
        currentPlayer.selectAction(selection.action);
        cancelSelection();
    }

    public void forEach(Action<Cell> callback)
    {
        Coord coord = new(0);
        callback(cellAt(coord));
        for (int i = 1; i <= radius; i++)
        {
            coord.y++;

            for (int k = 0; k < Coord.BASE_COORDS.Length; k++)
            {
                for (int j = 0; j < i; j++)
                {
                    callback(cellAt(coord));
                    coord += Coord.BASE_COORDS[k];
                }
            }
        }
    }

    private MoveAction action = new MoveAction();

    private SwordSlashAction swordSlashAction = new SwordSlashAction();


    private Transform lastParent;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            startSelection(new MoveAction());
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            startSelection(new SwordSlashAction());
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            cancelSelection();
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            startSelection(new BombAction());
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            startSelection(new ShootAction());
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            startSelection(new StunFieldAction());
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            startSelection(new MiningAction());
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentPlayer.removeActionAt(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentPlayer.removeActionAt(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentPlayer.removeActionAt(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentPlayer.removeActionAt(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            currentPlayer.removeActionAt(4);
        }



        if (selection != null)
        {
            if (Input.GetMouseButtonUp(1))
            {
                cancelSelection();
                return;
            }

            bool click = Input.GetMouseButtonUp(0);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction, Color.red);
            RaycastHit hit;
            bool racasthit = Physics.Raycast(ray, out hit, 200);
            if (hit.transform == null || lastParent != hit.transform)
                removeInnerIndicators();
            if (racasthit)
            {
                if (click)
                    select(hit.transform.parent.GetComponentInParent<Cell>());
                else
                    selectionHover(hit.transform.parent.GetComponentInParent<Cell>());
            }
            else if (click)
                cancelSelection();
            lastParent = hit.transform;
        }

        //Debug.Log(action.getValue().x + " " + action.getValue().y);
    }
}
