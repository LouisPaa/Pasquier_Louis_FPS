using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    public float mouseY = 1000f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float yRotation = Input.GetAxis("Mouse Y") * mouseY * Time.deltaTime;
        transform.Rotate(-yRotation, 0f, 0f);
    }
}
