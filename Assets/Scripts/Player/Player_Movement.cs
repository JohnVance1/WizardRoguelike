using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Player_Movement : MonoBehaviour
{
    [SerializeField]

    private Rigidbody2D rb;
    private float horizontal;
    private float vertical;
    public Player player;
    private float prevHorizontal;
    private float prevVertical;
    public Sprite[] directionSprites;
    private Animator animator;
    public float speed;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        speed = 5f;
    }

    

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        //Movement();

    }

    

    public void Movement(InputAction.CallbackContext context)
    {
        Vector2 move = context.ReadValue<Vector2>();
        //horizontal = Input.GetAxisRaw("Horizontal");

        //vertical = Input.GetAxisRaw("Vertical");
        if (move.x != 0 && move.y != 0)
        {
            prevHorizontal = move.x;
            prevVertical = move.y;

        }
        else if (move.x != 0)
        {
            prevHorizontal = move.y;
            prevVertical = 0;

        }
        else if (move.y != 0)
        {
            prevVertical = move.y;
            prevHorizontal = 0;

        }

        if (move.x < 0 && move.y < 0)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = directionSprites[(int)Sprites.FrontLeft];
        }

        if (move.x > 0 && move.y < 0)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = directionSprites[(int)Sprites.FrontRight];
        }

        if (move.x < 0 && move.y > 0)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = directionSprites[(int)Sprites.BackLeft];
        }

        if (move.x > 0 && move.y > 0)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = directionSprites[(int)Sprites.BackRight];
        }

        if (move.x > 0 && move.y == 0)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = directionSprites[(int)Sprites.FrontRight];
        }

        if (move.x < 0 && move.y == 0)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = directionSprites[(int)Sprites.FrontLeft];
        }

        if (move.x == 0 && move.y > 0)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = directionSprites[(int)Sprites.BackRight];
        }

        

        rb.velocity = move.normalized * speed;

        if (rb.velocity != Vector2.zero)
        {
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

    }

}
