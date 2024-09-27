using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float maxSpeed;
    public float minHight, maxHight;
    private float currentSpeed;
    public float damageTime = 0.5f;
    public int maxHealth;

    private int correntHealth;
  
    private Rigidbody rb;
    private Animator anim;
   [SerializeField] private Transform groundCheck;
    public float attackRate = 1.0f;
    private bool onGround;
    public string enemyName;
    private bool isDead = false;
    private bool facingRight = false;
    private bool jump = false;
    private Transform target;
    private float zForce;
    private float walkTime;
    private bool damaged = false;
    private float damageTimer;
    private float nextAttack;
    public Sprite enemyImage;
    private Transform[] players;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        groundCheck = transform.Find("GroundCheckE");
        target = FindObjectOfType<PersonMovement>().transform;
        correntHealth = maxHealth;
        players = new Transform[FindObjectsOfType<PersonMovement>().Length]; // Encontrar todos os jogadores
        for (int i = 0; i < players.Length; i++)
        {
            players[i] = FindObjectsOfType<PersonMovement>()[i].transform; // Armazenar as posições dos jogadores
        }

    }

    // Update is called once per frame
    void Update()
    {
        onGround = Physics.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        anim.SetBool("Grounded", onGround);
        anim.SetBool("Dead", isDead);
        target = FindClosestPlayer();

        facingRight = (target.position.x<transform.position.x) ?false : true;
        if(facingRight)
        {
            transform.eulerAngles = new Vector3(0,180,0);

        }
        else
        {
            transform.eulerAngles = new Vector3(0,0,0);
        }

        if(damaged && !isDead) 
        {
            damageTimer += Time.deltaTime;
            if(damageTimer >= damageTime)
            {
                damaged = false;
                damageTimer = 0;
            }
        }
        walkTime += Time.deltaTime;
    }
    private Transform FindClosestPlayer()
    {
        Transform closestPlayer = players[0];
        float shortestDistance = Vector3.Distance(transform.position, players[0].position);

        foreach (Transform player in players)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestPlayer = player;
            }
        }

        return closestPlayer;
    }
    private void FixedUpdate()
    {
        if(!isDead)
        {
            Vector3 targetDistace = target.position - transform.position;
            float hForce = targetDistace.x /Mathf.Abs(targetDistace.x);

            if(walkTime >=Random.Range(1f,2f))
            {
                zForce = Random.Range(-1, 2);
                walkTime = 0;
            }
            if(Mathf.Abs(targetDistace.x)<1.5f)
            {
                hForce = 0;
            }

            rb.velocity = new Vector3(hForce * currentSpeed, 0, zForce * currentSpeed);

            anim.SetFloat("Speed",Mathf.Abs(currentSpeed));

            if (Mathf.Abs(targetDistace.x) < 1.5f && Mathf.Abs(targetDistace.z )<1.5f && Time.time > nextAttack)
            {
                anim.SetTrigger("Attack");
                currentSpeed = 0;
                nextAttack = Time.time+ attackRate;
            }
        }
        rb.position = new Vector3(rb.position.x, rb.position.y, Mathf.Clamp(rb.position.z, minHight, maxHight));
    }
    public void TookDamege(int damage)
    {
        if(!isDead) 
        {
            damaged = true;
            correntHealth -= damage;
            anim.SetTrigger("HitDamage");
            FindObjectOfType<UIBavivor>().UpdateEnemy(maxHealth, correntHealth);
            if(correntHealth <=0)
            {
                isDead = true;
                rb.AddRelativeForce(new Vector3(3,5,0),ForceMode.Impulse);
            }
        }
    }
    public void DisableEnemy()
    {
        gameObject.SetActive(false);
    }
    void ResetSpeed()
    { 
        currentSpeed = maxSpeed;
    }

}
