using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawn : MonoBehaviour
{
    public List<GameObject> vfxList = new List<GameObject>();


    public int SpawnVFX(Vector3 firePointPosition, GameObject effectToSpawn, Quaternion rotation, GameObject emmitter, int damage)
    {
        // Create Object
        GameObject vfx = Instantiate(effectToSpawn, firePointPosition, Quaternion.identity);

        // Active It to Render
        vfx.SetActive(true);

        // Object position
        vfx.transform.localRotation = rotation;

        // Trace projectile emitter with emitter.name
        string id = emmitter.name;
        vfx.name = "Proj_" + id;        

        // For calculing damageFinal in Controller
        return damage;
    }


    public GameObject GetVFX(int vfxIndex)
    {
        return vfxList[vfxIndex];
    }


    // Fonctionne mais attention aux values
    public int CalculateDamageValue(float distance, int damage)
    {
        int damageProjectile = 0;

        if (distance >= 15)
        {
            damageProjectile = damage * 25 / 100;
        }
        else if (distance >= 10 && distance < 15)
        {
            damageProjectile =damage * 50 / 100;
        }
        else if (distance >= 5 && distance < 10)
        {
            damageProjectile = damage * 75 / 100;
        }
        else if (distance <= 5)
        {
            damageProjectile = damage;
        }
        return damageProjectile;
    }

}
