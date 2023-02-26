using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    [SerializeField] private AudioClip audioExplosion;
    [SerializeField] private bool isExplosion = false;


    void Start()
    {
        if (transform.position.y > -5.5f && isExplosion)
        {
            AudioSource.PlayClipAtPoint(audioExplosion, Vector3.zero);
        }
    }


    void Update()
    {

    }

    public void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
