using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy02Controller : DadEnemy
{
    private float yMax = 2.5f;
    private bool canMove = true;

    private Rigidbody2D enemyRB;

    [SerializeField] private Transform shotPosition;

    void Start()
    {
        entityVeloc = 1f;
        enemyRB = gameObject.GetComponent<Rigidbody2D>();
        enemyRB.velocity = new Vector2(0f, -entityVeloc);
    }


    void Update()
    {
        Shot();
        Moviment();
    }

    private void Moviment()
    {
        if (transform.position.y < yMax && canMove)
        {
            if (transform.position.x < 0)
            {
                enemyRB.velocity = new Vector2(entityVeloc, 0f);
                canMove = !canMove;
            }
            else
            {
                enemyRB.velocity = new Vector2(-entityVeloc, 0f);
                canMove = !canMove;
            }
        }
    }

    private void Shot()
    {

        var player = FindObjectOfType<PlayerController>();
        if (player)
        {
            if (gameObject.GetComponentInChildren<SpriteRenderer>().isVisible)
            {
                shootWait -= Time.deltaTime;
                if (shootWait <= 0)
                {
                    var shoot = Instantiate(enemyShot, shotPosition.position, transform.rotation);
                    Vector2 direct = player.transform.position - shoot.transform.position;

                    direct.Normalize();
                    shoot.GetComponent<Rigidbody2D>().velocity = direct * shootVeloc;

                    float angle = Mathf.Atan2(direct.y, direct.x) * Mathf.Rad2Deg;
                    shoot.transform.rotation = Quaternion.Euler(0f, 0f, angle + 90f);

                    shootWait = Random.Range(1f, 2.5f);

                    AudioShoot();
                }
            }
        }
    }
}
