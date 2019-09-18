using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskController : MonoBehaviour
{
    public GameObject btnclose;

    public GameObject parent;

    public TaskData[] taskDatas;

    private void Start()
    {
        Init();
    }
    
    public void Init()
    {
        btnclose.GetComponent<Button>().onClick.AddListener(OnBtnclose);
        for (int i=0;i<taskDatas.Length;i++)
        {
            GameObject obj = Instantiate(Resources.Load("prafab/taskitem")as GameObject);
            obj.transform.SetParent(parent.transform);
            obj.transform.localScale = new Vector3(1,1,1);
            obj.transform.localPosition = new Vector3(0,0,0);
            obj.GetComponent<TaskItem>().SetData(taskDatas[i]);
        }

    }

    public void OnBtnclose()
    {
        gameObject.SetActive(false);
    }

}
