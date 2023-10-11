using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWander : MonoBehaviour
{
    public float wanderSpeed = 2.0f;
    public float wanderInterval = 0.1f;
    public GameObject planeObject;
    public LayerMask obstacleLayer;
    public float changeTime;
    public float changerate;
    private Vector3 initialPosition;
    public Vector3 minBounds;
    public Vector3 maxBounds;
    public bool checkplayer;
    Vector3 targetPosition;
    private void Start()
    {
        initialPosition = transform.position;
        wanderSpeed = 40f;
        obstacleLayer = LayerMask.GetMask("Obstacle", "Player");
        planeObject = GameObject.FindWithTag("Plane");
        CalculateBounds();
        StartCoroutine(Wander());
    }


    private void CalculateBounds()
    {
        Renderer planeRenderer = planeObject.GetComponent<Renderer>();
        minBounds = planeRenderer.bounds.min;
        maxBounds = planeRenderer.bounds.max;
    }

    public void Update()
    {
        checkplayer = CheckForObstacles();
    }
    private IEnumerator Wander()
    {
        while (true)
        {
            targetPosition = GetRandomTarget();
            changeTime = Time.time;
            changerate = Random.Range(40f, 75f);
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f && changeTime >= Time.time)
            {
                if (CheckForObstacles())
                {
                    targetPosition = GetRandomTarget();
                }
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, wanderSpeed * Time.deltaTime);
                transform.position = new Vector3(
                 Mathf.Clamp(transform.position.x, minBounds.x, maxBounds.x),
                 transform.position.y,
                 Mathf.Clamp(transform.position.z, minBounds.z, maxBounds.z)
             );
                Vector3 direction = (targetPosition - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
                changeTime = Time.time + 1 / changerate;
                yield return null;
            }

            yield return new WaitForSeconds(wanderInterval);
        }
    }

    private Vector3 GetRandomTarget()
    {
        float randomX = Random.Range(minBounds.x + transform.lossyScale.x / 2, maxBounds.x - transform.lossyScale.x / 2);
        float randomZ = Random.Range(minBounds.z + transform.lossyScale.z / 2, maxBounds.z - transform.lossyScale.z / 2);

        return new Vector3(randomX, transform.position.y, randomZ);
    }

    private bool CheckForObstacles()
    {
        Vector3 origin = transform.position;
        Vector3 halfExtents = new Vector3(transform.lossyScale.x / 2, transform.lossyScale.y / 2, transform.lossyScale.z / 2);
        Quaternion orientation = transform.rotation;
        Debug.DrawLine(transform.position, targetPosition, Color.red);
        if (Physics.BoxCast(origin, halfExtents, transform.forward, out RaycastHit hit, orientation, 30f, obstacleLayer))
        {
            return true;
        }

        return false;
    }
}
