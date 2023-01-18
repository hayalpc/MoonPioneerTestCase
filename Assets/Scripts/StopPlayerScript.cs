using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Summary: Oyun bitince playerleri rockete koþturan sýnýftýr.
public class StopPlayerScript : MonoBehaviour
{
    GameObject Rocket;
    float startTime;

    void Start()
    {
        var pc = GetComponent<PlayerController>();
        if (pc != null)
            pc.enabled = false;
        var pom = GetComponent<PlayerObjectManager>();
        if (pom != null)
            pom.enabled = false;
        var w = GetComponent<WorkerScript>();
        if (w != null)
            w.enabled = false;
        var bc = GetComponent<BoxCollider>();
        if (bc != null)
            bc.enabled = false;

        var animator = GetComponent<Animator>();
        animator.SetBool("IsRunning", true);

        Rocket = GameObject.FindWithTag("Rocket");
        startTime = Time.time;
    }

    void Update()
    {
        var t = (Time.time - startTime) * Time.deltaTime;
        transform.localPosition = Vector3.Lerp(transform.position, Rocket.transform.position, t);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Rocket.transform.position, Vector3.up), 1);
        if (Vector3.Distance(transform.position, Rocket.transform.position) < 0.5f)
        {
            Destroy(gameObject);
        }
    }
}
