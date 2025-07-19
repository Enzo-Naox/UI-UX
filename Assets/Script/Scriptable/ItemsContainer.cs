using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemSlot
{
    public Items item;
}

[CreateAssetMenu(menuName = "Data/Item Container")]
public class ItemsContainer : ScriptableObject
{
    [SerializeField]
    private List<ItemSlot> slots = new List<ItemSlot>(2);
    public IReadOnlyList<ItemSlot> Slots => slots;

    // Validation dans l'éditeur Unity
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (slots.Count > 2)
        {
            Debug.LogWarning("L'inventaire ne peut pas contenir plus de 2 slots ! Les éléments excédentaires ont été supprimés.");
            while (slots.Count > 2)
            {
                slots.RemoveAt(slots.Count - 1); // Supprime les slots en trop.
            }
        }
    }
#endif
}
