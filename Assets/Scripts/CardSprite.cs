using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSprite : MonoBehaviour
{
    private Card card;
    public Image image;
    public Image back;
    private bool isback;

    void Start()
    {
    }

    /// <summary>
    /// sprite所装载的card
    /// </summary>
    public Card Poker
    {
        set
        {
            card = value;
            card.isSprite = true;
            SetSprite();
        }
        get { return card; }
    }

    public bool Back
    {
        set { isback = value; }
        get { return isback; }
    }


    /// <summary>
    /// 设置Sprite的显示
    /// </summary>
    void SetSprite()
    {
        if (isback == false)
        {
            string path = "Texture/puke/" + card.GetCardName;
            Sprite image3 = Resources.Load<Sprite>(path);
            //object image2 = Resources.Load(path);
            image.sprite = image3;
          
        }
        else
        {
            string path = "Texture/puke/" + card.GetCardName;
            Sprite image3 = Resources.Load<Sprite>(path);
            //object image2 = Resources.Load(path);
            image.sprite = image3;

            string path1 = "Texture/puke/beimian" ;
            Sprite image2 = Resources.Load<Sprite>(path1);
            //object image2 = Resources.Load(path);
            back.sprite = image2;
            back.gameObject.SetActive(true);
        }
       

        

    }

    public void DoMovePosition(GameObject parent, int index)
    {
        Vector3 tra  = new Vector3(); 

        //transform.SetSiblingIndex(index);
        if (card.Attribution == CharacterType.Player || card.Attribution == CharacterType.ComputerThree || card.Attribution == CharacterType.ComputerFour )
        {
           
            tra = Vector3.right * index*0.5f;          
            Sequence seq = DOTween.Sequence();
            seq.Join(transform.DOMove(parent.transform.GetChild(0).transform.position +tra, 0.5f));
        }
        else if (card.Attribution == CharacterType.ComputerTwo || card.Attribution == CharacterType.ComputerOne || card.Attribution == CharacterType.ComputerFive)
        {
            tra = Vector3.down * index*0.5f;
            Sequence seq = DOTween.Sequence();
            seq.Join(transform.DOMove(parent.transform.GetChild(0).transform.position + tra, 0.5f));
        }

       

    }


    public void GoToPosition(GameObject parent, int index)
    {
        transform.SetSiblingIndex(index);
        if (card.Attribution == CharacterType.Player || card.Attribution == CharacterType.ComputerOne)
        {
            transform.localPosition = Vector3.right * 25 * index;
        } else if (card.Attribution == CharacterType.ComputerTwo|| card.Attribution == CharacterType.ComputerThree|| card.Attribution == CharacterType.ComputerFour|| card.Attribution == CharacterType.ComputerFive)
        {
            transform.localPosition = Vector3.up * 25 * index;
        }
        



    }


    /// <summary>
    /// 销毁精灵
    /// </summary>
    public void Destroy()
    {
        //精灵化false
        card.isSprite = false;
        //销毁对象
        Destroy(this.gameObject);
    }


}
