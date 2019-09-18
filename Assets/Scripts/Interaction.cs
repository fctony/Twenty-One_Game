using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    public GameObject btnfapai;
    
    private void Start()
    {
        GameController instance = GameController.Instance;
        btnfapai.GetComponent<Button>().onClick.AddListener(DealCallBack);
    }

    /// <summary>
    /// 洗牌回调
    /// </summary>
    public void DealCallBack()
    {
        Debug.Log("洗牌回调");
        //GameController.Instance.DealCards();

    }

    
}
