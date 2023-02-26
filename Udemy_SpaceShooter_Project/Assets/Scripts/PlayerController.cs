using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private int life = 5;
    private float veloc = 10f;
    private float horizontal;
    private float vertical;
    private float shootVeloc = 10f;
    private float xLimit = 8.3f;
    private float yLimit = 4.4f;
    private bool isShield = false;
    private float shieldTimer = 0f;
    private int shieldQty = 3;

    private Rigidbody2D playerRB;
    private GameObject shieldNow;


    [SerializeField] private int shootLvl = 1;

    [SerializeField] private GameObject shootGB;
    [SerializeField] private GameObject shootGB2;
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject shield;
    [SerializeField] private Transform shotPosition;
    [SerializeField] private Text lifeTxt;
    [SerializeField] private Text shieldTxt;
    [SerializeField] private AudioClip audioShoot;
    // [SerializeField] private AudioClip audioDeath;
    [SerializeField] private AudioClip audioShieldPlus;
    [SerializeField] private AudioClip audioShield;
    [SerializeField] private AudioClip audioShieldDown;
    [SerializeField] private AudioClip audioPowerUp;


    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();

        lifeTxt.text = life.ToString();
        shieldTxt.text = shieldQty.ToString();
    }

    void Update()
    {
        Move();
        Fire();
        Shield();
    }

    private void Move()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        Vector2 newVeloc = new Vector2(horizontal, vertical);
        newVeloc.Normalize();
        playerRB.velocity = newVeloc * veloc;

        float playerX = Mathf.Clamp(transform.position.x, -xLimit, xLimit);
        float playerY = Mathf.Clamp(transform.position.y, -yLimit, yLimit);

        transform.position = new Vector3(playerX, playerY, transform.position.z);
    }

    private void Shield()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isShield && shieldQty > 0)
        {
            shieldNow = Instantiate(shield, transform.position, transform.rotation);
            isShield = true;
            shieldQty--;

            shieldTxt.text = shieldQty.ToString();

            AudioSource.PlayClipAtPoint(audioShield, Vector3.zero);
        }

        if (shieldNow)
        {
            shieldNow.transform.position = transform.position;

            shieldTimer += Time.deltaTime;
            if (shieldTimer > 5.2f)
            {
                Destroy(shieldNow);
                isShield = false;
                shieldTimer = 0f;
                AudioSource.PlayClipAtPoint(audioShieldDown, Vector3.zero);
            }

        }
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            AudioSource.PlayClipAtPoint(audioShoot, Vector3.zero);
            switch (shootLvl)
            {
                case 1:
                    ShootCreate(shootGB, shotPosition.position);
                    break;

                case 2:
                    Vector3 position = new Vector3(transform.position.x - 0.45f, transform.position.y + 0.1f, 0f);
                    ShootCreate(shootGB2, position);
                    position = new Vector3(transform.position.x + 0.45f, transform.position.y + 0.1f, 0f);
                    ShootCreate(shootGB2, position);
                    break;

                case 3:
                    ShootCreate(shootGB, shotPosition.position);

                    position = new Vector3(transform.position.x - 0.45f, transform.position.y + 0.1f, 0f);
                    ShootCreate(shootGB2, position);
                    position = new Vector3(transform.position.x + 0.45f, transform.position.y + 0.1f, 0f);
                    ShootCreate(shootGB2, position);
                    break;
            }


        }
    }

    private void ShootCreate(GameObject createdShoot, Vector3 position)
    {
        GameObject shoot = Instantiate(createdShoot, position, transform.rotation);
        shoot.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, shootVeloc);
    }

    public void TakeLife(int damage)
    {
        life -= damage;
        lifeTxt.text = life.ToString();

        if (life <= 0)
        {
            Destroy(gameObject);
            if (shieldNow)
            {
                Destroy(shieldNow);
            }

            Instantiate(explosion, transform.position, transform.rotation);

            var gameManager = FindObjectOfType<GameManager>();
            if (gameManager)
                gameManager.Menu();

            // AudioSource.PlayClipAtPoint(audioDeath, new Vector3(0f, 0f, -10f));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PowerUp"))
        {
            if (shootLvl < 3)
            {
                shootLvl++;
                AudioSource.PlayClipAtPoint(audioPowerUp, new Vector3(0f, 0f, -10f));
            }
            else
            {
                shieldQty++;
                shieldTxt.text = shieldQty.ToString();
                AudioSource.PlayClipAtPoint(audioShieldPlus, new Vector3(0f, 0f, -10f));
            }
            Destroy(other.gameObject);
        }

    }
}
