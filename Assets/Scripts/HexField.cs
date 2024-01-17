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

    // Start is called before the first frame update
    void Start()
    {
        Vector3 deltaX = new Vector3(1.0f, 0, 0);
        Vector3 deltaY = new Vector3(Mathf.Cos(2 * Mathf.PI / 3), 0, Mathf.Sin(2 * Mathf.PI / 3));
        Vector3 deltaZ = deltaX + deltaY;

        Debug.Log(deltaY);

        Coord c = new(1);

        cells = new GameObject[radius * 2 + 1][];
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i] = new GameObject[radius * 2 + 1];
        }


        Coord coord = new Coord(0);

        set(coord, Instantiate(cellPrefab, transform));
        at(coord).transform.position = coord.x * deltaX + coord.y * deltaY;

        for (int i = 1; i <= radius; i++)
        {
            coord.x++;

            for (int k = 0; k < CIRCLE_DELTAS.Length; k++)
            {
                for (int j = 0; j < i; j++)
                {
                    set(coord, Instantiate(cellPrefab, transform));
                    at(coord).transform.position = coord.x * deltaX + coord.y * deltaY;
                    coord += CIRCLE_DELTAS[k];
                }

            }
        }

        GameObject[] s;

        s = getSequence(new(radius - 1), new(0, 0, -1));
        foreach(GameObject obj in s)
        {
            obj.transform.position = obj.transform.position + Vector3.up * 0.5f;
        }

        s = getArea(new(0, 0), 2);
        foreach (GameObject obj in s)
        {
            obj.transform.position = obj.transform.position + Vector3.down * 0.5f;
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

    public GameObject[] getSequence(Coord start, Coord delta)
    {
        List<GameObject> sequence = new List<GameObject>();
        while(isValidCoord(start))
        {
            sequence.Add(at(start));
            start += delta;
        }

        return sequence.ToArray();
    }

    public GameObject[] getArea(Coord center, int radius)
    {
        List<GameObject> sequence = new List<GameObject>();

        if (isValidCoord(center))
            sequence.Add(at(center));
        for (int i = 1; i <= radius; i++)
        {
            center.x++;
            for (int k = 0; k < CIRCLE_DELTAS.Length; k++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (isValidCoord(center))
                        sequence.Add(at(center));
                    center += CIRCLE_DELTAS[k];
                }
            }
        }
        return sequence.ToArray();
    }

    private bool isValidCoord(Coord coord)
    {
        return Math.Abs(coord.x) <= radius && Math.Abs(coord.y) <= radius && Math.Abs(coord.x - coord.y) <= radius;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
