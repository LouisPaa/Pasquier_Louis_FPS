using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
public class EnnemyAnimation : MonoBehaviour
{
    public Transform Target;
    public NavMeshAgent agent;
    private Animation animations;
    private float Distance;
    public float chaseRange = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animations = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        Target = GameObject.Find("Joueur").transform;
        Distance = Vector3.Distance(Target.position, transform.position);
    }
}
