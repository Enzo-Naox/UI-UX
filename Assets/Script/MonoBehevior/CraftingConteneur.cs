using System.Collections.Generic;
using UnityEngine;
using DentedPixel;

public class CraftingConteneur : MonoBehaviour
{
    [SerializeField] private List<CraftingRecipe> craftingRecipes;
    public GameObject bar;
    private int time = 10;
    public GameObject parentObject; // Parent contenant "Content"
    public DropItems dropItems;
    private Canvas dropChoice;
    public void Craft()
    {
        // Parcours de chaque recette de craft
        foreach (var recipe in craftingRecipes)
        {
            // Vérifie si les items dans le conteneur correspondent à la recette
            if (IsRecipeValid(recipe, parentObject.transform))
            {
                Debug.Log($"Craft réussi : {recipe.output.item.name}");

                // Récupère tous les Canvases enfants et trouve celui nommé "UI_Content"
                Canvas[] canvases = dropItems.targetedObject.GetComponentsInChildren<Canvas>(true);
                dropChoice = System.Array.Find(canvases, canvas => canvas.gameObject.name == "UI_GetContent");

                if (dropChoice != null)
                {
                    dropChoice.gameObject.SetActive(true);
                }
                // Supprime tous les objets enfants de Content
                RemoveAllItems(parentObject.transform);

                // Crée un nouveau GameObject vide avec le nom de l'item output
                CreateCraftedItem(parentObject.transform, recipe);

                return;
            }
        }

        Debug.Log("Aucune recette correspondante.");
    }

    private List<string> GetContainerItems(Transform contentTransform)
    {
        // Extrait la liste des noms des items des enfants de "Content"
        var items = new List<string>();
        foreach (Transform child in contentTransform)
        {
            var itemComponent = child.GetComponent<Component>(); // Vérifie si le child a des composants attachés
            if (itemComponent != null)
            {
                items.Add(child.name); // Ajoute le nom de l'objet à la liste
            }
        }
        return items;
    }

    private bool IsRecipeValid(CraftingRecipe recipe, Transform contentTransform)
    {
        // Récupère les items dans le conteneur (Content)
        var containerItems = GetContainerItems(contentTransform);

        // Compare les items de la recette avec ceux du conteneur
        foreach (var requiredItem in recipe.elements)
        {
            // Si un élément requis manque, la recette n'est pas valide
            if (!containerItems.Contains(requiredItem.item.name))
            {
                return false;
            }

            // Retire l'élément consommé de la liste
            containerItems.Remove(requiredItem.item.name);
        }

        return containerItems.Count == 0; // Assure que tous les éléments requis sont utilisés
    }

    public void RemoveAllItems(Transform contentTransform)
    {
        // Supprime tous les enfants de Content
        foreach (Transform child in contentTransform)
        {
            Destroy(child.gameObject); // Supprime l'objet du GameObject parent
        }
    }

    private void CreateCraftedItem(Transform contentTransform, CraftingRecipe recipe)
    {
        // Crée un nouveau GameObject vide avec le nom de l'item
        GameObject newItem = new GameObject(recipe.output.item.name);
        newItem.transform.SetParent(contentTransform); // Ajoute ce nouvel objet comme enfant du conteneur "Content"
        PickableItems pickableScript = newItem.AddComponent<PickableItems>();
        pickableScript.item = recipe.output.item;
    }

    public void AnimatedBar()
    {
        // Animer l'échelle en X du GameObject 'bar' pendant 'time' secondes
        LeanTween.scaleX(bar, 1f, time).setOnComplete(OnAnimationComplete);
    }

    private void OnAnimationComplete()
    {
        bar.transform.localScale = new Vector3(0f, bar.transform.localScale.y, bar.transform.localScale.z);
        Craft();
    }

    public void VerifLaunch()
    {
        // Vérifie si un craft est possible
        bool canCraft = false;

        foreach (var recipe in craftingRecipes)
        {
            if (IsRecipeValid(recipe, parentObject.transform))
            {
                canCraft = true;
                break;
            }
        }

        if (canCraft && dropItems.activeCraft)
        {
            AnimatedBar();
        }
        else
        {
            Debug.Log("Aucune recette valide. Animation annulée.");
        }
    }

}
