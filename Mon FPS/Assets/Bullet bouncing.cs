using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Bulletbouncing : MonoBehaviour
{
    public float AutoDestructionTime = 5f;
    public float MoveSpeed = 2f;
    public int Damage = 5;
    public Rigidbody Rigidbody;
    public BulletTrailScriptableObject TrailConfig;
    protected TrailRenderer Trail;
    protected Transform Target;
    [SerializeField] private Renderer Renderer;

    protected const string DISABLE_METHOD_NAME = "Disable";

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    protected virtual void OnEnable()
    {
        CancelInvoke(DISABLE_METHOD_NAME);
        Invoke(DISABLE_METHOD_NAME, AutoDestructionTime);
    }

    public virtual void Spawn(Vector3 Forward, int Damage, Transform Target)
    {
 
        this.Damage = Damage;
        this.Target = Target;
        Rigidbody.AddForce(Forward * MoveSpeed, ForceMode.VelocityChange);
    }
}
