using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    public SteamVR_Action_Vector2 touchpadAction;
    public SteamVR_Action_Boolean touchpadButtonAction;

    public Transform head;

    public float moveSpeed = 2.5f;
    public float turnSpeed = 100f;
    public float jumpForce = 1f;

    private Vector2 touchpadLeft;
    private Vector2 touchpadRight;

    private bool touchpadLeftDown;
    private bool touchpadRightDown;

    public bool isGrounded;
    public bool comfortTurning;
    public float comfortTurnDegrees = 30f;

    private Player player;
    private Rigidbody rb;

    public PhysicsResolver resolver;

    void Awake()
    {
        player = Player.instance;
        rb = GetComponent<Rigidbody>();
    }

    void OnGUI()
    {
        // LEFT
        if (player.leftHand != null)
        {
            GUILayout.Label(string.Format("Left X: {0:F2}, {1:F2}", touchpadLeft.x, touchpadLeft.y));
        }

        // RIGHT
        if (player.rightHand != null)
        {
            GUILayout.Label(string.Format("Right X: {0:F2}, {1:F2}", touchpadRight.x, touchpadRight.y));
        }
    }

    void Update()
    {
        if (player == null)
        {
            Debug.Log("Player is null!");
            return;
        }

        touchpadLeft = touchpadAction.GetAxis(SteamVR_Input_Sources.LeftHand);
        touchpadRight = touchpadAction.GetAxis(SteamVR_Input_Sources.RightHand);

        touchpadLeftDown = touchpadButtonAction.GetStateDown(SteamVR_Input_Sources.LeftHand);
        touchpadRightDown = touchpadButtonAction.GetStateDown(SteamVR_Input_Sources.RightHand);

        // LEFT
        if (player.leftHand != null)
        {
            Quaternion orientation = Camera.main.transform.rotation;

            Vector3 moveDirection = orientation * Vector3.forward * touchpadLeft.y + orientation * Vector3.right * touchpadLeft.x;
            Vector3 pos = player.transform.position;
            pos.x += moveDirection.x * moveSpeed * Time.deltaTime;
            pos.z += moveDirection.z * moveSpeed * Time.deltaTime;
            player.transform.position = pos;
        }

        // RIGHT
        if (player.rightHand != null)
        {
            // Jumping
            if (resolver.isGrounded && touchpadRightDown && touchpadRight.y > 0.5f)
            {
                Jump();
            }

            // Rotation
            if (comfortTurning && touchpadRightDown)
            {
                if (touchpadRight.x > 0.3f)
                {
                    transform.RotateAround(head.position, Vector3.up, comfortTurnDegrees);
                }
                else if (touchpadRight.x < -0.3f)
                {
                    transform.RotateAround(head.position, Vector3.up, -comfortTurnDegrees);
                }
            }
            else if (!comfortTurning)
            {
                if (touchpadRight.x > 0.3f)
                {
                    transform.RotateAround(head.position, Vector3.up, turnSpeed * Time.deltaTime);
                }
                else if (touchpadRight.x < -0.3f)
                {
                    transform.RotateAround(head.position, Vector3.up, -turnSpeed * Time.deltaTime);
                }
            }
        }
    }

    void Jump()
    {
        rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
    }
}
