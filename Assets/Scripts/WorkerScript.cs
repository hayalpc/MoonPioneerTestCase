using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerScript : MonoBehaviour
{
    public Transform TargetObject;

    float startTime;
    void Start()
    {
        var animator = GetComponent<Animator>();
        animator.SetBool("IsRunning", true);
        OilGeneratorScript.Instance.AddWorker(TargetObject);
        startTime = Time.time;
    }

    //Summary: Worker gerektiren makinelerin rocketten player gelme iþlemini saðlayan methodtur.
    void Update()
    {
        var t = (Time.time - startTime) * Time.deltaTime;
        transform.localPosition = Vector3.Lerp(transform.position, TargetObject.position, t);
        if (Vector3.Distance(transform.position, TargetObject.position) < 0.25f)
        {
            enabled = false;
        }
    }
}
