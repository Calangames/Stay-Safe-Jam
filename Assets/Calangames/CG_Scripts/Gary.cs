using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gary : MonoBehaviour
{
    public float playerSpeed;
    public float runSpeed;
    public float jumpForce;
    public float gravityScale;

    private CharacterController characterController;

    private Vector3 moveDirection;
    private Vector2 position;
    private bool headTowardsCenter, linked = true, grounded;
    
    void Start ()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal") * playerSpeed, moveDirection.y, Input.GetAxis("Vertical") * playerSpeed);
                
        if (grounded)
        {
            gameObject.layer = 9; //GaryBodyOnGround
            //moveDirection.y = 0f;
            if (Input.GetButtonDown("Jump"))
            {
                grounded = false;
                moveDirection.y = jumpForce;
            }
        }
        else
        {
            gameObject.layer = 10; //GaryBodyOnAir
        }
        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
    }

    void LateUpdate()
    {
        position = new Vector2(transform.position.x, transform.position.z);
        float distance = Vector2.Distance(position, Crowd.instance.Center);
        linked = linked & distance <= Crowd.instance.maxDistance;
        headTowardsCenter = distance > Crowd.instance.minDistance && !linked;

        if (headTowardsCenter)
        {
            Vector2 centerDirection = new Vector2(Crowd.instance.Center.x - transform.position.x, Crowd.instance.Center.y - transform.position.z);
            centerDirection = centerDirection.normalized;
            moveDirection = new Vector3(centerDirection.x * runSpeed, moveDirection.y, centerDirection.y * runSpeed);
        }
        characterController.Move(moveDirection * Time.deltaTime);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        grounded = hit.normal.y > 0 && !hit.gameObject.CompareTag("Gary");
    }

    void OnTriggerEnter(Collider other)
    {
        linked = Vector2.Distance(position, Crowd.instance.Center) <= Crowd.instance.maxDistance;
    }
}
