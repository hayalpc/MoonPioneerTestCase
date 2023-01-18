using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PlatformGenerateScript : MonoBehaviour
{
    [SerializeField]
    GameObject Platform;
    [SerializeField]
    int NeededOil;
    [SerializeField]
    int NeededStone;

    [SerializeField]
    string ScriptName;
    [SerializeField]
    string ScriptObjTag;

    [SerializeField]
    GameObject OilInfo;

    [SerializeField]
    GameObject StoneInfo;

    void Start()
    {
        if (NeededOil > 0 && OilInfo != null)
        {
            OilInfo.SetActive(true);
            OilInfo.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = NeededOil.ToString();
        }
        if (NeededStone > 0 && StoneInfo != null)
        {
            StoneInfo.SetActive(true);
            StoneInfo.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = NeededStone.ToString();
        }
    }

    void Update()
    {

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

    //Summary: Oyuncu gerekli bölgeye girdiðinde üzerindeki malzemelerin ihtiyaç kadarýnýn kullanýldýðý methodtur
    IEnumerator UseElements(GameObject obj)
    {
        while (NeededOil > 0 || NeededStone > 0)
        {
            if (!coroutine)
            {
                break;
            }
            if (NeededOil > 0 && obj.GetComponent<PlayerObjectManager>().OutGameObject("OilBox"))
            {
                NeededOil--;
                UpdateOilInfo();
                PlayerObjectManager.Instance.ReSortObjects();

                StartCoroutine(InstantiatePlatform());
                yield return new WaitForSeconds(0.25f);
            }
            else if (NeededStone > 0 && obj.GetComponent<PlayerObjectManager>().OutGameObject("StoneBox"))
            {
                NeededStone--;
                UpdateStoneInfo();
                PlayerObjectManager.Instance.ReSortObjects();

                StartCoroutine(InstantiatePlatform());
                yield return new WaitForSeconds(0.25f);
            }
            yield return new WaitForSeconds(0.25f);
        }
        yield return null;
    }

    //Summary: Gereksinimler tamamlandýðýnda tanýmlanan platformun oluþturulmasýndan sorumlu method
    IEnumerator InstantiatePlatform()
    {
        if (NeededOil == 0 && NeededStone == 0)
        {
            if (Platform != null)
            {
                Instantiate(Platform, new Vector3(transform.position.x, Platform.transform.position.y, transform.position.z), Platform.transform.rotation);
            }
            else
            {
                var scriptObjs = GameObject.FindGameObjectsWithTag("Player").ToList();
                if (scriptObjs.Count > 0)
                {
                    foreach (var scriptObj in scriptObjs)
                    {
                        //TODO: Generic olmalý burasý
                        scriptObj.AddComponent<StopPlayerScript>();
                    }
                }
                var rocket = GameObject.FindWithTag("Rocket");
                ((MonoBehaviour)rocket.GetComponent(ScriptName)).enabled = true;
            }
            Destroy(gameObject);
        }
        yield return null;
    }

    //Summary: Platform gereksinimlerinde ihtiyaç olan oil bilgisinin gösterildiði method
    void UpdateOilInfo()
    {
        OilInfo.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = NeededOil.ToString();
    }

    //Summary: Platform gereksinimlerinde ihtiyaç olan stone bilgisinin gösterildiði method
    void UpdateStoneInfo()
    {
        StoneInfo.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = NeededStone.ToString();
    }
}
