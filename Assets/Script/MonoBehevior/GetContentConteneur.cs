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
        // Recherche d'un enfant nomm� "Content" dans l'objet cibl�
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
            // R�cup�re l'objet "Content" cibl�
            Transform content = GetContentChild(dropItems.targetedObject.transform);

            if (content != null)
            {
                // Parcourt les enfants de "Content" et r�cup�re leurs scripts PickableItems
                foreach (Transform child in content)
                {
                    PickableItems pickable = child.GetComponent<PickableItems>();
                    if (pickable != null)
                    {
                        Debug.Log($"Enfant trouv� : {child.name}, Item : {pickable.item.name}");

                        // Ajoute l'item trouv� dans la main droite
                        AddItemToInventory(pickable.item);

                        Canvas[] canvases = dropItems.targetedObject.GetComponentsInChildren<Canvas>(true);
                        dropChoice = System.Array.Find(canvases, canvas => canvas.gameObject.name == "UI_GetContent");

                        if (slotFull)
                        {
                            dropChoice.gameObject.SetActive(false);
                            Destroy(child.gameObject);
                        } 
                        return; // Stoppe apr�s avoir r�cup�r� un item
                    }
                }
                Debug.Log("Aucun item valide trouv� dans Content.");
            }
            else
            {
                Debug.Log("Content introuvable dans l'objet cibl�.");
            }
        }
        else
        {
            Debug.Log("Aucun objet cibl�.");
        }
    }

    public void AddItemToInventory(Items item)
    {
        // V�rifie si l'inventaire est valide
        if (inventory != null && inventory.Slots.Count > 0)
        {
            // Parcourt les slots de l'inventaire pour trouver un slot vide
            foreach (var slot in inventory.Slots)
            {
                if (slot.item == null)
                {
                    slot.item = item;
                    Debug.Log($"Item {item.name} ajout� dans un slot vide de l'inventaire.");
                    slotFull = true;
                    return; // Quitte la m�thode apr�s avoir ajout� l'item
                }
            }

            // Si aucun slot vide n'est trouv�
            Debug.LogWarning("Tous les slots de l'inventaire sont pleins. Impossible d'ajouter l'item.");
            slotFull = false;
        }
        else
        {
            Debug.LogError("Inventaire invalide ou inexistant.");
        }
    }
}
