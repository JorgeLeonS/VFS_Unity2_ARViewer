using System;
using UnityEngine;

public class DirectARController : MonoBehaviour
{
    static RaycastHit[] s_RaycastResults = new RaycastHit[10];

    const float k_MaxRayDistance = 20.0f;       // How far we raycast into the distance to find a movement destination
    const float k_MinMoveDistance = 0.1f;       // The threshhold of motion where a straight-up jump is checked
    const float k_JumpTestDistance = 0.3f;      // The step size along the collider path to ensure the path is unobstructed
    const float k_GroundLookHeight = 0.25f;     // Offset from the character's position to look for valid ground
    const float k_GroundLockDistance = 0.75f;   // How far down to look on the ground for a valid position

    [SerializeField]
    [Tooltip("How quickly the character reaches full speed")]
    float m_Acceleration = 10.0f;

    [SerializeField]
    [Tooltip("How quickly the character slows to a stop")]
    float m_Deceleration = 10.0f;

    [SerializeField]
    [Tooltip("Character's maximum speed")]
    float m_Speed = 2.0f;

    [SerializeField]
    [Tooltip("Which layers this character considers as valid surfaces")]
    LayerMask m_InteractionLayer = 1 << 29;

    Camera m_Camera;

    Vector3 m_GroundTarget = Vector3.zero;

    Vector3 m_JumpStart = Vector3.zero;
    Vector3 m_JumpTarget = Vector3.zero;
    float m_JumpDuration = 0.0f;
    float m_JumpTimer = 0.0f;
    float m_JumpHeight = 0.0f;
    float m_JumpAngle = 0.0f;

    float m_CurrentSpeed = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_Camera = Camera.main;
        m_GroundTarget = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Lock to ground
        // Find out what surface the object is currently over
        if (Physics.Raycast(transform.position + (Vector3.up * k_GroundLookHeight), Vector3.down, out var rayCastHit, k_GroundLockDistance, m_InteractionLayer))
        {
            transform.position = rayCastHit.point;
        }

        // If we just started interacting with the screen this frame, try to find a valid destination from that point
        if (Input.GetMouseButtonDown(0))
        {
            //if (!GetTapLocation(Input.mousePosition))
            //    break;

            // See if we can move unobstructed to the destination
            var currentPosition = transform.position;
            var destination = s_RaycastResults[0].point;

            var toDestination = (destination - currentPosition);
            var distToDest = toDestination.sqrMagnitude;
            var motionDirection = toDestination.normalized;

            bool successfulPath = true;

            while (distToDest > k_MinMoveDistance * k_MinMoveDistance)
            {
                currentPosition += motionDirection * k_MinMoveDistance;

                // Move along ground in direction of motion in min distance steps
                if (Physics.Raycast(currentPosition + (Vector3.up * k_GroundLookHeight), Vector3.down, out rayCastHit, k_JumpTestDistance, m_InteractionLayer))
                {
                    currentPosition = rayCastHit.point;
                    toDestination = (destination - currentPosition);
                    distToDest = toDestination.sqrMagnitude;
                }
                else
                {
                    successfulPath = false;
                    break;
                }

                // If XZ distance is near 0 but Y still varies, path is not successful either
                var xzOffset = toDestination;
                xzOffset.y = 0;
                if (xzOffset.sqrMagnitude <= k_MinMoveDistance * k_MinMoveDistance && toDestination.y > k_GroundLookHeight)
                {
                    successfulPath = false;
                    break;
                }
            }

            // If we can't make it, initiate a jump.
            //if (!successfulPath)
            //{
            //    m_State = State.JumpStart;
            //    m_JumpTarget = destination;
            //}

            // Otherwise, do nothing and normal motion starts next frame
        }
        else
        {
            // If we're holding input accelerate to the destination, if we're not, slow down
            if (Input.GetMouseButton(0))
            {
                // Raycast from the touch/mouse point to find what, if anything, we have tapped on
                m_GroundTarget = GetPlanarTapLocation(Input.mousePosition, transform.position.y);
                m_CurrentSpeed = Mathf.Clamp(m_CurrentSpeed + (0.5f * m_Acceleration * Time.deltaTime), 0.0f, m_Speed);
            }
            else
            {
                if (m_CurrentSpeed > 0)
                    m_CurrentSpeed = Mathf.Clamp(m_CurrentSpeed - (0.5f * m_Deceleration * Time.deltaTime), 0.0f, m_Speed);
            }


            // Move towards ray hit
            var toHit = (m_GroundTarget - transform.position);
            toHit.y = 0;

            //if (toHit.magnitude < k_MinMoveDistance)
            //    break;
                
            toHit.Normalize();
            transform.forward = toHit;

            var newDestination = transform.position + (toHit * Time.deltaTime * m_CurrentSpeed);

            // Ensure the destination is valid
            if (Physics.Raycast(newDestination + (Vector3.up * k_GroundLookHeight), Vector3.down, out rayCastHit, k_GroundLockDistance, m_InteractionLayer))
                transform.position = rayCastHit.point;
        }
    }

    static int CompareRayHitsByDistance(RaycastHit x, RaycastHit y)
    {
        return x.distance.CompareTo(y.distance);
    }

    bool GetTapLocation(Vector3 tapPosition)
    {
        // Raycast from the touch/mouse point to find what, if anything, we have tapped on
        var screenRay = m_Camera.ScreenPointToRay(tapPosition, Camera.MonoOrStereoscopicEye.Mono);

        var foundHits = Physics.RaycastNonAlloc(screenRay, s_RaycastResults, k_MaxRayDistance, m_InteractionLayer);
        var rayHits = foundHits;

        // Don't allow jumping on ourself
        var rayCounter = 0;

        while (rayCounter < foundHits)
        {
            if (s_RaycastResults[rayCounter].collider.GetComponentInParent<DirectARController>() == this)
            {
                s_RaycastResults[rayCounter].distance = k_MaxRayDistance + 1.0f;
                rayHits--;
            }
            rayCounter++;
        }

        while (rayCounter < s_RaycastResults.Length)
        {
            s_RaycastResults[rayCounter].distance = k_MaxRayDistance + 1.0f;
            rayCounter++;
        }

        Array.Sort(s_RaycastResults, CompareRayHitsByDistance);

        return (rayHits > 0);
    }

    Vector3 GetPlanarTapLocation(Vector3 tapPosition, float targetHeight)
    {
        var screenRay = m_Camera.ScreenPointToRay(tapPosition, Camera.MonoOrStereoscopicEye.Mono);
        var heightDistance = screenRay.origin.y - targetHeight;
        var timeToHeight = -heightDistance / screenRay.direction.y;

        return screenRay.origin + (screenRay.direction * timeToHeight);
    }
}
