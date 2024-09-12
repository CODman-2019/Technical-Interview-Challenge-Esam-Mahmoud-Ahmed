using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float      player_Speed;
    [SerializeField] private float      groundDistance;
    [SerializeField] private float      jumpForce;
    [SerializeField] private float      groundCheckRaidius;
    [SerializeField] private LayerMask  groundMask;

    private Rigidbody                   rb;
    private Vector3                     originDistance;
    private float                       player_ForwardMovement;
    private float                       player_SideMovement;
    private bool                        check_Grounded;

    private RaycastHit groundCheck;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player_ForwardMovement = 0;
        player_SideMovement = 0;
        check_Grounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        //MOVEMENT
        player_ForwardMovement = Input.GetAxisRaw("Vertical") * player_Speed;
        player_SideMovement = Input.GetAxisRaw("Horizontal") * player_Speed;

        rb.velocity = new Vector3(player_SideMovement, rb.velocity.y, player_ForwardMovement);

        //ground check set up
        originDistance = new Vector3(transform.position.x, transform.position.y - groundDistance, transform.position.z);
        GroundCheck();

        if (Input.GetButton("Jump"))
        {
            if(check_Grounded)
            {
                rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
                check_Grounded = false;
            }
        }
    }

    void GroundCheck()
    {
        if (!Physics.SphereCast(originDistance, groundCheckRaidius, Vector3.down, out groundCheck, groundMask))
        {
            //Debug.Log("hiting the ground");
            if(!check_Grounded)
            check_Grounded = true;
        }
        else 
        {
            //Debug.Log("not hitting the ground");
            if (check_Grounded)
                check_Grounded = false;
        }
    }
    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.yellow; 
        Gizmos.DrawSphere(originDistance, groundCheckRaidius);
    }
}
