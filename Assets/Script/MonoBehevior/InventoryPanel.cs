using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField] ItemsContainer inventory;
    [SerializeField] List<InventoryButton> buttons;

    private void Update()
    {
        Show();
    }

    private void Show()
    {
        for (int i = 0; i < inventory.Slots.Count; i++)
        {
            if (inventory.Slots[i].item == null)
            {
                buttons[i].Clean();
            }
            else
            {
                buttons[i].Set(inventory.Slots[i]);
            }
        }
    }
}
