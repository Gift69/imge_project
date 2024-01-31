using System;
using Mirror;
using UnityEngine;

public class Spielfigur : NetworkBehaviour  
{
    public enum COLOR
    {
        Pink,
        Yellow,
        Blue,
        Red
    }

    [SyncVar(hook = nameof(OnChangeMaterial))]
    public int materialIndex;

    [Header("Colors")][SerializeField] private Material pink;
    [SerializeField] private Material yellow;
    [SerializeField] private Material blue;
    [SerializeField] private Material red;


    [Header("Effects")][SerializeField] private GameObject stunned_effect;

    [Header("TEST")] public bool test = false;

    private GameObject _stunned;

    [SerializeField] private GameObject figure;


    /* public void SetupFigure(COLOR color)
    {
        Material material = color switch
        {
            COLOR.Pink => pink,
            COLOR.Yellow => yellow,
            COLOR.Blue => blue,
            COLOR.Red => red,
            _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
        };
        Debug.Log(material);
        figure.GetComponent<Renderer>().material = material;
        //  _stunned.SetActive(false);
    } */

    public void OnChangeMaterial(int oldIndex, int newIndex)
    {
        Material material = (COLOR)materialIndex switch
        {
            COLOR.Pink => pink,
            COLOR.Yellow => yellow,
            COLOR.Blue => blue,
            COLOR.Red => red,
            _ => throw new ArgumentOutOfRangeException(nameof(materialIndex), materialIndex, null)
        };
        Debug.Log(material);
        this.GetComponent<Renderer>().material = material;
        Debug.Log(this.GetComponent<Renderer>().material);
    }

    public void setStunned(bool stunned)
    {
        _stunned.SetActive(stunned);
    }

    static void testAllCombinations(GameObject instance)
    {
        instance.GetComponent<Spielfigur>().test = false;

        float distance = 1f;
        int amountFiguren = 4;
        int amountColors = 4;

        Vector3 startOffset = new Vector3(-amountFiguren / 2.0f * distance, 0, -amountColors / 2.0f * distance);
        startOffset += instance.transform.position;
        for (int i = 0; i < amountFiguren; i++)
        {
            for (int j = 0; j < amountColors; j++)
            {
                Vector3 pos = new Vector3(i * distance, 0, j * distance) + startOffset;
                GameObject temp = Instantiate(instance, pos, Quaternion.identity);
                //temp.GetComponent<Spielfigur>().SetupFigure((COLOR)j);
                temp.GetComponent<Spielfigur>().setStunned(true);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (test)
        {
            testAllCombinations(this.gameObject);
        }
    }
}