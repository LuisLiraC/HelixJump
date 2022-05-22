using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixGoalController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        GameManager.instance.NextLevel();
    }
}
