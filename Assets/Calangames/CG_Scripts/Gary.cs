using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gary : MonoBehaviour
{
    public float playerSpeed;
    public float runSpeed;
    public float jumpForce;
    public float gravityScale;
    public GameObject broomPrefab;

    private CharacterController characterController;
    private Animator animator;

    private Vector3 moveDirection;
    private Vector2 position;
    private bool headTowardsCenter, linked = true, grounded, followingCrowd, dead;
    private float cameraDirection;

    void Start ()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (dead)
        {
            return;
        }

        if (followingCrowd)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Vector3 heightOffset = Vector3.up * 1.5f;
                Instantiate(broomPrefab, transform.position + heightOffset + transform.forward, Quaternion.Euler(animator.transform.eulerAngles));
            }
            float movement = new Vector3(moveDirection.x, moveDirection.z).magnitude;
            animator.SetBool("moving", movement != 0f);
            float y = moveDirection.y;
            moveDirection = new Vector3(Input.GetAxis("Horizontal") * playerSpeed, 0f, Input.GetAxis("Vertical") * playerSpeed);

            cameraDirection = Crowd.instance.cameraPivot.eulerAngles.y;

            moveDirection = Quaternion.AngleAxis(cameraDirection, Vector3.up) * moveDirection;
            moveDirection.y = y;
            if (characterController.isGrounded)
            {
                moveDirection.y = 0f;
            }
            if (grounded)
            {
                gameObject.layer = 9; //GaryBodyOnGround
                if (Input.GetButtonDown("Jump"))
                {
                    grounded = false;
                    moveDirection.y = jumpForce;
                    animator.SetTrigger("jump");
                }
            }
            else
            {
                gameObject.layer = 10; //GaryBodyOnAir
            }
        }

        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
    }

    void LateUpdate()
    {
        if (dead)
        {
            return;
        }
        if (followingCrowd)
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
        }
        characterController.Move(moveDirection * Time.deltaTime);
        Vector3 lookDirection = new Vector3(moveDirection.x, 0f, moveDirection.z) + transform.position;
        animator.transform.LookAt(lookDirection);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (dead)
        {
            return;
        }
        bool notHuggingWall = hit.normal.x > -0.2f && hit.normal.x < 0.2f && hit.normal.z > -0.2f && hit.normal.z < 0.2f;
        grounded = notHuggingWall && hit.normal.y > 0 && !hit.gameObject.CompareTag("Gary");
    }

    void OnTriggerEnter(Collider other)
    {
        if (dead)
        {
            return;
        }
        float distance = Vector2.Distance(position, Crowd.instance.Center);
        linked = distance <= Crowd.instance.maxDistance;
        if (other.CompareTag("Crowd"))
        {
            Crowd.instance.CrowdList.Add(transform);
            followingCrowd = true;
        }
        else if (other.CompareTag("Enemy"))
        {
            dead = true;
            Crowd.instance.CrowdList.Remove(transform);
            animator.SetTrigger("death");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (dead)
        {
            return;
        }
        if (followingCrowd && other.CompareTag("Crowd") && Crowd.instance.CrowdList.Count > 1)
        {
            Crowd.instance.CrowdList.Remove(transform);
        }
    }
}
