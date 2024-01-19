using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveKart : MonoBehaviour
{
    public float velocity;
    public float jumpingStrength;
    public LayerMask layerGround;
    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    private CapsuleCollider2D capsuleCollider;

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, Vector3.down * 0.9f, Color.red);

        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        ProcessMovement();
        ProcessJump();
    }
    bool canJump()
    {
        RaycastHit2D rayCast = Physics2D.BoxCast(boxCollider.bounds.center, new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y), 0f, Vector2.down, 0.2f, layerGround);
        return rayCast.collider != null;
    }
    void ProcessJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump())
        {
            if (coyoteTimeCounter > 0f)
            {
                rigidBody.AddForce(Vector2.up * jumpingStrength, ForceMode2D.Impulse);
                coyoteTimeCounter = 0f;
            }
        }
    }

    void ProcessMovement()
    {
        float inputMovement = Input.GetAxis("Horizontal");
        rigidBody.velocity = new Vector2(inputMovement * velocity, rigidBody.velocity.y);
    }

    bool IsGrounded()
    {
        // Adjust the parameters based on your character's collider and the ground
        return Physics2D.Raycast(transform.position, Vector3.down, 0.9f);
    }
}
