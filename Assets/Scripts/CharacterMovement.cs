using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    [SerializeField] private float maxSpeed = 6.0f;
    public bool facingRight = true;
    public float moveDirection;
    new Rigidbody rigidbody;
    private Animator animator;

    public float jumpSpeed = 600.0f;
    public bool grounded = false;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask whatIsGround;

    public float swordSpeed = 600.0f;
    public Transform swordSpawn;
    public Rigidbody swordPrefab;
    private AudioSource audio;
    public AudioClip jump;
    public AudioClip attackAudio;

    Rigidbody clone;

    void Awake()
    {
        groundCheck = GameObject.Find("GroundCheck").transform;
        swordSpawn = GameObject.Find("SwordSpawn").transform;
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        moveDirection = Input.GetAxis("Horizontal");

        if(grounded && Input.GetButton("Jump"))
        {
            animator.SetTrigger("isJumping");
            rigidbody.AddForce(new Vector2(0, jumpSpeed));
            audio.PlayOneShot(jump);
        }

        if(Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        rigidbody.velocity = new Vector2(moveDirection * maxSpeed, rigidbody.velocity.y);
  
        if(moveDirection > 0.0f && !facingRight)
        {
            Flip();
        }
        else if (moveDirection < 0.0f && facingRight)
        {
            Flip();
        }

        animator.SetFloat("Speed", Mathf.Abs(moveDirection));
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(Vector3.up, 180.0f, Space.World);
    }

    void Attack()
    {
        animator.SetTrigger("isAttacking");
        audio.PlayOneShot(attackAudio);
    }

    public void CallFireProyectile()
    {
        clone = Instantiate(swordPrefab, swordSpawn.position, swordSpawn.rotation) as Rigidbody;
        clone.AddForce(swordSpawn.transform.right * swordSpeed);
    }
}
