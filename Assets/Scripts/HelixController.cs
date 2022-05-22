using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixController : MonoBehaviour
{
    private Vector2 lastTapPosition;
    private Vector3 startRotation;

    public Transform topTransform;
    public Transform goalTransform;

    public GameObject helixLevelPrefab;

    public List<Stage> stages = new List<Stage>();

    public float helixDistance;

    private List<GameObject> spawnedLevels = new List<GameObject>();
    private int MAX_PARTS = 12;

    private void Awake()
    {
        startRotation = transform.eulerAngles;
        helixDistance = topTransform.localPosition.y - (goalTransform.localPosition.y + 0.1f);
        LoadStage(0);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 currentTapPosition = Input.mousePosition;

            if (lastTapPosition == Vector2.zero)
            {
                lastTapPosition = currentTapPosition;
            }

            float distance = lastTapPosition.x - currentTapPosition.x;
            lastTapPosition = currentTapPosition;

            transform.Rotate(Vector3.up * distance);
        }

        if (Input.GetMouseButtonUp(0))
        {
            lastTapPosition = Vector2.zero;
        }
    }

    public void LoadStage(int stageNumber)
    {
        Stage stage = stages[Mathf.Clamp(stageNumber, 0, stages.Count - 1)];

        if (stage == null)
        {
            Debug.Log("No Stages");
            return;
        }

        Camera.main.backgroundColor = stages[stageNumber].stageBackgroundColor;

        FindObjectOfType<BallController>().GetComponent<Renderer>().material.color = stages[stageNumber].stageBallColor;

        transform.localEulerAngles = startRotation;

        foreach (GameObject go in spawnedLevels)
        {
            Destroy(go);
        }

        float levelDistance = helixDistance / stage.levels.Count;
        float spawmPosY = topTransform.localPosition.y;

        for (int i = 0; i < stage.levels.Count; i++)
        {
            spawmPosY -= levelDistance;

            GameObject level = Instantiate(helixLevelPrefab, transform);

            level.transform.localPosition = new Vector3(0, spawmPosY, 0);

            spawnedLevels.Add(level);

            int partsToDisable = MAX_PARTS - stage.levels[i].partCount;

            List<GameObject> disabledParts = new List<GameObject>();

            while (disabledParts.Count < partsToDisable)
            {
                GameObject randomPart = level.transform.GetChild(Random.Range(0, level.transform.childCount)).gameObject;
                if (!disabledParts.Contains(randomPart))
                {
                    randomPart.SetActive(false);
                    disabledParts.Add(randomPart);
                }
            }

            List<GameObject> leftParts = new List<GameObject>();
            foreach (Transform t in level.transform)
            {
                t.GetComponent<Renderer>().material.color = stages[stageNumber].stageLevelPartColor;
                if (t.gameObject.activeInHierarchy)
                {
                    leftParts.Add(t.gameObject);
                }
            }

            List<GameObject> deathZones = new List<GameObject>();

            while (deathZones.Count < stage.levels[i].deathZoneCount)
            {
                GameObject randomPart = leftParts[(Random.Range(0, leftParts.Count))];
                if (!deathZones.Contains(randomPart))
                {
                    randomPart.gameObject.AddComponent<DeathZone>();
                    deathZones.Add(randomPart);
                }
            }
        }
    }
}
