using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoneGeneratorScript : MonoBehaviour
{
    [SerializeField]
    int maxOilCount;
    [SerializeField]
    Transform stockArea;
    [SerializeField]
    Transform stoneArea;
    [SerializeField]
    int produceTime;
    [SerializeField]
    GameObject produceObj;
    [SerializeField]
    int capacity;
    public GameObject[] Stones;
    public GameObject[] Oils;

    float machineTime = 0;

    void Start()
    {
        Stones = new GameObject[capacity];
        Oils = new GameObject[maxOilCount];
    }

    void Update()
    {
        if (machineTime >= produceTime)
        {
            StartCoroutine(ProduceStone());
            machineTime = 0;
            //obj sýralamasý yapýlmalý
        }
        machineTime += Time.deltaTime;
    }

    //Summary: Makinenin stone üretimi iþlemini yapan methodtur. Oil deposu ve stone depolarý kontrol edilerek üretim yapar.
    IEnumerator ProduceStone()
    {
        var usableOilIndex = GetUsableOilIndex();
        if (usableOilIndex >= 0)
        {
            var index = GetEmptyStoneIndex();
            if (index >= 0)
            {
                var stone = Instantiate(produceObj, transform.position, produceObj.transform.rotation, stoneArea);
                Stones[index] = stone;

                var moveScript = stone.GetComponent<MoveFromGeneratorScript>();
                moveScript.newPosition = GetNewPosition(index,true);
                moveScript.enabled = true;

                var takebleScript = stone.GetComponent<TakebleObjectScript>();
                takebleScript.removeFromList += delegate ()
                {
                    Stones[index] = null;
                };

                var obj = Oils[usableOilIndex];
                Oils[usableOilIndex] = null;
                Destroy(obj);
                yield return null;
            }
            yield return null;
        }
        yield return null;
    }

    //Summary: Stone deposunda boþluðu bulan methodtur.
    int GetEmptyStoneIndex()
    {
        for (int i = 0; i < capacity; i++)
        {
            var objx = Stones[i];
            if (objx == null)
                return i;
        }
        return -1;
    }

    //Summary: Player üzerindeki malzemeyi makinenin deposuna almak için boþluðu veren methodtur.
    int GetEmptyOilIndex()
    {
        for (int i = 0; i < maxOilCount; i++)
        {
            var objx = Oils[i];
            if (objx == null)
                return i;
        }
        return -1;
    }

    //Summary: Makine üretim için oil deposundaki malzemenin yerini getiren methodtur.
    int GetUsableOilIndex()
    {
        for (int i = maxOilCount - 1; i >= 0; i--)
        {
            var objx = Oils[i];
            if (objx != null)
                return i;
        }
        return -1;
    }

    bool coroutine;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            coroutine = true;
            StartCoroutine(UseElements(collision.gameObject));
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            coroutine = false;
        }
    }

    //Summary: Stone deposundaki boþluða göre konulmasý gereken noktayý belirleyen methodtur.
    Vector3 GetNewPosition(int index, bool rightDirection = false)
    {
        if (index < 1)
        {
            return rightDirection ? new Vector3(0.1f, 0f, 0f) : new Vector3(0f, 0f, 0.1f);
        }

        //TODO: mod ile yan yana sýralama yap!
        var newZ = (0.05f + produceObj.transform.localScale.z) * ((index) % 10);
        //var newZ = 0.25f * Mathf.FloorToInt((index) / 10);

        return rightDirection ? new Vector3(0.1f + ( - newZ), 0, 0) : new Vector3(0f, 0, 0.1f + newZ);

        //var lastObj = gameObjects.LastOrDefault();
        //if (lastObj != null)
        //{
        //    return lastObj.transform.localPosition + new Vector3(0, 0, 0.25f);
        //}
        //return stockArea.localPosition;
    }

    //Summary: Player üzerindeki malzemeyi makinenin deposuna alan methodtur.
    IEnumerator UseElements(GameObject obj)
    {
        while (true)
        {
            if(!coroutine)
            {
                break;
            }
            var index = GetEmptyOilIndex();
            if (index >= 0)
            {
                var oilObj = obj.GetComponent<PlayerObjectManager>().TransferGameObject("OilBox", GetNewPosition(index), stockArea);
                if (oilObj != null)
                {
                    Oils[index] = oilObj;
                    //StartCoroutine(InstantiatePlatform());
                    yield return new WaitForSeconds(0.5f);
                }
                else
                {
                    yield return new WaitForSeconds(1f);
                }
            }
            else
            {
                yield return new WaitForSeconds(1f);
            }
        }

    }
}
