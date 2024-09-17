using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour
{

    public bool xrot, yrot, zrot;

    public float x, y, z;

    private Vector3 rotation = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        if (xrot) rotation.x = x * Time.deltaTime;
        if (yrot) rotation.y = y * Time.deltaTime;
        if (zrot) rotation.z = z * Time.deltaTime;

        transform.Rotate(rotation);
    }
}
