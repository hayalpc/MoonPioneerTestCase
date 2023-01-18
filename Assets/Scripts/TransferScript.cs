using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferScript : MonoBehaviour
{
    public Vector3 newPosition = Vector3.zero;

    void Start()
    {
        transform.rotation = Quaternion.identity;
        GetComponent<Collider>().enabled = false;
    }

    //Summary: Player üzerindeki malzemeyi makineye doðru gitme iþlemini yapan methodtur.
    void LateUpdate()
    {
        if (newPosition != Vector3.zero)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, newPosition, 0.1f);
            if (Vector3.Distance(transform.localPosition, newPosition) < 0.05f)
            {
                enabled = false;
            }
        }
    }
}
