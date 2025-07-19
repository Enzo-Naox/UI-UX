using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MagicChest : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Items[] itemsToPickup;
    private int randomSlotCount1 = 1;
    private int randomSlotCount2 = 1;
    private int randomSlotCount3 = 1;
    private int randomItemId = 1;

    public void Pickup(int id)
    {
        inventoryManager.AddItem(itemsToPickup[id]);
    }

    public void PickupSlot1()
    {
        randomSlotCount1 = Random.Range(1, 25);
        for (int i = 0; i < randomSlotCount1; i++)
        {
            inventoryManager.SpawnNewSlot1();
        }
    }
    
    public void PickupSlot2()
    {
        randomSlotCount2 = Random.Range(1, 25);
        for (int i = 0; i < randomSlotCount2; i++)
        {
            inventoryManager.SpawnNewSlot2();
        }
    }
    
    public void PickupSlot3()
    {
        randomSlotCount3 = Random.Range(1, 25);
        for (int i = 0; i < randomSlotCount3; i++)
        {
            inventoryManager.SpawnNewSlot3();
        }
    }

    public void RandomItem()
    {
        int totalSlot = randomSlotCount1 + randomSlotCount2 + randomSlotCount3;
        int randomItemCount = Random.Range(1, totalSlot);
        for (int i = 0; i < randomItemCount; i++)
        {
            randomItemId = Random.Range(0, 16);
            Pickup(randomItemId);
        }
    }


}
