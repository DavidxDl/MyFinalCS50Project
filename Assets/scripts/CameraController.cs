using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform duck;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private float transitionSpeed = 5.0f;
    private CinemachineFramingTransposer framingTransposer;
    private float onPlataformScreenY = 0.41f;
    private float onGroundScreenY = 0.71f;

    public float transitionDuration = 0.5f; // Duration of the transition in seconds

    private float transitionTimer; // Timer for tracking the transition progress
    private float initialScreenY; // Starting Screen Y value for the transition
    private float targetScreenY; // Target Screen Y value for the transition


    private void Start()
    {
        framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();

        // Initialize initial and target Screen Y values
        initialScreenY = framingTransposer.m_ScreenY;
        targetScreenY = initialScreenY;
    }
    private void Update()
    {
        float targetScreenY = duck.position.y > 2 ? framingTransposer.m_ScreenY = onPlataformScreenY : framingTransposer.m_ScreenY = onGroundScreenY;

        // Update the transition timer
        transitionTimer += Time.deltaTime;

        // Calculate the current Screen Y value based on the transition progress
        float newScreenY = Mathf.Lerp(initialScreenY, targetScreenY, transitionTimer / transitionDuration);
        framingTransposer.m_ScreenY = newScreenY;

        // Reset the transition timer if the target has changed
        if (initialScreenY != targetScreenY)
        {
            transitionTimer = 0f;
            initialScreenY = newScreenY;
        }



    }


    public void NextLevel()
    {
        GameManager.instance.NextLevel();
    }
}
