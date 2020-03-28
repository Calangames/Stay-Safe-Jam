using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float playerSpeed;
    public float jumpForce;

    public Transform cameraPivot;
    private float cameraDirection;

    public CharacterController playerController;

    private Vector3 moveDirection;
    public float gravityScale;
    
    void Start ()
    {
        playerController = GetComponent<CharacterController>();
    }

    void Update()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal") * playerSpeed, moveDirection.y, Input.GetAxis("Vertical") * playerSpeed);

        cameraDirection = cameraPivot.eulerAngles.y;

        moveDirection = Quaternion.AngleAxis(cameraDirection, Vector3.up) * moveDirection;

        if (playerController.isGrounded)
        {
            moveDirection.y = 0f;
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
            }
        }
        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        playerController.Move(moveDirection * Time.deltaTime);

    }

}
