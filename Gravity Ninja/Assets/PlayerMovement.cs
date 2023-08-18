using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//to do 
/**
 * cooldown on press dash
 * timer then push back against dash to prevent floating
 * stop more horizontal movement after gravity shift
 * dash on diagonal axis
 * stop gravity flipping mid air
 * 
 */
public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D player;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float dashSpeed = 1f;
    float totalMovement;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        SetVelocity();
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
        player.AddForce(new Vector2(moveSpeed * Input.GetAxisRaw("Horizontal"), 0));
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
            if (player.velocity.x > 0)
            {
                player.AddForce(new Vector2(dashSpeed, 0), ForceMode2D.Impulse);
            }
            else if (player.velocity.x < 0)
            {
                player.AddForce(new Vector2(-dashSpeed, 0), ForceMode2D.Impulse);

            }

        }
    }

    private void SetVelocity()
    {

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


}
