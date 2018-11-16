using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerMovement : MonoBehaviour
{
    public SteamVR_Action_Vector2 touchPadAction;
    public SteamVR_Action_Boolean touchPadDownAction;

    public Transform playerBody;
    public Transform playerCamera;
    public float speed = 5f;
    public float sensitivityX = 1.5F;

    private Vector2 touchpadLeft;
    private Vector2 touchpadRight;
    private bool touchpadDown;

    private Player player;
    private Vector3 playerPos;

    void Start()
    {
        player = Player.instance;
    }

    void Update()
    {
        touchpadLeft = touchPadAction.GetAxis(SteamVR_Input_Sources.LeftHand);
        touchpadRight = touchPadAction.GetAxis(SteamVR_Input_Sources.RightHand);
        touchpadDown = touchPadDownAction.GetState(SteamVR_Input_Sources.Any);

        playerBody.transform.position = new Vector3(playerCamera.transform.position.x, playerBody.transform.position.y, playerCamera.transform.position.z);

        if (touchpadDown)
        {
            // Handle movement via touchpad
            if (touchpadLeft.y > 0.2f || touchpadLeft.y < -0.2f)
            {
                // Move Forward
                player.transform.position += player.transform.forward * Time.deltaTime * (touchpadLeft.y * speed);
            }

            // handle rotation via touchpad
            if (touchpadLeft.x > 0.3f)
            {
                player.transform.position += player.transform.right * Time.deltaTime * (touchpadLeft.x * speed);
               
            }
            else if( touchpadLeft.x < -0.3f)
            {
                player.transform.position -= -player.transform.right * Time.deltaTime * (touchpadLeft.x * speed);
            }

            if (touchpadRight.x > 0.3f || touchpadRight.x < -0.3f)
            {
                player.transform.Rotate(0, touchpadRight.x * sensitivityX, 0);
            }
        }
    }
}
