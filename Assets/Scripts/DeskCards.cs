using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskCards
{
    private static DeskCards instance;
    private List<Card> library;
    private CharacterType ctype;
    private CardsType rule;

    public void Init()
    {

    }

    /// <summary>
    /// 单例属性
    /// </summary>
    public static DeskCards Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new DeskCards();
            }
            return instance;
        }
    }

    public CardsType Rule
    {
        set { rule = value; }
        get { return rule; }
    }

    /// <summary>
    /// 索引器
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public Card this[int index]
    {
        get
        {
            return library[index];
        }
    }

    /// <summary>
    /// 获取牌库中牌的数量
    /// </summary>
    public int CardsCount
    {
        get { return library.Count; }
    }

    /// <summary>
    /// 总权值
    /// </summary>
    public int TotalWeight
    {
        get
        {
            return GameController.Instance.GetCardsWeight(library.ToArray());
        }
    }

    /// <summary>
    /// 私有构造
    /// </summary>
    private DeskCards()
    {
        library = new List<Card>();
        ctype = CharacterType.Desk;
        rule = CardsType.None;
    }

    /// <summary>
    /// 发牌
    /// </summary>
    public Card Deal()
    {
        Card ret = library[library.Count - 1];
        library.Remove(ret);
        return ret;
    }

    /// <summary>
    /// 向牌库中添加牌
    /// </summary>
    /// <param name="card"></param>
    public void AddCard(Card card)
    {
        card.Attribution = ctype;
        library.Add(card);
    }

    /// <summary>
    /// 清空桌面
    /// </summary>
    public void Clear()
    {
        if (library.Count != 0)
        {
            CardSprite[] cardSprites = GameObject.Find("").GetComponentsInChildren<CardSprite>();
            for (int i = 0; i < cardSprites.Length; i++)
            {
                cardSprites[i].transform.parent = null;
                cardSprites[i].Destroy();
            }

            while (library.Count != 0)
            {
                Card card = library[library.Count - 1];
                library.Remove(card);
                Deck.Instance.AddCard(card);
            }

            rule = CardsType.None;
        }
    }

    /// <summary>
    /// 手牌排序
    /// </summary>
    public void Sort()
    {
        CardRules.SortCards(library, true);
    }

}
