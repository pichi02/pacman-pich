using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    public static DontDestroy inst;
    void Awake()
    {
        if (DontDestroy.inst == null)
        {
            DontDestroy.inst = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
       
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Gameplay")
        {
            Destroy(gameObject);
        }
    }
}
