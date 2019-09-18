using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 出牌规则
/// </summary>
public class CardRules
{

    /// <summary>
    /// 卡牌数组排序
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static void SortCards(List<Card> cards, bool ascending)
    {
        
        cards.Sort(
            (Card a, Card b) =>
            {
                if (!ascending)
                {
                   
                    //先按照权重降序，再按花色升序
                    return -a.GetCardWeight.CompareTo(b.GetCardWeight) * 2 +
                        a.GetCardSuit.CompareTo(b.GetCardSuit);
                }
                else
                    //按照权重升序
                    return a.GetCardWeight.CompareTo(b.GetCardWeight);
            }
        );
    }

    /// <summary>
    /// 黑杰克
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool IsBlack(Card[] cards)
    {
        if (cards.Length == 2)
        {
            if (cards[0].GetCardWeight == Weight.One || cards[1].GetCardWeight == Weight.One)
            {
                if (cards[0].GetCardWeight == Weight.Jack || cards[0].GetCardWeight == Weight.King || cards[0].GetCardWeight == Weight.Queen)
                    return true;

            }

        }
        return false;
    }

    /// <summary>
    /// 顺子
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool IsStraight(Card[] cards)
    {
        if (cards.Length != 3)
            return false;
        for (int i = 0; i < cards.Length - 1; i++)
        {
            Weight w = cards[i].GetCardWeight;
            Debug.Log("进入顺子判断，权值为："+w);
            if ( w - cards[i+1].GetCardWeight != 1)
                return false;
        }

        return true;
    }

    /// <summary>
    /// 炸弹
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool IsBoom(Card[] cards)
    {
        if (cards.Length != 3)
            return false;

        if (cards[0].GetCardWeight != cards[1].GetCardWeight)
            return false;
        if (cards[1].GetCardWeight != cards[2].GetCardWeight)
            return false;

        return true;
    }

    /// <summary>
    /// 五小龙
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool IsFiveLoong(Card[] cards)
    {
        if (cards.Length != 5)
            return false;
        int wAll = 0;
        for (int i = 0; i < cards.Length - 1; i++)
        {

            
            Weight w = cards[i].GetCardWeight;
            wAll = wAll + (int) w;            
        }
        if (wAll > 16)
            return false;

        return true;
    }

    /// <summary>
    /// 六小龙
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool IsSixLoong(Card[] cards)
    {
        if (cards.Length != 6)
            return false;
        int wAll = 0;
        for (int i = 0; i < cards.Length - 1; i++)
        {


            Weight w = cards[i].GetCardWeight;
            wAll = wAll + (int)w;
        }
        if (wAll > 15)
            return false;

        return true;
    }

    public static bool IsCardBoom(Card[] cards)
    {
        int wAll = 0;
        for (int i = 0; i < cards.Length; i++)
        {
            Weight w = cards[i].GetCardWeight;
            if ((int)w>9)
            {
                w = Weight.Ten;
            }
            wAll = wAll + (int)w;
        }
        if (wAll > 21- cards.Length)
        {
          
            Debug.Log("<color=red>爆牌</color> ");
           
            return true;            
        }
        return false;
    }

    /// <summary>
    /// 判断为什么牌型
    /// </summary>
    /// <param name="cards"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public static void IsCardtype(List<Card> cardlist,out CardsType type)
    {
        SortCards(cardlist,false);
        Card[] cards = cardlist.ToArray();
        
        type = CardsType.None;
        switch (cards.Length)
        {
            case 2:
                if (IsBlack(cards))
                {
                    type = CardsType.BlackJoker;
                    Debug.Log("<color=red>黑杰克</color> ");
                }
                else
                {
                    type = CardsType.Single;
                }
                break;
            case 3:
                if (IsBoom(cards))
                {
                    type = CardsType.Boom;
                    Debug.Log("<color=yellow>炸弹</color> ");
                }
                else if (IsStraight(cards))
                {
                    type = CardsType.Straight;
                    Debug.Log("<color=blue>顺子</color> ");
                }
                else
                {
                    type = CardsType.Single;
                    if (IsCardBoom(cards))
                    {
                        type = CardsType.CardBoom;
                    }
                }
                break;
            case 4:

                type = CardsType.Single;
                if (IsCardBoom(cards))
                {
                    type = CardsType.CardBoom;
                }
                break;
            case 5:
                if (IsFiveLoong(cards))
                {
                    type = CardsType.FiveLoong;
                    Debug.Log("<color=green>五小龙</color> ");
                }
                else
                {
                    type = CardsType.Single;
                    if (IsCardBoom(cards))
                    {
                        type = CardsType.CardBoom;
                    }
                }
                break;
            case 6:
                if (IsSixLoong(cards))
                {
                    type = CardsType.SixLoong;
                    Debug.Log("<color=yellow>六小龙</color> ");
                }
                else
                {
                    type = CardsType.Single;
                    if (IsCardBoom(cards))
                    {
                        type = CardsType.CardBoom;
                    }
                }
                break;
            default:
                type = CardsType.Single;
                break;

        }

    }

}

	

