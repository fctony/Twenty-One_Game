using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    public GameObject btnsound;
    public AudioSource audio;
    //public bool isStop;


    private void Start()
    {
        if (PlayerPrefs.GetInt("sound") == 0)
        {
            audio.Play();
            audio.volume = 1;

        }
        if (audio.isPlaying == false)
        {
            btnsound.GetComponent<Image>().sprite= Resources.Load<Sprite>("Texture/dating/tanchuang/shezhi/shezhi_guanbi");
        }
        else
        {
            btnsound.GetComponent<Image>().sprite = Resources.Load<Sprite>("Texture/dating/tanchuang/shezhi/shezhi_dakai");
        }


        btnsound.GetComponent<Button>().onClick.AddListener(CloseBG);

    }    
    public void CloseBG()
    {
        Debug.Log(PlayerPrefs.GetInt("sound"));
        if (PlayerPrefs.GetInt("sound") == 0)
        {

            audio.Stop();
            btnsound.GetComponent<Image>().sprite = Resources.Load<Sprite>("Texture/dating/tanchuang/shezhi/shezhi_guanbi");
            PlayerPrefs.SetInt("sound", 1);
        }
        else if (PlayerPrefs.GetInt("sound") == 1)
        {
            audio.Play();
            btnsound.GetComponent<Image>().sprite = Resources.Load<Sprite>("Texture/dating/tanchuang/shezhi/shezhi_dakai");
            PlayerPrefs.SetInt("sound", 0);

        }

    }

}

