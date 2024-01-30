using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gentleman_shot_vfx : MonoBehaviour
{
    public GameObject cartridge;
    public GameObject shot;
    public GameObject bang;

    private float shotSpeed = 7.5f;
    private float cartridgePushStrength = 10f;
    private bool _shooting;
    private bool _falling;

    private float horizontalBound = 50;
    private float verticalBound = 5;

    private void Awake()
    {
        Vector3 globalUpRight = (gameObject.transform.forward + gameObject.transform.up).normalized;
        cartridge.GetComponent<Rigidbody>().AddForce(globalUpRight * cartridgePushStrength, ForceMode.Impulse);
        _falling = true;
        _shooting = true;
        Instantiate(bang, transform.position + Vector3.up * 1f, Quaternion.identity);
    }

    private void Update()
    {
        if (_shooting)
        {
            shot.transform.position += -Time.deltaTime * shotSpeed * gameObject.transform.right.normalized;

            if (Mathf.Abs(shot.transform.position.y) > verticalBound ||
                Mathf.Max(Mathf.Abs(shot.transform.position.x), Mathf.Abs(shot.transform.position.z)) > horizontalBound)
            {
                Destroy(shot.gameObject);
                _shooting = false;
            }
        }

        if (_falling)
        {
            if (Mathf.Abs(cartridge.transform.position.y) > verticalBound ||
                Mathf.Max(Mathf.Abs(cartridge.transform.position.x), Mathf.Abs(cartridge.transform.position.z)) >
                horizontalBound)
            {
                Destroy(cartridge.gameObject);
                _falling = false;
            }
        }

        if (!_falling && !_shooting)
        {
            Destroy(this.gameObject);
        }
    }
}