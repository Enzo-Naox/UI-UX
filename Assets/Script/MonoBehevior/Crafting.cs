using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Crafting : MonoBehaviour
{
    [SerializeField] private List<CraftingRecipe> craftingRecipes;
    public ItemsContainer inventory; // Conteneur des objets dans l'inventaire
    public InputActionReference ClickActionRef;

    public delegate void OnInventoryUpdated();
    public static event OnInventoryUpdated InventoryUpdated;

    private void Start()
    {
        ClickActionRef.action.performed += OnClick;
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        Craft();
    }

    void OnEnable()
    {
        ClickActionRef.action.Enable();
    }

    void OnDisable()
    {
        ClickActionRef.action.Disable();
    }
    public void Craft()
    {
        // Parcours de chaque recette de craft
        foreach (var recipe in craftingRecipes)
        {
            // V�rifie si les items dans l'inventaire correspondent � la recette
            if (IsRecipeValid(recipe))
            {
                ReplaceItemsWithCraftResult(recipe);
                InventoryUpdated?.Invoke();
                Debug.Log($"Craft r�ussi : {recipe.output.item.name}");
                return;
            }
        }

        Debug.Log("Aucune recette correspondante.");
    }

    private bool IsRecipeValid(CraftingRecipe recipe)
    {
        // V�rifie que l'inventaire contient exactement les �l�ments de la recette
        var inventoryItems = new List<Items>
        {
            inventory.Slots[0].item,
            inventory.Slots[1].item
        };

        // Compare les items de la recette avec ceux de l'inventaire
        foreach (var requiredItem in recipe.elements)
        {
            if (!inventoryItems.Remove(requiredItem.item))
            {
                return false; // Si un �l�ment requis manque, la recette n'est pas valide
            }
        }

        return inventoryItems.Count == 0; // Assure que tous les slots sont utilis�s
    }

    private void ReplaceItemsWithCraftResult(CraftingRecipe recipe)
    {
        // Efface les �l�ments consomm�s par le craft
        for (int i = 0; i < inventory.Slots.Count; i++)
        {
            var slot = inventory.Slots[i];
            if (slot.item != null && recipe.elements.Exists(e => e.item == slot.item))
            {
                slot.item = null; // Supprime l'item du slot
            }
        }

        // Ajoute le nouvel item craft� dans le premier slot vide
        foreach (var slot in inventory.Slots)
        {
            if (slot.item == null)
            {
                slot.item = recipe.output.item;
                return;
            }
        }

        Debug.LogWarning("Inventaire plein, impossible d'ajouter l'item craft�.");
    }

}
