using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public GameObject btnclose;
    public GameObject parent;
    public ShopData[] shopDatas;

    private void Start()
    {
        Init();
    }
   
    public void Init()
    {
        btnclose.GetComponent<Button>().onClick.AddListener(OnBtnclose);
        for (int i = 0; i < shopDatas.Length; i++)
        {
            GameObject obj = Instantiate(Resources.Load("prafab/shopitem") as GameObject);
            obj.transform.SetParent(parent.transform);
            obj.transform.localScale = new Vector3(1, 1, 1);
            obj.GetComponent<ShopItem>().SetData(shopDatas[i]);
            obj.transform.localPosition = new Vector3(0, 0, 0);
           
        }
    }

    public void OnBtnclose()
    {
        gameObject.SetActive(false);
    }


}
