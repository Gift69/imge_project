using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexField : MonoBehaviour
{
    public int radius;
    public GameObject cellPrefab;

    private GameObject[][] cells;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 deltaX = new Vector3 (1.0f, 0, 0);
        Vector3 deltaY = new Vector3(Mathf.Cos(2 * Mathf.PI / 3), 0, Mathf.Sin(2 * Mathf.PI / 3));
        Vector3 deltaZ = deltaX + deltaY;

        Debug.Log(deltaY);

        cells = new GameObject[radius * 2 + 1][];
        for(int i = 0; i < cells.Length; i++)
        {
            cells[i] = new GameObject[radius * 2 + 1];
        }
        

        int x = 0, y = 0, z = 0;
        set(x, y, Instantiate(cellPrefab, transform));
        get(x, y).transform.position = x * deltaX + y * deltaY;

        for(int i = 1; i <= radius; i++)
        {
            x++;
            for(int j = 0; j < i; j++)
            {
                set(x, y, z, Instantiate(cellPrefab, transform));
                get(x, y, z).transform.position = x * deltaX + y * deltaY + z * deltaZ;
                z--;
            }

            for (int j = 0; j < i; j++)
            {
                set(x, y, z, Instantiate(cellPrefab, transform));
                get(x, y, z).transform.position = x * deltaX + y * deltaY + z * deltaZ;
                x--;
            }

            for (int j = 0; j < i; j++)
            {
                set(x, y, z, Instantiate(cellPrefab, transform));
                get(x, y, z).transform.position = x * deltaX + y * deltaY + z * deltaZ;
                y++;
            }

            for (int j = 0; j < i; j++)
            {
                set(x, y, z, Instantiate(cellPrefab, transform));
                get(x, y, z).transform.position = x * deltaX + y * deltaY + z * deltaZ;
                z++;
            }

            for (int j = 0; j < i; j++)
            {
                set(x, y, z, Instantiate(cellPrefab, transform));
                get(x, y, z).transform.position = x * deltaX + y * deltaY + z * deltaZ;
                x++;
            }

            for (int j = 0; j < i; j++)
            {
                set(x, y, z, Instantiate(cellPrefab, transform));
                get(x, y, z).transform.position = x * deltaX + y * deltaY + z * deltaZ;
                y--;
            }
        }
    }

    public GameObject get(int x, int y, int z = 0)
    {
        return cells[x + z + radius][y + z + radius];
    }

    public void set(int x, int y, GameObject cell)
    {
        cells[x + radius][y + radius] = cell;
    }

    public void set(int x, int y, int z, GameObject cell)
    {
        cells[x + z + radius][y + z + radius] = cell;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
