using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 结算界面
/// </summary>
public class WinFailSceneController : MonoBehaviour
{
    public GameObject mark; 

    private void Start()
    {
        //ShowWinFail();       
    }

    
    public void ShowWinFail()
    {
        mark.SetActive(true);
        List<int> playerslist = new List<int>();
        playerslist = PlayerController.Instance.Playerlist;
        foreach (int i in playerslist)
        {
            CharacterType type = (CharacterType)i;
            GameObject player = GameObject.Find(type.ToString());
            HandCards hc = player.GetComponent<HandCards>();
            Image iswin = Instantiate(Resources.Load<Image>("prafab/Iswin"));
            iswin.transform.SetParent(player.transform.GetChild(1).transform);
            iswin.transform.localScale = new Vector3(1,1,1);
            iswin.transform.localPosition = new Vector3(0, 0, 0);
            ShowWinFailScore(hc);
            if (hc.IsWin == true)
            {
                iswin.sprite = Resources.Load<Sprite>("Texture/youxi/ziti/yingle");
               
            }
            else
            {
                iswin.sprite = Resources.Load<Sprite>("Texture/youxi/ziti/shule");
            }
            //iswin.transform.localPosition = player.transform.GetChild(1).transform.position;
        }
    }

    public void ShowWinFailScore(HandCards hc)
    {
        GameController.Instance.ShowScore(hc);
        string icon;
        string pass = "prafab/score";
        if (hc.IsWin == true)
        {
            icon = "Texture/youxi/ziti/+";           
        }
        else
        {
            icon = "Texture/youxi/ziti/-";       
        }
        Image iconimage = Instantiate(Resources.Load<Image>(pass));
        iconimage.transform.SetParent(hc.transform.GetChild(2).transform);
        iconimage.transform.localScale = new Vector3(1, 1, 1);
        iconimage.transform.SetParent(hc.transform.GetChild(2));
        iconimage.transform.localPosition = new Vector3(0, 0, 0);
        iconimage.sprite = Resources.Load<Sprite>(icon);
        iconimage.SetNativeSize();      
        string icon1 = "Texture/youxi/ziti/";
        if (hc.Nowintegration != 0)
        {
            List<int> scorelist = GameController.Instance.scorelist;
            for (int i = 0; i < scorelist.Count; i++)
            {
                Debug.Log("玩家积分：" + scorelist[i]);
                string icon2;
                if (hc.IsWin == true)
                {
                    icon2 = icon1 + scorelist[i];
                }
                else
                {
                    icon2 = icon1 + scorelist[i] + "_2";
                }
                iconimage = Instantiate(Resources.Load<Image>(pass));
                iconimage.transform.SetParent(hc.transform.GetChild(2).transform);
                iconimage.transform.localScale = new Vector3(1, 1, 1);
                iconimage.transform.SetParent(hc.transform.GetChild(2));
                iconimage.transform.localPosition = new Vector3((i + 1) * 40, 0, 0);
                iconimage.sprite = Resources.Load<Sprite>(icon2);
                iconimage.SetNativeSize();
            }
            List<int> beishulist = GameController.Instance.beishulist;
            //Debug.Log(beishulist[scorelist.Count - 1]);
            for (int i = 1; i < beishulist[scorelist.Count - 1] + 1; i++)
            {
                string icon3;
                if (hc.IsWin == true)
                {
                    icon3 = "Texture/youxi/ziti/0";
                }
                else
                {
                    icon3 = "Texture/youxi/ziti/0_2";
                }
                iconimage = Instantiate(Resources.Load<Image>(pass));
                iconimage.transform.SetParent(hc.transform.GetChild(2).transform);
                iconimage.transform.localScale = new Vector3(1, 1, 1);
                iconimage.transform.SetParent(hc.transform.GetChild(2));
                iconimage.transform.localPosition = new Vector3((scorelist.Count + i) * 40, 0, 0);
                iconimage.sprite = Resources.Load<Sprite>(icon3);
                iconimage.SetNativeSize();
            }
        }
        else
        {
            string icon2;
            if (hc.IsWin == true)
            {
                icon2 = icon1 +"0";
            }
            else
            {
                icon2 = icon1 + "0" + "_2";
            }
            iconimage = Instantiate(Resources.Load<Image>(pass));
            iconimage.transform.SetParent(hc.transform.GetChild(2).transform);
            iconimage.transform.localScale = new Vector3(1, 1, 1);
            iconimage.transform.SetParent(hc.transform.GetChild(2));
            iconimage.transform.localPosition = new Vector3((0 + 1) * 40, 0, 0);
            iconimage.sprite = Resources.Load<Sprite>(icon2);
            iconimage.SetNativeSize();
        }
       
        


        //for ()
        //{

        //}
    }
}
