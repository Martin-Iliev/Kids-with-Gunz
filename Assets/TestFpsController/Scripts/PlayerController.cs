using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
   
    private CharacterController character;

    [Header("Movement Speed")]
    [SerializeField] public float _MovementSpeed = 3f;
    [Header("Gravity")]
    [SerializeField] private float _stickForce = -9.8f;
    [Header("Jump Force")]
    [SerializeField] private float _JumpHeight = 30f;
    [SerializeField] private float itemPickupDistance;
    Vector3 velocity;
    bool isGrounded;

    [SerializeField]
    private float _gravityMultiplier = 12.5f;

    private Vector3 _moveDir = Vector3.zero;
    public Camera head;
    Transform attachedObject = null;
    public float attachedDistance = 0f;
    public float throwForce = 1f;
    private float walkingMultiplier = 1f;

    public AudioSource walk;
    public Object startText;

    private bool cursorVisible = false;

    [Header("DishWashGame")]
    [SerializeField]
    public bool DishGameStart;
    public Camera playerCam;
    public Camera dishCam;
    public GameObject dishAssets;

    private void OnEnable()
    {
        character = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        cursorVisible = false;
        walk.Play();
        walk.Pause();
    }


    // Update is called once per frame
    void Update()
    {
        if (DishGameStart)
        {
            playerCam.enabled = false;
            dishCam.enabled = true;
            DishGameStart = false;
            dishAssets.SetActive(true);
        }
        if (Input.GetAxis("Vertical") != 0f || Input.GetAxis("Horizontal") != 0f)
        {
            walk.UnPause();
        }
        else
        {
            walk.Pause();
        }
        // Create local variable and initialize it
        // Multiple x and z with the movementspeed
        Vector3 desiredMove = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
        if (Input.GetKey("left shift"))
        {
            walkingMultiplier = 1.5f;
        }
        else
        {
            walkingMultiplier = 1f;
        }
        _moveDir.x = (desiredMove.x * _MovementSpeed) * walkingMultiplier;
        _moveDir.z = (desiredMove.z * _MovementSpeed) * walkingMultiplier;

        // If the spacebar is pressed AND we are on the ground
        // set the y pos with the jump amount
        if (Input.GetKeyDown("space") && character.isGrounded)
        {
            _moveDir.y = _JumpHeight;
        }

        // if the character is not grounded
        // Apply gravity
        if (!character.isGrounded)
        {
            _moveDir.y += _stickForce * _gravityMultiplier * Time.deltaTime;
        }

        character.Move(_moveDir * Time.deltaTime);

        //cursor
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!cursorVisible) 
            {
                cursorVisible = true;
                //Cursor.lockState = CursorLockMode.None;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (cursorVisible)
            {
                //Cursor.lockState = CursorLockMode.Locked;
                //cursorVisible = false;
            }
        }

        // picking up objects
        RaycastHit hit;
        bool cast = Physics.Raycast(head.transform.position, head.transform.forward, out hit, itemPickupDistance);

        if (Input.GetMouseButtonDown(0))
        {
            if (attachedObject != null)
            {
                attachedObject.SetParent(null);

                if (attachedObject.GetComponent<Rigidbody>() != null)
                    attachedObject.GetComponent<Rigidbody>().isKinematic = false;
                if (attachedObject.GetComponent<Collider>() != null)
                    attachedObject.GetComponent<Collider>().enabled = true;

                attachedObject.GetComponent<Rigidbody>().AddForce(head.transform.forward * throwForce, ForceMode.Impulse);

                attachedObject = null;
            }
            else
            {
                if (cast)
                {
                    if (hit.transform.CompareTag("pickable"))
                    {
                        attachedObject = hit.transform;
                        attachedObject.SetParent(transform);

                        if (attachedObject.GetComponent<Rigidbody>() != null)
                            attachedObject.GetComponent<Rigidbody>().isKinematic = true;
                        if (attachedObject.GetComponent<Collider>() != null)
                            attachedObject.GetComponent<Collider>().enabled = true;
                    }
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (attachedObject != null)
            {
                attachedObject.SetParent(null);

                if (attachedObject.GetComponent<Rigidbody>() != null)
                    attachedObject.GetComponent<Rigidbody>().isKinematic = false;
                if (attachedObject.GetComponent<Collider>() != null)
                    attachedObject.GetComponent<Collider>().enabled = true;

                attachedObject = null;
            }
        }
    }
    private void LateUpdate()
    {
        if (attachedObject != null)
        {
            int mask = 1 << 3;
            int notmask = ~mask;
            Debug.Log(notmask);
            RaycastHit hit2;
            bool cast2 = Physics.Raycast(head.transform.position, head.transform.forward, out hit2, attachedDistance , notmask);
            if(cast2)
            {
                attachedObject.position = head.transform.position + head.transform.forward * hit2.distance;
            }
            else
            {
                attachedObject.position = head.transform.position + head.transform.forward * attachedDistance;
            }
            attachedObject.Rotate(transform.right * Input.mouseScrollDelta.y * 15f, Space.World);
        }
    }
}
