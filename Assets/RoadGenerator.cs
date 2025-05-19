using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{

    public GameObject RoadPrefab;
    private List<GameObject> roads = new List<GameObject>();
    public float maxSpeed = 10;
    private float speed = 0;
    public int maxRoads = 10;
    public GameObject ObstaclePrefab;
    public float obstacleChance = 1f;
    public bool running = false;
    private GameController game;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        game = GetComponent<GameController>();
        Debug.Log("RoadGenerator Start");
        //ResetLevel();
        //StartLevel();
        CreateNextRoad();
    }

    // Update is called once per frame
    void Update()
    {
        if (!running) return;
        if (speed == 0) return;
        foreach (GameObject road in roads)
        {
            road.transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
        }
        if (roads[0].transform.position.z < -20)
        {
            Destroy(roads[0]);
            roads.RemoveAt(0);
            CreateNextRoad();
        }
    }

    private void CreateNextRoad()
    {
        Vector3 pos = Vector3.zero;
        if (roads.Count > 0)
        {
            pos = roads[roads.Count - 1].transform.position + new Vector3(0, 0, (float)10);
        }
        GameObject road = Instantiate(RoadPrefab, pos, Quaternion.identity);
        if (Random.value < obstacleChance)
        {
            float xOffset = Random.Range(-3f, 6f);
            GameObject obstacle = Instantiate(ObstaclePrefab, Vector3.zero, Quaternion.identity);
            obstacle.transform.SetParent(road.transform);
            obstacle.transform.localPosition = new Vector3(xOffset, 1.6f, 0f);
            obstacle.transform.localScale = new Vector3(Random.Range(1f, 4f), 0.8f, 0.1f);
            Debug.Log("Obstacle created at local pos: " + obstacle.transform.localPosition);
        }
        road.transform.SetParent(transform);
        roads.Add(road);
    }


    public void StartLevel()
    {
        running = true;
        ResetLevel();
        speed = maxSpeed;
    }
    public void StopLevel()
    {
        running = false;
        speed = 0;
    }

    public void ResetLevel()
    {
        speed = 0;
        while (roads.Count > 0)
        {
            Destroy(roads[0]);
            roads.RemoveAt(0);
        }
        for (int i = 0; i < maxRoads; i++)
        {
            CreateNextRoad();
        }
    }
}