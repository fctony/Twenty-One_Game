using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameController 
{
    public int basePointPerMatch;//底分
    //private int multiples;//全场倍数
    public List<int> playerlist = new List<int>();
    public List<int> scorelist = new List<int>();
    public List<int> beishulist = new List<int>();
    private static GameController instance;
    public int typeint = PlayerPrefs.GetInt("type");//模式

    public static GameController Instance
    {
        get
        {
            if (instance ==null)
            {
                instance = new GameController() ;
            }
            return instance;
        }
    }


    private GameController()
    {
       // multiples = 1;
        basePointPerMatch = 100;
        playerlist = PlayerController.Instance.Playerlist;
    }

    public HandCards GetHandCards(CharacterType type)
    {
        GameObject parentObj = GameObject.Find(type.ToString());
        HandCards cards = parentObj.GetComponent<HandCards>();
        return cards;
    }



    /// <summary>
    /// 发牌
    /// </summary>
    /// <param name="person"></param>
    public void DealTo(CharacterType person)
    {
     
            GameObject playerObj = GameObject.Find(person.ToString());
            HandCards cards = playerObj.GetComponent<HandCards>();

            Card movedCard = Deck.Instance.Deal();
            cards.AddCard(movedCard);
    }

    /// <summary>
    /// 获取单牌类型的权值
    /// </summary>
    /// <param name="cards"></param>
    /// <param name="rule"></param>
    /// <returns></returns>
    public int GetCardsWeight(Card[] cards)
    {
           int totalWeight = 0;
        
            for (int i = 0; i < cards.Length; i++)
            {
            int y = (int)cards[i].GetCardWeight;
            if (y>9)
            {
                y = 9;
            }
                totalWeight = totalWeight + y;
            }
        
        return totalWeight;
    }

    /// <summary>
    /// 更新庄闲
    /// </summary>
    /// <param name="type"></param>
    /// <param name="identity"></param>
    public void UpdateIndentity()
    {
        foreach (int i in playerlist)
        {
            CharacterType type = (CharacterType)i;
            HandCards handcards = GetHandCards(type);
            if (handcards.Integration >= 5000)
            {
                handcards.AccessIdentity = Identity.zhuang;
                Debug.Log("<color=red>更新庄闲</color>");
                break;
            }
            
        }
       
    }

    /// <summary>
    /// 判断得到牌型
    /// </summary>
    /// <param name="type"></param>
    public void GetCardsType(CharacterType type)
    {
        GameObject obj = GameObject.Find(type.ToString());
        HandCards play = obj.GetComponent<HandCards>();        
        List<Card> cardlist = play.Getlibrary;
        Card[] cards = cardlist.ToArray();
        CardsType cardsType;
        CardRules.IsCardtype(cardlist, out cardsType);
       
        if (typeint == 0)
        {
            play.HandCardsType = cardsType;
        }
        else
        {
            if (play.AllCardsWeight>21-play.CardsCount)
            {
                play.HandCardsType = CardsType.CardBoom;
            }

            if (cardsType == CardsType.Single || cardsType == CardsType.CardBoom)
            {
                play.HandCardsType = cardsType;
            }
            else
            {
                play.HandCardsType = CardsType.Single;
            }
           
        }
      
        
    }

    /// <summary>
    /// 初始化当前庄和下一个出牌人
    /// </summary>
    /// <param name="zhuangtype"></param>
    public void Play(CharacterType zhuangtype )
    {       
        HandCards play = GetHandCards(zhuangtype);
        if (play.AccessIdentity ==Identity.zhuang)
        {
            int i = GetTypeNextIndex(zhuangtype);
            CharacterType type = (CharacterType) PlayerController.Instance.Playerlist[i];
            //OrderController.Instance.Round = 0;
            OrderController.Instance.Init(zhuangtype,type);
        }

    }

    /// <summary>
    /// 得到当前type的下一个索引
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public int GetTypeNextIndex(CharacterType type)
    {
        int x = 0;
        int i = (int)type;
        foreach (int y in PlayerController.Instance.Playerlist)
        {
            if (i==y)
            {
               x = PlayerController.Instance.Playerlist.IndexOf(y);
               x = x + 1;
            }
        }

        return x;

    }

    /// <summary>
    /// 要牌
    /// </summary>
    /// <param name="type"></param>
    /// <param name="hand"></param>
    public void IsAddCard(CharacterType type)
    {
        DealTo(type);
        GameObject.Find("GameObject").GetComponent<PlaySceneController>().DealOneCards(type);        
    }

    /// <summary>
    /// 得到玩家每一张手牌
    /// </summary>
    public Card[] GetCards(CharacterType type)
    {
        GameObject player = GameObject.Find(type.ToString());
        HandCards hc = player.GetComponent<HandCards>();
        Card[] cards = hc.Getlibrary.ToArray();
        return cards;
    }

    /// <summary>
    /// 比牌，判断胜利
    /// </summary>
    public void ComparecardAllplayer()
    {
        GameObject zhuang = GameObject.Find(OrderController.Instance.Banker.ToString());
        HandCards zhuanghc = zhuang.GetComponent<HandCards>();
        CardsType zhuangcardtype = zhuanghc.HandCardsType;     
        int zhuangnum = (int)zhuangcardtype;
        int zhuangallnowintegration =0;
        //判断输赢
        foreach (int i in playerlist)
        {
            CharacterType type = (CharacterType)i;
            if (type != CharacterType.Player)
            {
                GameObject.Find("GameObject").GetComponent<PlaySceneController>().OutBack(type);
            }
            
            HandCards hc = GameController.instance.GetHandCards(type);
            CardsType cardtype = hc.HandCardsType;
            int farmercardtype = (int)cardtype;
            if (OrderController.Instance.Banker!=type)
            {
                if (zhuangnum != 1 && farmercardtype == 1)
                {
                    //zhuanghc.IsWin = true;
                    hc.IsWin = false;
                }
                else if (zhuangnum==1&&farmercardtype!=1)
                {
                    hc.IsWin = true;
                }
                else if (zhuangnum == 2 && farmercardtype == 2)//牌型为单排
                {
                    Debug.Log(zhuanghc.AllCardsWeight);
                    Debug.Log(hc.AllCardsWeight);
                    if (zhuanghc.AllCardsWeight >= hc.AllCardsWeight)
                    {
                        //zhuanghc.IsWin = true;
                        hc.IsWin = false;
                        Debug.Log("庄家赢");

                    }
                    else
                    {
                        hc.IsWin = true;
                        //zhuanghc.IsWin = false;
                        Debug.Log("闲家赢");
                    }
                }
                else
                {
                    if (zhuangnum >= farmercardtype)
                    {
                        //zhuanghc.IsWin = true;
                        hc.IsWin = false;
                        Debug.Log("庄家赢");
                    }
                    else
                    {
                        hc.IsWin = true;
                        //zhuanghc.IsWin = false;
                        Debug.Log("闲家赢");
                    }
                }
                if (hc.IsWin == false)
                {
                    zhuangallnowintegration = zhuangallnowintegration + hc.Nowintegration;
                    
                }
                else
                {
                    zhuangallnowintegration = zhuangallnowintegration - hc.Nowintegration;
                }
                if (zhuangallnowintegration >= 0)
                {
                    zhuanghc.IsWin = true;
                }
                else
                {
                    zhuanghc.IsWin = false;
                }
               
            }          
        }
        Debug.Log("庄家此局的全部分数：" + zhuangallnowintegration);
        zhuanghc.Nowintegration = Mathf.Abs(zhuangallnowintegration);        
    }

    /// <summary>
    /// 每一个玩家计算积分
    /// </summary>
    public void GetGameEndScore()
    {
        foreach (int i in playerlist)
        {
            CharacterType type = (CharacterType)i;
            HandCards handcards = GetHandCards(type);
            if (handcards.IsWin == true)
            {
                handcards.Integration = handcards.Integration + handcards.Nowintegration;
            }
            else
            {
                handcards.Integration = handcards.Integration - handcards.Nowintegration;
            }
            Debug.Log("玩家计算积分:" + handcards.Integration);
        }
    }

    /// <summary>
    /// 算出积分个位数的值
    /// </summary>
    /// <param name="hc"></param>
    public void ShowScore(HandCards hc)
    {
        
        int score = hc.Nowintegration;
        Debug.Log(score);        
        for (int i=0;i<5;i++)
        {
            int y = score / (int)(Mathf.Pow(10, 4 - i));
            
            if (y > 10)
            {
                y = y% (int)(Mathf.Pow(10, 4 - i));
            }
            if (y < 10&&y!=0)
            {
                scorelist.Clear();
                beishulist.Clear();
                scorelist.Add(y);
                Debug.Log("积分："+y);
                beishulist.Add(4-i);
                Debug.Log(4-i);
            }
            if (score==0)
            {
                scorelist.Clear();
                beishulist.Clear();
                beishulist.Add(0);

            }            
        }      
    }

    /// <summary>
    /// 结束当前牌局
    /// </summary>
    public void End()
    {
        foreach (int i in playerlist)
        {
            CharacterType type = (CharacterType)i;
            GameObject player = GameObject.Find(type.ToString());
            HandCards hc = GameController.Instance.GetHandCards(type);
            CardSprite[] cs = player.GetComponentsInChildren<CardSprite>();
            ScoreController[] sc = player.GetComponentsInChildren<ScoreController>();
            for (int y = 0; y < hc.CardsCount; y++)
            {
                cs[y].Destroy();

            }
            for (int y = 0; y < sc.Length; y++)
            {
                sc[y].Destroy();
            }
            hc.ClearList();
        }       
    }

}
