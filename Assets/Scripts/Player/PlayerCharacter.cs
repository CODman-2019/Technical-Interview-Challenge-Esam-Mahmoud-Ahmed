using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private float              player_Speed;
    [SerializeField] private float              groundDistance;
    [SerializeField] private float              jumpForce;
    [SerializeField] private float              groundCheckRaidius;
    [SerializeField] private LayerMask          groundMask;
    [SerializeField] private float              cooldownTimer;
    [SerializeField] private Animator           animor;
    [SerializeField] private PlayerActions      playerInput;

    private Rigidbody                           rb;
    private Vector3                             originDistance;
    private Vector2                             movement;
    private bool                                check_Grounded;
    private RaycastHit                          groundCheck;

    private InputAction move;
    private InputAction attack;
    private InputAction jump;

    private void Awake()
    {
        playerInput = new PlayerActions();
        rb = GetComponent<Rigidbody>();
        check_Grounded = false;
    }


    private void OnEnable()
    {
        move = playerInput.Character.Move;
        jump = playerInput.Character.Jump;
        attack = playerInput.Character.Attack;

        move.Enable();
        jump.Enable();
        attack.Enable();

        jump.performed += Jump;
        attack.performed += Attack;
    }

    private void OnDisable()
    {
        move.Disable();
        jump.Disable();
        attack.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.gameManager.IsGamePause())
        {
            //MOVEMENT
            movement = move.ReadValue<Vector2>() * player_Speed;
            //player_ForwardMovement = Input.GetAxisRaw("Vertical") * player_Speed;
            //player_SideMovement = Input.GetAxisRaw("Horizontal") * player_Speed;

            rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.y);

            //ground check set up
            originDistance = new Vector3(transform.position.x, transform.position.y - groundDistance, transform.position.z);
            GroundCheck();

        }

    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (check_Grounded)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            check_Grounded = false;
        }
    }

    private void Attack(InputAction.CallbackContext context)
    {
        animor.SetTrigger("Attack");
        StartCoroutine(Resetanim());
    }

    void GroundCheck()
    {
        if (!Physics.SphereCast(originDistance, groundCheckRaidius, Vector3.down, out groundCheck, groundMask))
        {
            //Debug.Log("hiting the ground");
            if (!check_Grounded)
                check_Grounded = true;
        }
        else
        {
            //Debug.Log("not hitting the ground");
            if (check_Grounded)
                check_Grounded = false;
        }
    }

    IEnumerator Resetanim()
    {
        yield return new WaitForSeconds(cooldownTimer);
        animor.ResetTrigger("Attack");
        //Debug.Log("reset complete");
    }
}
