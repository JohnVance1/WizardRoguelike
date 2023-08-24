using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

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
        Movement();

    }

    public void Movement()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0 && vertical != 0)
        {
            prevHorizontal = horizontal;
            prevVertical = vertical;

        }
        else if (horizontal != 0)
        {
            prevHorizontal = horizontal;
            prevVertical = 0;

        }
        else if (vertical != 0)
        {
            prevVertical = vertical;
            prevHorizontal = 0;

        }

        if (horizontal < 0 && vertical < 0)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = directionSprites[(int)Sprites.FrontLeft];
        }

        if (horizontal > 0 && vertical < 0)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = directionSprites[(int)Sprites.FrontRight];
        }

        if (horizontal < 0 && vertical > 0)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = directionSprites[(int)Sprites.BackLeft];
        }

        if (horizontal > 0 && vertical > 0)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = directionSprites[(int)Sprites.BackRight];
        }

        if (horizontal > 0 && vertical == 0)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = directionSprites[(int)Sprites.FrontRight];
        }

        if (horizontal < 0 && vertical == 0)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = directionSprites[(int)Sprites.FrontLeft];
        }

        if (horizontal == 0 && vertical > 0)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = directionSprites[(int)Sprites.BackRight];
        }
        rb.velocity = new Vector3(horizontal, vertical, 0).normalized * speed;

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
