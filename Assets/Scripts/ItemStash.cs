using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class ItemStash : MonoBehaviour
{
    private Interactable interactable;
    [SerializeField]
    private GameObject itemPrefab;
    [EnumFlags]
    [Tooltip("The flags used to attach this object to the hand.")]
    public Hand.AttachmentFlags attachmentFlags = Hand.AttachmentFlags.ParentToHand | Hand.AttachmentFlags.DetachFromOtherHand | Hand.AttachmentFlags.TurnOnKinematic;


    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (interactable.isHovering)
        {
            foreach(var hand in interactable.hoveringHands)
            {
                var grab = hand.GetBestGrabbingType();
                if (grab != GrabTypes.None)
                {
                    var item = Instantiate(itemPrefab, hand.transform.position, hand.transform.rotation);
                    hand.AttachObject(item, grab, attachmentFlags);
                }
            }
        }
    }
}
