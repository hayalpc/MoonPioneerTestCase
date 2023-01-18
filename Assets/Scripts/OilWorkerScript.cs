using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class OilWorkerScript : MonoBehaviour
{
    [SerializeField]
    GameObject character;

    void Start()
    {
        //character.GetComponent<PlayerController>().enabled = false;
        //character.GetComponent<WorkerScript>().enabled = true;
        //character.GetComponent<WorkerScript>().TargetObject = transform;
        var worker = Instantiate(character, new Vector3(0f,1f,-10f),Quaternion.identity);
        var workerScript = worker.AddComponent<WorkerScript>();
        workerScript.enabled = true;
        workerScript.TargetObject = transform;
    }

}
