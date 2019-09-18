using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public Image Bg;
    public Text goldnum;
    public Text moneynum;
    public GameObject btnshopitem;

    private ShopData Data;

    private void Start()
    {
        btnshopitem.GetComponent<Button>().onClick.AddListener(OnBtnshopitem);
    }

    public void SetData(ShopData shopData)
    {
        Data = shopData;
        Bg.sprite = Data.bg;
        goldnum.text = Data.goldnum;
        moneynum.text = Data.moneynum;
    }

    public void OnBtnshopitem()
    {
        Debug.Log("点击");
        int score = PlayerPrefs.GetInt("score") + (int)float.Parse(goldnum.text);
        PlayerPrefs.SetInt("score",score);
    }


}
