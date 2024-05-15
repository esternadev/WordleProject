using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance => instance;

    private static T instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = (T)FindObjectOfType(typeof(T));


            if (instance == null)
            {
                GameObject singleton = new GameObject();
                singleton.AddComponent<T>();

                singleton.name = "(MonoSingleton) " + typeof(T);

                DontDestroyOnLoad(singleton);
            }
        }

    }
}
