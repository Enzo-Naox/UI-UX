using System.Collections.Generic;
using UnityEngine;

public class GetContentConteneur : MonoBehaviour
{
    public DropItems dropItems;
    public ItemsContainer inventory;
    private Canvas dropChoice;
    private bool slotFull = false;

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

    public void GetItem()
    {
        if (dropItems.targetedObject != null)
        {
            // Récupère l'objet "Content" ciblé
            Transform content = GetContentChild(dropItems.targetedObject.transform);

            if (content != null)
            {
                // Parcourt les enfants de "Content" et récupère leurs scripts PickableItems
                foreach (Transform child in content)
                {
                    PickableItems pickable = child.GetComponent<PickableItems>();
                    if (pickable != null)
                    {
                        Debug.Log($"Enfant trouvé : {child.name}, Item : {pickable.item.name}");

                        // Ajoute l'item trouvé dans la main droite
                        AddItemToInventory(pickable.item);

                        Canvas[] canvases = dropItems.targetedObject.GetComponentsInChildren<Canvas>(true);
                        dropChoice = System.Array.Find(canvases, canvas => canvas.gameObject.name == "UI_GetContent");

                        if (slotFull)
                        {
                            dropChoice.gameObject.SetActive(false);
                            Destroy(child.gameObject);
                        } 
                        return; // Stoppe après avoir récupéré un item
                    }
                }
                Debug.Log("Aucun item valide trouvé dans Content.");
            }
            else
            {
                Debug.Log("Content introuvable dans l'objet ciblé.");
            }
        }
        else
        {
            Debug.Log("Aucun objet ciblé.");
        }
    }

    public void AddItemToInventory(Items item)
    {
        // Vérifie si l'inventaire est valide
        if (inventory != null && inventory.Slots.Count > 0)
        {
            // Parcourt les slots de l'inventaire pour trouver un slot vide
            foreach (var slot in inventory.Slots)
            {
                if (slot.item == null)
                {
                    slot.item = item;
                    Debug.Log($"Item {item.name} ajouté dans un slot vide de l'inventaire.");
                    slotFull = true;
                    return; // Quitte la méthode après avoir ajouté l'item
                }
            }

            // Si aucun slot vide n'est trouvé
            Debug.LogWarning("Tous les slots de l'inventaire sont pleins. Impossible d'ajouter l'item.");
            slotFull = false;
        }
        else
        {
            Debug.LogError("Inventaire invalide ou inexistant.");
        }
    }
}
