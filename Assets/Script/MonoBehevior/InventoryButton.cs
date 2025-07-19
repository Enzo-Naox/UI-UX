using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    [SerializeField] private Image icon; // Image du bouton
    private ItemSlot currentSlot;
    public Sprite iconSprite;

    // Méthode pour configurer l'icône
    public void Set(ItemSlot slot)
    {
        currentSlot = slot; // Mémorise le slot
        if (slot != null && slot.item != null)
        {
            icon.sprite = slot.item.icon;
        }
        else
        {
            Clean();
        }
    }

    // Méthode pour nettoyer l'icône
    public void Clean()
    {
        icon.sprite = iconSprite;
    }

    // Méthode appelée lorsqu'une mise à jour est nécessaire
    public void UpdateHand()
    {
        Set(currentSlot);
    }

    // Gestion d'événements pour une mise à jour automatique (facultatif)
    private void OnEnable()
    {
        Crafting.InventoryUpdated += UpdateHand;
        ChoiceHand.HudUpdated += UpdateHand;
    }

    private void OnDisable()
    {
        Crafting.InventoryUpdated -= UpdateHand;
        ChoiceHand.HudUpdated -= UpdateHand;
    }
}
