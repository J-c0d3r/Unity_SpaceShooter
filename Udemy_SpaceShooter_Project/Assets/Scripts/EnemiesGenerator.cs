using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesGenerator : MonoBehaviour
{
    [SerializeField] private int points = 0;
    private int baseLvl = 100;
    private bool isAnimBoss = false;
    [SerializeField] private int level = 1;
    [SerializeField] private float enemyWait = 0f;
    [SerializeField] private int eneAmount = 0;
    [SerializeField] private float waitTime = 2f;

    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject bossAnimation;
    [SerializeField] private Text pointsTxt;
    [SerializeField] private AudioClip songBoss;
    [SerializeField] private AudioSource songGame;


    void Start()
    {

    }

    void Update()
    {
        if (level < 10)
        {
            EnemyGenerator();
        }
        else
        {
            BossGenerator();
        }

        pointsTxt.text = points.ToString();
    }

    private void BossGenerator()
    {
        if (eneAmount <= 0 && waitTime > 0)
        {
            waitTime -= Time.deltaTime;
        }
        if (!isAnimBoss && waitTime <= 0)
        {
            GameObject animBoss = Instantiate(bossAnimation, Vector3.zero, transform.rotation);
            Destroy(animBoss, 6f);
            isAnimBoss = true;

            songGame.clip = songBoss;
            songGame.Play();
        }

    }

    public void GainPoint(int points)
    {
        this.points += points * level;

        if (this.points > baseLvl)
        {
            level++;
            baseLvl = (int)(baseLvl * 2);
        }
    }

    public void DecreaseAmount()
    {
        eneAmount--;
    }

    private void EnemyGenerator()
    {
        if (enemyWait > 0 && eneAmount <= 0)
        {
            enemyWait -= Time.deltaTime;
        }

        if (enemyWait <= 0f && eneAmount <= 0)
        {
            int amount = level * 9;
            int tries = 0;

            while (eneAmount < amount)
            {
                tries++;
                if (tries > 500)
                {
                    break;
                }

                GameObject enemyCreated;

                float chance = Random.Range(0f, level);
                if (chance > 0.6 * level && level > 2)
                {
                    enemyCreated = enemies[1];
                }
                else
                {
                    enemyCreated = enemies[0];
                }

                Vector3 position = new Vector3(Random.Range(-8f, 8f), Random.Range(5.7f, 20f), 0f);

                bool collision = PositionCheck(position, enemyCreated.transform.localScale);

                if (collision)
                {
                    Instantiate(enemyCreated, position, transform.rotation);
                    eneAmount++;
                }

                enemyWait = waitTime;
            }
        }
    }

    private bool PositionCheck(Vector3 position, Vector3 size)
    {
        Collider2D hit = Physics2D.OverlapBox(position, size, 0f);

        if (hit == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
