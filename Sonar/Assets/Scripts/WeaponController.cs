using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject gunObject;
    public GameObject screenPlane;

    // Projectile to spawn
    public GameObject bulletProjectile;
    public GameObject sonarProjectile;

    // Location for projectiles to spawn
    public Transform projectileSpawn;

    // Rate at which projectiles can spawn
    public float bulletFireRate;
    public float sonarFireRate;

    // Used to calculate when next projectile can spawn
    private float bulletNextFire = 0.0f;
    private float sonarNextFire = 0.0f;

    public float fireForce;

    // Use this for initialization
    void Start()
    {
        projectileSpawn = gunObject.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        WeaponMovementUpdate();
        WeaponFireUpdate();
    }

    void WeaponMovementUpdate()
    {
        // Rotate player to follow cursor
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(/*new Vector3(*/Input.mousePosition/*.x, Input.mousePosition.y, Camera.main.nearClipPlane)*/);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, (Vector3)mousePos - transform.position);

		////var mousePos = Input.mousePosition;
		////mousePos.z = Camera.main.nearClipPlane;
		////var point = Camera.main.ScreenToWorldPoint(mousePos);
		////Instantiate(Resources.Load("Sphere_Test"), point, transform.rotation);

        //var mousePos = Input.mousePosition;
        //mousePos.z = Camera.main.nearClipPlane;
        //var point = Camera.main.ScreenToWorldPoint(mousePos);
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
    }

    void WeaponFireUpdate()
    {

        // Fire bullet, left click
        if (Input.GetButton("Fire1") && Time.time > bulletNextFire)
        {
            bulletNextFire = Time.time + bulletFireRate;
            Fire(bulletProjectile);
        }

        // Fire sonar, right click
        else if (Input.GetButton("Fire2") && Time.time > sonarNextFire)
        {
            sonarNextFire = Time.time + sonarFireRate;
            Fire(sonarProjectile);
        }
    }

    // Shoot bullet/sonar
    void Fire(GameObject projectile)
    {
        // Spawn gameobject
        GameObject instance = (GameObject)Instantiate(
            projectile,
            projectileSpawn.position,
            projectileSpawn.rotation);
        instance.name = projectile.name;

        // Calculate "forward" TODO: Find last dir moved
        Vector2 dir = (instance.transform.right);

        // Fire projectile
        instance.GetComponent<Rigidbody>().AddForce(fireForce * dir);

        // Destroy in 3.0s
        Destroy(instance, 3.0f);
    }
}
