using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement_controller : MonoBehaviour
{

    static public bool UI_active = false;
    private Vector3 playerMovementInput;
    private Vector2 playerMouseInput;

    private Transform playerCamera;
    private Rigidbody playerRB;
    [SerializeField] private float speed;
    [SerializeField] private float sensitivityX;
    [SerializeField] private float sensitivityY;
    private float xRot;
    private Vector3 moveVector;

    // Start is called before the first frame update
    void Start()
    {
        UI_active = false;
        playerCamera = transform.GetChild(0);
        Cursor.lockState = CursorLockMode.Locked;
        playerRB = GetComponent<Rigidbody>();
        GetComponent<CapsuleCollider>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!UI_active)
        {
            Cursor.lockState = CursorLockMode.Locked;
            playerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            playerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            MovePlayer();
            MoveCamera();
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            playerRB.velocity = new Vector3(0f, 0f, 0f);
        }
    }
    private void MovePlayer()
    {
        moveVector = transform.TransformDirection(playerMovementInput) * speed;
        playerRB.velocity = new Vector3(moveVector.x, 0f, moveVector.z);
    }


    private void MoveCamera()
    {
        xRot -= playerMouseInput.y * sensitivityY;
        transform.Rotate(0f, playerMouseInput.x * sensitivityX, 0f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
        transform.position = new Vector3(transform.position.x, 1.65f, transform.position.z);
    }
    public void setUIactive()
    {
        UI_active = true;
    }
}
