using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //[SerializeField]
    public float moveSpeed = 5.0f; // made public to allow adjustment in the unity editor
    private Rigidbody2D rb;

    private Vector2 movement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Input for movement in the X and Y axis
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the movement vector
        movement = new Vector2(horizontalInput, verticalInput).normalized;
    }

    private void FixedUpdate()
    {
        // Move the character
        rb.velocity = movement * moveSpeed;

        // Face left or right depending on the horizontal movement input
        if (movement.x > 0) // Moving right
        {
            transform.localScale = new Vector3(1, 1, 1); // No flip
        }
        else if (movement.x < 0) // Moving left
        {
            transform.localScale = new Vector3(-1, 1, 1); // Flip horizontally
        }
    }
