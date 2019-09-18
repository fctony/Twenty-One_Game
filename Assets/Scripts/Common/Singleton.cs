using UnityEngine;
using System.Collections;

public abstract class Singleton<T> where T : new()
{
    private static T _instance;
    private static readonly object _lock = new object();
    public static T GetSingleton()
    {
        if (_instance == null)
        {
            lock (_lock)
            {
                if (_instance == null)
                    _instance = new T();
            }
        }
        return _instance;
    }
}
