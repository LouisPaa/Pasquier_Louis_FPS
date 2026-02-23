using UnityEngine;  
using System.Collections.Generic;
using System.Collections;

public class Gun : MonoBehaviour
{

    //[SerializeField] private GameObject bulletPrefab;
  //  [SerializeField] private Transform bulletPoint;
    [SerializeField] private ParticleSystem ShootingSystem;
    [SerializeField] private Transform BulletSpawnPoint;
    [SerializeField] private ParticleSystem ImpactParticleSystem;
    [SerializeField] private TrailRenderer BulletTrail;
    [SerializeField] private float ShootDelay = 0.5f;
    [SerializeField] private float Speed = 100f; // La vitesse à laquelle la balle se déplace
    [SerializeField] private LayerMask Mask;
    [SerializeField] private bool BouncingBullets;
    [SerializeField] private float BounceDistance = 10f; // La distance maximale à laquelle la balle peut rebondir
    
    private float LastShootTime;
    public float Dammage;
    private TrailRenderer trail;
void Awake()
    {
        trail = BulletTrail.GetComponent<TrailRenderer>();
    }
    public void Shoot()
    {
        if ( LastShootTime + ShootDelay < Time.time)
        {
            ShootingSystem.Play();
            Vector3 direction = transform.forward;
            TrailRenderer trail  = Instantiate(BulletTrail, BulletSpawnPoint.position, Quaternion.identity);
           
            if (Physics.Raycast(BulletSpawnPoint.position, direction, out RaycastHit hit, float .MaxValue, Mask))
            {
                StartCoroutine(SpawnTrail(trail, hit.point, hit.normal, BounceDistance, true));
            }
            else
            {
                StartCoroutine(SpawnTrail(trail, direction * 100, Vector3.zero, BounceDistance, false));
            }
                LastShootTime = Time.time;

            if ( Physics.Raycast(BulletSpawnPoint.position, direction , out RaycastHit hitInfo, float. MaxValue, Mask))
            {
                EnnemyAI Unit;
                if (hitInfo.collider.TryGetComponent<EnnemyAI>(out Unit))
                {
                    Unit.DoDammage((int)Dammage);
                }
            }
        }
    }

    private IEnumerator SpawnTrail(TrailRenderer Trail, Vector3 HitPoint, Vector3 HitNormal, float BounceDistance, bool MadeImpact)
    {
        Vector3 startPosition = Trail.transform.position;
        Vector3 direction = (HitPoint - Trail.transform.position).normalized;

        float distance = Vector3.Distance(startPosition, HitPoint);
        float startingDistance = distance;

        while (distance > 0)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, HitPoint, 1 - (distance / startingDistance));
            distance -= Time.deltaTime * Speed;

            yield return null;
        }

        Trail.transform.position = HitPoint;
        if (MadeImpact)
        {
            Instantiate(ImpactParticleSystem, HitPoint, Quaternion.LookRotation(HitNormal));

            if (BouncingBullets && BounceDistance > 0)
            {
                Vector3 bounceDirection = Vector3.Reflect(direction, HitNormal);

                if (Physics.Raycast(HitPoint, bounceDirection, out RaycastHit hit, BounceDistance , Mask))
                {
                    yield return StartCoroutine(SpawnTrail(Trail, hit.point, hit.normal, BounceDistance - Vector3.Distance(hit.point, HitPoint), true));
                }
                else
                {
                    yield return StartCoroutine(SpawnTrail(Trail, bounceDirection * BounceDistance, Vector3.zero, 0, false));
                }

                
            }
        }

        Destroy(Trail.gameObject, Trail.time); 
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
   //   if (Input.GetMouseButtonDown(0)) // clic gauche
        {

       //    Instantiate(BulletTrail, bulletPoint.position, Quaternion.LookRotation(transform.forward, Vector3.up)); // instancie une balle qui partira vers l'avant
        } 
    }
}
