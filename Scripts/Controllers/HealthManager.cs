using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int life;

    private readonly HealthManager healthManager;


    public HealthManager(HealthManager healthManager)
    {
        this.healthManager = healthManager;
    }


    public void ApplyDamage(int damage)
    {
        if (healthManager)
        {
            healthManager.ApplyDamage(damage);
        }
        else { life -= damage; }
    }
}