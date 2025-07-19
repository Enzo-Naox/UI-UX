using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    [SerializeField] private Image icon; // Image du bouton
    private ItemSlot currentSlot;
    public Sprite iconSprite;

    // M�thode pour configurer l'ic�ne
    public void Set(ItemSlot slot)
    {
        currentSlot = slot; // M�morise le slot
        if (slot != null && slot.item != null)
        {
            icon.sprite = slot.item.icon;
        }
        else
        {
            Clean();
        }
    }

    // M�thode pour nettoyer l'ic�ne
    public void Clean()
    {
        icon.sprite = iconSprite;
    }

    // M�thode appel�e lorsqu'une mise � jour est n�cessaire
    public void UpdateHand()
    {
        Set(currentSlot);
    }

    // Gestion d'�v�nements pour une mise � jour automatique (facultatif)
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
