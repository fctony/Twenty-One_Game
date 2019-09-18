using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMgr : MonoBehaviour
    {

        public GameObject btnStart;        
      
        void Start()
        {
           
         
            btnStart.transform.SetSiblingIndex(1000);
            btnStart.GetComponent<Button>().onClick.AddListener(Onclock);
        
        }
  
        public void Onclock()
        {
           
            SceneManager.LoadScene(1);

        }
    }
