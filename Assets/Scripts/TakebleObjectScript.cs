using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Summary: Player �zerine al�nabilecek malzemeleri i�aretleyen s�n�ft�r.
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

    //Summary: Malzeme player �zerine ta��yan methodtur.
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

    //Summary: Malzemeye player de�di�inde alma i�lemini tetikleyen methodtur.
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
