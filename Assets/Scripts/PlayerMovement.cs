    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed;
    private Rigidbody2D rb;
	private Vector2 playerDirection;
    private Animator animator;
    private bool Moving;
    public static float PlayerY;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        PlayerY = transform.position.y;
        float directionX = Input.GetAxisRaw("Horizontal");
        float directionY = Input.GetAxisRaw("Vertical");

        playerDirection = new Vector2(directionX, directionY).normalized;



        if (playerDirection != Vector2.zero)
        {
            animator.SetFloat("moveX", directionX);
            animator.SetFloat("moveY", directionY);
            animator.SetBool("Moving", true);
        }
        else
            animator.SetBool("Moving", false);
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(playerDirection.x * playerSpeed, playerDirection.y * playerSpeed);
    }
}
