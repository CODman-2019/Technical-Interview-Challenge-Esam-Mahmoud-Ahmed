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
    [SerializeField] private float              dashDistance;
    [SerializeField] private float              groundCheckRaidius;
    [SerializeField] private LayerMask          groundMask;
    [SerializeField] private float              attackCooldown;
    [SerializeField] private float              dashCooldown;

    [SerializeField] private Animator           animor;
    [SerializeField] private PlayerActions      playerInput;

    [SerializeField] private InputAction        cameraRotate;
    [SerializeField] private GameObject         cameraAnchor;

    private Rigidbody                           rb;
    private Vector3                             originDistance;
    private Vector2                             movement;
    private int                                 health;
    private bool                                dashAvail;
    private bool                                check_Grounded;
    private RaycastHit                          groundCheck;
    private float                               cameraRotation;

    private InputAction                         move;
    private InputAction                         attack;
    private InputAction                         jump;
    private InputAction                         dash;


    private void Awake()
    {
        playerInput = new PlayerActions();
        rb = GetComponent<Rigidbody>();
        check_Grounded = false;
        dashAvail = true;
        health = 5;
        cameraRotation = 0;
        UIManager.uI.UpdatePlayerHealthBar(health);
    }

    private void OnEnable()
    {
        move = playerInput.Character.Move;
        jump = playerInput.Character.Jump;
        attack = playerInput.Character.Attack;
        dash = playerInput.Character.Dash;

        move.Enable();
        jump.Enable();
        attack.Enable();
        dash.Enable();
        cameraRotate.Enable();


        jump.performed += Jump;
        attack.performed += Attack;
        dash.performed += Dash;
    }

    private void OnDisable()
    {
        move.Disable();
        jump.Disable();
        attack.Disable();
        dash.Disable();
        cameraRotate.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.gameManager.IsGamePause() && !GameManager.gameManager.IsGameOver())
        {
            //MOVEMENT
            movement = move.ReadValue<Vector2>() * player_Speed;
            //cameraRotation = cameraRotate.ReadValue<float>();

            rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.y);
            //rb.AddRelativeForce(new Vector3(movement.x, rb.velocity.y, movement.y));
            //transform.Rotate(0, cameraRotation, 0);
            //rb.velocity = Vector3.zero;

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
            AudioManager.sound.TriggerSound("Jump");
        }
    }

    private void Attack(InputAction.CallbackContext context)
    {
        animor.SetTrigger("Attack");
        AudioManager.sound.TriggerSound("Attack");
        StartCoroutine(Resetanim());
    }

    private void Dash(InputAction.CallbackContext context)
    {
        if (!GameManager.gameManager.IsGamePause() || !GameManager.gameManager.IsGameOver())
        {
            if(dashAvail)
            {
                float                    xdash;
                float                    ydash;

                if      (movement.x < 0) xdash = -1;
                else if (movement.x > 0) xdash = 1;
                else                     xdash = 0;

                if      (movement.y < 0) ydash = -1;
                else if (movement.y > 0) ydash = 1;
                else                     ydash = 0;
                
                rb.MovePosition(new Vector3(transform.position.x + (xdash * dashDistance), transform.position.y, transform.position.z + (ydash * dashDistance)));
                dashAvail = false;
                AudioManager.sound.TriggerSound("Dash");
                UIManager.uI.ChangeDashIconColor(1);
                StartCoroutine(ResetDash());
            }
        }
    }

    public void TakeDamage()
    {
        health--;
        UIManager.uI.UpdatePlayerHealthBar(health);
        AudioManager.sound.TriggerSound("Hit");
        if (health == 0)
        {
            GameManager.gameManager.GameOver();
        }
    }

    public int GetHealth() => health;

    void GroundCheck()
    {
        if (Physics.SphereCast(originDistance, groundCheckRaidius, Vector3.down, out groundCheck, groundMask))
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

    IEnumerator ResetDash()
    {
        yield return new WaitForSeconds(dashCooldown);
        dashAvail = true;
        UIManager.uI.ChangeDashIconColor(0);
        Debug.Log("dash reset");
    }

    IEnumerator Resetanim()
    {
        yield return new WaitForSeconds(attackCooldown);
        animor.ResetTrigger("Attack");
        //Debug.Log("reset complete");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy_Basic_Attack" || other.gameObject.tag == "Enemy_Finisher_Attack")  TakeDamage();


    }
}
