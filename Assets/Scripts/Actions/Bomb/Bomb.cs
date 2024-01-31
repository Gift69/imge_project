using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : BoardPiece
{
    private int time = 2;
    public GameObject Explosion;

    public void tick()
    {
        this.time -= 1;
        if (time <= 0)
        {
            Instantiate(Explosion, transform.position, Quaternion.identity);
            foreach (var cell1 in this.cell.getCircle(1))
            {
                foreach (var boardPiece in cell1.getBoardPieces())
                {
                    if (boardPiece is Player)
                    {
                        (boardPiece as Player).stunDuration = 2;
                    }
                }
            }
            Destroy(this.gameObject);
        }
    }
}
