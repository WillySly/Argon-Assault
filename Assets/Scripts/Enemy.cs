using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] GameObject deathFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] int destroyScore = 5;
    [SerializeField] int hitPoints = 10;
    [SerializeField] int loseOnHit = 5;

    ScoreBoard scoreBoard;
    GameObject parentGameObject;

    // Start is called before the first frame update
    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        parentGameObject = GameObject.FindWithTag("spawnAtRuntime");
        AddRigibody();
    }

    private void AddRigibody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();

        if (hitPoints <= 0)
        {
            KillEnemy();
        }

    }

    private void ProcessHit()
    {
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform; //doesn't really need it
        hitPoints -= loseOnHit;
        scoreBoard.IncreaseScore(destroyScore);
    }

    private void KillEnemy()
    {
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parentGameObject.transform; //doesn't really need it
        Destroy(gameObject);

        scoreBoard.IncreaseScore(2*destroyScore);

    }


}
