using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneAreaGeneratorScript : MonoBehaviour
{
    [SerializeField]
    GameObject stoneAreaObj;

    void Start()
    {
        Instantiate(stoneAreaObj, transform.position + new Vector3(6f, stoneAreaObj.transform.position.y, -1f), stoneAreaObj.transform.rotation);
        enabled = false;
    }

}
