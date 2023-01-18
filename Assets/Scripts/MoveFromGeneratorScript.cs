using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MoveFromGeneratorScript : MonoBehaviour
{
    public Vector3 newPosition = Vector3.zero;

    void Start()
    {

    }

    void LateUpdate()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, newPosition, 0.1f);
        if(Vector3.Distance(newPosition, transform.localPosition) < 0.1f)
        {
            enabled = false;
        }
    }
}
