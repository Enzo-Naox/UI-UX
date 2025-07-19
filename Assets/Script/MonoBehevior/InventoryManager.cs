using System;
using TMPro;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// ReSharper disable once CheckNamespace
public class InventoryManager : MonoBehaviour
{ 
    public List<InventorySlot> inventorySlots = new List<InventorySlot>();
    public GameObject inventorySlotPrefab;
    public GameObject inventoryItemPrefab;
    public Canvas canvaItem;
    public GameObject canvaSlot1;
    public GameObject canvaSlot2;
    public GameObject canvaSlot3;
    public TMP_Text hudDescItem;

    private void Awake()
    {
            inventoryItemPrefab.GetComponent<InventoryItem>().hudDesc = hudDescItem;
    }

    public void AddItem(Items item)
    {
        // Vérifier s'il y a un slot disponible pour le nouvel objet
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return;
            }
        }
    }
    void SpawnNewItem(Items item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.mainCanvas = canvaItem;
        inventoryItem.Initialiseitem(item);
    }
    
    public void SpawnNewSlot1()
    {
        // Instancier un nouveau slot dans le Canvas
        GameObject newSlot1 = Instantiate(inventorySlotPrefab, canvaSlot1.transform);
        // Récupérer le composant InventorySlot du nouveau slot
        InventorySlot newInventorySlot1 = newSlot1.GetComponent<InventorySlot>();
        inventorySlots.Add(newInventorySlot1);
    }

    public void SpawnNewSlot2()
    {
        GameObject newSlot2 = Instantiate(inventorySlotPrefab, canvaSlot2.transform);
        // Récupérer le composant InventorySlot du nouveau slot
        InventorySlot newInventorySlot2 = newSlot2.GetComponent<InventorySlot>();
        inventorySlots.Add(newInventorySlot2);
    }
    
    public void SpawnNewSlot3()
    {
        GameObject newSlot3 = Instantiate(inventorySlotPrefab, canvaSlot3.transform);
        // Récupérer le composant InventorySlot du nouveau slot
        InventorySlot newInventorySlot3 = newSlot3.GetComponent<InventorySlot>();
        inventorySlots.Add(newInventorySlot3);
    }
    
    public void ClearInventory()
    {
        // Parcourir chaque slot de l'inventaire
        foreach (InventorySlot slot in inventorySlots)
        {
            // Détruire tous les objets enfants dans chaque slot
            foreach (Transform child in slot.transform)
            {
                Destroy(child.gameObject);
            }
            foreach (Transform child in canvaSlot1.transform)
            {
                Destroy(child.gameObject);
            }
            foreach (Transform child in canvaSlot2.transform)
            {
                Destroy(child.gameObject);
            }
            foreach (Transform child in canvaSlot3.transform)
            {
                Destroy(child.gameObject);
            }
        }
        // Vider la liste des slots
        inventorySlots.Clear();
        
    }

}
