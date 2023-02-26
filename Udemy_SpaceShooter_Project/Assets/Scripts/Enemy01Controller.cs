using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01Controller : DadEnemy
{
    private Rigidbody2D enemyRB;

    [SerializeField] private Transform shotPosition;

    void Start()
    {
        entityVeloc = 1.5f;
        enemyRB = gameObject.GetComponent<Rigidbody2D>();
        enemyRB.velocity = new Vector2(0f, -entityVeloc);
    }

    void Update()
    {
        Shot();
    }

    private void Shot()
    {
        if (gameObject.GetComponentInChildren<SpriteRenderer>().isVisible)
        {
            shootWait -= Time.deltaTime;
            if (shootWait <= 0)
            {
                var shoot = Instantiate(enemyShot, shotPosition.position, transform.rotation);
                shoot.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -shootVeloc);

                shootWait = Random.Range(1.5f, 2.25f);

                AudioShoot();
            }
        }
    }


}
