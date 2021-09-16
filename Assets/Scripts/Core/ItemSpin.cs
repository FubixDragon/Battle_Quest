using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpin : MonoBehaviour
{
    public float rotationSpeed = 10.0f;

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(Vector3.forward * rotationSpeed);
        transform.Rotate(Vector3.up * rotationSpeed);
    }
}


