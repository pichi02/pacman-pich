using UnityEngine;


public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    private float speedMultiplier = 1f;
    [SerializeField] private Vector2 initialDirection;
    [SerializeField] private LayerMask obstacleLayer;

    private Rigidbody2D rigidbody;
    private Vector2 direction;
    private Vector2 nextDirection;
    private Vector3 startingPosition;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
    }

    private void Start()
    {
        //ResetState();
    }

    public void ResetState()
    {
        speedMultiplier = 1f;
        direction = initialDirection;
        nextDirection = Vector2.zero;
        transform.position = startingPosition;
        rigidbody.isKinematic = false;
        enabled = true;
    }

    private void Update()
    {

        if (nextDirection != Vector2.zero)
        {
            SetDirection(nextDirection);
        }
        if (gameObject.CompareTag("Player"))
        {
            Debug.DrawLine(transform.position, transform.position + Vector3.right);

        }
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidbody.position;
        Vector2 translation = direction * speed * speedMultiplier * Time.fixedDeltaTime;

        rigidbody.MovePosition(position + translation);
    }

    public void SetDirection(Vector2 direction, bool forced = false)
    {

        if (!Occupied(direction) || forced)
        {
            this.direction = direction;
            nextDirection = Vector2.zero;
        }
        else
        {
            nextDirection = direction;
        }
    }

    public bool Occupied(Vector2 direction)
    {
        RaycastHit2D hit;
        if (direction == Vector2.up || direction == Vector2.down)
        {
            hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.46f, 0f, direction, 1.5f, obstacleLayer);

        }
        else
        {
            hit = Physics2D.BoxCast(transform.position, new Vector2(0.3f, 0.92f), 0f, direction, 1.5f, obstacleLayer);
        }
        return hit.collider != null;
    }
    public Vector2 GetDirection()
    {
        return this.direction;
    }
    public void ChangeKinematicRBValue(bool value)
    {
        rigidbody.isKinematic = value;
    }
    public void ChangeSpeedMultiplier(float speedMultiplier)
    {
        this.speedMultiplier = speedMultiplier;
    }


}