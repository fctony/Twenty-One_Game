using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySceneController : MonoBehaviour
{
   
    private List<int> playerlist = new List<int>();
    private GameObject playerItem;
    private GameObject playerItem2;

    public GameObject btnready;
    public GameObject btnaddcard;
    public GameObject btnnotaddcard;
    public GameObject btnboomcard;
    public GameObject btncomparecard;
    public GameObject btnhidecard;
    public GameObject btninstall;
    public GameObject btnone;
    public GameObject btnfive;
    public GameObject btnten;
    public GameObject bttomnote;
    public GameObject btnnext;

    public GameObject install;
    public GameObject mark;
    public GameObject cardboom;
    private int cardcount;

    private void Start()
    {
        cardcount = 2;
        Init();       
        PlayerController.Instance.GetComputerPlayer();
        playerlist = PlayerController.Instance.Playerlist;
        playerItem = Resources.Load("prafab/playerItem") as GameObject;       
        playerItem2 = Resources.Load("prafab/playerItem2") as GameObject;
        PlayerController.Instance.GiveComputerIntegration();
        PlayerController.Instance.PlayerIntegration();
       
        CharacterType type = (CharacterType)playerlist[0];
        GameObject parentObj = GameObject.Find(type.ToString());
        HandCards player = parentObj.GetComponent<HandCards>();
        player.AccessIdentity = Identity.zhuang;
        GetPlayerItem();       
    }

    private void Update()
    {
        Singleton<Timer>.GetSingleton().Update();
    }

    private void Init()
    {
        btnready.GetComponent<Button>().onClick.AddListener(Btnready);
        btnaddcard.GetComponent<Button>().onClick.AddListener(PlayerController.Instance.OnbtnAdd);
        btnnotaddcard.GetComponent<Button>().onClick.AddListener(PlayerController.Instance.OnbtnDontAddCard);
        btncomparecard.GetComponent<Button>().onClick.AddListener(PlayerController.Instance.OnbtnComparecard);
        btnone.GetComponent<Button>().onClick.AddListener(delegate () {BtnBottomnote(btnone.name);});
        btnten.GetComponent<Button>().onClick.AddListener(delegate () { BtnBottomnote(btnten.name); });
        btnfive.GetComponent<Button>().onClick.AddListener(delegate () { BtnBottomnote(btnfive.name); });
        btnnext.GetComponent<Button>().onClick.AddListener(BtnNext);
        btninstall.GetComponent<Button>().onClick.AddListener(OnBtninstall);
    }

    public void Btnready()
    {
        HandCards hc = GameController.Instance.GetHandCards(CharacterType.Player);
        mark.SetActive(false);
        bttomnote.SetActive(true);
        btnready.SetActive(false);
        if (OrderController.Instance.Round >=1)
        {
            //Debug.Log("更新");
            GameController.Instance.UpdateIndentity();
        }
    }
    /// <summary>
    /// 洗牌回调
    /// </summary>
    public void DealCallBack()
    {
        StartCoroutine(IEDealCards());
    }

    public IEnumerator IEDealCards()
    {       
        yield return new WaitForSeconds(0.5f);
        DealCards();
    }

    public List<int> PlayerList
    {
        set { playerlist = value; }
    }

    /// <summary>
    /// 洗牌  生成头两张牌
    /// </summary>
    public void DealCards()
    {
        //洗牌
        Deck.Instance.Shuffle();
        //发牌
        CharacterType currentCharacter = CharacterType.Player;
        for (int i = 0; i < playerlist.Count * cardcount; i++)
        {
            GameController.Instance.DealTo(currentCharacter);
            if (i >= playerlist.Count|| currentCharacter == CharacterType.Player)
            {
                MakeHandCardsSprite(currentCharacter, false);
            }
            else
            {
                MakeHandCardsSprite(currentCharacter, true);
            }           
            currentCharacter = (CharacterType)playerlist[(i + 1) % playerlist.Count];
        }     
        StartCoroutine(DoTweenParent());
        foreach (int i in playerlist)
        {
            HandCards hc = GameController.Instance.GetHandCards((CharacterType)i);           
            //判断牌类型
            GameController.Instance.GetCardsType((CharacterType)i);                       
            Debug.Log("牌型：" + hc.HandCardsType+"模式:"+ PlayerPrefs.GetInt("type"));
            PlayerController.Instance.PlayerAllWeight((CharacterType)i);
        }       
        StartCoroutine(ComputerAiBagin());
       
    }

    /// <summary>
    /// 电脑和玩家要牌后发给他一张
    /// </summary>
    /// <param name="type"></param>
    public void DealOneCards(CharacterType type)
    {        
        HandCards cards = GameController.Instance.GetHandCards(type);
        PlayerController.Instance.PlayerAllWeight(type);
        //排序
        //cards.Sort();
        //判断牌类型
        GameController.Instance.GetCardsType(type);
        Debug.Log("发一张牌："+ cards.HandCardsType);
        if (type == CharacterType.Player&&cards.HandCardsType==CardsType.CardBoom)
        {
            cardboom.SetActive(true);
        }
        bool isback = false;
        for (int i =0;i<cards.CardsCount;i++)
        {
            if (!cards[i].isSprite)
            {
                MakeSprite(type, cards[i], isback);
                StartCoroutine(DoTweenOneParent(type, cards.CardsCount-1));
            }            
        }       
    }

    /// <summary>
    /// 移动一张牌
    /// </summary>
    /// <param name="type"></param>
    /// <param name="i"></param>
    /// <returns></returns>
    public IEnumerator DoTweenOneParent(CharacterType type, int i)
    {
        yield return new WaitForSeconds(1);
        GameObject player = GameObject.Find(type.ToString());
        CardSprite[] cs = player.GetComponentsInChildren<CardSprite>();
        cs[i].DoMovePosition(player, i);
    }

    /// <summary>
    /// 显示玩家信息
    /// </summary>
    public void GetPlayerItem()
    {
        //playerDatas. 
        for (int i = 0; i < playerlist.Count; i++)
        {

            CharacterType type = (CharacterType)playerlist[i];
            if (type == CharacterType.Player || type == CharacterType.ComputerThree || type == CharacterType.ComputerFour)
            {
                GameObject obj = Instantiate(playerItem);
                GameObject parent = GameObject.Find(type.ToString());
                obj.transform.SetParent(parent.transform);
                obj.transform.localScale = new Vector3(1, 1, 1);
                obj.transform.localPosition = parent.transform.position;
                PlayerController.Instance.ShowplayerHead(obj, type);
                PlayerController.Instance.ShowPlayerScore(obj, type);
                PlayerController.Instance.ShowIndentity(obj, type);
                if (type == CharacterType.Player)
                {
                    obj.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetString("name");
                }
            }
            else
            {
                GameObject obj = Instantiate(playerItem2);
                GameObject parent = GameObject.Find(type.ToString());
                obj.transform.SetParent(parent.transform);
                obj.transform.localScale = new Vector3(1, 1, 1);
                obj.transform.localPosition = parent.transform.position;
                PlayerController.Instance.ShowplayerHead(obj, type);
                PlayerController.Instance.ShowPlayerScore(obj, type);
                PlayerController.Instance.ShowIndentity(obj, type);
            }

        }
    }

    /// <summary>
    /// 精灵化角色手牌
    /// </summary>
    /// <param name="type"></param>
    /// <param name="isSelected"></param>
    void MakeHandCardsSprite(CharacterType type,bool isback)
    {       
        HandCards cards = GameController.Instance.GetHandCards(type);
        //排序
        //cards.Sort();
       
        //精灵化
        for (int i = 0; i < cards.CardsCount; i++)
        {           
            if (!cards[i].isSprite)
            {               
                MakeSprite(type, cards[i], isback);
            }
        }
    }

    /// <summary>
    /// 使卡牌精灵化
    /// </summary>
    /// <param name="type"></param>
    /// <param name="card"></param>
    /// <param name="selected"></param>
    public void MakeSprite(CharacterType type, Card card,bool isback)
    {       
        if (!card.isSprite)
        {
            GameObject obj = Instantiate(Resources.Load("prafab/poker")) as GameObject;
            GameObject parent = GameObject.Find(type.ToString());
            obj.transform.SetParent(parent.transform);
            obj.transform.localScale = new Vector3(1, 1, 1);

            //obj.transform.localPosition = new Vector3(0,0,0);
            CardSprite sprite = obj.GetComponent<CardSprite>();
            sprite.Back = isback;
            sprite.Poker = card;
        }
    }

    //}
    /// <summary>
    /// 将生成的牌的精灵发到play位置
    /// </summary>
    public IEnumerator DoTweenParent()
    {      
        int y = 0;       
        CharacterType currentCharacter = CharacterType.Player;
        for (int i=0; i<playerlist.Count*cardcount;i++)
        {
            CharacterType type = currentCharacter;
            GameObject player = GameObject.Find(type.ToString());
            CardSprite[] cs = player.GetComponentsInChildren<CardSprite>();
            cs[y].DoMovePosition(player, y);
            yield return new WaitForSeconds(1);
            currentCharacter = (CharacterType)playerlist[(i + 1) % playerlist.Count];
            if (i == playerlist.Count * (y + 1)-1)
            {
                y ++;
            }
        }

    }

   

    /// <summary>
    /// 派发完后，电脑AI
    /// </summary>
    /// <returns></returns>
    public IEnumerator ComputerAiBagin()
    {
        float i = playerlist.Count*2 * 1.5f;
        yield return new WaitForSeconds(i);
        GameController.Instance.Play(OrderController.Instance.Banker);
    }

    /// <summary>
    /// 要牌和不要牌出现
    /// </summary>
    public void ShowBtnAdd()
    {
        if (OrderController.Instance.Type == CharacterType.Player && OrderController.Instance.Banker != CharacterType.Player)
        {
            btnaddcard.SetActive(true);
            btnnotaddcard.SetActive(true);
        }
        else
        {
            btnaddcard.SetActive(true);
            btncomparecard.SetActive(true);
        }
    }

    public void ShowBtnboom()
    {
        btnaddcard.SetActive(false);
        btnnotaddcard.SetActive(false);
        btnboomcard.SetActive(true);
        btnhidecard.SetActive(true);
    }

    public void ShowBtnNext()
    {
        btnnext.SetActive(true);
    }

    /// <summary>
    /// 明牌
    /// </summary>
    /// <param name="type"></param>
    public void OutBack(CharacterType type)
    {
        GameObject playerObj = GameObject.Find(type.ToString());       
        playerObj.transform.GetChild(4).transform.GetChild(1).gameObject.SetActive(false);
    }

    /// <summary>
    /// 注码按钮
    /// </summary>
    /// <param name="btnname"></param>
    public void BtnBottomnote(string btnname)
    {       
        DealCallBack();
        GameController.Instance.basePointPerMatch = (int)float.Parse(btnname);
        GameObject.Find("basepointpermatch").GetComponent<Text>().text = GameController.Instance.basePointPerMatch.ToString();       
        bttomnote.SetActive(false);
    }

    /// <summary>
    /// 下一局
    /// </summary>
    public void BtnNext()
    {
        GameController.Instance.End();
       //GameObject.Find("GameObject").GetComponent<WinFailSceneController>().enabled = false;
        btnaddcard.SetActive(false);
        btnnotaddcard.SetActive(false);
        btnboomcard.SetActive(false);
        btnhidecard.SetActive(false);
        btncomparecard.SetActive(false);
        btnready.SetActive(true);
        int i = 1;
        
        OrderController.Instance.Round = i;
        Debug.Log(OrderController.Instance.Round);
    }

    public void OnBtninstall()
    {
        install.SetActive(true);
    }

}
