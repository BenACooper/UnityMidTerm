using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float sprintMultiplier;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private bool invertMouse;
    [SerializeField] private Transform cameraTransform;

    [Header("Ground Checker")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float groundCheckDistance;

    [Header("Shoot")]
    [SerializeField] private Rigidbody bulletPrefab;
    [SerializeField] private Rigidbody rocketPrefab;
    [SerializeField] private float shootForceBullet;
    [SerializeField] private float shootForceRocket;
    [SerializeField] private Transform shootPoint;

    [Header("Interact")]
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask interactionLayer;
    [SerializeField] private float interactionDistance;

    [Header("Pick And Drop")]
    [SerializeField] private Transform attachPoint;
    [SerializeField] private float pickupDistance;
    [SerializeField] private LayerMask pickupLayer;

    private CharacterController characterController;

    private float horizontal, vertical, mouseX, mouseY, camXRotation;
    private float moveMultiplier = 1;
    private Vector3 playerVelocity;
    private bool isGrounded;

    //Ray cast
    private RaycastHit raycastHit;
    private ISelectable selectable;

    //Pick and Drop
    private bool isPicked = false;
    private IPickable pickable;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        GroundCheck();
        MovePlayer();
        JumpCheck();
        RotatePlayer();

        shootBullet();
        shootRocket();

        Interact();
        PickAndDrop();
    }

    void GetInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        moveMultiplier = Input.GetButton("Sprint") ? sprintMultiplier : 1;
    }

    void MovePlayer()
    {
        characterController.Move((transform.forward * vertical + transform.right * horizontal) * moveSpeed * Time.deltaTime * moveMultiplier);

        //Ground check
        if(isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 2f;
        }

        playerVelocity.y += gravity * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundLayerMask);
    }

    void JumpCheck()
    {
        if (Input.GetButtonDown("Jump"))
        {
            playerVelocity.y = jumpForce;
        }
    }

    void RotatePlayer()
    {
        transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime * mouseX);

        camXRotation += Time.deltaTime * mouseY * turnSpeed * (invertMouse ? 1 : -1);
        camXRotation = Mathf.Clamp(camXRotation, -35f, 35f);

        cameraTransform.localRotation = Quaternion.Euler(camXRotation, 0, 0);
    }

    void shootBullet()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Rigidbody bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            bullet.AddForce(shootPoint.forward * shootForceBullet);
            Destroy(bullet.gameObject, 5.0f);
        }
    }
    
    void shootRocket()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            // Offset the spawn position of rocket slightly forward so it doesn't collide with player OR make rocket not interact with player's layer?
            //Vector3 spawnPosition = shootPoint.position + shootPoint.forward * 0.5f;

            Rigidbody rocket = Instantiate(rocketPrefab, shootPoint.position, shootPoint.rotation);
            rocket.AddForce(shootPoint.forward * shootForceRocket);
            Destroy(rocket.gameObject, 5.0f);
        }
    }

    void Interact()
    {
        //Cast a ray.
        //Draw the ray from a point.

        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
        if(Physics.Raycast(ray, out raycastHit, interactionDistance, interactionLayer))
        {
            selectable = raycastHit.transform.GetComponent<ISelectable>();

            if(selectable != null)
            {
                selectable.OnHoverEnter();

                if (Input.GetKeyDown(KeyCode.E))
                {
                    selectable.OnSelect();
                }
            }
        }

        if(raycastHit.transform == null && selectable != null){
            selectable.OnHoverExit();
            selectable = null;
        }
    }

    void PickAndDrop()
    {
        //Cast a ray.
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out raycastHit, pickupDistance, pickupLayer))
        {
            if (Input.GetKeyDown(KeyCode.E) && !isPicked)
            {
                pickable = raycastHit.transform.GetComponent<IPickable>();

                if (pickable == null)
                    return;

                pickable.OnPicked(attachPoint);
                isPicked = true;
                return;
            }
        }

        if(Input.GetKeyDown(KeyCode.E) && isPicked && pickable != null)
        {
            pickable.OnDropped();
            isPicked = false;
        }
    }

}
