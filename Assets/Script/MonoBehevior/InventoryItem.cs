using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    [HideInInspector] public Items item;
    [HideInInspector] public Transform parentAfterDrag;
    public Canvas mainCanvas;
    public TMP_Text hudDesc;
    public bool onDrag = false;

    public void Initialiseitem(Items newItem)
    {
        item = newItem;
        image.sprite = newItem.icon;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(mainCanvas.transform);
        transform.SetAsLastSibling();
        transform.gameObject.GetComponent<InventoryItem>().onDrag = true;
        image.raycastTarget = false;
        hudDesc.text = item.description;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (onDrag)
        {
            transform.SetParent(parentAfterDrag);
        }
        image.raycastTarget = true;
    }

    public void SwapWith(Transform otherSlot)
    {
        if (otherSlot.childCount > 0)
        {
            // Obtenez l'objet actuel dans le slot cible
            Transform otherItem = otherSlot.GetChild(0);

            // Déplacez l'autre objet vers le parent d'origine
            otherItem.SetParent(parentAfterDrag);
            otherItem.SetSiblingIndex(parentAfterDrag.GetSiblingIndex());
        }
        // Déplacez l'objet actuel vers le nouveau slot
        transform.SetParent(otherSlot);
        transform.SetSiblingIndex(otherSlot.GetSiblingIndex());
        
    }
}
