using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakController : MonoBehaviour
{
    [Header("Input KeyCodes")]
    private KeyCode keycodeRun = KeyCode.LeftShift;
    private KeyCode keycodeJump = KeyCode.Space;
    private CharacterController characterController;
    private RotateToMoues rotateToMouse;
    private WakMove wakmove;
    private WakStatus status;
    
    static public bool overlap = true;

    //0925
    public GameObject WakHead;
    [SerializeField] private GameObject system;
    private CameraCon cameraCon;

    [SerializeField] Texture2D blackcursor;
    [SerializeField] GameObject cursorUI;

    private void Awake()
    {
        rotateToMouse = GetComponent<RotateToMoues>();
        wakmove = GetComponent<WakMove>();
        status = GetComponent<WakStatus>();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        cursorUI.SetActive(false);
        overlap = true;

        cameraCon = system.GetComponent<CameraCon>();
    }

    static public void NoneCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        overlap = true;
    }

    void Update()
    {
        if ((cameraCon.nowCam == CameraCon.CamType.messi) && overlap)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            overlap = false;
        }
        else if ((cameraCon.nowCam == CameraCon.CamType.wak) && !overlap)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            cursorUI.SetActive(true);
            overlap = true;
        }
        else if ((cameraCon.nowCam == CameraCon.CamType.interactionCam) && overlap)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            cursorUI.SetActive(false);
            overlap = false;
        }

        if (cameraCon.nowCam == CameraCon.CamType.wak)
        {
            UpdateRotate();
            UpdateJump();
            //if (StopMove.stopMove == false) UpdateMove();
            UpdateMove();
        }
    }

    private void UpdateRotate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        rotateToMouse.UpdateRotate(mouseX, mouseY);
    }

    private void UpdateMove()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        if (x != 0 || z != 0)
        {
            bool isRun = false;

            if (z > 0) isRun = Input.GetKey(keycodeRun);
            wakmove.MoveSpeed = isRun == true ? status.RunSpeed : status.WalkSpeed;
        }

        wakmove.MoveTo(new Vector3(x, 0, z));
    }


    private void UpdateJump()
    {
        if (Input.GetKeyDown(keycodeJump))
        {
            wakmove.Jump();
        }
    }
}
