using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAreaExploreLookAt : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 positionOffset;

    private void LateUpdate()
    {
        transform.position = target.position + positionOffset;
    }
}
