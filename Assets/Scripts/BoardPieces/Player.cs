using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class Player : BoardPiece
{
    public int score = 0;

    public const int ACTION_COUNT = 5;

    public Action[] selectableActions = new Action[ACTION_COUNT];
    public Action[] selectedAction = new Action[ACTION_COUNT];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            var newCell = cell.getCellRelative(new(0, 1, 0));
            if(newCell != null )
                newCell.placeBoardPiece(this);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            var newCell = cell.getCellRelative(new(0, -1, 0));
            if (newCell != null)
                newCell.placeBoardPiece(this);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            var newCell = cell.getCellRelative(new(-1, 0, 0));
            if (newCell != null)
                newCell.placeBoardPiece(this);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            var newCell = cell.getCellRelative(new(1, 0, 0));
            if (newCell != null)
                newCell.placeBoardPiece(this);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            var newCell = cell.getCellRelative(new(0, 0, 1));
            if (newCell != null)
                newCell.placeBoardPiece(this);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            var newCell = cell.getCellRelative(new(0, 0, -1));
            if (newCell != null)
                newCell.placeBoardPiece(this);
        }
    }
}
