using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class OscillatingPlatform : MonoBehaviour
{
    [SerializeField] private Vector2 amplitude = new Vector2(2f, 0f); // Adjust the amplitude of the oscillation
    [SerializeField] private float frequency = 1f; // Adjust the frequency of the oscillation

    private Vector2 initialPosition;
    private Vector2 previousPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        float time = Time.time * frequency;
        float xOffset = Mathf.Sin(time) * amplitude.x;
        float yOffset = Mathf.Cos(time) * amplitude.y;
        previousPosition = transform.position;

        Vector2 newPosition = initialPosition + new Vector2(xOffset, yOffset);
        transform.position = newPosition;
    }
    public Vector2 GetPlatformMovement()
    {
        // Calculate the movement based on the difference between the current and previous positions
        Vector2 platformMovement = (Vector2)transform.position - previousPosition;

        return platformMovement;
    }
}
