using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//to do 
/**
 * cooldown on press dash
 * timer then push back against dash to prevent floating
 * while player is not in contact with "surface", decrease force
 * dash on diagonal axis
 * stop gravity flipping mid air
 * 
 */
public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D player;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float dashSpeed = 1f;
    [SerializeField] float dashTime = 1f;

    bool movementEnabled = true;
    float totalMovement;
    Vector2 playerVel;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
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
            LockMovement();
            playerVel = player.velocity;

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                player.AddForce(new Vector2(dashSpeed, 0), ForceMode2D.Impulse);
                Invoke("CounterDash", dashTime);
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                player.AddForce(new Vector2(-dashSpeed, 0), ForceMode2D.Impulse);
                Invoke("CounterDash", dashTime);
            }

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

    private void LockMovement()
    {
        movementEnabled = false;
    }

    private void CounterDash()
    {
        //if (player.velocity.x > 0)
        //{

        player.velocity = new Vector2(playerVel.x, player.velocity.y);
        movementEnabled = true;
        //}
        //else if (player.velocity.x < 0)
        //{
        //    player.AddForce(new Vector2(dashSpeed, 0), ForceMode2D.Impulse);
        //    movementEnabled = true;
        //}
    }


}
