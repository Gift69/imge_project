using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static HexField;

public class HexField : MonoBehaviour
{
    public int radius;
    public GameObject cellPrefab;
    public GameObject playerPrefab;

    private GameObject[][] cells;

    public struct Coord
    {
        public int x, y;

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
    }

    private static readonly Coord[] CIRCLE_DELTAS = {
            new(0, 0, -1),
            new(-1),
            new(0, 1),
            new(0, 0, 1),
            new(1),
            new(0, -1)
        };

    public Vector3 DELTA_X = new Vector3(1.0f, 0, 0);
    public Vector3 DELTA_Y = new Vector3(Mathf.Cos(2 * Mathf.PI / 3), 0, Mathf.Sin(2 * Mathf.PI / 3));

    // Start is called before the first frame update
    void Start()
    {
        cells = new GameObject[radius * 2 + 1][];
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i] = new GameObject[radius * 2 + 1];
        }


        Coord coord = new Coord(0);

        set(coord, Instantiate(cellPrefab, transform));
        at(coord).transform.position = coord.x * DELTA_X + coord.y * DELTA_Y;

        for (int i = 1; i <= radius; i++)
        {
            coord.x++;

            for (int k = 0; k < CIRCLE_DELTAS.Length; k++)
            {
                for (int j = 0; j < i; j++)
                {
                    set(coord, Instantiate(cellPrefab, transform));
                    var obj = at(coord);
                    obj.transform.position = coord.x * DELTA_X + coord.y * DELTA_Y;
                    obj.GetComponent<Cell>().setCoord(coord);
                    coord += CIRCLE_DELTAS[k];
                }

            }
        }

        var player = Instantiate(playerPrefab);
        cellAt(2, 0).placeBoardPiece(player.GetComponent<Player>());

        var player2 = Instantiate(playerPrefab);
        cellAt(-2, 0).placeBoardPiece(player2.GetComponent<Player>());

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
            center.x++;
            for (int k = 0; k < CIRCLE_DELTAS.Length; k++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (isValidCoord(center))
                        sequence.Add(cellAt(center));
                    center += CIRCLE_DELTAS[k];
                }
            }
        }
        return sequence.ToArray();
    }

    public bool isValidCoord(Coord coord)
    {
        return Math.Abs(coord.x) <= radius && Math.Abs(coord.y) <= radius && Math.Abs(coord.x - coord.y) <= radius;
    }

    public void removeHighlight()
    {
        for(int i = 0; i < cells.Length; i++)
        {
            for(int j = 0; j < cells[i].Length; j++)
                if (cells[i][j] != null)
                    cells[i][j].GetComponent<Cell>().removeHighlight();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            removeHighlight();
            Cell.highlight(getSequence(new(2, 0), new(0, 0, 1)));
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            removeHighlight();
            Cell.highlight(getArea(new(2, 0), 2));
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            removeHighlight();
        }
    }
}
