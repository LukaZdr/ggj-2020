using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        hand.otherHand.enabled = false;
    }

    private void OnDetachedFromHand(Hand hand)
    {
        hand.otherHand.enabled = true;
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
