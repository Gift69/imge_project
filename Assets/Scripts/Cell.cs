using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using static HexField;

public class Cell : MonoBehaviour
{
    private HexField.Coord _coord;
    private List<BoardPiece> _boardPieces = new List<BoardPiece>();
    public GameObject highlightObj;

    public void setCoord(HexField.Coord coord)
    { 
        this._coord = coord;
    }

    public HexField.Coord getCoord()
    {
        return _coord;
    }

    public Cell[] getSequence(HexField.Coord delta, int maxCount = -1)
    {
        return transform.GetComponentInParent<HexField>().getSequence(_coord, delta, maxCount);
    }

    public Cell[] getArea(int radius)
    {
        return transform.GetComponentInParent<HexField>().getArea(_coord, radius);
    }

    public Cell getCellRelative(Coord relCoord)
    {
        var field = transform.GetComponentInParent<HexField>();
        if (field.isValidCoord(_coord + relCoord))
        {
            return field.cellAt(_coord + relCoord);
        }
        return null;
    }

    public void placeBoardPiece(BoardPiece piece)
    {
        if (piece.cell != null)
        {
            piece.cell._boardPieces.Remove(piece);
        }
        piece.cell = this;
        _boardPieces.Add(piece);
        piece.transform.SetParent(transform, false);
    }

    public void highlight()
    {
        highlightObj.gameObject.SetActive(true);
    }

    public void removeHighlight()
    {
        highlightObj.gameObject.SetActive(false);
    }

    public static void highlight(Cell[] cells)
    {
        foreach (Cell cell in cells)
        {
            cell.highlight();
        }
    }

    public static void removeHighlight(Cell[] cells)
    {
        foreach (Cell cell in cells)
        {
            cell.removeHighlight();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
