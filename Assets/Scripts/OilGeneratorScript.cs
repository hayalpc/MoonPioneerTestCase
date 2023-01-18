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

    //Summary: Oil makinesinin �retim yapmas�n� sa�layan methodtur
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

    //Summary: Oil makinesinin �retim yapmas� i�in deponun bo� olan nokta bilgisini veren methodtur.
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

    //Summary: �retilen malzemenin depoda gitmesi gereken noktay� belirleyen methodtur.
    Vector3 GetNewPosition(int index)
    {
        //TODO: array index ile y�r�mek laz�m. bo�alan yeri doldurmal�d�r!
        if (index < 1)
        {
            return new Vector3(0.4f, 0.16f, -0.3f);
        }

        //TODO: mod ile yan yana s�ralama yap!
        var newX = -(0.05f + lastObj.transform.localScale.x) * ((index) % 10);
        var newZ = 0.25f * Mathf.FloorToInt((index) / 10);
        return new Vector3(0.4f + newX, 0.16f, -0.3f + newZ);
    }

    //Summary: Worker eklendi�inde makine h�z�n� g�ncellemek i�in �a�r�lan methodtur.
    void UpdateMachineSpeed()
    {
        machineTime = Mathf.Clamp((100 - (Speed * 10)) * produceTime / 100, 1, produceTime);
    }

    //Summary: Gereksinimler tamamlan�nca worker eklenip makine h�z�n� g�ncelleyen methodtur.
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
