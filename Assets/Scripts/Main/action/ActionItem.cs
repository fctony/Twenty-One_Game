using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionItem : MonoBehaviour
{
    public Text daytext;
    public Text goldnum;
    public Image goldimage;

    private ActionData ActionData;


    public void SetData(ActionData data)
    {
        daytext.text = data.day;
        goldnum.text = data.goldnum;
        goldimage.sprite = data.sprite;
        goldimage.SetNativeSize();
    }
	
}
