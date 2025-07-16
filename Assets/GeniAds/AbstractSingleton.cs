using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractSingleton <T> : MonoBehaviour where T : AbstractSingleton<T>
{
    public static T instance { get; private set; }
   
    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
