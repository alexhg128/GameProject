using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy02Health : MonoBehaviour
{

    [SerializeField] private int startingHealth = 20;
    [SerializeField] private float timeSinceLastHit = 0.5f;
    [SerializeField] private float dissapearSpeed = 2f;
    [SerializeField] private int currrentHealth;
    private float timer = 0f;
    private Animator animator;
    private bool isAlive;
    private Rigidbody rigidbody;
    private CapsuleCollider capsuleCollider;
    private bool dissapearEnemy = false;
    private BoxCollider weaponCollider;
    private AudioSource audio;
    public AudioClip hurtAudio;
    public AudioClip killAudio;
    private DropItems dropItems;

    public bool IsAlive
    {
        get { return isAlive; }
    }     
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        isAlive = true;
        currrentHealth = startingHealth;
        dropItems = GetComponent<DropItems>();  
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (dissapearEnemy)
        {
            transform.Translate(-Vector3.up * dissapearSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(timer >= timeSinceLastHit && !GameManager.instance.GameOver)
        {
            if(other.tag == "PlayerWeapon")
            {
                TakeHit();
                timer = 0f;
            }
        }
    }

    void TakeHit()
    {
        if(currrentHealth > 0)
        {
            animator.Play("Hurt");
            currrentHealth -= 10;
        }
        if(currrentHealth <= 0)
        {
            isAlive = false;
            KillEnemy();
        }
    }

    void KillEnemy()
    {
        capsuleCollider.enabled = false;
        animator.SetTrigger("EnemyDie");
        rigidbody.isKinematic = true;
        StartCoroutine(RemoveEnemy());
        dropItems.Drop();
    }

    IEnumerator RemoveEnemy()
    {
        yield return new WaitForSeconds(2f);
        dissapearEnemy = true;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
