using UnityEngine;
using System.Collections;

public class miniMapContoller : MonoBehaviour
{
    public GameObject room;
    public Sprite completedRoom;
    public Sprite enteredRoom;
    private Vector2 margin;
    private GameObject[,] mapObjects;
    private int[,] map;

    // Use this for initialization
    void Start()
    {
        margin = transform.parent.gameObject.GetComponent<superMap>().getMargin();
        map = transform.parent.gameObject.GetComponent<superMap>().getMatrix();
        mapObjects = new GameObject[map.GetLength(0), map.GetLength(1)];
        room.transform.localScale = new Vector3(3 / (float)map.GetLength(1), 3 / (float)map.GetLength(0), 1f);
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (map[i, j] > 1)
                {
                    mapObjects[i, j] = Instantiate(room, new Vector3(j * 3 / (float)map.GetLength(1) - 1.4f, -i * 3 / (float)map.GetLength(0) + 1.4f, 0), room.transform.rotation, transform) as GameObject;
                    mapObjects[i, j].SetActive(false);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Camera.main.transform.position.x + 7.4f, Camera.main.transform.position.y + 3.5f, 0);
    }

    public void newPosition(float x, float y)
    {
        x = (x - margin.x) / 16;
        if (x < 0) x = -x;
        y = (y - margin.y) / 10;
        if (y < 0) y = -y;
        mapObjects[(int)y, (int)x].GetComponent<SpriteRenderer>().sprite = enteredRoom;
        mapObjects[(int)y, (int)x].SetActive(true);
        if (map[(int)y, (int)x - 1] > 1) mapObjects[(int)y, (int)x - 1].SetActive(true);
        if (map[(int)y, (int)x + 1] > 1) mapObjects[(int)y, (int)x + 1].SetActive(true);
        if (map[(int)y - 1, (int)x] > 1) mapObjects[(int)y - 1, (int)x].SetActive(true);
        if (map[(int)y + 1, (int)x] > 1) mapObjects[(int)y + 1, (int)x].SetActive(true);
    }

    public void finishPosition(float x, float y)
    {
        x = (x - margin.x) / 16;
        if (x < 0) x = -x;
        y = (y - margin.y) / 10;
        if (y < 0) y = -y;
        mapObjects[(int)y, (int)x].GetComponent<SpriteRenderer>().sprite = completedRoom;
    }
}
