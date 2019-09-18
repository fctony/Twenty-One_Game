using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色类型
/// </summary>
public enum CharacterType
{
    Library = 0,
    Player,
    ComputerOne,
    ComputerTwo,
    ComputerThree,
    ComputerFour,
    ComputerFive,
    Desk,
}


/// <summary>
/// 花色
/// </summary>
//public enum Suits
//{
//    Club,
//    Diamond,
//    Heart,
//    Spade,
//    None
//}

public enum Suits
{
    fangkuai,
    heitao,
    hongtao,
    meihua,
    None
}


/// <summary>
/// 卡牌权值
/// </summary>
public enum Weight
{
   
    One =0 ,
    Two ,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King,
   
}

/// <summary>
/// 身份
/// </summary>
public enum Identity
{
    Farmer,
    zhuang
}

/// <summary>
/// 出牌类型
/// </summary>
public enum CardsType
{
    //未知类型
    None,
    //爆牌
    CardBoom,
    //单牌
    Single,
    //黑杰克
    BlackJoker,
    //顺子
    Straight,
    //炸弹
    Boom,
    //五小龙
    FiveLoong,
    //六小龙
    SixLoong,

}

/// <summary>
/// 存储数据类型
/// </summary>
[System.Serializable]
public class GameData
{
    public int playerIntegration;
    public int computerOneIntegration;
    public int computerTwoIntegration;
}
