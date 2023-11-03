using UnityEngine;
using TMPro;

public class PlayerShooting : MonoBehaviour
{
    // basic shooting settings
    public GameObject bulletPrefab;
    public int maxBullets = 10;
    public int currentBullets;
    public float bulletSpeed = 10f;
    public float fireRate = 0.2f;
    private float nextFireTime;
    private bool isReloading = false;
    public float reloadDelay = 2.0f;
    private float reloadTimer = 0f;

    // sounds
    [SerializeField] private AudioClip sound;
    private AudioSource audioSource;

    //ui
    public TMP_Text ammoCounter;

    private void Start()
    {
        currentBullets = maxBullets;
        audioSource = GetComponent<AudioSource>();

        // Find the UI Text GameObject by tag
        GameObject textObject = GameObject.FindWithTag("AmmoText");

    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime && currentBullets > 0 && isReloading == false)
        {
            // shoot
            Shoot();

            // play sound
            audioSource.clip = sound;
            audioSource.Play();

            // shoot delay
            nextFireTime = Time.time + fireRate;
        }

        // If no bullets left
        if (currentBullets == 0)
        {
            isReloading = true;

            reloadTimer += Time.deltaTime;
            if (reloadTimer >= reloadDelay)
            {
                Reload();
                isReloading = false;
                reloadTimer = 0f;
            }
        }

    }

    private void Shoot()
    {
        // get mouse pos
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePosition - transform.position).normalized;

        // init bullet in mouse direction with bullet speed
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction * bulletSpeed;

        // update bullet coutn

        currentBullets--;

        // update ui
         ammoCounter.text = "Ammo: " + currentBullets.ToString();
    }

    public void IncreasePower(float power)
    {
        fireRate *= power;
    }
    private void Reload()
    {
        currentBullets = maxBullets;
    }

}
