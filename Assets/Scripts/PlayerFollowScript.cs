using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowScript : MonoBehaviour
{
    GameObject player;
    Vector3 distance;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        distance = transform.position - player.gameObject.transform.position;
    }

    //Summary: Kameran�n takibi i�indir.
    void Update()
    {
        if (player != null)
        {
            transform.position = player.gameObject.transform.position + distance;
        }
    }
}
