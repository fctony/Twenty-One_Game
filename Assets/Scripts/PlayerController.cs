using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{

    private List<int> playerlist = new List<int>();
    private static PlayerController instance;
    private int w;
    private GameObject playerItem; 

    public static PlayerController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PlayerController();

            }
            return instance;
        }

    }

   

    private PlayerController()
    {
       
        playerItem = Resources.Load("prafab/playerItem") as GameObject;
        //GetComputerPlayer();
        //GetPlayerItem();
    }


    /// <summary>
    /// 随机生成电脑数目
    /// </summary>
    public void GetComputerPlayer()
    {
        playerlist.Add(1);
        List<int> play = new List<int>();
        play.Add(2);
        play.Add(3);
        play.Add(4);
        play.Add(5);
        play.Add(6);
        //int Computerplayercout = Random.Range(1,5);
        int Computerplayercout = 5;
        Debug.Log("add:"+Computerplayercout);
        for (int i = 0; i < Computerplayercout; i++)
        {
           
            int Computerplayer = Random.Range(0, play.Count);           
            int x = play[Computerplayer];
            play.Remove(x);          
            playerlist.Add(x);

        }
        playerlist.Sort((x, y) => x.CompareTo(y));
      
    }

    public List<int> Playerlist
    {
        //set { playerlist = value; }
        get {  return playerlist; }
    }
    public void PlayerlistClear()
    {
        playerlist.Clear();
    }

        /// <summary>
        /// 显示玩家头像
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="type"></param>
    public void ShowplayerHead(GameObject obj,CharacterType type)
    {
        if (type == CharacterType.Player)
        {
            int i = (int)CharacterType.Player;
            string pase = "Texture/head/" + i.ToString();
            obj.GetComponent<PlayerItem>().image.sprite = Resources.Load<Sprite>(pase);
        }
        else
        {           
            int y = Random.Range(2,7);
            string pase = "Texture/head/" + y.ToString();
            obj.GetComponent<PlayerItem>().image.sprite = Resources.Load<Sprite>(pase);
        }
    }


    /// <summary>
    /// 显示玩家的积分
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="type"></param>
    public void ShowPlayerScore(GameObject obj, CharacterType type)
    {
        if (type == CharacterType.Player)
        {                       
            HandCards cards = GameController.Instance.GetHandCards(type);           
            //GameController.Instance.UpdateIndentity(type);
            obj.GetComponent<PlayerItem>().scoretext.text = cards.Integration.ToString();
        }
        else
        {           
            GameObject playerObj = GameObject.Find(type.ToString());
            HandCards cards = playerObj.GetComponent<HandCards>();            
            //GameController.Instance.UpdateIndentity(type);
            obj.GetComponent<PlayerItem>().scoretext.text = cards.Integration.ToString();
        }

    }

    /// <summary>
    /// 更新全部玩家的积分
    /// </summary>
    public void UpdatePlayerScore()
    {
        GameController.Instance.GetGameEndScore();
        foreach (int i in playerlist)
        {
            HandCards cards = GameController.Instance.GetHandCards((CharacterType)i);
            GameObject obj = GameObject.Find(((CharacterType)i).ToString());
            obj.transform.GetChild(3).gameObject.GetComponent<PlayerItem>().scoretext.text = cards.Integration.ToString();
            if (i == 1)
            {
                PlayerPrefs.SetInt("score", cards.Integration);
            }
        }
    }

    /// <summary>
    /// 给电脑积分
    /// </summary>
    public void GiveComputerIntegration()
    {
        
        for (int i =1;i<playerlist.Count;i++)
        {
            int integration = Random.Range(1000, 6000);

            CharacterType type = (CharacterType)playerlist[i];
            CharacterType type2 = (CharacterType)playerlist[i-1];
            GameObject playerObj = GameObject.Find(type.ToString());
            GameObject playerObj2 = GameObject.Find(type2.ToString());
            HandCards cards = playerObj.GetComponent<HandCards>();
            cards.Integration = integration;
        }
    }

    /// <summary>
    /// 玩家积分
    /// </summary>
    public void PlayerIntegration()
    {
        int integration = PlayerPrefs.GetInt("score");
        GameObject playerObj = GameObject.Find(((CharacterType)playerlist[0]).ToString());
        HandCards cards = playerObj.GetComponent<HandCards>();
        cards.Integration = integration;
    }


    //public void MakeSureBanker()
    //{
    //    int i = 10000;


    //}
    /// <summary>
    /// 显示庄家
    /// </summary>
    public void ShowIndentity(GameObject obj, CharacterType type)
    {
       
        GameObject playerObj = GameObject.Find(type.ToString());
        HandCards playcards = playerObj.GetComponent<HandCards>();
        
        
        if (playcards.AccessIdentity == Identity.zhuang)
        {
            OrderController.Instance.Banker = type;
            obj.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 玩家要牌
    /// </summary>
    public void OnbtnAdd()
    {     
        GameController.Instance.GetCardsType(CharacterType.Player);                 
        HandCards hc = GameController.Instance.GetHandCards(CharacterType.Player);
        CardsType cardtype = hc.HandCardsType;
        PlayerAllWeight(CharacterType.Player);
        hc.Multiples = Mathf.Pow(2, (float)cardtype-2.0f);
        Debug.Log("<color=blue>牌型:</color>"+cardtype+" 权值" +hc.AllCardsWeight+"倍数"+hc.Multiples);              
        if (cardtype == CardsType.Single)
        {
            if (w <= 18)
            {
                GameController.Instance.DealTo(CharacterType.Player);
                GameObject.Find("GameObject").GetComponent<PlaySceneController>().DealOneCards(CharacterType.Player);
                w = GameController.Instance.GetCardsWeight(GameController.Instance.GetCards(CharacterType.Player));
            }
            
            Debug.Log(w);
            if (w > 18 && OrderController.Instance.Banker!=CharacterType.Player)
            {
                GameObject.Find("GameObject").GetComponent<PlaySceneController>().ShowBtnboom();
            }
        }
    }

    public void PlayerAllWeight(CharacterType type)
    {
        HandCards hc = GameController.Instance.GetHandCards(type);
        w = GameController.Instance.GetCardsWeight(GameController.Instance.GetCards(type));
        hc.AllCardsWeight = w;
    }

    /// <summary>
    /// 玩家不要拍
    /// </summary>
    public void OnbtnDontAddCard()
    {
        GameController.Instance.GetCardsType(CharacterType.Player);
        GameObject player = GameObject.Find(CharacterType.Player.ToString());
        HandCards hc = player.GetComponent<HandCards>();
        CardsType cardtype = hc.HandCardsType;
        if (cardtype == CardsType.Single)
        {
            int w = GameController.Instance.GetCardsWeight(GameController.Instance.GetCards(CharacterType.Player));
            if (w > 18)
            {
                GameObject.Find("GameObject").GetComponent<PlaySceneController>().ShowBtnboom();
            }
        }
    }

    public void OnbtnComparecard()
    {      
        HandCards hc = GameController.Instance.GetHandCards(CharacterType.Player);
        CardsType cardtype = hc.HandCardsType;
        Debug.Log("玩家输赢"+ hc.IsWin+ "玩家输赢"+GameController.Instance.GetHandCards((CharacterType)playerlist[1]).IsWin);
        hc.Nowintegration =  (int)hc.Multiples* GameController.Instance.basePointPerMatch; 
        GameController.Instance.ComparecardAllplayer();
        GameObject.Find("GameObject").GetComponent<WinFailSceneController>().ShowWinFail();
        UpdatePlayerScore();
        GameObject.Find("GameObject").GetComponent<PlaySceneController>().ShowBtnNext();
    }
}
