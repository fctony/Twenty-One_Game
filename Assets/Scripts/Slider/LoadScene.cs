using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    private Slider slider;
    private int currentProgress;//当前进度
    private int targetProgress;//目标进度

    //public static 

    private void Start()
    {
        currentProgress = 0;
        targetProgress = 0;
        slider = GameObject.Find("Slider").GetComponent<Slider>();
        StartCoroutine(LoadingScene());

    }


    private IEnumerator LoadingScene()
    {
        Debug.Log("qidong");
        AsyncOperation async = SceneManager.LoadSceneAsync(2);
        async.allowSceneActivation = false;
        while (async.progress < 0.9f)
        {           
            targetProgress = (int)(async.progress * 100);
            Debug.Log(targetProgress);
            yield return LoadProgress();
        }
        targetProgress = 100;       
        yield return LoadProgress();
        async.allowSceneActivation = true;      
    }

    private IEnumerator<WaitForEndOfFrame> LoadProgress()
    {
        while (currentProgress<targetProgress)
        {
            currentProgress+=2;
            slider.value = (float)currentProgress / 100;
            yield return new WaitForEndOfFrame();
        }
    }

}
