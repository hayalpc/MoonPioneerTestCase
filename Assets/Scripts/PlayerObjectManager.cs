using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerObjectManager : MonoBehaviour
{
    List<GameObject> gameObjects = new List<GameObject>();
    int maxObjectCount = 20;

    public static PlayerObjectManager Instance;

    private void Start()
    {
        Instance = this;
    }

    //Summary: �retilen bir malzemenin player �zerine al�nmas�n� sa�layan methodtur.
    public bool AddGameObject(GameObject obj)
    {
        if (gameObjects.Count >= maxObjectCount)
        {
            //GetComponent<Collider>().isTrigger = true;
            return false;
        }
        else//TODO: �ok sa�ma bunu d�zeltmelisin!
        {
            //GetComponent<Collider>().isTrigger = false;
        }
        gameObjects.Add(obj);

        if (obj.CompareTag("OilBox"))
        {
            UIManagerScript.Instance.UpdateOil(gameObjects.Where(x => x.gameObject.CompareTag("OilBox")).Count());
        }
        else if (obj.CompareTag("StoneBox"))
        {
            UIManagerScript.Instance.UpdateStone(gameObjects.Where(x => x.gameObject.CompareTag("StoneBox")).Count());
        }

        return true;
    }

    //Summary: Player �zerinde malzemenin �retim i�in kullan�lmas�nda ��kartma i�lemini sa�layan methodtur.
    public bool OutGameObject(string tagName)
    {
        var objsCount = gameObjects.Where(x => x.gameObject.CompareTag(tagName)).Count();
        if (objsCount > 0)
        {
            //objs.RemoveAt(objs.Count - 1);
            //Debug.Log("RemoveAt " + objs.Count);
            //gameObjects.RemoveLast(obj);
            for (int i = gameObjects.Count - 1; i >= 0; i--)
            {
                var objx = gameObjects[i];
                if (objx.CompareTag(tagName))
                {
                    gameObjects.RemoveAt(i);

                    Destroy(PlayerController.Instance.Slot.transform.GetChild(i).gameObject);

                    objsCount--;
                    if (tagName.Equals("OilBox"))
                    {
                        UIManagerScript.Instance.UpdateOil(objsCount);
                    }
                    else
                    {
                        UIManagerScript.Instance.UpdateStone(objsCount);
                    }
                    break;
                }
            }

            //Stone'dan sonra olanlar� i�in ReSortObjects gerekir. Ba�tan yapmaya gerek yok.
            //if (gameObjects.Count != objs.Count)
            ReSortObjects();
            return true;
        }
        return false;
    }

    //Summary: Bir makinenin player �zerindeki malzemeyi kendi deposuna kullanmak i�in ald���nda kullan�lan methodtur.
    public GameObject TransferGameObject(string tagName, Vector3 target, Transform parent)
    {
        var objsCount = gameObjects.Where(x => x.gameObject.CompareTag(tagName)).Count();
        if (objsCount > 0)
        {
            for (int i = gameObjects.Count - 1; i >= 0; i--)
            {
                var objx = gameObjects[i];
                if (objx.CompareTag(tagName))
                {
                    gameObjects.RemoveAt(i);

                    objx.transform.parent = parent;
                    var transferScript = objx.AddComponent<TransferScript>();
                    transferScript.newPosition = target;

                    objsCount--;
                    if (tagName.Equals("OilBox"))
                    {
                        UIManagerScript.Instance.UpdateOil(objsCount);
                    }
                    else
                    {
                        UIManagerScript.Instance.UpdateStone(objsCount);
                    }
                    return objx;
                }
            }
        }
        return null;
    }


    //Summary: Player �zerindeki malzemeleri yeniden s�ralama i�lemi i�in kullan�l�yor. Buras� ilkel oldu!
    public void ReSortObjects()
    {
        var parent = PlayerController.Instance.Slot.transform;
        var childCount = parent.childCount;

        if (childCount > 0)
        {
            for (int i = 0; i < childCount; i++)
            {
                var child = parent.GetChild(i);
                var yy = 0.2f * (i % 10);
                var zz = -0.2f * Mathf.FloorToInt(i / 10);

                var newPosition = new Vector3(0, yy, zz);
                child.localPosition = newPosition;
            }
        }
    }


}
