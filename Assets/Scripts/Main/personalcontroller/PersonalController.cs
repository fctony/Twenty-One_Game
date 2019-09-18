using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonalController : MonoBehaviour
{
    public GameObject btnclose;
    public GameObject btnsure;
    public InputField playername;

    private void Start()
    {
        btnclose.GetComponent<Button>().onClick.AddListener(OnBtnclose);
        btnsure.GetComponent<Button>().onClick.AddListener(OnBtnsure);
     
    }
    
    public void OnBtnclose()
    {
        gameObject.SetActive(false);
    }


    public void OnBtnsure()
    {
        PlayerPrefs.SetString("name",playername.text); 
    }
}
