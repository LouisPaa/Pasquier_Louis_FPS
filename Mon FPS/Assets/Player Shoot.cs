using UnityEngine;


public class PlayerShoot : MonoBehaviour
{
    public PlayerWeapon weapon;
    [SerializeField] // Permet de renseigner la caméra dans l'inspecteur 
    private Camera Camera;

    [SerializeField]
    private LayerMask mask;
    private void Start()
    {
        if (Camera == null)
        {
            Debug.LogError("Pas de caméra renseigné sur le système de tir ");
            this.enabled = false; // désactive le script player shoot sur le joueur
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot() 
    {
        RaycastHit hit; //Permet de savoir ce que le rayon a touché 

        if (Physics.Raycast(Camera.transform.position,Camera.transform.forward, out hit, weapon.range,mask )) // Permet de créer un raycast depuis la caméra vers l'avant

        {
            Debug.Log("Objet touché:" + hit.collider.name);
        }
    }
}

