using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{
    // Variables pour la cam�ra et la rotation de la souris
    public Camera playerCamera;
    public float mouseSensitivity = 2.0f;
    public float lookUpLimit = 80f;
    public float lookDownLimit = 80f;

    // Variables pour les d�placements du joueur
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    private float currentSpeed;

    private float rotationX = 0f;

    // R�f�rences au CharacterController
    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;  // Verrouille le curseur de la souris
        Cursor.visible = false;  // Cache le curseur de la souris
    }

    // Update is called once per frame
    void Update()
    {
        // Mouvement du joueur
        MovePlayer();

        // Rotation de la souris pour regarder autour
        LookAround();
    }

    // D�placement du joueur
    private void MovePlayer()
    {
        float moveX = Input.GetAxis("Horizontal");  // A/D ou Fl�ches gauche/droite
        float moveZ = Input.GetAxis("Vertical");    // W/S ou Fl�ches haut/bas

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Choix de la vitesse
        if (Input.GetKey(KeyCode.LeftShift))  // Si on appuie sur Shift, on court
            currentSpeed = runSpeed;
        else
            currentSpeed = walkSpeed;

        // Appliquer le d�placement
        characterController.Move(move * currentSpeed * Time.deltaTime);
    }

    // Rotation de la cam�ra avec la souris
    private void LookAround()
    {
        // Rotation de la souris sur l'axe X (haut/bas)
        rotationX -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        rotationX = Mathf.Clamp(rotationX, -lookUpLimit, lookDownLimit);

        // Appliquer la rotation de la cam�ra
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

        // Rotation du joueur sur l'axe Y (gauche/droite)
        float mouseY = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(Vector3.up * mouseY);
    }
}
