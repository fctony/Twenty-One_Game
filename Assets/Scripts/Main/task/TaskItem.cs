using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskItem : MonoBehaviour {

    public Text itemname;
    public Image[] images;
    public GameObject btnreceive;

    private void Start()
    {
        btnreceive.GetComponent<Button>().onClick.AddListener(OnBtnreceive);
    }

    private TaskData taskDatas;

    public void SetData(TaskData taskData)
    {
        taskDatas = taskData;
        itemname.text = taskDatas.name;
        for (int i = 0; i < images.Length; i++)
        {
            images[i].sprite = taskDatas.sprites[i];
        }
        btnreceive.GetComponent<Image>().sprite = taskDatas.receive;
    }

    public void OnBtnreceive()
    {
        btnreceive.GetComponent<Image>().sprite = Resources.Load<Sprite>("Texture/dating/tanchuang/renwu/renwu_an2");
        int score = PlayerPrefs.GetInt("score") + 100;
        PlayerPrefs.SetInt("score",score);
    }
}
