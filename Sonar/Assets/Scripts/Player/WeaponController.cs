using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject gunObject;

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

    public EnemyController enemyController;

    // sounds
    public AudioClip sonarSfx;
    public AudioClip weaponSfx;
    private AudioSource dj;


    // animation clips
    Animator anim;
    public AnimationClip idleClip;
    public AnimationClip fireClip;
    PlayerGlowAnim glowEffect;
    float delta = 0.0f;
    float duration;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play(idleClip.name);
        duration = fireClip.length;


        projectileSpawn = gunObject.transform;
        dj = GameObject.FindObjectOfType<AudioSource>();

        glowEffect = GetComponentInChildren<PlayerGlowAnim>();
    }

    void Update()
    {
        if (delta > 0.0f)
        {
            delta -= Time.deltaTime;
            if (delta <= 0.0f)
            {
                anim.Play(idleClip.name);
            }
        }
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
            dj.PlayOneShot(weaponSfx);
        }

        // Fire sonar, right click
        else if (Input.GetButton("Fire2") && Time.time > sonarNextFire)
        {
            // Alert enemies
            //enemyController.SendSonar(transform.position);

            sonarNextFire = Time.time + sonarFireRate;
            Fire(sonarProjectile, dir);
            dj.PlayOneShot(sonarSfx);
        }
    }

    // Shoot bullet/sonar
    void Fire(GameObject projectile, Vector3 dir)
    {
        anim.Play(fireClip.name);
        delta = duration;

        glowEffect.Animate();

        // Spawn gameobject
        GameObject instance = (GameObject)Instantiate(
            projectile,
            projectileSpawn.position,
            projectileSpawn.rotation);
        instance.name = projectile.name;

        // Fire projectile
        instance.GetComponent<Rigidbody>().AddForce(fireForce * dir);

        // Destroy in 3.0s
        Destroy(instance, 3.0f);
    }
}
