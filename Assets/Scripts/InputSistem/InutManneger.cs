using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class InutManneger : MonoBehaviour
{
    public static InutManneger Instance;
    public Vector2 moviment;
    public Vector2 secondMoviment; // Input do segundo controle
    public bool attackInput;
    public bool secondAttackInput; // Ataque do segundo controle
    public bool jumpInput;
    public bool secondJumpInput; // Pulo do segundo controle
    public bool teleportInput;
    public PlayerInput playerInput;
    private PersonMovement PersonMovement;
 
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        var person = FindObjectsOfType<PersonMovement>();
        var index = playerInput.playerIndex;
        PersonMovement = person.FirstOrDefault(m => m.GetPlayerIndex() == index);



        if (Instance == null)
        {
            Instance = this;
        }   
        else
        {
            Destroy(gameObject);
        }
    }

    void LateUpdate()
    {
       
        // Atualiza sempre o último input recebido
        attackInput = false;
        jumpInput = false;
        teleportInput = false;
    }

    public void OnMove(InputValue value)
    {
        moviment = value.Get<Vector2>();
    }
    
    //public void OnMoveSecond(InputValue value) // Input do segundo controle
    //{
    //    secondMoviment = value.Get<Vector2>();
    //}

    public void OnAttaque(InputValue value)
    {
        attackInput = value.isPressed;
    }
    public void OnTeleport(InputValue value)
    {
        teleportInput = value.isPressed;
    }
    //public void OnAttaqueSecond(InputValue value) // Input de ataque do segundo controle
    //{
    //    secondAttackInput = value.isPressed;
    //}

    public void OnPulo(InputValue value)
    {
        jumpInput = value.isPressed;
    }

    //public void OnPuloSecond(InputValue value) // Input de pulo do segundo controle
    //{
    //    secondJumpInput = value.isPressed;
    //}

    public static Vector2 GetMoviment()
    {
        return Instance.moviment;
    }

    //public static Vector2 GetMovimentSecond()
    //{
    //    return Instance.secondMoviment;
    //}
    public static bool GetAttackInput()
    {
        return Instance.attackInput;
    }

    public static bool GetTeleportInput()
    {
        return Instance.teleportInput; 
    }

    public static bool IsJumpInput()
    {
        return Instance.jumpInput;
    }
}
