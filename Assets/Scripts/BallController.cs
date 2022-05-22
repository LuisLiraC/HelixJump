using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float impulseForce = 3f;

    private bool ignoreNextCollision;
    private Vector3 startPosition;

    [HideInInspector]
    public int perfectPass;
    public float superSpeed = 0;

    private bool isSuperSpeedActive;

    private int perfectPassCount = 3;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (ignoreNextCollision) return;

        if (isSuperSpeedActive && !collision.transform.GetComponent<HelixGoalController>())
        {
            Destroy(collision.transform.parent.gameObject, 0.2f);
        }

        DeathZone deatZone = collision.transform.GetComponent<DeathZone>();

        if (deatZone)
        {
            GameManager.instance.RestartLevel();
        }

        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * impulseForce, ForceMode.Impulse);

        ignoreNextCollision = true;
        Invoke("AllowNextCollision", 0.2f);

        perfectPass = 0;
        isSuperSpeedActive = false;
    }

    private void Update()
    {
        if (perfectPass >= perfectPassCount && !isSuperSpeedActive)
        {
            isSuperSpeedActive = true;
            rb.AddForce(Vector3.down * superSpeed, ForceMode.Impulse);
        }
    }

    private void AllowNextCollision()
    {
        ignoreNextCollision = false;
    }

    public void ResetBall()
    {
        transform.position = startPosition;
    }
}
