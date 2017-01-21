using UnityEngine;
using System.Collections;

public class miniMapContoller : MonoBehaviour
{
    public GameObject room;
    public Sprite initialRoom;
    public Sprite bossRoom;
    public Sprite chestRoom;
    public Sprite completedRoom;
    public Sprite enteredRoom;
    private Vector2 margin;
    private GameObject[,] mapObjects;
    private int[,] map;
    public GameObject playerObject;
    private GameObject player;

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
        playerObject.transform.localScale = new Vector3(3 / (float)map.GetLength(1), 3 / (float)map.GetLength(0), 1f);
        player = Instantiate(playerObject, new Vector3(0, 0, 0), playerObject.transform.rotation, transform) as GameObject;
        player.name = "miniMapPlayer";
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Camera.main.transform.position.x + 7.4f, Camera.main.transform.position.y + 3.5f, 0);
    }

    public void newPosition(float x, float y, bool chest)
    {
        x = (x - margin.x) / 16;
        if (x < 0) x = -x;
        y = (y - margin.y) / 10;
        if (y < 0) y = -y;
        int i = (int)y;
        int j = (int)x;
        if (chest)
            mapObjects[i, j].GetComponent<SpriteRenderer>().sprite = chestRoom;
        else if (map[i + 1, j] == -2)
        {
            mapObjects[i, j].GetComponent<SpriteRenderer>().sprite = initialRoom;
            mapObjects[i, j].transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (map[i - 1, j] == -2)
        {
            mapObjects[i, j].GetComponent<SpriteRenderer>().sprite = initialRoom;
            mapObjects[i, j].transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if (map[i, j + 1] == -2)
        {
            mapObjects[i, j].GetComponent<SpriteRenderer>().sprite = initialRoom;
            mapObjects[i, j].transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (map[i, j - 1] == -2)
        {
            mapObjects[i, j].GetComponent<SpriteRenderer>().sprite = initialRoom;
            mapObjects[i, j].transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else if (map[i + 1, j] == -3 || map[i - 1, j] == -3 || map[i, j + 1] == -3 || map[i, j - 1] == -3)
            mapObjects[i, j].GetComponent<SpriteRenderer>().sprite = bossRoom;
        else mapObjects[(int)y, (int)x].GetComponent<SpriteRenderer>().sprite = enteredRoom;
        mapObjects[(int)y, (int)x].SetActive(true);
        player.transform.position = mapObjects[(int)y, (int)x].transform.position;
        if (map[(int)y, (int)x - 1] > 1) mapObjects[(int)y, (int)x - 1].SetActive(true);
        if (map[(int)y, (int)x + 1] > 1) mapObjects[(int)y, (int)x + 1].SetActive(true);
        if (map[(int)y - 1, (int)x] > 1) mapObjects[(int)y - 1, (int)x].SetActive(true);
        if (map[(int)y + 1, (int)x] > 1) mapObjects[(int)y + 1, (int)x].SetActive(true);
    }
    /*
    public void finishPosition(float x, float y)
    {
        x = (x - margin.x) / 16;
        if (x < 0) x = -x;
        y = (y - margin.y) / 10;
        if (y < 0) y = -y;
        mapObjects[(int)y, (int)x].GetComponent<SpriteRenderer>().sprite = completedRoom;
    }
    */
    public void movePlayer(float x, float y)
    {
        x = (x - margin.x) / 16;
        if (x < 0) x = -x;
        y = (y - margin.y) / 10;
        if (y < 0) y = -y;
        player.transform.position = mapObjects[(int)y, (int)x].transform.position;
    }
}
