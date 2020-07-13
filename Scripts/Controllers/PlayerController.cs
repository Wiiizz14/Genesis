using Assets.Ressources.Scripts.Models;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player player; // Model
    private ProjectileSpawn ps;
    private Quaternion targetRotation;
    private float timeToFire = 0;

    // DEBUG (Set to private)
    public GameObject hitObject;
    public int distance;
    public int damageFinal = 0;


    private void Awake()
    {
        // Get the model
        player = GetComponent<Player>();

        // Get Components
        ps = GameObject.Find("Scene").GetComponent<ProjectileSpawn>();
    }


    private void Update()
    {
        // Moves
        MovePlayer();

        // RaycastHit
        RaycastHit hit = GetRaycastHit();
        distance = (int) hit.distance;

        // DEUG IF
        if(hit.collider != null)
        {
            hitObject = hit.collider.gameObject;
        }

        // Get realtime damage value
        int damageRealtime = ps.CalculateDamageValue(distance, ps.GetVFX(0).GetComponent<Projectile>().DamageBase);            

        // Fire
        if (Input.GetMouseButton(0) && Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1 / ps.GetVFX(0).GetComponent<ProjectileController>().projectile.FireRate;
            Vector3 emitterPosition = player.FirePoint.transform.position;

            // Spawn VFX
            damageFinal = ps.SpawnVFX(emitterPosition, ps.GetVFX(0), GetPlayerRotation(), gameObject, damageRealtime);
        }
    }


    public Quaternion GetPlayerRotation()
    {
        return transform.rotation;
    }


    public Vector3 GetPosition()
    {
        return transform.position;
    }


    public int GetDamageProjectile()
    {
        return this.damageFinal;
    }


    public RaycastHit GetRaycastHit()
    {
        // Excluding LayerMask 20 (Projectile) & LayerMask 21 (ProjectileEnnemy)
        LayerMask layerMask = ~((1 << 20) | (1 << 21));
        Physics.Raycast(player.FirePoint.transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, Mathf.Infinity, layerMask);

        return hit;
    }


    private void MovePlayer()
    {
        // Move Control (Keyboard)
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            player.Controller.Move(direction * player.SpeedMove * Time.deltaTime);
        }

        // Rotate Control (Mouse)
        // Generate a plane that intersects the transform's position with an upwards normal.
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        // Generate a ray from the cursor position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Determine the point where the cursor ray intersects the plane.
        // This will be the point that the object must look towards to be looking at the mouse.
        // Raycasting to a Plane object only gives us a distance, so we'll have to take the distance,
        //   then find the point along that ray that meets that distance.  This will be the point
        //   to look at.
        // If the ray is parallel to the plane, Raycast will return false.
        if (playerPlane.Raycast(ray, out float hitdist))
        {
            // Get the point along the ray that hits the calculated distance.
            Vector3 targetPoint = ray.GetPoint(hitdist);
            // Determine the target rotation.  This is the rotation if the transform looks at the target point.
            targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, player.SpeedRotation * Time.deltaTime);
        }
    }
}