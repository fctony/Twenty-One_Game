using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    /// <summary>
    /// 销毁精灵
    /// </summary>
    public void Destroy()
    {           
        //销毁对象
        Destroy(this.gameObject);
    }

}
