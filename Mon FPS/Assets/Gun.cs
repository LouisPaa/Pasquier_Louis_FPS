using UnityEngine;  
using System.Collections.Generic;
public class Gun : MonoBehaviour
{

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetMouseButtonDown(0)) // clic gauche
        {

            Instantiate(bulletPrefab, bulletPoint.position, Quaternion.LookRotation(transform.forward, Vector3.up)); // instancie une balle qui partira vers l'avant
        } 
    }
}
