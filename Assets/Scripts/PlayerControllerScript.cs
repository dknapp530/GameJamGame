using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    #region MovementInputs
    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private float speed;
    #endregion
    #region CameraInputs
    [SerializeField]
    private float sensitivity;

    [SerializeField]
    private Transform playerBody;
    [SerializeField]
    private float xRotation = 0f;
    [SerializeField]
    private Transform cameraTransform;
    #endregion
    #region Weapon
    [SerializeField]
    private Weapon weapon;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        //Locking Cursor in the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMouelook();
        if(Input.anyKey)
        {
            HandleInput();
        }

        if (Input.GetKeyDown("q"))
        {
            GameManager.Instance.StartWave();
        }

        if (Input.GetMouseButtonDown(0))
        {
            weapon.Fire();
        }
    }

    private void HandleMouelook()
    {
        float mouseY = Input.GetAxis("Mouse Y") * this.sensitivity * Time.deltaTime;
        float mouseX = Input.GetAxis("Mouse X") * this.sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

    }

    private void HandleInput()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = playerBody.right * moveX + playerBody.forward * moveZ;
        controller.Move(move * speed * Time.deltaTime);
    }

    public Quaternion GetPlayerLookRotation()
    {
        return playerBody.rotation;
    }
    public Vector3 GetPlayerPosition()
    {
        return playerBody.position;
    }
    public Vector3 GetPlayerForward()
    {
        return playerBody.forward;
    }
}
