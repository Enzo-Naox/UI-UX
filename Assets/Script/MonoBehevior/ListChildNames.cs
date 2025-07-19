using UnityEngine;
using TMPro;

public class ListChildNames : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI uiText; // Référence au TextMeshPro UI où afficher les noms des enfants.

    private void Update()
    {
        if (uiText == null)
        {
            Debug.LogError("Le champ uiText n'est pas assigné !");
            return;
        }

        // Récupérer tous les enfants du GameObject
        Transform[] children = GetComponentsInChildren<Transform>();

        // Initialiser une chaîne pour stocker les noms
        string childNames = "Stockage :\n";

        // Boucler sur les enfants (ignorer le parent lui-même)
        foreach (Transform child in children)
        {
            if (child != transform) // Ignorer le GameObject parent
            {
                childNames += $"{child.name}\n";
            }
        }

        // Afficher les noms dans le TextMeshPro UI
        uiText.text = childNames;
    }
}