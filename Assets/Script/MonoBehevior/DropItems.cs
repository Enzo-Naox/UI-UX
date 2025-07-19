using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DropItems : MonoBehaviour
{
    [SerializeField] private RectTransform crosshair;
    private Canvas dropChoice;
    private Canvas sliderUI;
    private Canvas stockUI;
    public GameObject targetedObject;
    private Slider slider; // R�f�rence au Slider dans le prefab cibl�
    public InputActionReference mouseLeftClick;
    private GameObject currentHighlightedObject;

    private bool showHud = false;
    private bool isDraggingSlider = false;
    public bool activeCraft = false;// Indique si le slider est en train d'�tre manipul�

    private void Start()
    {
        mouseLeftClick.action.started += OnClickStarted;
        mouseLeftClick.action.canceled += OnClickReleased;
    }

    private void Update()
    {
        Vector2 crosshairScreenPosition = RectTransformUtility.WorldToScreenPoint(null, crosshair.position);

        // Cr�er un rayon � partir de cette position � travers la cam�ra
        Ray ray = Camera.main.ScreenPointToRay(crosshairScreenPosition);

        RaycastHit hitInfo;
        
        if (Physics.Raycast(ray, out hitInfo, 10f, LayerMask.GetMask("Conteneur")))
        {
            GameObject hitObject = hitInfo.transform.gameObject;
            if (hitObject != currentHighlightedObject)
            {
                Outline outline = hitObject.GetComponent<Outline>();
                if (outline != null)
                {
                    outline.enabled = true;
                }
                currentHighlightedObject = hitObject; // Mettre � jour l'objet surlign�
            }
        }
        else
        {
            ClearHighlight();
        }
        if (showHud)
        {
            if (Physics.Raycast(ray, out hitInfo, 10f, LayerMask.GetMask("Conteneur")))
            {
                targetedObject = hitInfo.transform.gameObject;
                Canvas[] canvases = targetedObject.GetComponentsInChildren<Canvas>(true);
                sliderUI = System.Array.Find(canvases, canvas => canvas.gameObject.name == "UI_Craft");
                dropChoice = System.Array.Find(canvases, canvas => canvas.gameObject.name == "UI_Contener");
                stockUI = System.Array.Find(canvases, canvas => canvas.gameObject.name == "UI_Stock");

                if (sliderUI)
                {
                    sliderUI.gameObject.SetActive(true);
                    slider = sliderUI.GetComponentInChildren<Slider>();
                }

                if (dropChoice)
                {
                    dropChoice.gameObject.SetActive(true);
                }
                
                if (stockUI)
                {
                    stockUI.gameObject.SetActive(true);
                }
            }
            else
            {
                if (dropChoice)
                {
                    dropChoice.gameObject.SetActive(false);
                }
                if (sliderUI)
                {
                    sliderUI.gameObject.SetActive(false);
                }
                if (stockUI)
                {
                    stockUI.gameObject.SetActive(false);
                }
            }

            // Si on glisse sur le slider, mettez � jour la valeur
            if (isDraggingSlider && slider != null)
            {
                UpdateSliderValue(crosshairScreenPosition);
            }
        }
        // V�rifier s'il y a un objet "Pickable" touch� par le rayon
    }
    
    private void OnClickStarted(InputAction.CallbackContext context)
    {
        showHud = true;
        if (slider != null && sliderUI != null)
        {
            Vector2 crosshairScreenPosition = RectTransformUtility.WorldToScreenPoint(null, crosshair.position);
            RectTransform sliderRect = slider.GetComponent<RectTransform>();

            // V�rifiez si le crosshair est au-dessus du slider
            if (RectTransformUtility.RectangleContainsScreenPoint(sliderRect, crosshairScreenPosition, Camera.main))
            {
                isDraggingSlider = true; // Activer le glissement
                UpdateSliderValue(crosshairScreenPosition); // Mettre � jour imm�diatement la valeur
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

    private void OnClickReleased(InputAction.CallbackContext context)
    {
        isDraggingSlider = false; // D�sactiver le glissement
        showHud = false;
    }

    private void UpdateSliderValue(Vector2 crosshairScreenPosition)
    {
        RectTransform sliderRect = slider.GetComponent<RectTransform>();

        // Convertir la position du crosshair en coordonn�es locales du slider
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            sliderRect,
            crosshairScreenPosition,
            Camera.main,
            out localPoint
        );

        if (slider.value >= 0.6f)
        {
            activeCraft = true;
        }
        else
        {
            activeCraft = false;
        }

        // Calculer la valeur normalis�e entre 0 et 1 pour la position X du clic
        float normalizedPosition = Mathf.Clamp01((localPoint.x - sliderRect.rect.xMin) / sliderRect.rect.width);

        // Mettre � jour la valeur du slider en fonction de la position normalis�e
        slider.value = Mathf.Lerp(slider.minValue, slider.maxValue, normalizedPosition);
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