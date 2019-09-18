using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InstallController : MonoBehaviour
{
    public GameObject btnclose;
    public GameObject btnquit;
    public GameObject scend;
    public GameObject btnsure;
    public GameObject btnreturn;
    public GameObject btnreturnmain;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        btnclose.GetComponent<Button>().onClick.AddListener(OnBtnclose);
        btnquit.GetComponent<Button>().onClick.AddListener(OnBtnquit);
        btnsure.GetComponent<Button>().onClick.AddListener(OnBtnsure);
        btnreturn.GetComponent<Button>().onClick.AddListener(OnBtnreturnt);
        btnreturnmain.GetComponent<Button>().onClick.AddListener(OnBtnreturntmain);
    }

    public void OnBtnclose()
    {
        gameObject.SetActive(false);
    }

    public void OnBtnquit()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            scend.SetActive(true);
        }
    }
    public void OnBtnsure()
    {
        SceneManager.LoadScene(2);
    }
    public void OnBtnreturnt()
    {
        scend.SetActive(false);
    }
    public void OnBtnreturntmain()
    {
        if ( SceneManager.GetActiveScene().buildIndex==1)
        {
            GameController.Instance.End();
            PlayerController.Instance.PlayerlistClear();
            SceneManager.LoadScene(0);
            
        }
        
    }
}
