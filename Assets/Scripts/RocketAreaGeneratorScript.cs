using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketAreaGeneratorScript : MonoBehaviour
{
    [SerializeField]
    GameObject rocketAreaObj;

    void Start()
    {
        var rocket = GameObject.FindWithTag("Rocket");
        Instantiate(rocketAreaObj, new Vector3(rocket.transform.position.x, rocketAreaObj.transform.position.y, rocket.transform.position.z + 2f), rocketAreaObj.transform.rotation);
        enabled = false;
    }
   
}
