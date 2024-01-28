using UnityEngine;

public class VirtualPlayer : BoardPiece
{
    public GameObject arrow;
    // Start is called before the first frame update
    public GameObject bomb;
    public GameObject shoot;
    public GameObject stunfield;
    public GameObject mining;

    public GameObject init(GameObject obj, HexField.Coord dir)
    {
        var ret = Instantiate(obj, this.cell.transform);
        var vec = dir.toCartesian();
        float arctanDir = Mathf.Atan2(vec.z, vec.x);
        ret.transform.rotation = Quaternion.Euler(0, -arctanDir * 180 / Mathf.PI, 0);
        return ret;
    }
}
