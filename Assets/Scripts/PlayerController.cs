using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("General Setup Settings")]
    [Tooltip("How fast ship moves up and down")] 
    [SerializeField] float controlSpeed = 10f;
    [SerializeField] float xRange = 5f;
    [SerializeField] float yRange = 3.5f;

    [SerializeField] GameObject[] lasers;

    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -10f;

    [SerializeField] float positionYawFactor = 3f;
    [SerializeField] float controlRollFactor = -10f;

    float xThrow;
    float yThrow;
    // Update is called once per frame
    void Update()
    {
        processTranslation();
        processRotation();
        processFiring();

    }


    private void processFiring()
    {
        if (Input.GetButton("Fire1"))
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }
    }

    void SetLasersActive(bool active)
    {

        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = active;
        }
    }


    void processRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueTocontrol = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueTocontrol;

        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void processTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float yOffset = yThrow * Time.deltaTime * controlSpeed;


        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);


        transform.localPosition = new Vector3(clampedXPos,
            transform.localPosition.y, transform.localPosition.z);

        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(transform.localPosition.x,
           rawYPos, transform.localPosition.z);
    }
}
