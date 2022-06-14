using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Ghost[] ghost;
    [SerializeField] private Player pacman;
    [SerializeField] private GameOverPanel gameOverPanel;
    void Start()
    {
        for (int i = 0; i < ghost.Length; i++)
        {
            pacman.OnPillEat += ghost[i].frightened.Enable;
            ghost[i].frightened.OnGhostEat += pacman.IncreaseScore;
            Ghost.OnPacmanKill += ghost[i].ResetState;
        }
        Ghost.OnPacmanKill += pacman.SubstractLive;
        Ghost.OnPacmanKill += pacman.ResetPacman;
        pacman.OnGameOver += gameOverPanel.Active;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
