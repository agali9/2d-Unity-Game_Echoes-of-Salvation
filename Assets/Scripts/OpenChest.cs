using System.Collections;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
     GameObject OpenChestText;
     GameObject CloseChestText;
     public GameObject DisablePlayerMovement;
     GameObject MainInventory;

     InventoryManager inventoryManager;

     private bool isChestOpen = false;
     private bool isPlayerNearChest = false;

     private void Start()
     {
         GameObject InventoryManager = GameObject.Find("InventoryManager");

         inventoryManager = InventoryManager.GetComponent<InventoryManager>();

         OpenChestText = GameManager.Instance.OpenChestText;
         CloseChestText = GameManager.Instance.CloseChestText;
         MainInventory = GameManager.Instance.MainInventory;

         CloseChestText.SetActive(isChestOpen);
         OpenChestText.SetActive(false);

         inventoryManager.CheckItemSpawn();
         inventoryManager.SpawnItems();

     }

     private void Update()
     {
         if (isPlayerNearChest && Input.GetKeyDown(KeyCode.Q))
         {
             if (isChestOpen)
             {
                 CloseChest();
             }
             else
             {
                 ChestOpen();
             }
         }
     }

     private void ChestOpen()
     {
         DisablePlayerMovement.GetComponent<PlayerMovement>().enabled = false;
         MainInventory.SetActive(true);
         CloseChestText.SetActive(true);
         OpenChestText.SetActive(false);
         isChestOpen = true;
     }

     private void CloseChest()
     {
         DisablePlayerMovement.GetComponent<PlayerMovement>().enabled = true;
         MainInventory.SetActive(false);
         OpenChestText.SetActive(true);
         CloseChestText.SetActive(false);
         isChestOpen = false;
     }

     private void OnCollisionEnter2D(Collision2D collision)
     {
         if (collision.gameObject.name == "Chest")
         {
             isPlayerNearChest = true;

             if (!isChestOpen)
             {
                 OpenChestText.SetActive(true);
             }
         }
     }

     private void OnCollisionExit2D(Collision2D collision)
     {
         if (collision.gameObject.name == "Chest")
         {
             isPlayerNearChest = false;

             if (!isChestOpen)
             {
                 OpenChestText.SetActive(false);
             }
         }
     }
}
