using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HoldItem : MonoBehaviour
{
    public GameObject heldItem = null;
    public GameObject nearItem = null;
    public PlaceObject nearHolder = null;
    //private Animator heroAnim;
    private Transform objectSpot;
    private TextMeshProUGUI uiTextBox;
    private void Awake()
    {
        //heroAnim = GetComponent<Animator>();
        objectSpot = this.transform.Find("mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/mixamorig:Spine2/mixamorig:RightShoulder/mixamorig:RightArm/mixamorig:RightForeArm/mixamorig:RightHand/ObjectSpot");
        if (heldItem != null)
            ReceiveItem(heldItem, true);

        //uiTextBox = GameObject.Find("Inventory Text").GetComponent<TextMeshProUGUI>();


    }

    public void ReceiveItem(GameObject newItem, bool force = false)
    {
        ReceiveItem(newItem.GetComponent<Collider>(), force);
    }

    private void ReceiveItem(Collider itemCollider, bool force = false)
    {
        if (force || heldItem == null)
        {

            Rigidbody rb = itemCollider.gameObject.GetComponent<Rigidbody>();
            HoldPosition holdPos = itemCollider.gameObject.GetComponent<HoldPosition>();
            if (rb == null)
                Debug.Log("Item has no rigidbody.");
            //Physics.IgnoreCollision(itemCollider, gameObject.GetComponent<Collider>());
            //Physics.IgnoreCollision(itemCollider, gameObject.GetComponent<SphereCollider>());
            Physics.IgnoreLayerCollision(8, 9, true);
            rb.isKinematic = true;
            rb.transform.SetParent(objectSpot);
            rb.transform.localPosition = holdPos.localPosition;
            rb.transform.localRotation = holdPos.localRotation;
            //rb.gameObject.transform.localPosition = Vector3.zero;
            heldItem = itemCollider.gameObject;
        }
    }

    private void SignalNearItem(GameObject item)
    {
        // Signal to player that we are near an item that can be picked up
        // Get the text box that shows what item you're over
        nearItem = item;
        //uiTextBox.text = "Press (A) or [Ctrl] to  Pick Up " + item.name;
    }

    private void SignalNearInteract(GameObject interactObject)
    {
        // Signal to player that we are near an item that can be picked up
        // Get the text box that shows what item you're over
        nearItem = interactObject.gameObject;
        uiTextBox.text = "Press (A) or [Ctrl] to Use " + interactObject.name.ToString();
    }

    private void SignalNearItem(Collider itemCollider)
    {
        SignalNearItem(itemCollider.gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Holdable Item")
            SignalNearItem(other);
        else if (other.tag == "Interact")
        {
            SignalNearInteract(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Holdable Item" || other.tag == "Interact")
            if (nearItem != null && other.gameObject == nearItem)
            {
                nearItem = null;
                //uiTextBox.text = "";
            }
    }
}
