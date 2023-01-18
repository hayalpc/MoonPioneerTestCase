using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OilGeneratorScript : MonoBehaviour
{
    [SerializeField]
    float produceTime = 2f;
    [SerializeField]
    int capacity = 20;
    [SerializeField]
    int maxWorker = 3;

    [SerializeField]
    GameObject obj;
    [SerializeField]
    GameObject oilArea;
    [SerializeField]
    GameObject oilWorkerArea;

    public float Speed = 0;
    public GameObject[] Oils;
    public static OilGeneratorScript Instance;

    public float elapsedTime = 0f;
    public float machineTime = 0f;

    GameObject lastObj;
    void Start()
    {
        Instance = this;
        Oils = new GameObject[capacity];
        UpdateMachineSpeed();
    }

    void Update()
    {
        if (elapsedTime >= machineTime)
        {
            StartCoroutine(ProduceOil());
            elapsedTime = 0;
        }
        elapsedTime += Time.deltaTime;
    }

    //Summary: Oil makinesinin üretim yapmasýný saðlayan methodtur
    IEnumerator ProduceOil()
    {
        var index = GetEmptyOilIndex();
        if (index >= 0 && index < capacity)
        {
            lastObj = Instantiate(obj, Vector3.zero, obj.transform.localRotation, oilArea.transform);
            lastObj.transform.localPosition = new Vector3(0.34f, 0.16f, -0.9f);
            Oils[index] = lastObj;

            var moveScript = lastObj.GetComponent<MoveFromGeneratorScript>();
            moveScript.newPosition = GetNewPosition(index);
            moveScript.enabled = true;
            var takebleScript = lastObj.GetComponent<TakebleObjectScript>();

            takebleScript.removeFromList += delegate ()
            {
                Oils[index] = null;
            };
        }
        yield return null;
    }

    //Summary: Oil makinesinin üretim yapmasý için deponun boþ olan nokta bilgisini veren methodtur.
    int GetEmptyOilIndex()
    {
        for (int i = 0; i < capacity; i++)
        {
            var objx = Oils[i];
            if (objx == null)
                return i;
        }
        return -1;
    }

    //Summary: Üretilen malzemenin depoda gitmesi gereken noktayý belirleyen methodtur.
    Vector3 GetNewPosition(int index)
    {
        //TODO: array index ile yürümek lazým. boþalan yeri doldurmalýdýr!
        if (index < 1)
        {
            return new Vector3(0.4f, 0.16f, -0.3f);
        }

        //TODO: mod ile yan yana sýralama yap!
        var newX = -(0.05f + lastObj.transform.localScale.x) * ((index) % 10);
        var newZ = 0.25f * Mathf.FloorToInt((index) / 10);
        return new Vector3(0.4f + newX, 0.16f, -0.3f + newZ);
    }

    //Summary: Worker eklendiðinde makine hýzýný güncellemek için çaðrýlan methodtur.
    void UpdateMachineSpeed()
    {
        machineTime = Mathf.Clamp((100 - (Speed * 10)) * produceTime / 100, 1, produceTime);
    }

    //Summary: Gereksinimler tamamlanýnca worker eklenip makine hýzýný güncelleyen methodtur.
    public void AddWorker(Transform workerPosition)
    {
        Speed++;
        UpdateMachineSpeed();
        if (Speed < maxWorker)
        {
            Instantiate(oilWorkerArea, workerPosition.position + new Vector3(1, 0, 0), workerPosition.rotation, transform.parent);
        }
    }

}
