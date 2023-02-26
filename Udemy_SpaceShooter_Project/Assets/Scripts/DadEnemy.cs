using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DadEnemy : MonoBehaviour
{
    [SerializeField] protected float entityVeloc;
    [SerializeField] protected int life = 1;
    [SerializeField] protected int points = 10;
    [SerializeField] protected float shootWait = 1f;
    [SerializeField] protected float shootVeloc = 5f;
    [SerializeField] protected float itemRate = 0.7f;
    [SerializeField] protected GameObject explosion;
    [SerializeField] protected GameObject enemyShot;
    [SerializeField] protected GameObject powerUp;
    [SerializeField] protected AudioClip audioShoot;


    void Start()
    {

    }


    void Update()
    {

    }

    protected void AudioShoot()
    {
        AudioSource.PlayClipAtPoint(audioShoot, Vector3.zero);
    }

    public void TakeDamage(int damage)
    {
        if (transform.position.y < 5f)
        {
            life -= damage;

            if (life <= 0)
            {
                Destroy(gameObject);
                Instantiate(explosion, transform.position, transform.rotation);

                if (powerUp)
                {
                    DropItem();
                }

                var gerador = FindObjectOfType<EnemiesGenerator>();
                //gerador.DecreaseAmount();
                if (gerador)
                {
                    gerador.GainPoint(points);
                }
            }
        }
    }
    private void OnDestroy()
    {
        var gerador = FindObjectOfType<EnemiesGenerator>();
        if (gerador)
        {
            gerador.DecreaseAmount();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Destroyer"))
        {
            Destroy(gameObject);
            var gerador = FindObjectOfType<EnemiesGenerator>();
            //gerador.DecreaseAmount();
            Instantiate(explosion, transform.position, transform.rotation);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            var gerador = FindObjectOfType<EnemiesGenerator>();
            //gerador.DecreaseAmount();
            Instantiate(explosion, transform.position, transform.rotation);

            other.gameObject.GetComponent<PlayerController>().TakeLife(1);
            DropItem();
        }
    }

    private void DropItem()
    {
        float chance = Random.Range(0f, 1f);

        if (chance > itemRate)
        {
            GameObject pUp = Instantiate(powerUp, transform.position, transform.rotation);
            Destroy(pUp, 5f);
            Vector2 dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            pUp.GetComponent<Rigidbody2D>().velocity = dir;
        }
    }
}
