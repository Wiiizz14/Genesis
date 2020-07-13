using Assets.Ressources.Scripts.Models;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private GameObject muzzlePrefab;
    [SerializeField] private GameObject hitPrefab;
    [SerializeField] private Transform damagePopupPrefab;

    public Projectile projectile; // Public to Access EnnemyRobotController
    private bool isDamaged = false;

    // DEBUG (Set to private)
    public int damageFinal;


    private void Awake()
    {
        // Get the model
        projectile = GetComponent<Projectile>();
    }


    void Start()
    {
        // Split name to identify emitter (Proj_emitterName)
        string emitterName = gameObject.name.Split('_').Last(); 

        // Find GameOject & get damageFinal
        if (emitterName.Equals("Player"))
        {
            // Case Player
            PlayerController pc = GameObject.Find(emitterName).GetComponent<PlayerController>();
            damageFinal = pc.GetDamageProjectile();
        } 
        else
        {
            // Case Ennemy
            EnnemyRobotController erc = GameObject.Find(emitterName).GetComponent<EnnemyRobotController>();
            damageFinal = erc.GetDamageProjectile();
        }

        // Muzzle Effet
        if (muzzlePrefab != null)
        {
            GameObject muzzleVFX = CreateVFX(muzzlePrefab, transform.position, Quaternion.identity);
            DestroyParticles(muzzleVFX);
        }
    }


    private void Update()
    {
        // Set direction of projectile & move
        transform.position += transform.forward * (projectile.Speed * Time.deltaTime);

        // Manual destroy if out zone
        if (transform.position.x > 50 || transform.position.x < -50 || transform.position.y > 50 || transform.position.y < -50 || transform.position.z > 50 || transform.position.z < -50)
        {
            Destroy(gameObject);
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        // Stop the projectile
        projectile.Speed = 0f;

        // Hit rotation/position
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;

        // DoChase even Player is not in lookRadius
        if (collision.gameObject.CompareTag("EnnemyRobot"))
        {
            EnnemyRobotController ec = collision.gameObject.GetComponent<EnnemyRobotController>();
            ec.DoChase();
        }

        // Projectile's Logic with Layers :
        //      - Avoid collision between projectiles colliders 
        //      - Avoid friendly fire between ennemies

        if (hitPrefab != null)
        {
            // Hit Effect
            GameObject hitVFX = CreateVFX(hitPrefab, pos, rot);
            DestroyParticles(hitVFX);

            // Get HealthManager on GameObject if exist
            HealthManager healthManager = collision.gameObject.GetComponent<HealthManager>();

            if (damageFinal > 0)
            {
                // Apply Damage on it & eventually kill it
                DoDamage(damageFinal, healthManager, collision.gameObject);

                if (isDamaged)
                {
                    // UI damage indicator
                    DamagePopupController.Create(transform.position, damageFinal, damagePopupPrefab);
                }
            }
        }
        // Destroy projectile
        Destroy(gameObject);
    }


    private GameObject CreateVFX(GameObject particlesPrefab, Vector3 particlesPosition, Quaternion particlesAngle)
    {
        // Create VFX Object & Set it active
        GameObject particlesVFX = Instantiate(particlesPrefab, particlesPosition, particlesAngle);
        particlesVFX.SetActive(true);
        // GameObject position
        particlesVFX.transform.forward = gameObject.transform.forward;

        return particlesVFX;
    }


    private void DestroyParticles(GameObject particlesVFX)
    {
        // Get the component for destroy if exist
        ParticleSystem psParticles = particlesVFX.GetComponent<ParticleSystem>();
        if (psParticles != null)
        {
            Destroy(particlesVFX, psParticles.main.duration);
        }
        else
        {
            // Else Get the child component and destroy it
            ParticleSystem psChild = particlesVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
            Destroy(particlesVFX, psChild.main.duration);
        }
    }


    private bool DoDamage(int damage, HealthManager hm, GameObject target)
    {
        if (hm)
        {
            // Apply damage on target
            hm.ApplyDamage(damage);

            // if Alive
            if (hm.life <= 0)
            {
                // Destroy target (ennemy or player)
                Destroy(target);

                // Case player death
                if (hm.gameObject.name.Equals("Player"))
                {
                    // GoBack MainMenu
                    SceneManager.LoadScene("MainMenu");
                }
            }
            // Set boolean
            isDamaged = true;
        }     
        return isDamaged;
    }
}



