using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{

    public Image image;
    public Color selectedColor, notSelectedColor;

    private void Awake()
    {
        Deselect();
    }

    public void Select()
    {
        image.color = selectedColor;
    }

    public void Deselect()
    {
        image.color = notSelectedColor;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();

            // Check if the item is being moved to a different slot.
            if (inventoryItem != null && inventoryItem.parentAfterDrag != transform)
            {
                // Remove the item from its current parent (if any).
                Transform currentParent = inventoryItem.transform.parent;
                if (currentParent != null)
                {
                    InventorySlot currentSlot = currentParent.GetComponent<InventorySlot>();
                    if (currentSlot != null)
                    {
                        currentSlot.Deselect(); // Deselect the current slot if needed
                    }
                    inventoryItem.transform.SetParent(null); // Unparent the item
                }

                // Set the new parent (the current slot).
                inventoryItem.parentAfterDrag = transform;
                inventoryItem.transform.SetParent(transform);
            }
        }
    }

}
