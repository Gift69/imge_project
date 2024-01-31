using System;
using UnityEngine;

public class Spielfigur : MonoBehaviour
{
    public enum CHAMPION
    {
        Mage,
        Gentleman,
        Knight,
        Miner
    }

    public GameObject offset;

    [Header("Gentleman")][SerializeField] private GameObject gentleman_base;
    [SerializeField] private GameObject gentleman_extras;
    [SerializeField] private GameObject gentleman_outline;

    [Header("Mage")][SerializeField] private GameObject mage_base;
    [SerializeField] private GameObject mage_extras;
    [SerializeField] private GameObject mage_outline;

    [Header("Miner")][SerializeField] private GameObject miner_base;
    [SerializeField] private GameObject miner_extras;
    [SerializeField] private GameObject miner_outline;

    [Header("Knight")][SerializeField] private GameObject knight_base;
    [SerializeField] private GameObject knight_extras;
    [SerializeField] private GameObject knight_outline;

    public enum COLOR
    {
        Pink,
        Yellow,
        Blue,
        Red
    }

    [Header("Colors")][SerializeField] private Material pink;
    [SerializeField] private Material yellow;
    [SerializeField] private Material blue;
    [SerializeField] private Material red;

    [Header("Other Materials")]
    [SerializeField]
    private Material outline_material;

    [SerializeField] private Material extras_material;

    [Header("Effects")][SerializeField] private GameObject stunned_effect;

    [Header("TEST")] public bool test = false;

    private GameObject _base;
    private GameObject _extras;
    private GameObject _outline;
    private GameObject _stunned;

    [SerializeField] private GameObject gentleman;
    [SerializeField] private GameObject mage;
    [SerializeField] private GameObject miner;
    [SerializeField] private GameObject knight;


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

        switch (champion)
        {
            case CHAMPION.Gentleman:
                gentleman.SetActive(true);
                gentleman_base.GetComponent<Renderer>().material = material;
                gentleman_extras.GetComponent<Renderer>().material = extras_material;
                gentleman_outline.GetComponent<Renderer>().material = outline_material;
                break;
            case CHAMPION.Mage:
                mage.SetActive(true);
                mage_base.GetComponent<Renderer>().material = material;
                mage_extras.GetComponent<Renderer>().material = extras_material;
                mage_outline.GetComponent<Renderer>().material = outline_material;
                break;
            case CHAMPION.Miner:
                miner.SetActive(true);
                miner_base.GetComponent<Renderer>().material = material;
                miner_extras.GetComponent<Renderer>().material = extras_material;
                miner_outline.GetComponent<Renderer>().material = outline_material;
                break;
            case CHAMPION.Knight:
                knight.SetActive(true);
                knight_base.GetComponent<Renderer>().material = material;
                knight_extras.GetComponent<Renderer>().material = extras_material;
                knight_outline.GetComponent<Renderer>().material = outline_material;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(champion), champion, null);
        }


        //  _stunned.SetActive(false);
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
                temp.GetComponent<Spielfigur>().SetupFigure((CHAMPION)i, (COLOR)j);
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