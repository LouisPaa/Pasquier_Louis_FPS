using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] public Gun Gun;

    public void OnShoot() // appel la méthode depuis l'input system pour tirer
    {
        Gun.Shoot();
    }
}
