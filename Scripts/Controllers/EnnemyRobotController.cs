using Assets.Ressources.Scripts.Models;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnnemyRobotController : MonoBehaviour
{
    private EnnemyRobot ennemyRobot; // Model
    private GameObject player;
    private Quaternion angleToTarget;
    private NavMeshAgent navMeshAgent;
    private EnnemySpawn es;
    private ProjectileSpawn ps;
    private HealthManager hm;

    private float timeToFire = 0;

    // DEBUG (Set to private)
    public int distanceToPlayer;
    public int damageFinal;


    private void Awake()
    {
        // Get the model
        ennemyRobot = GetComponent<EnnemyRobot>();

        // Get player from Scene
        player = GameObject.Find("Player");

        // Get Components
        es = GameObject.Find("Scene").GetComponent<EnnemySpawn>();
        ps = GameObject.Find("Scene").GetComponent<ProjectileSpawn>();
        hm = player.GetComponent<HealthManager>();
    }


    void Start()
    {
        // Build NavMeshAgent
        navMeshAgent = GetComponent<NavMeshAgent>();
        BuildNavMeshAgent(navMeshAgent);
    }


    private void Update()
    {
        // If Player alive
        if (player != null)
        {
            // Get distance between ennemy and player
            distanceToPlayer = (int) Vector3.Distance(player.transform.position, transform.position);

            // If in radius go chase him
            if (distanceToPlayer <= ennemyRobot.LookRadius)
            {
                DoChase();
            }               

            // Attack if in radius and eventually kill player
            if (distanceToPlayer < ennemyRobot.AttackRadius && Time.time >= timeToFire)         
            {
                // Set target & timeTofire & adjust Reactivity with coroutine
                timeToFire = Time.time + 1 / ps.GetVFX(1).GetComponent<ProjectileController>().projectile.FireRate;
                StartCoroutine(DoAttack(ennemyRobot.Reactivity));
            }

            // Instant kill if too closer ----> min 2.5f
            if (distanceToPlayer < 2.5f)
            {
                hm.ApplyDamage(hm.life + 1);
            }
        }
    }


    protected void LateUpdate()
    {
        // Manual Freeze rotation for x Axis
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }


    private NavMeshAgent BuildNavMeshAgent(NavMeshAgent navMeshAgent)
    {
        // Build the NavMeshAgent
        navMeshAgent.speed = ennemyRobot.SpeedMove;
        navMeshAgent.angularSpeed = ennemyRobot.SpeedRotation;
        navMeshAgent.acceleration = ennemyRobot.Acceleration;
        return navMeshAgent;
    }


    // Move this method to Utils class
    public Quaternion GetAngleToTarget(GameObject target)
    {
        if (player != null)
        {
            Vector3 targetPoint = target.transform.position;
            angleToTarget = Quaternion.LookRotation(targetPoint - transform.position);
        }
        return angleToTarget;
    }


    private IEnumerator DoAttack(float reactivity)
    {
        yield return new WaitForSecondsRealtime(reactivity);
        Attack();
    }


    private int Attack()
    {
        // Calculate damage from distance
        int damage = ps.CalculateDamageValue(distanceToPlayer, ps.GetVFX(1).GetComponent<Projectile>().DamageBase);

        // Spawn VFX with damage value
        damageFinal = ps.SpawnVFX(ennemyRobot.FirePoint.transform.position, ps.GetVFX(1), Quaternion.LookRotation(transform.forward), gameObject, damage);
        return damageFinal;
    }


    public int GetDamageProjectile()
    {
        return this.damageFinal;
    }


    public void DoChase()
    {
        // Focus on player
        transform.rotation = Quaternion.Slerp(transform.rotation, GetAngleToTarget(player), (ennemyRobot.SpeedRotation * 2) * Time.deltaTime);
        // Set destination
        navMeshAgent.SetDestination(player.transform.position);
    }  


    public void OnDestroy()
    {
        if(this != null)
        {
            // Remove Object from List
            es.RemoveToList(gameObject);     
        }
    }


    // Method to draw detection zone in editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ennemyRobot.LookRadius);
    }
}
