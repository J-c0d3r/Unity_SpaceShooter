using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBoss : MonoBehaviour
{

    [SerializeField] private GameObject boss;

    void Start()
    {

    }


    void Update()
    {

    }

    public void CreateBoss()
    {
        Instantiate(boss, transform.position, transform.rotation);
    }

    public void AfterDeathBoss()
    {
        var gm = FindObjectOfType<GameManager>();
        if (gm)
        {
            gm.Menu();
        }
    }
}
