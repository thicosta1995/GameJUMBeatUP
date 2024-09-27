using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PersonMovement : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float jumpForce;
    private bool player1, player2;
    [SerializeField] int playerIndex=0;
    private float currentSpeed;
    private Rigidbody rb;
    private Animator anim;
    public int maxHelth = 10;
    public string playerName;
    public Sprite playerImage;
    public int currentHealth;
    private Transform groundCheck;
    public float minHight;
    public float maxHight;
    public float h, z;
    private bool onGround;
    private bool isDead = false;
    private bool facingRight = true;
    private bool jump = false;
    private MenuScript menu;
    [SerializeField]private Transform[] PontoDeSpawn;
    [SerializeField] private GameObject[] Players;
    private bool teleported = false; // Variável para controlar o estado do teleporte


    public int GetPlayerIndex()
    {
        return playerIndex;
    }
    // Start is called before the first frame update
    void Start()
    {
     
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        PontoDeSpawn = new Transform[GameObject.FindGameObjectsWithTag("ponto").Length]; // Encontrar todos os jogadores
        for (int i = 0; i < PontoDeSpawn.Length; i++)
        {
            PontoDeSpawn[i] = GameObject.FindGameObjectsWithTag("ponto")[i].transform; // Armazenar as posições dos jogadores
        }
        Players = new GameObject[FindObjectsOfType<PersonMovement>().Length]; // Encontrar todos os jogadores
        for (int i = 0; i < Players.Length; i++)
        {
            Players[i] = FindObjectsOfType<PersonMovement>()[i].gameObject; // Armazenar as posições dos jogadores

        }
        groundCheck = gameObject.transform.Find("GroundCheck");
        menu = GetComponent<MenuScript>();
        currentHealth = maxHelth;
        player1 = true; player2 = false;
    }

  
    // Update is called once per frame
    void Update()
    {
        onGround = Physics.Linecast(transform.position,groundCheck.position,1<< LayerMask.NameToLayer("Ground"));
        anim.SetBool("OnGround", onGround);
        anim.SetBool("Dead", isDead);
        if(InutManneger.IsJumpInput() && onGround ) 
        {
            jump = true;
        }
        if(InutManneger.GetAttackInput())
        {
            anim.SetTrigger("Attack");
        }
        if (InutManneger.GetTeleportInput())
        {
            GetTeleport();
        }

    }
    IEnumerator DisableMovementTemporarily(GameObject player)
    {
        player.GetComponent<PersonMovement>().enabled = false;
        yield return new WaitForSeconds(0.1f);  // Desativa por 0.1 segundos
        player.GetComponent<PersonMovement>().enabled = true;
    }

    void GetTeleport()
    {
        if (Players.Length < 2) return;

        Rigidbody rbPlayer1 = Players[0].GetComponent<Rigidbody>();
        Rigidbody rbPlayer2 = Players[1].GetComponent<Rigidbody>();

        rbPlayer1.velocity = Vector3.zero;
        rbPlayer2.velocity = Vector3.zero;

        if (!teleported)
        {
            Vector3 tempPosition = rbPlayer1.position;
            rbPlayer1.position = rbPlayer2.position;
            rbPlayer2.position = tempPosition;
            teleported = true;
        }
        else
        {
            Vector3 tempPosition = rbPlayer1.position;
            rbPlayer1.position = rbPlayer2.position;
            rbPlayer2.position = tempPosition;
            teleported = false;
        }

        StartCoroutine(DisableMovementTemporarily(Players[0].gameObject));
        StartCoroutine(DisableMovementTemporarily(Players[1].gameObject));

        Debug.Log("Teleportação concluída.");
    }

    private void FixedUpdate()
    {
       if(!isDead)
        {
            //    if (player1 == true)
            //    {
            h = InutManneger.GetMoviment().x;
                 z = InutManneger.GetMoviment().y;

           
            //    if(h< 0) 
            //    {
            //        h = 0;
            //        player1 = false;
            //       player2 = true;
            //    }
            //}
            //if (player2 == true)
            //{
            //    h = InutManneger.GetMovimentSecond().x;
            //    z = InutManneger.GetMovimentSecond().y;

            //    if (h > 0)
            //    {
            //        h = 0;
            //        player1 = true;
            //        player2 = false;
            //    }
            //}
            //if (!onGround)
            //    z = 0;



            rb.velocity = new Vector3(h*currentSpeed,rb.velocity.y,z*currentSpeed);
            anim.SetFloat("Speed", Mathf.Abs(rb.velocity.magnitude));
            
            if(h>0 && !facingRight)
            {
                Flip();
            }
            else if(h<0 && facingRight) 
            {
                Flip();
            }

            if(jump)
            {
                jump = false;
                rb.AddForce(Vector3.up * jumpForce);
            }
            float minWidth = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10)).x;
            float maxWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 10)).x;
            rb.position = new Vector3(Mathf.Clamp(rb.position.x, minWidth + 1 ,maxWidth - 1),rb.position.y,Mathf.Clamp(rb.position.z,minHight,maxHight));

        }

    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    void ZeroSpeed()
    {
        currentSpeed = 0;
    }
    void ResetSpeed()
    {
        currentSpeed = maxSpeed;
    }
    public void TookDamage(int damege)
    {
        if(!isDead)
        {
            if(playerIndex == 1)
            {
                currentHealth -= damege+2;
                anim.SetTrigger("HitDamage");
                FindObjectOfType<UIBavivor>().UpdateHealt(currentHealth);
            }
            else
            {
                currentHealth -= damege;
                anim.SetTrigger("HitDamage");
                FindObjectOfType<UIBavivor>().UpdateHealt(currentHealth);
            }
            
        }


    }
}
