using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class DualHand : MonoBehaviour
{
    private Interactable interactable;
    private Hand hand;
    private Transform secondHand;

    void Start()
    {
        interactable = GetComponent<Interactable>();
    }

    private void OnAttachedToHand(Hand attachedHand)
    {
        hand = attachedHand;
        secondHand = hand.otherHand.transform;
        hand.otherHand.hapticAction = null;
    }

    private void OnDetachedFromHand(Hand hand)
    {
        hand.otherHand.hapticAction = SteamVR_Input.GetAction<SteamVR_Action_Vibration>("Haptic");
    }

    void FixedUpdate()
    {
        if (interactable.attachedToHand)
        {
            interactable.transform.position = hand.transform.position;
            interactable.transform.LookAt(secondHand);
        }
    }
}
