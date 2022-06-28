using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Ghost[] ghost;
    [SerializeField] private Player pacman;
    [SerializeField] private Panel gameOverPanel;
    [SerializeField] private Panel winPanel;
    [SerializeField] private Portal portal1;
    [SerializeField] private Portal portal2;
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
        pacman.OnGameOver += gameOverPanel.Activate;
        pacman.OnWin += winPanel.Activate;
        portal1.OnPortalCollision += pacman.PortalColiision;
        portal2.OnPortalCollision += pacman.PortalColiision;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnDestroy()
    {
        for (int i = 0; i < ghost.Length; i++)
        {
            pacman.OnPillEat -= ghost[i].frightened.Enable;
            ghost[i].frightened.OnGhostEat -= pacman.IncreaseScore;
            Ghost.OnPacmanKill -= ghost[i].ResetState;
        }
        Ghost.OnPacmanKill -= pacman.SubstractLive;
        Ghost.OnPacmanKill -= pacman.ResetPacman;
        pacman.OnGameOver -= gameOverPanel.Activate;
        pacman.OnWin -= winPanel.Activate;
        portal1.OnPortalCollision -= pacman.PortalColiision;
        portal2.OnPortalCollision -= pacman.PortalColiision;

    }
}
