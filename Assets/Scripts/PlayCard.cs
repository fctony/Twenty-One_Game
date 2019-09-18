using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCard 
{
    private static PlayCard instance;

    public static PlayCard Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PlayCard();
            }
            return instance;
        }
    }





}
