using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    AudioSource audioSource;
    GameObject gun;
    float horizontalInput;
    float verticalInput;
    public float projectileSpeed = 50;
    public float speed = 5;
    public float rotationSpeed = 180;
    float movingRange = 20;
    int health = 100;
    bool isAlive = true;

    //ENCAPSULATION
    public int Health
    {
        get { return health; }
        set { health = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        gun = GameObject.Find("Gun");
    }

    // Update is called once per frame
    void Update()
    {
        if(isAlive)
        {
            CheckRange();
            Move();
            Shoot();
            CheckHealth();
        }
    }

    //ASTRACTION
    private void CheckRange()
    {
        if (transform.position.x < -movingRange)
        {
            transform.position = new Vector3(-movingRange, transform.position.y, transform.position.z);
        }

        if (transform.position.x > movingRange)
        {
            transform.position = new Vector3(movingRange, transform.position.y, transform.position.z);
        }

        if (transform.position.z < -movingRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -movingRange);
        }

        if (transform.position.z > movingRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, movingRange);
        }
    }

    //ASTRACTION
    private void Move()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(horizontalInput * speed * Time.deltaTime * Vector3.right, Space.World);

        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(verticalInput * speed * Time.deltaTime * Vector3.forward, Space.World);

        if (Input.GetKey(KeyCode.I))
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.U))
        {
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }
    }

    //ASTRACTION
    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject projectile = Instantiate(projectilePrefab, gun.transform.position, transform.rotation);
            projectile.GetComponent<Rigidbody>().AddForce(projectileSpeed * transform.forward, ForceMode.Impulse);
            audioSource.Play();
        }
    }

    //ASTRACTION
    void CheckHealth()
    {
        if (Health <= 0)
        {
            GameManager.SharedInstance.GameOver();
            isAlive = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.Attack(this);
            Destroy(enemy.gameObject);
        }

        if (other.CompareTag("Egg"))
        {
            Egg egg = other.GetComponent<Egg>();

            egg.Damage(this);
            Destroy(other.gameObject);
        }
    }
}
