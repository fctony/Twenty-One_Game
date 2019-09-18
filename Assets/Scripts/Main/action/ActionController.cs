using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    public GameObject btnclose;

    public ActionData[] actionDatas;
    public GameObject parent;
    public GameObject btnreceive;    
    private List<GameObject> obj=new List<GameObject>();

    private int signday;
    private int firsttime;
    private int endtime;
    private int onoff;

    private void Start()
    {
        GetTime();
        Init();
    }

    public void Init()
    {
        btnclose.GetComponent<Button>().onClick.AddListener(OnBtnclose);
        btnreceive.GetComponent<Button>().onClick.AddListener(OnBtnreceive);
        ShowActionItems();
        onoff = PlayerPrefs.GetInt("onoff");
        if (onoff ==0)
        {
        Debug.Log(signday);
            obj[signday].transform.GetChild(2).gameObject.SetActive(true);
        }
       
    }

    public void OnBtnclose()
    {
        gameObject.SetActive(false);
    }
    
    
    public void ShowActionItems()
    {
        for (int i = 0; i < actionDatas.Length; i++)
        {

            GameObject temp = Instantiate(Resources.Load("prafab/actionitem") as GameObject);
            obj.Add(temp);
            temp.transform.SetParent(parent.transform);
            temp.transform.localScale = new Vector3(1, 1, 1);
            temp.GetComponent<ActionItem>().SetData(actionDatas[i]);
            temp.transform.localPosition = new Vector3(0, 0, 0);
            temp.transform.GetChild(2).gameObject.SetActive(false);
           
        } 
    }

    public void OnBtnreceive()
    {
        signday = PlayerPrefs.GetInt("signday");
        Debug.Log("日期"+signday);
        int score;
        if (firsttime != endtime)
        {          
            if (signday >= 7)
            {
                Debug.Log("循环");
                signday = 0;
            }
            Debug.Log("领取");
            obj[signday-1].transform.GetChild(2).gameObject.SetActive(true);
            onoff = 0;
            score = PlayerPrefs.GetInt("score")+(int)float.Parse(obj[signday].GetComponent<ActionItem>().goldnum.text);
            PlayerPrefs.SetInt("score", score);
            PlayerPrefs.SetInt("onoff", onoff);
            firsttime = endtime;
            PlayerPrefs.SetInt("signtime", endtime);
        }
    }

    public void GetTime()
    {
        firsttime = PlayerPrefs.GetInt("signtime");
        endtime = System.DateTime.Now.Day;
        signday = PlayerPrefs.GetInt("signday");
        onoff = PlayerPrefs.GetInt("onoff");
        if (signday >= 7)
        {
            Debug.Log("循环");
            signday = 0;
        }
        if (firsttime != endtime)
        {
            onoff = 1;
           
            signday = signday + 1;
            PlayerPrefs.SetInt("signday", signday);
            PlayerPrefs.SetInt("onoff", onoff);
            Debug.Log(signday);
        }

    }
}
