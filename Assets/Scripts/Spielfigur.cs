using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spielfigur : MonoBehaviour
{
    public enum CHAMPION
    {
        Gentleman,
        Mage,
        Miner,
        Knight
    }

    [Header("Gentleman")] 
    [SerializeField] private GameObject gentleman_base;
    [SerializeField] private GameObject gentleman_extras;
    [SerializeField] private GameObject gentleman_outline;

    [Header("Mage")] 
    [SerializeField] private GameObject mage_base;
    [SerializeField] private GameObject mage_extras;
    [SerializeField] private GameObject mage_outline;

    [Header("Miner")] 
    [SerializeField] private GameObject miner_base;
    [SerializeField] private GameObject miner_extras;
    [SerializeField] private GameObject miner_outline;

    [Header("Knight")] 
    [SerializeField] private GameObject knight_base;
    [SerializeField] private GameObject knight_extras;
    [SerializeField] private GameObject knight_outline;

    public enum COLOR
    {
        Pink,
        Yellow,
        Blue,
        Red
    }

    [Header("Colors")] 
    [SerializeField] private Material pink;
    [SerializeField] private Material yellow;
    [SerializeField] private Material blue;
    [SerializeField] private Material red;

    [Header("Other Materials")] 
    [SerializeField] private Material outline_material;
    [SerializeField] private Material extras_material;

    [Header("TEST")] public bool test = false;

    private GameObject _base;
    private GameObject _extras;
    private GameObject _outline;

    public void SetupFigure(CHAMPION champion, COLOR color)
    {
        Material material = color switch
        {
            COLOR.Pink => pink,
            COLOR.Yellow => yellow,
            COLOR.Blue => blue,
            COLOR.Red => red,
            _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
        };

        Transform t = transform;
        Quaternion r = Quaternion.Euler(0, 180, 0);
        switch (champion)
        {
            case CHAMPION.Gentleman:
                _base = Instantiate(gentleman_base, t.position, r, t);
                _extras = Instantiate(gentleman_extras, t.position, r, t);
                _outline = Instantiate(gentleman_outline, t.position, r, t);
                break;
            case CHAMPION.Mage:
                _base = Instantiate(mage_base, t.position, r, t);
                _extras = Instantiate(mage_extras, t.position, r, t);
                _outline = Instantiate(mage_outline, t.position, r, t);
                break;
            case CHAMPION.Miner:
                _base = Instantiate(miner_base, t.position, r, t);
                _extras = Instantiate(miner_extras, t.position, r, t);
                _outline = Instantiate(miner_outline, t.position, r, t);
                break;
            case CHAMPION.Knight:
                _base = Instantiate(knight_base, t.position, r, t);
                _extras = Instantiate(knight_extras, t.position, r, t);
                _outline = Instantiate(knight_outline, t.position, r, t);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(champion), champion, null);
        }
        
        _base.GetComponent<Renderer>().material = material;
        _extras.GetComponent<Renderer>().material = extras_material;
        _outline.GetComponent<Renderer>().material = outline_material;
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
                temp.GetComponent<Spielfigur>().SetupFigure((CHAMPION)i, (COLOR)j);
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

    // Update is called once per frame
    void Update()
    {
    }
}