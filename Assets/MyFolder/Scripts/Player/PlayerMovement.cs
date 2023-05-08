using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField]private float speed, jumpSpeed, gravity, turnSpeed;
    private Vector3 moveDir = Vector3.zero;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!controller.enabled)
        {
            controller.enabled = true;
            return;
        }
        moveDir.x = Input.GetAxis("Horizontal") * speed;
        moveDir.z = Input.GetAxis("Vertical") * speed;
        var movement = new Vector3(moveDir.x, 0, moveDir.z);

        if (controller.isGrounded && Input.GetButtonDown("Jump"))
            moveDir.y = jumpSpeed;


        moveDir.y -= gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);
        animator.SetFloat("Speed", movement.magnitude);

        //will only rotate if there is a magnitude here
        if(movement.magnitude > 0)
        {
            Quaternion newDir = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, newDir, Time.deltaTime * turnSpeed);
        }
    }

    public void ResetSpeed()
    {
        speed = 5;
    }
}
