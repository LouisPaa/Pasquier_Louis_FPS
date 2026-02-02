using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Bullet : MonoBehaviour
{
    // propiétés de la balle 
    public float speed = 100f;
    private BoxCollider collider;
    private Rigidbody rb;    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collider = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = transform.forward * speed * Time.deltaTime; 
    }

    private void OnCollisionEnter(Collision collision)
    {
       Destroy(gameObject);
    }

}
