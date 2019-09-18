using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : Singleton<Timer>
{
    public delegate void function();
    public delegate void functionParam(object obj);

    List<InvokePair> mFunctionList = new List<InvokePair>();
    List<InvokePairParam> mFunctionParamList = new List<InvokePairParam>();

    public class InvokePair
    {
        public function func;
        public float dealyTime;
    }
    public class InvokePairParam
    {
        public functionParam func;
        public float dealyTime;
        public object param;
    }


    public void Update()
    {
        if(mFunctionParamList.Count > 0)
        {
            for(int i = 0;i< mFunctionParamList.Count;i++)
            {
                mFunctionParamList[i].dealyTime -= Time.deltaTime;
                if(mFunctionParamList[i].dealyTime <= 0)
                {
                    mFunctionParamList[i].func(mFunctionParamList[i].param);
                    mFunctionParamList.Remove(mFunctionParamList[i]);
                }
            }
        }
        if (mFunctionList.Count > 0)
        {
            for (int i = 0; i < mFunctionList.Count; i++)
            {
                mFunctionList[i].dealyTime -= Time.deltaTime;
                if (mFunctionList[i].dealyTime <= 0)
                {
                    mFunctionList[i].func();
                    mFunctionList.Remove(mFunctionList[i]);
                }
            }
        }
    }

    public void AddFunction(function func,float dealyTime)
    {
        InvokePair invokePair = new InvokePair();
        invokePair.func = func;
        invokePair.dealyTime = dealyTime;
        mFunctionList.Add(invokePair);
    }

    public void RemoveFunction(function func)
    {
        InvokePair invokePair = mFunctionList.Find(x => x.func == func);
        if (invokePair != null)
        {
            mFunctionList.Remove(invokePair);
        }
    }
    public void AddFunction(functionParam func,object obj, float dealyTime)
    {
        InvokePairParam invokePair = new InvokePairParam();
        invokePair.func = func;
        invokePair.dealyTime = dealyTime;
        invokePair.param = obj;
        mFunctionParamList.Add(invokePair);
    }

    public void RemoveFunction(functionParam func)
    {
        InvokePairParam invokePair = mFunctionParamList.Find(x => x.func == func);
        if (invokePair != null)
        {
            mFunctionParamList.Remove(invokePair);
        }
    }
}
