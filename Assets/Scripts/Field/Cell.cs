using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEditor.Build;
using UnityEngine;
using static HexField;
using Random = UnityEngine.Random;

public class Cell : MonoBehaviour
{
    private HexField.Coord _coord;
    private List<BoardPiece> _boardPieces = new List<BoardPiece>();

    public GameObject[] outerIndicators;
    public GameObject[] innerIndicators;
    public GameObject[] edgePieces;
    public GameObject[] innerPieces;
    public bool isEdgePiece;

    private GameObject piece, outerIndicator, innerIndicator;

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

    public Cell[] getCircle(int outerRadius, int innerRadius = 1)
    {
        return transform.GetComponentInParent<HexField>().getCircle(_coord, outerRadius, innerRadius);
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

    public void indicateOuter()
    {
        outerIndicator.SetActive(true);
    }

    public void removeOuterIndicator()
    {
        outerIndicator.SetActive(false);
    }

    public static void indicateOuter(Cell[] cells)
    {
        foreach (Cell cell in cells)
        {
            if (cell != null)
                cell.indicateOuter();
        }
    }

    public static void removeOuterIndicators(Cell[] cells)
    {
        foreach (Cell cell in cells)
        {
            if (cell != null)
                cell.removeOuterIndicator();
        }
    }

    public void indicateInner()
    {
        if (innerIndicator.activeInHierarchy == false)
        {
            innerIndicator.transform.localScale = new Vector3(0.97f,0.97f,0.97f);
            innerIndicator.SetActive(true);
            PlayAnimationOnce(innerIndicator.GetComponent<Animator>());
        }
    }

    public void removeInnerIndicator()
    {
        innerIndicator.SetActive(false);
    }

    public static void indicateInner(Cell[] cells)
    {
        foreach (Cell cell in cells)
        {
            if (cell != null)
                cell.indicateInner();
        }
    }

    public static void removeInnerIndicators(Cell[] cells)
    {
        foreach (Cell cell in cells)
        {
            if (cell != null)
                cell.removeInnerIndicator();
        }
    }

    public void setupPiece()
    {
        if (piece != null)
        {
            return;
        }
        if (isEdgePiece)
        {
            piece = Instantiate(edgePieces[Random.Range(0, edgePieces.Length)], this.transform.position,
                Quaternion.identity, this.transform);
        }
        else
        {
            piece = Instantiate(innerPieces[Random.Range(0, innerPieces.Length)], this.transform.position,
                quaternion.identity, this.transform);
        }
        innerIndicator = Instantiate(innerIndicators[Random.Range(0, innerIndicators.Length)], this.transform.position,
            Quaternion.identity, this.transform);
        innerIndicator.SetActive(false);

        outerIndicator = Instantiate(outerIndicators[Random.Range(0, outerIndicators.Length)], this.transform.position,
            Quaternion.identity, this.transform);
        outerIndicator.SetActive(false);
    }


    void PlayAnimationOnce(Animator animator)
    {
        // Play the PlayOnce state
        animator.Play("Scale_Highlight");
    }
}