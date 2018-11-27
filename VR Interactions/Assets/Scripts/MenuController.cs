using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class MenuController : MonoBehaviour
{
    public GameObject leftHandMenu;
    public GameObject rightHandMenu;

    public GameObject leftLaser;
    public GameObject rightLaser;

    [SteamVR_DefaultAction("Menu")]
    public SteamVR_Action_Boolean menuAction;

    private bool menuLeftDown;
    private bool menuRightDown;

    void Update()
    {
        menuLeftDown = menuAction.GetStateDown(SteamVR_Input_Sources.LeftHand);
        menuRightDown = menuAction.GetStateDown(SteamVR_Input_Sources.RightHand);

        // LEFT
        if (menuLeftDown && !leftHandMenu.activeSelf)
        {
            leftHandMenu.SetActive(true);
            leftLaser.SetActive(true);
        }
        else if (menuLeftDown && leftHandMenu.activeSelf)
        {
            leftHandMenu.SetActive(false);
            leftLaser.SetActive(false);
        }

        // RIGHT
        if (menuRightDown && !rightHandMenu.activeSelf)
        {
            rightHandMenu.SetActive(true);
            rightLaser.SetActive(true);
        }
        else if (menuRightDown && rightHandMenu.activeSelf)
        {
            rightHandMenu.SetActive(false);
            rightLaser.SetActive(false);
        }
    }
}
