using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneController : MonoBehaviour
{
    public GameObject btnshop;
    public GameObject btntask;
    public GameObject btnaction;
    public GameObject btninstall;
    public GameObject btnclassic;
    public GameObject btnsuperlative;
    public GameObject btnhead;

    public GameObject action;
    public GameObject score;
    public GameObject task;
    public GameObject shop;
    public GameObject install;
    public GameObject personalnformation;
    public Text name;

    private void Start()
    {
        Init();
    }
   

    public void Init()
    {
        btnaction.GetComponent<Button>().onClick.AddListener(OnBtnaction);
        btnshop.GetComponent<Button>().onClick.AddListener(OnBtnshop);
        btntask.GetComponent<Button>().onClick.AddListener(OnBtntask);
        btninstall.GetComponent<Button>().onClick.AddListener(OnBtninstall);
        btnclassic.GetComponent<Button>().onClick.AddListener(OnBtnClassic);
        btnsuperlative.GetComponent<Button>().onClick.AddListener(OnBtnSuperlative);
        btnhead.GetComponent<Button>().onClick.AddListener(OnBtnhead);
    }

    private void Update()
    {
        UpdatePlayerScore();
        name.text = PlayerPrefs.GetString("name");
    }

    public void OnBtnaction()
    {
        action.SetActive(true);
    }

    public void OnBtnshop()
    {
        shop.SetActive(true);
    }

    public void OnBtntask()
    {
        task.SetActive(true);
    }

    public void OnBtninstall()
    {
        install.SetActive(true);
    }

    public void OnBtnClassic()
    {
        int i = 1;
        PlayerPrefs.SetInt("type",i);
        SceneManager.LoadScene(3);
    }
    public void OnBtnSuperlative()
    {
        int i = 0;
        PlayerPrefs.SetInt("type", i);
        SceneManager.LoadScene(3);
    }
    public void OnBtnhead()
    {
        personalnformation.SetActive(true);
    }

    public void UpdatePlayerScore()
    {
        int i = PlayerPrefs.GetInt("score");
        if (i == 0)
        {
            i = 5000;
            PlayerPrefs.SetInt("score",i);
        }
        score.GetComponent<Text>().text = i.ToString();
    }
}
