using Mirror;
using UnityEngine;

public class BoardPiece : NetworkBehaviour
{
    [SyncVar]
    public HexField.Coord coord;
    [SyncVar]
    public bool isPlaced = false;

    private HexField hexfield;


    public Cell cell
    {
        get { return isPlaced ? hexfield.cellAt(coord) : null; }
        set
        {
            if (value == null)
            {
                isPlaced = false;
                transform.position = new Vector3(0, 100, 0);
            }
            else
            {
                isPlaced = true;
                coord = value.getCoord();

                transform.position = value.transform.position;
            }
        }
    }

    // Start is called before the first frame update
    public void Awake()
    {
        hexfield = GameObject.FindGameObjectWithTag("HexField").GetComponent<HexField>();

        this.cell = hexfield.cellAt(new(0, 0, 0));
    }
}
