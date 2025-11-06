using UnityEngine;

public class LookAheadCameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public float lookAheadDistance = 2f; // How far ahead the camera looks
    private float currentLookAhead = 0f;

    void Update()
    {
        float targetLookAhead = 0f;
        // Calculate target based on player's horizontal velocity
        if (target.GetComponent<Rigidbody2D>().linearVelocity.x > 0.1f)
        {
            targetLookAhead = lookAheadDistance;
        }
        else if (target.GetComponent<Rigidbody2D>().linearVelocity.x < -0.1f)
        {
            targetLookAhead = -lookAheadDistance;
        }

        // Smooth the look ahead
        currentLookAhead = Mathf.Lerp(currentLookAhead, targetLookAhead, smoothSpeed);

        // Calculate camera position
        Vector3 desiredPosition = target.position + new Vector3(currentLookAhead, 0, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }
}