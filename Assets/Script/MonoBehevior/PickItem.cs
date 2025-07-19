using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PickItem : MonoBehaviour
{
    [SerializeField] private RectTransform crosshairUI; // R�f�rence au RectTransform du crosshair UI
    [SerializeField] private Canvas HandChoice;
    private GameObject currentHighlightedObject; // Objet actuellement en surbrillance
    private Button button;
    public InputActionReference ClickActionRef;

    public Items pickableItems;
    public delegate void OnChoiceHand(Items pickableItems);
    public static event OnChoiceHand choiceHand;

    private bool test = false;
    private Canvas stockHandChoice;

    private void Start()
    {
        ClickActionRef.action.performed += OnClick;
    }

    private void Update()
    {
        // Convertir la position du crosshair UI en position �cran
        Vector2 crosshairScreenPosition = RectTransformUtility.WorldToScreenPoint(null, crosshairUI.position);

        // Cr�er un rayon � partir de cette position � travers la cam�ra
        Ray ray = Camera.main.ScreenPointToRay(crosshairScreenPosition);

        RaycastHit hitInfo;

        // V�rifier s'il y a un objet "Pickable" touch� par le rayon
        if (Physics.Raycast(ray, out hitInfo, 10f, LayerMask.GetMask("Pickable")))
        {
            GameObject hitObject = hitInfo.transform.gameObject;
            // Si c'est un nouvel objet touch�
            if (hitObject != currentHighlightedObject && HandChoice != stockHandChoice)
            {
                ClearHighlight(); // Efface la surbrillance pr�c�dente
                test = false;

                // Appliquer la surbrillance sur le nouvel objet
                Outline outline = hitObject.GetComponent<Outline>();
                if (outline != null)
                {
                    outline.enabled = true;
                }

                currentHighlightedObject = hitObject; // Mettre � jour l'objet surlign�
            }
            else if (test)
            {
                HandChoice = hitObject.GetComponentInChildren<Canvas>(true);
                pickableItems = hitObject.GetComponent<PickableItems>().item;
                choiceHand?.Invoke(pickableItems);
                HandChoice.gameObject.SetActive(true);
                if (HandChoice != stockHandChoice && stockHandChoice != null)
                {
                    stockHandChoice.gameObject.SetActive(false);
                    pickableItems = null;
                }
            }
        }
        else
        {
            test = false;
            ClearHighlight();
        }
    }
    

    private void OnClick(InputAction.CallbackContext context)
    {
        test = true;
        // V�rifier si le clic est en cours
        if (context.phase == InputActionPhase.Performed)
        {
            // Convertir la position du crosshair UI en position �cran
            Vector2 crosshairScreenPosition = RectTransformUtility.WorldToScreenPoint(null, crosshairUI.position);

            // V�rifier si le crosshair est au-dessus d'un bouton interactif
            if (IsPointerOverUIButton(crosshairScreenPosition))
            {
                button.onClick.Invoke(); // Invoquer l'action du bouton
            }
            if (HandChoice != stockHandChoice && stockHandChoice != null)
            {
                stockHandChoice.gameObject.SetActive(false);
                pickableItems = null;
            }
            if (HandChoice) stockHandChoice = HandChoice;
        }
    }

    // V�rifier si le curseur est au-dessus d'un bouton UI interactif
    private bool IsPointerOverUIButton(Vector2 screenPosition)
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
        {
            position = screenPosition
        };

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        foreach (RaycastResult result in raycastResults)
        {
            if (result.gameObject.GetComponent<Button>() != null) // Si c'est un bouton
            {
                button = result.gameObject.GetComponent<Button>(); // R�f�rencer le bouton trouv�
                return true;
            }
        }

        return false;
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

    void OnEnable()
    {
        ClickActionRef.action.Enable();
    }

    void OnDisable()
    {
        ClickActionRef.action.performed -= OnClick;
    }
}