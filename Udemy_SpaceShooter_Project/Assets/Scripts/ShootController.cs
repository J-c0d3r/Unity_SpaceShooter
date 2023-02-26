using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    private Rigidbody2D shootRB;

    [SerializeField] private GameObject shootImpact;
    [SerializeField] private int damage = 1;


    void Start()
    {
        shootRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<DadEnemy>().TakeDamage(damage);
        }

        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().TakeLife(damage);
        }

        Destroy(gameObject);

        Instantiate(shootImpact, transform.position, transform.rotation);
    }
}
