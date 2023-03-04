using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class WakMove : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    private Vector3 moveForce;

    private Transform trans;

    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float gravity;
    private float fallDelay = 0f;
  
    public float MoveSpeed
    {
        set => moveSpeed = Mathf.Max(0, value);
        get => moveSpeed;
    }

    private CharacterController characterController;

    private void Awake()
    {
        trans = GetComponent<Transform>();
        characterController = GetComponent<CharacterController>();
        //characterController.height = 0f;
    }

    void Update()
    {
        if (!characterController.isGrounded)
        {
           fallDelay += Time.deltaTime;

            if (fallDelay < 0.2f)
            {
                 moveForce.y += gravity * Time.deltaTime;
            }

            else
            {
                 moveForce.y += gravity * 1.8f * Time.deltaTime;
            }
        }

        else fallDelay = 0f;
        
        characterController.Move(moveForce * Time.deltaTime);
    }

    public void MoveTo(Vector3 direction)
    {
        direction = transform.rotation * new Vector3(direction.x, 0, direction.z);
        direction = direction.normalized;
        moveForce = new Vector3(direction.x * moveSpeed, moveForce.y, direction.z * moveSpeed);
    }

    public void Jump()
    {
       if (characterController.isGrounded)
       {
            moveForce.y = jumpForce;
       }
    }
}
