using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItem : MonoBehaviour
{
    public Image image;
    public Text nametext;
    public Text scoretext;

    private PlayerData playerData;


    public void SetData(PlayerData data )
    {
        playerData = data;
        image.sprite = playerData.image;
        nametext.text = playerData.nametext;
        scoretext.text = playerData.scoretext;
    }
	
}
