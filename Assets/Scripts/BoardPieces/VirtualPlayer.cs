using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TerrainUtils;

public class VirtualPlayer : BoardPiece
{
    public GameObject arrow;
    // Start is called before the first frame update

    public GameObject init(GameObject obj, HexField.Coord dir)
    {
        var ret = Instantiate(obj, this.cell.transform);
        var vec = dir.toCartesian();
        float arctanDir = Mathf.Atan2(vec.x, vec.z);
        ret.transform.rotation = Quaternion.Euler(0, arctanDir * 180 / Mathf.PI, 0);
        return ret;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
}
