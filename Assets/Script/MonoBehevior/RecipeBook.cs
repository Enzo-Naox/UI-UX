using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RecipeBook : MonoBehaviour
{
    [SerializeField] private List<CraftingRecipe> craftingRecipes;
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text listeIngredients;
    [SerializeField] private Items item;
    
    private int index = 0;

    private void Start()
    {
        ShowRecipe();
    }
    public void ShowRecipe()
    {
        for (int i = 0; i < craftingRecipes.Count; i++)
        {
            if (index == i)
            {
                image.sprite = craftingRecipes[i].output.item.icon;
                title.text = craftingRecipes[i].output.item.name;
                description.text = craftingRecipes[i].output.item.description;
                listeIngredients.text = GetIngredientsList(craftingRecipes[i]);
                break;
            }
        }
    }

    public void SwitchNext()
    {
        index++;
        ShowRecipe();
    }
    
    public void SwitchPrevious()
    {
        index--;
        ShowRecipe();
    }
    
    private string GetIngredientsList(CraftingRecipe recipe)
    {
        List<ItemSlot> ingredients = recipe.elements; // Liste des ingrédients
        string result = "Ingrédients nécessaires :\n";

        // Parcourir les ingrédients et formater leur affichage
        foreach (var ingredient in recipe.elements)
        {
            result += $"- {ingredient.item.name}\n";
        }

        return result;
    }
}
