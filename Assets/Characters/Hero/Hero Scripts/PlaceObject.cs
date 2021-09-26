using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlaceObject : MonoBehaviour
{
    public string correctTag = "Tablet";
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && other.GetType() == typeof(CapsuleCollider))
        {
            HoldItem heldItem = other.GetComponent<HoldItem>();
            if (heldItem.heldItem != null && heldItem.heldItem.tag == "Holdable Item")
            {
                if (heldItem.nearHolder == null)
                    heldItem.nearHolder = this;

                // Only show placement prompt if there is no tablet already in this slot (even an incorrect one)
                if (heldItem.nearHolder.gameObject.transform.Find("Slot").gameObject.transform.childCount == 0)
                {
                    // Signal you can place the tablet
                    TextMeshProUGUI uiTextBox = GameObject.Find("Inventory Text").GetComponent<TextMeshProUGUI>();
                    uiTextBox.text = "Press (A) or [Ctrl] to Place Tablet";
                }
                else
                    // If the slot isn't avaiable, set it back to null
                    heldItem.nearHolder = null;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && other.GetType() == typeof(CapsuleCollider))
        {
            HoldItem heldItem = other.GetComponent<HoldItem>();
            if (heldItem.heldItem != null && heldItem.heldItem.tag == "Holdable Item")
            {
                // End signal
                TextMeshProUGUI uiTextBox = GameObject.Find("Inventory Text").GetComponent<TextMeshProUGUI>();
                uiTextBox.text = "";
                heldItem.nearHolder = null;
            }
        }
    }
}


