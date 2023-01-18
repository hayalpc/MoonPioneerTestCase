using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Summary: Player üzerine alýnabilecek malzemeleri iþaretleyen sýnýftýr.
public class TakebleObjectScript : MonoBehaviour
{
    public event Action removeFromList = null;

    bool used = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Summary: Malzeme player üzerine taþýyan methodtur.
    public void Take()
    {
        if(removeFromList != null)
        {
            removeFromList();
        }
        var msfg = GetComponent<MoveFromGeneratorScript>();
        if(msfg != null)
        {
            msfg.enabled = false;
        }
        GetComponent<MoveScript>().enabled = true;

        PlayerObjectManager.Instance.ReSortObjects();
        //transform.localPosition = Vector3.zero;

        // transform.Rotate(new Vector3(0, 0, 90));
        //transform.localPosition = Vector3.MoveTowards(transform.position, Vector3.zero, 10 * Time.deltaTime);
    }

    //Summary: Malzemeye player deðdiðinde alma iþlemini tetikleyen methodtur.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!used && PlayerObjectManager.Instance.AddGameObject(gameObject))
            {
                used = true;
                Take();
            }
        }
    }
}
