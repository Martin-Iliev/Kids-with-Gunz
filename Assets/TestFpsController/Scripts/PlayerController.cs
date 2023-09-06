using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
   
    private CharacterController character;

    [Header("Movement Speed")]
    [SerializeField] private float _MovementSpeed = 3f;
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

    private void OnEnable()
    {
        character = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
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
            attachedObject.position = head.transform.position + head.transform.forward * attachedDistance;
            attachedObject.Rotate(transform.right * Input.mouseScrollDelta.y * 15f, Space.World);
        }
    }
}
