using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Movement movement;
    public Home home { get; private set; }
    public Scatter scatter { get; private set; }
    public Chase chase { get; private set; }
    public Frightened frightened { get; private set; }
    public GhostModes initialBehavior;
    public Transform target;
    public int points = 200;
    [SerializeField]
    private SpriteRenderer sr;
    private Color initialColor;

    public delegate void PacmanKilled();
    public static PacmanKilled OnPacmanKill;

    
    private void Awake()
    {
        movement = GetComponent<Movement>();
        home = GetComponent<Home>();
        scatter = GetComponent<Scatter>();
        chase = GetComponent<Chase>();
        frightened = GetComponent<Frightened>();
        sr = GetComponent<SpriteRenderer>();
        initialColor = sr.color;
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        gameObject.SetActive(true);
        movement.ResetState();

        frightened.Disable();
        chase.Disable();
        scatter.Enable();

        if (home != initialBehavior)
        {
            home.Disable();
        }

        if (initialBehavior != null)
        {
            initialBehavior.Enable();
        }
    }

    public void SetPosition(Vector3 position)
    {

        position.z = transform.position.z;
        transform.position = position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (!frightened.enabled)
            {
                OnPacmanKill?.Invoke();
            }
        }
    }
    public void ChangeColor(Color color)
    {
        sr.color = color;
    }
    public Color GetInitialColor()
    {
        return initialColor;
    }

}
