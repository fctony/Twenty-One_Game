using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiController :MonoBehaviour
{

    /// <summary>
    /// 电脑判断
    /// </summary>
    /// <param name="type"></param>
    public static void IsAddHandcardsAi(object obj)
    {
        CharacterType type = (CharacterType)obj;
        GameObject player = GameObject.Find(type.ToString());
        HandCards hc = player.GetComponent<HandCards>();
        CardsType cardtype = hc.HandCardsType;
        switch (cardtype)
        {
            case CardsType.CardBoom:
                Debug.Log("爆牌");
                DontAddCard();
                GameObject.Find("GameObject").GetComponent<PlaySceneController>().OutBack(type);
                hc.Nowintegration = (int)hc.Multiples * GameController.Instance.basePointPerMatch;
                break;
            case CardsType.Single:
                hc.Multiples = Mathf.Pow(2f, 0f);
                Debug.Log("<color=yellow>单排</color> ");
                int weight = GameController.Instance.GetCardsWeight(GameController.Instance.GetCards(type));
                //Debug.Log("牌数"+ GameController.Instance.GetCards(type).Length+ "权值" + weight);
                hc.AllCardsWeight = weight;
                ///牌的权值都要减一
                if (weight >= 15-hc.CardsCount)
                {
                    hc.Nowintegration = (int)hc.Multiples * GameController.Instance.basePointPerMatch;
                    DontAddCard();
                }
                else
                {                   
                    GameController.Instance.IsAddCard(type);
                    Singleton<Timer>.GetSingleton().AddFunction(IsAddHandcardsAi, type, 2.0f);                  
                }

                break;
            case CardsType.BlackJoker:
                GameObject.Find("GameObject").GetComponent<PlaySceneController>().OutBack(type);
                Debug.Log( "<color=yellow>黑杰克</color> ");
                hc.Multiples = Mathf.Pow(2f, 1f);
                Debug.Log(hc.Multiples);
                DontAddCard();
                hc.Nowintegration =(int)hc.Multiples * GameController.Instance.basePointPerMatch;

                break;
            case CardsType.Boom:
                GameObject.Find("GameObject").GetComponent<PlaySceneController>().OutBack(type);
                hc.Multiples = Mathf.Pow(2f, 2f);
                Debug.Log(hc.Multiples);
                Debug.Log("<color=yellow>炸弹</color> ");
                DontAddCard();
                hc.Nowintegration = (int)hc.Multiples * GameController.Instance.basePointPerMatch;

                break;
            case CardsType.Straight:
                GameObject.Find("GameObject").GetComponent<PlaySceneController>().OutBack(type);
                hc.Multiples = Mathf.Pow(2f, 3f);
                Debug.Log(hc.Multiples);
                Debug.Log("<color=yellow>顺子</color> ");
                DontAddCard();
                hc.Nowintegration = (int)hc.Multiples * GameController.Instance.basePointPerMatch;

                break;
            case CardsType.FiveLoong:
                GameObject.Find("GameObject").GetComponent<PlaySceneController>().OutBack(type);
                hc.Multiples = Mathf.Pow(2f, 4f);
                Debug.Log(hc.Multiples);
                Debug.Log("<color=yellow>五小龙</color> ");
                DontAddCard();
                hc.Nowintegration = (int)hc.Multiples * GameController.Instance.basePointPerMatch;

                break;
            case CardsType.SixLoong:
                GameObject.Find("GameObject").GetComponent<PlaySceneController>().OutBack(type);
                hc.Multiples = Mathf.Pow(2f, 5f);
                Debug.Log(hc.Multiples);
                Debug.Log("<color=yellow>六小龙</color> ");
                DontAddCard();
                hc.Nowintegration = (int)hc.Multiples * GameController.Instance.basePointPerMatch;

                break;
        }
    }

    

    /// <summary>
    /// 不要牌
    /// </summary>
    public static void DontAddCard( )
    {
        OrderController.Instance.Turn();
        CharacterType zhuang = OrderController.Instance.Banker;
        CharacterType type = OrderController.Instance.Type;
        OrderController.Instance.Init(zhuang,type);
    }

      
  

}
