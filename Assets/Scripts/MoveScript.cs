using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    Vector3 newPosition = Vector3.zero;

    void Start()
    {
        var parent = PlayerController.Instance.Slot.transform;
        if (parent.childCount > 0)
        {
            var yy = 0.2f * (parent.childCount % 10);
            var zz = -0.2f * Mathf.FloorToInt((parent.childCount) / 10);
            newPosition = new Vector3(0, yy, zz);
        }
        transform.parent = parent;
        //transform.localPosition = Vector3.MoveTowards(transform.localPosition, newPosition, 1f);
        transform.localPosition = newPosition;
        transform.localRotation = Quaternion.Euler(0, 0, 90);
    }

}
