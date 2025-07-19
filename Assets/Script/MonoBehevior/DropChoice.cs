using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DropChoice : MonoBehaviour
{
    public ItemsContainer inventory;
    private string items;
    public DropItems dropItems;
    private Canvas dropChoice;

    private Transform GetContentChild(Transform parent)
    {
        // Recherche d'un enfant nommé "Content" dans l'objet ciblé
        foreach (Transform child in parent)
        {
            if (child.name == "Content")
            {
                return child;
            }
        }
        return null;
    }

    public void DropRightHand()
    {
        if (inventory.Slots[1].item != null)
        {
            items = inventory.Slots[1].item.name;

            // Création d'un GameObject avec le nom de l'item
            GameObject newItem = new GameObject(items);

            // Vérifier si un objet est ciblé par le raycast dans DropItems
            if (dropItems.targetedObject != null)
            {
                // Recherche de l'enfant "Content" dans l'objet ciblé
                Transform content = GetContentChild(dropItems.targetedObject.transform);
                if (content != null)
                {
                    newItem.transform.SetParent(content); // Définir comme enfant de Content
                }
            }
            inventory.Slots[1].item = null;
        }
        else
        {
            Debug.Log("Pas d'aliments à déposer");
        }
    }

    public void DropLeftHand()
    {
        if (inventory.Slots[0].item != null)
        {
            items = inventory.Slots[0].item.name;

            // Création d'un GameObject avec le nom de l'item
            GameObject newItem = new GameObject(items);

            // Vérifier si un objet est ciblé par le raycast dans DropItems
            if (dropItems.targetedObject != null)
            {
                // Recherche de l'enfant "Content" dans l'objet ciblé
                Transform content = GetContentChild(dropItems.targetedObject.transform);
                if (content != null)
                {
                    newItem.transform.SetParent(content); // Définir comme enfant de Content
                }
            }
            inventory.Slots[0].item = null;
        }
        else
        {
            Debug.Log("Pas d'aliments à déposer");
        }
    }
}
