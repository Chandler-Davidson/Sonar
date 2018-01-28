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
        Vector3 mousePos = Input.mousePosition;// new Vector3(Event.current.mousePosition.x, Event.current.mousePosition.y, 0.0f);
        //mousePos.y = Camera.main.pixelHeight - mousePos.y;
        mousePos.z = Camera.main.nearClipPlane;
        Vector3 point = Camera.main.ScreenToWorldPoint(mousePos);
        point.y = transform.position.y;
        Vector3 dir = Vector3.Normalize(point - transform.position);

        WeaponMovementUpdate(dir);
        WeaponFireUpdate(dir);
    }

    void WeaponMovementUpdate(Vector3 dir)
    {
        // Rotate player to follow cursor
        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(/*new Vector3(*/Input.mousePosition/*.x, Input.mousePosition.y, Camera.main.nearClipPlane)*/);
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, (Vector3)mousePos - transform.position);

        ////var mousePos = Input.mousePosition;
        ////mousePos.z = Camera.main.nearClipPlane;
        ////var point = Camera.main.ScreenToWorldPoint(mousePos);
        ////Instantiate(Resources.Load("Sphere_Test"), point, transform.rotation);

        //var mousePos = Input.mousePosition;
        //mousePos.z = Camera.main.nearClipPlane;
        //var point = Camera.main.ScreenToWorldPoint(mousePos);
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);

        Quaternion q = new Quaternion();
        q = Quaternion.LookRotation(dir);//, Vector3.up);
        transform.rotation = Quaternion.Euler(90.0f, q.eulerAngles.y, 0.0f);
    }

    // debuging orientation stuff
    //void OnGUI()
    //{
    //    Vector3 p = new Vector3();
    //    Camera c = Camera.main;
    //    Event e = Event.current;
    //    Vector2 mousePos = new Vector2();
    //    Quaternion q = new Quaternion();
    //
    //    // Get the mouse position from Event.
    //    // Note that the y position from Event is inverted.
    //    mousePos.x = Input.mousePosition.x;
    //    mousePos.y = c.pixelHeight - Input.mousePosition.y;
    //
    //    p = c.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, c.nearClipPlane));
    //
    //    GUILayout.BeginArea(new Rect(20, 20, 250, 220));
    //    GUILayout.Label("Screen pixels: " + c.pixelWidth + ":" + c.pixelHeight);
    //    GUILayout.Label("Mouse position: " + mousePos);
    //    GUILayout.Label("World position: " + p.ToString("F3"));
    //    p.y = transform.position.y;
    //    GUILayout.Label("World position at Gun: " + p.ToString("F3"));
    //    GUILayout.Label("Player Position: " + transform.position.ToString("F3"));
    //    q = Quaternion.LookRotation(p - transform.position);//, Vector3.up);
    //    GUILayout.Label("rotation: " + q.eulerAngles.ToString("F3"));
    //    GUILayout.EndArea();
    //}
    void WeaponFireUpdate(Vector3 dir)
    {

        // Fire bullet, left click
        if (Input.GetButton("Fire1") && Time.time > bulletNextFire)
        {
            bulletNextFire = Time.time + bulletFireRate;
            Fire(bulletProjectile, dir);
        }

        // Fire sonar, right click
        else if (Input.GetButton("Fire2") && Time.time > sonarNextFire)
        {
            sonarNextFire = Time.time + sonarFireRate;
            Fire(sonarProjectile, dir);
        }
    }

    // Shoot bullet/sonar
    void Fire(GameObject projectile, Vector3 dir)
    {
        // Spawn gameobject
        GameObject instance = (GameObject)Instantiate(
            projectile,
            projectileSpawn.position,
            projectileSpawn.rotation);
        instance.name = projectile.name;

        // Calculate "forward" TODO: Find last dir moved
        //Vector3 dir = transform.up;

        // Fire projectile
        instance.GetComponent<Rigidbody>().AddForce(fireForce * dir);

        // Destroy in 3.0s
        Destroy(instance, 3.0f);
    }
}
