using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Background : MonoBehaviour
{
    public GameObject tile;

    public int size;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 xOffset = new Vector3(Mathf.Cos(30 * Mathf.Deg2Rad), 0, 0);
        Vector3 zOffset = new Vector3(-Mathf.Cos(30 * Mathf.Deg2Rad) / 2, 0, 0.75f);
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                GameObject temp = Instantiate(tile, transform.position + i * xOffset + j * zOffset, quaternion.identity,
                    transform);
                temp.GetComponent<Cell>().setupPiece();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}