using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 发牌顺序和游戏要牌顺序
/// </summary>
public class OrderController 
{
    private CharacterType banker;//庄家
    private CharacterType currentplayer;//当前发牌者
    private int round=0;
    private static OrderController instance;

    public static OrderController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new OrderController();
            }
            return instance;
        }

    }

    /// <summary>
    /// 当前出牌者
    /// </summary>
    public CharacterType Type
    {
        get { return currentplayer; }
    }

    /// <summary>
    /// 庄家
    /// </summary>
    public CharacterType Banker
    {
        set { banker = value; }
        get { return banker; }
    }

    public int Round
    {
        set { round += value; }
        get { return round; }
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="type"></param>
    public void Init(CharacterType zhuangtype,CharacterType type)
    {       
        banker = zhuangtype;
        currentplayer = type;

        //Debug.Log("当前庄"+ banker +"当前出牌者"+ currentplayer);
        if (currentplayer == CharacterType.Player)
        {
            GameObject.Find("GameObject").GetComponent<PlaySceneController>().ShowBtnAdd();
        }
        else
        {
            AiController.IsAddHandcardsAi(currentplayer);
        }
    }




    /// <summary>
    /// 出牌轮转
    /// </summary>
    public void Turn()
    {              
        int i =GameController.Instance.GetTypeNextIndex(currentplayer);
        if (i != PlayerController.Instance.Playerlist.Count)
        {
            
            currentplayer = (CharacterType)PlayerController.Instance.Playerlist[i];
            //Debug.Log("下一个出牌电脑"+ currentplayer);
        }
        else
        {
            currentplayer = CharacterType.Player;
            //Debug.Log("当前出派人"+currentplayer);
        }      

    }

}
