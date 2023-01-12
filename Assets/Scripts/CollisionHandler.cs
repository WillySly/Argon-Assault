using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float loadDelay = 1f;
    [SerializeField] ParticleSystem explosion;

    PlayerController playerControllers;
    void Start()
    {
        playerControllers = GetComponent<PlayerController>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        startCrashSequence();

    }

    private void startCrashSequence()
    {
        Debug.Log("crash");
        explosion.Play();
        Debug.Log("explosion: " + explosion.gameObject.name + "  is playing " + explosion.isPlaying);
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;

        playerControllers.enabled = false;
        Invoke("ReloadLevel", loadDelay);
        
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
