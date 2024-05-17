using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryManager : MonoBehaviour
{

    public int maxStackedItems = 4;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    public Item[] itemsToPickup;
    public GameObject Canvas;
    public GameObject inventoryManager;

    public bool isItem8;
    public bool isItem9;
   
    int selectedSlot = -1;

    private void Start()
    {
        
    }

    private void Update()
    {
        if(Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if(isNumber && number > 0 && number < 9)
            {
                ChangeSelectedSlot(number - 1);
            }
        }
    }

    void ChangeSelectedSlot(int newValue)
    {
        if(selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].Deselect();
        }

        inventorySlots[newValue].Select();
        selectedSlot = newValue;
    }

    public bool AddItem(Item item)
    {

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < maxStackedItems && itemInSlot.item.stackable == true)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if(itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }

        return false;
    }

    public void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }

    public Item GetSelectedItem(bool use)
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null)
        {
            Item item = itemInSlot.item;
            if (use == true)
            {
                itemInSlot.count--;
                if (itemInSlot.count <= 0)
                {
                    Destroy(itemInSlot.gameObject);
                }
                else
                    itemInSlot.RefreshCount();
            }
            return item;
        }

        return null;
    }

    public void SpawnItems()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        Debug.Log("Scene Name: " + sceneName);

        InventorySlot slot = inventorySlots[8];
        InventorySlot slot1 = inventorySlots[9];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        InventoryItem itemInSlot1 = slot1.GetComponentInChildren<InventoryItem>();

        if (itemInSlot == null && isItem8 && sceneName == "CabinInterior")
        {
            Debug.Log("Spawning items...");
            SpawnNewItem(itemsToPickup[0], slot);
            for (int i = 0; i < 4; i++)
                AddItem(itemsToPickup[0]);
        }

        if (itemInSlot1 == null && isItem9 && sceneName == "CabinInterior")
        {
            Debug.Log("Spawning items (slot1)...");
            SpawnNewItem(itemsToPickup[1], slot1);
        }
    }

    public void CheckItemSpawn()
    {
        if (HasItemsInSlotsRange(0, 7))
        {
            isItem8 = false;
            isItem9 = false;
            if (HasItemsInSlots(8))
                isItem8 = true;
            if (HasItemsInSlots(9))
                isItem9 = true;
        }
        else if (!HasItemsInSlotsRange(0, 7))
        {
            isItem8 = true;
            isItem9 = true;
        }
    }

    bool HasItemsInSlots(int slotIndex)
    {
        InventorySlot slot = inventorySlots[slotIndex];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        return (itemInSlot != null);
    }

    bool HasItemsInSlotsRange(int startIndex, int endIndex)
    {
        for (int i = startIndex; i <= endIndex; i++)
        {
            if (HasItemsInSlots(i))
            {
                return true;
            }
        }
        return false;
    }


}
