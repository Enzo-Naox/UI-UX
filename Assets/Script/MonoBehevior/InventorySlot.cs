using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public InventoryManager inventoryManager;
    public Items[] itemsToPickup;
    
    public void OnDrop(PointerEventData eventData)
    {
        InventoryItem draggedItem = eventData.pointerDrag.GetComponent<InventoryItem>();
        
        if (draggedItem != null)
        {
            if (transform.childCount == 0)
            {
                // Déplacer l'objet dans un slot vide
                draggedItem.onDrag = true;
                draggedItem.parentAfterDrag = transform;
            }
            else
            {
                // Échanger les objets si le slot n'est pas vide
                draggedItem.onDrag = false;
                draggedItem.SwapWith(transform);
            }
        }
    }
}

