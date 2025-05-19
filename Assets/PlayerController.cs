using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    private Rigidbody rb;
    private bool isGrounded;
    private Animator animator;
    private bool running = false;
    private GameController game; 

    void Start()
    {
        game = FindFirstObjectByType<GameController>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!running) return;
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        if (!running) return;
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontalInput, 0f, 0f) * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!running) return;
        if (collision.gameObject.CompareTag("Road"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            game.StopGame();
        }
    }

    public void StartPlayer()
    {
        running = true;
    }

    public void StopPlayer()
    {
        running = false;
    }
}