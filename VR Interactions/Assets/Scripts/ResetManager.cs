using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class ResetManager : MonoBehaviour
{
    [SteamVR_DefaultAction("Squeeze")]
    public SteamVR_Action_Single squeezeAction;

    private Hand hoverHand;

    void Update()
    {
        float triggerValue = squeezeAction.GetAxis(SteamVR_Input_Sources.Any);

        if (triggerValue >= 1f && hoverHand != null)
        {
            ResetScene();
        }
    }

    void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            hoverHand = other.gameObject.GetComponent<Hand>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            if (hoverHand != null)
            {
                hoverHand = null;
            }
        }
    }
}
