using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Ghost[] ghost;
    [SerializeField] private Player pacman;
    void Start()
    {
        for (int i = 0; i < ghost.Length; i++)
        {
            pacman.OnPillEat += ghost[i].frightened.Enable;
        }
       
    }

    // Update is called once per frame
    void Update()
    {

    }
}
