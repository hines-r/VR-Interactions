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

    private Hand hand;

    void Update()
    {
        if (hand == null) return;

        float triggerValue = squeezeAction.GetAxis(hand.handType);

        if (triggerValue >= 1f)
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
            hand = other.gameObject.GetComponent<Hand>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            if (hand != null)
            {
                hand = null;
            }
        }
    }
}
