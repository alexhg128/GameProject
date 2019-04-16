using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy01Health : MonoBehaviour
{

    [SerializeField] private int startingHealth = 20;
    [SerializeField] private float timeSinceLastHit = 0.5f;
    [SerializeField] private float dissapearSpeed = 2f;
    [SerializeField] private int currrentHealth;
    private float timer = 0f;
    private Animator animator;
    private NavMeshAgent nav;
    private bool isAlive;
    private Rigidbody rigidbody;
    private CapsuleCollider capsuleCollider;
    private bool dissapearEnemy = false;
    private BoxCollider weaponCollider;
    private DropItems dropItems;

    public bool IsAlive
    {
        get { return isAlive; }
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        isAlive = true;
        currrentHealth = startingHealth;
        weaponCollider = GetComponentInChildren<BoxCollider>();
        dropItems = GetComponent<DropItems>();    
    }

    void Update()
    {
        timer += Time.deltaTime;
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
            animator.Play("Hit");
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
        nav.enabled = false;
        animator.SetTrigger("EnemyDie");
        rigidbody.isKinematic = true;
        weaponCollider.enabled = false;
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
