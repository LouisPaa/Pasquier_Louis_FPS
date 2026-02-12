using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] public Gun Gun;

    public void OnShoot()
    {
        Gun.Shoot();
    }
}
