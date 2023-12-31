using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//to do 
/**
 * make the game shut down on over
 * add enemy spawning
 * add score and ui
 * add art and animation
 */
public class PlayerBehaviour : MonoBehaviour
{
    Rigidbody2D player;
    [Header("Movement")]
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float maxSpeed = 1f;

    [Header("Dash")]
    [SerializeField] float dashSpeed = 1f;
    [SerializeField] float dashTime = 1f;
    [SerializeField] float dashCooldown = 1f;
    float lastDash = 0f;

    [Header("Stats")]
    [SerializeField] float playerHealth = 5f;

    bool movementEnabled = true;
    bool limitVelocity = true;
    float totalMovement;
    Vector2 playerVel;

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        CorrectRotation();
        FlipGravity();
        PlayerDash();

    }

    private void FixedUpdate()
    {
        HorizontalMovement();
    }

    private void HorizontalMovement()
    {
        if (movementEnabled)
        {
            player.AddForce(new Vector2(moveSpeed * Input.GetAxisRaw("Horizontal"), 0));
        }

        if (player.velocity.x > maxSpeed && limitVelocity)
        {
            player.velocity = new Vector2(maxSpeed, player.velocity.y);
        }
        else if (player.velocity.x < -maxSpeed && limitVelocity)
        {
            player.velocity = new Vector2(-maxSpeed, player.velocity.y);

        }
    }

    private void CorrectRotation()
    {
        if (gameObject.transform.eulerAngles.z != 0)
        {
            gameObject.transform.eulerAngles = new Vector3(
            gameObject.transform.eulerAngles.x,
            gameObject.transform.eulerAngles.y,
            0
        );
        }
    }



    private void FlipGravity()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.gravityScale = -(player.gravityScale);
        }
    }



    private void PlayerDash()
    {


        if (Input.GetKeyDown(KeyCode.LeftShift))
        {


            if (Time.time - lastDash < dashCooldown)
            {
                return;
            }
            playerVel = player.velocity;
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                LockMovement();
                player.AddForce(new Vector2(dashSpeed, 0), ForceMode2D.Impulse);
                Invoke("CompleteDash", dashTime);
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                LockMovement();
                player.AddForce(new Vector2(-dashSpeed, 0), ForceMode2D.Impulse);
                Invoke("CompleteDash", dashTime);
            }

        }


    }

    private void LockMovement()
    {
        movementEnabled = false;
        limitVelocity = false;
    }

    private void CompleteDash()
    {
        CounterDash();
        movementEnabled = true;
        limitVelocity = true;
        lastDash = Time.time;
    }

    private void CounterDash()
    {
        player.velocity = new Vector2(playerVel.x, player.velocity.y);
    }

    public void TakeDamage(float damage)
    {
        playerHealth -= damage;

        Debug.Log("Player health: " + playerHealth);

        if (playerHealth <= 0)
        {
            Debug.Log("Game Over");
            Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;

        }
    }
}
