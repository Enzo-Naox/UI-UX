using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShowChest : MonoBehaviour
{
    [SerializeField] private RectTransform crosshair;
    [SerializeField] private Canvas hudChest;
    public GameObject mouvement;
    public InputActionReference mouseLeftClick;
    private bool showChest = false;
    private GameObject currentHighlightedObject;
    public MagicChest magicChest;
    public InventoryManager inventoryManager;
    private int max = 1;

    private void Start()
    {
        mouseLeftClick.action.started += OnClick;
    }

    private void Update()
    {
        Vector2 crosshairScreenPosition = RectTransformUtility.WorldToScreenPoint(null, crosshair.position);

        Ray ray = Camera.main.ScreenPointToRay(crosshairScreenPosition);

        RaycastHit hitInfo;
        
        if (Physics.Raycast(ray, out hitInfo, 10f, LayerMask.GetMask("Coffre")))
        {
            GameObject hitObject = hitInfo.transform.gameObject;
            if (hitObject != currentHighlightedObject)
            {
                ClearHighlight(); // Efface la surbrillance pr�c�dente

                // Appliquer la surbrillance sur le nouvel objet
                Outline outline = hitObject.GetComponent<Outline>();
                if (outline != null)
                {
                    outline.enabled = true;
                }

                currentHighlightedObject = hitObject; // Mettre � jour l'objet surlign�
            }
        }
        
        if (showChest)
        {
            if (Physics.Raycast(ray, out hitInfo, 10f, LayerMask.GetMask("Coffre")))
            {
                if (max == 1)
                {
                    magicChest.PickupSlot1();
                    magicChest.PickupSlot2();
                    magicChest.PickupSlot3();
                    magicChest.RandomItem();
                }
                GameObject hitObject = hitInfo.transform.gameObject;
                hudChest = hitObject.GetComponentInChildren<Canvas>(true);
                hudChest.gameObject.SetActive(true);
                EnableCursor();
                max = 0;
            }
            else
            {
                showChest = false;
            }
        }
        
    }
    
    private void ClearHighlight()
    {
        // D�sactiver la surbrillance de l'objet actuel
        if (currentHighlightedObject != null)
        {
            Outline outline = currentHighlightedObject.GetComponent<Outline>();
            if (outline != null)
            {
                outline.enabled = false;
            }
            currentHighlightedObject = null; // R�initialiser l'objet surlign�
        }
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        showChest = true;
    }
    private void EnableCursor()
    {
        // D�verrouille et affiche le curseur
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // R�cup�re le script attach� au GameObject "mouvement" et le d�sactive
        var script = mouvement.GetComponent<PlayerMouvement>();
        if (script != null)
        {
            script.enabled = false; // D�sactive le script
        }
        else
        {
            Debug.LogWarning("Le script n'a pas �t� trouv� sur le GameObject 'mouvement'.");
        }
    }

    public void DisableCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        hudChest.gameObject.SetActive(false);
        showChest = false;
        inventoryManager.ClearInventory();
        max = 1;

        var script = mouvement.GetComponent<PlayerMouvement>();
        if (script != null)
        {
            script.enabled = true;
        }
    }
    

    void OnEnable()
    {
        mouseLeftClick.action.Enable();
    }

    void OnDisable()
    {
        mouseLeftClick.action.Disable();
    }

}