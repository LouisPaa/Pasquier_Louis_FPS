using System.Runtime.CompilerServices;
using UnityEngine;

public class Déplacement : MonoBehaviour
{
    public float jumpspeed = 300f;
        public float speed = 5.0f; 
    public Rigidbody rb;
    public bool Grounded = true; // Permet de savoir si le personnage est au sol ou non 
    public Vector3 velocity;

    private Vector3 moveD = Vector3.zero;

    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Je fais en sorte que mon personnage puisse se déplacer avec les touches ZQSD et saute avec Espace
    void Update()
    {
        rb = GetComponent<Rigidbody>();

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 5);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * Time.deltaTime * 5);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Time.deltaTime * 5);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Time.deltaTime * 5);
        }

        if (Input.GetKeyDown(KeyCode.Space) && Grounded == true)
        {
            rb.AddForce(Vector3.up * jumpspeed);
            Grounded = false;
        } 
    }

         private void OnCollisionEnter(Collision collision) // Permet de détecter la collision avec le sol
    {
            if (collision.gameObject.name == "Sol")
            {
                Grounded = true;
            }
        }

       /* if (velocity.y == 0)
        {
            Grounded = true;
        }

        else
        {
            Grounded = false;
        }*/
    

}
