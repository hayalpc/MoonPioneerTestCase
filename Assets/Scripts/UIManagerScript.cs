using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagerScript : MonoBehaviour
{
    [SerializeField]
    TMP_Text OilCount;
    
    [SerializeField]
    TMP_Text StoneCount;

    [SerializeField]
    GameObject panel;

    public static UIManagerScript Instance;

    void Start()
    {
        Instance = this;
    }

    public void UpdateOil(int count)
    {
        OilCount.text = count.ToString();
    }

    public void UpdateStone(int count)
    {
        StoneCount.text = count.ToString();
    }

    public void OpenPanel()
    {
        panel.SetActive(true);
    }

    public void RePlay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
