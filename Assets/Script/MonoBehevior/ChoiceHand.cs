using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChoiceHand : MonoBehaviour
{
    private Items items;
    public ItemsContainer inventory;
    public delegate void OnHudUpdated();
    public static event OnHudUpdated HudUpdated;

    public InputActionReference KeyboardA;
    public InputActionReference KeyboardB;
    private void Start()
    {
        KeyboardA.action.performed += DropLeftHand;
        KeyboardB.action.performed += DropRightHand;
    }

    private void GetPickableitem(Items selecteditems)
    {
        items = selecteditems;
    }

    public void RightHand()
    {
        inventory.Slots[1].item = items;
        HudUpdated?.Invoke();
    }
    
    public void RightHand(Items _item)
    {
        _item = items;
        HudUpdated?.Invoke();
    }

    public void leftHand()
    {
        inventory.Slots[0].item = items;
        HudUpdated?.Invoke();
    }
    
    public void LeftHand(Items _item)
    {
        _item = items;
        HudUpdated?.Invoke();
    }

    public void DropRightHand(InputAction.CallbackContext context)
    {
        inventory.Slots[1].item = null;
        HudUpdated?.Invoke();
    }

    public void DropLeftHand(InputAction.CallbackContext context)
    {
        inventory.Slots[0].item = null;
        HudUpdated?.Invoke();
    }

    public void OnEnable()
    {
        PickItem.choiceHand += GetPickableitem;
        KeyboardA.action.Enable();
        KeyboardB.action.Enable();
    }
    public void OnDisable()
    {
        PickItem.choiceHand -= GetPickableitem;
        KeyboardA.action.Disable();
        KeyboardB.action.Disable();
    }
}
