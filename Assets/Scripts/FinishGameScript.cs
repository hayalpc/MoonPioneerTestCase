using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGameScript : MonoBehaviour
{
    [SerializeField]
    GameObject flame;
    [SerializeField]
    float speed = 2;

    bool start;

    // Start is called before the first frame update
    void Start()
    {
        flame.SetActive(true);
        StartCoroutine(StartFly());
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            transform.position += Vector3.up * Time.deltaTime * speed;
        }
    }

    //Summary: Script çalýþtýðýnda 3 saniye sonra rocketin uçmasýný tetikleyen methodtur.
    IEnumerator StartFly()
    {
        yield return new WaitForSeconds(3f);
        start = true;
        yield return new WaitForSeconds(3f);
        UIManagerScript.Instance.OpenPanel();
    }

}
