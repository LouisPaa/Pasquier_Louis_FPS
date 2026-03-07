using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    public float mouseY = 1000f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      Cursor.lockState = CursorLockMode.Locked; // Verrouille le curseur au centre de l'écran
    }

    // Update is called once per frame
    void Update()
    {
        float yRotation = Input.GetAxis("Mouse Y") * mouseY * Time.deltaTime; // Récupère le mouvement de la souris sur l'axe Y 
        transform.Rotate(-yRotation, 0f, 0f); // Fait tourner la caméra en fonction du mouvement de la souris
    }
}
