using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : DadEnemy
{
    private float stateWait = 5f;
    private float horizontalLimit = 6f;
    private bool right = true;
    private float shootDelay = 1f;
    private float shootWait2 = 1f;
    private int maxLife = 1000;
    private int probState4;

    [SerializeField] private string state = "state3";
    [SerializeField] private string[] states;

    private Rigidbody2D bossRB;
    [SerializeField] private Transform shootPosition1;
    [SerializeField] private Transform shootPosition2;
    [SerializeField] private Transform shootPosition3;
    [SerializeField] private GameObject shoot1;
    [SerializeField] private GameObject shoot2;
    [SerializeField] private Image lifeImage;


    // Start is called before the first frame update
    void Start()
    {
        bossRB = GetComponent<Rigidbody2D>();

        GetComponentInChildren<Canvas>().worldCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeState();
        switch (state)
        {
            case "state1":
                State1();
                break;

            case "state2":
                State2();
                break;

            case "state3":
                State3();
                break;

            case "state4":
                State4();
                break;
        }

        lifeImage.fillAmount = ((float)life / (float)maxLife);
        lifeImage.color = new Color32(140, (byte)(lifeImage.fillAmount * 255), 54, 255);

        IncreaseDifficult();
    }

    private void IncreaseDifficult()
    {
        if (life <= maxLife / 2)
        {
            shootDelay = 0.7f;
        }
    }

    private void State1()
    {
        if (shootWait <= 0f)
        {
            Shoot1();
            shootWait = shootDelay;
        }
        else
        {
            shootWait -= Time.deltaTime;
        }

        if (transform.position.x < horizontalLimit && right)
        {
            bossRB.velocity = new Vector2(entityVeloc, 0f);
            right = true;
        }
        else if (transform.position.x > -horizontalLimit)
        {
            bossRB.velocity = new Vector2(-entityVeloc, 0f);
            right = false;
        }
        else
        {
            right = true;
        }
    }

    private void State2()
    {
        bossRB.velocity = Vector2.zero;
        if (shootWait <= 0f)
        {
            Shoot2();
            shootWait = shootDelay / 2;
        }
        else
        {
            shootWait -= Time.deltaTime;
        }
    }

    private void State3()
    {
        bossRB.velocity = Vector2.zero;
        if (shootWait <= 0f)
        {
            Shoot1();
            shootWait = shootDelay;
        }
        else
        {
            shootWait -= Time.deltaTime;
            if (shootWait2 <= 0f)
            {
                Shoot2();
                shootWait2 = shootDelay;
            }
            else
            {
                shootWait2 -= Time.deltaTime;
            }

        }

    }

    private void State4()
    {
        if (probState4 == 0)
        {
            // bossRB.velocity = Vector2.zero;
            if (shootWait <= 0f)
            {
                Shoot1();
                shootWait = shootDelay;
            }
            else
            {
                shootWait -= Time.deltaTime;
                if (shootWait2 <= 0f)
                {
                    Shoot2();
                    shootWait2 = shootDelay;
                }
                else
                {
                    shootWait2 -= Time.deltaTime;
                }

            }
        }
        else
        {
            // bossRB.velocity = Vector2.zero;
            if (shootWait <= 0f)
            {
                Shoot2();
                shootWait = shootDelay / 2;
            }
            else
            {
                shootWait -= Time.deltaTime;
            }
        }

        if (transform.position.x < horizontalLimit && right)
        {
            bossRB.velocity = new Vector2(entityVeloc, 0f);
            right = true;
        }
        else if (transform.position.x > -horizontalLimit)
        {
            bossRB.velocity = new Vector2(-entityVeloc, 0f);
            right = false;
        }
        else
        {
            right = true;
        }
    }

    private void Shoot1()
    {
        GameObject shoot = Instantiate(shoot1, shootPosition1.position, transform.rotation);
        shoot.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -shootVeloc);

        shoot = Instantiate(shoot1, shootPosition2.position, transform.rotation);
        shoot.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -shootVeloc);

        AudioShoot();
    }

    private void Shoot2()
    {
        var player = FindObjectOfType<PlayerController>();
        if (player)
        {
            var shoot = Instantiate(shoot2, shootPosition3.position, transform.rotation);
            Vector2 direct = player.transform.position - shoot.transform.position;

            direct.Normalize();
            shoot.GetComponent<Rigidbody2D>().velocity = direct * shootVeloc;

            float angle = Mathf.Atan2(direct.y, direct.x) * Mathf.Rad2Deg;
            shoot.transform.rotation = Quaternion.Euler(0f, 0f, angle + 90f);

            AudioShoot();
        }

    }

    private void ChangeState()
    {
        if (stateWait <= 0f)
        {
            int number = Random.Range(0, states.Length);
            int probState4 = Random.Range(0, 2);
            state = states[number];

            stateWait = 5f;
        }
        else
        {
            stateWait -= Time.deltaTime;
        }
    }
}
