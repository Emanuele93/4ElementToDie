using UnityEngine;
using System.Collections;

public abstract class roomFactory : MonoBehaviour
{
    public GameObject wallHorizontal;
    public GameObject wallHorizontalAngle;
    public GameObject wallHorizontalDoubleAngle;
    public GameObject wallVertical;
    public GameObject wallVerticalAngle;
    public GameObject wallVerticalDoubleAngle;
    public GameObject angleWall;
    public GameObject floor;

    public GameObject cameraMenager;

    public GameObject obstacleObject;
    public GameObject enemiesActivator;
    public GameObject miniMapActivator;
    public GameObject enemyObjectCollection;
    private GameObject miniMap;

    protected int[,] roomStructure;

    void Start()
    {
        roomStructure = new int[13, 7];
    }

    void Update()
    {

    }

    public GameObject makeRoom(bool u, bool r, bool l, bool d, bool ur, bool ul, bool dr, bool dl, float x, float y, bool door)
    {
        miniMap = transform.parent.gameObject.GetComponent<superMap>().getMiniMap();
        miniMapActivator.GetComponent<miniMapSetter>().controller = miniMap;

        GameObject room = null;

        if (u && r && l && d)
            room = fourEnterRoom(ur, ul, dr, dl);
        else if (l && !u && !r && !d)
        {
            room = oneEnterHorizontalRoom();
            room.transform.eulerAngles = new Vector3(0, 0, 180);
        }
        else if (!l && u && !r && !d)
        {
            room = oneEnterVerticalRoom();
            room.transform.eulerAngles = new Vector3(0, 0, 180);
        }
        else if (!l && !u && r && !d)
        {
            room = oneEnterHorizontalRoom();
            room.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (!l && !u && !r && d)
        {
            room = oneEnterVerticalRoom();
            room.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (l && u && !r && !d)
        {
            room = angleRoom(!ul);
            room.transform.eulerAngles = new Vector3(0, 0, 180);
        }
        else if (l && !u && !r && d)
        {
            room = angleRoom(!dl);
            room.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (!l && u && r && !d)
        {
            room = angleRoom(!ur);
            room.transform.eulerAngles = new Vector3(180, 0, 0);
        }
        else if (!l && !u && r && d)
        {
            room = angleRoom(!dr);
            room.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (l && u && r && !d)
        {
            room = threeEnterHorizontalRoom(!ul, !ur);
            room.transform.eulerAngles = new Vector3(180, 0, 0);
        }
        else if (l && !u && r && d)
        {
            room = threeEnterHorizontalRoom(!dl, !dr);
            room.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (l && u && !r && d)
        {
            room = threeEnterVerticalRoom(!ul, !dl);
            room.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (!l && u && r && d)
        {
            room = threeEnterVerticalRoom(!ur, !dr);
            room.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (!l && u && !r && d)
            room = straightVerticalRoom();
        else if (l && !u && r && !d)
            room = straightHorizontalRoom();

        Instantiate(floor, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), room.transform);

        if (!door)
        {
            generateObstacle().transform.parent = room.transform;
            generateEnemies().transform.parent = room.transform;
        }
        else
            Instantiate(miniMapActivator, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), room.transform);

        room.transform.position = new Vector3(x, y, 0);
        room.name = "room";
        return room;
    }

    private GameObject fourEnterRoom(bool ur, bool ul, bool dr, bool dl)
    {
        GameObject room = new GameObject();
        GameObject cameraController;
        if (!ur)
            Instantiate(angleWall, new Vector3(7, 4, 0), Quaternion.Euler(0, 0, -90), room.transform);
        if (!dr)
            Instantiate(angleWall, new Vector3(7, -4, 0), Quaternion.Euler(0, 0, 180), room.transform);
        if (!ul)
            Instantiate(angleWall, new Vector3(-7, 4, 0), Quaternion.Euler(0, 0, 0), room.transform);
        if (!dl)
            Instantiate(angleWall, new Vector3(-7, -4, 0), Quaternion.Euler(0, 0, 90), room.transform);
        cameraController = Instantiate(cameraMenager, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), room.transform) as GameObject;
        cameraController.transform.localScale = new Vector3(2, 2, 1);
        cameraController.GetComponent<colliderCameraMenager>().horizontal = true;
        cameraController.GetComponent<colliderCameraMenager>().vertical = true;
        return room;
    }

    private GameObject oneEnterHorizontalRoom()
    {
        GameObject room = new GameObject();
        GameObject cameraController;

        Instantiate(wallVerticalDoubleAngle, new Vector3(-7, 0, 0), Quaternion.Euler(0, 0, 0), room.transform);
        Instantiate(wallHorizontalAngle, new Vector3(0, 4, 0), Quaternion.Euler(0, 0, 0), room.transform);
        Instantiate(wallHorizontalAngle, new Vector3(0, -4, 0), Quaternion.Euler(180, 0, 0), room.transform);
        cameraController = Instantiate(cameraMenager, new Vector3(4, 0, 0), Quaternion.Euler(0, 0, 0), room.transform) as GameObject;
        cameraController.transform.localScale = new Vector3(0.935f, 2, 1);
        cameraController.GetComponent<colliderCameraMenager>().horizontal = true;
        cameraController.GetComponent<colliderCameraMenager>().vertical = false;
        cameraController = Instantiate(cameraMenager, new Vector3(-4f, 0, 0), Quaternion.Euler(0, 0, 0), room.transform) as GameObject;
        cameraController.transform.localScale = new Vector3(0.935f, 2, 1);
        cameraController.GetComponent<colliderCameraMenager>().horizontal = false;
        cameraController.GetComponent<colliderCameraMenager>().vertical = false;

        return room;
    }

    private GameObject oneEnterVerticalRoom()
    {
        GameObject room = new GameObject();
        GameObject cameraController;

        Instantiate(wallHorizontalDoubleAngle, new Vector3(0, 4, 0), Quaternion.Euler(0, 0, 0), room.transform);
        Instantiate(wallVerticalAngle, new Vector3(7, 0, 0), Quaternion.Euler(0, 180, 0), room.transform);
        Instantiate(wallVerticalAngle, new Vector3(-7, 0, 0), Quaternion.Euler(0, 0, 0), room.transform);
        cameraController = Instantiate(cameraMenager, new Vector3(0, 2f, 0), Quaternion.Euler(0, 0, 0), room.transform) as GameObject;
        cameraController.transform.localScale = new Vector3(2, 1, 1);
        cameraController.GetComponent<colliderCameraMenager>().horizontal = false;
        cameraController.GetComponent<colliderCameraMenager>().vertical = false;
        cameraController = Instantiate(cameraMenager, new Vector3(0, -3f, 0), Quaternion.Euler(0, 0, 0), room.transform) as GameObject;
        cameraController.transform.localScale = new Vector3(2, 0.75f, 1);
        cameraController.GetComponent<colliderCameraMenager>().horizontal = false;
        cameraController.GetComponent<colliderCameraMenager>().vertical = true;

        return room;
    }

    private GameObject angleRoom(bool angle)
    {
        GameObject room = new GameObject();
        GameObject cameraController;

        Instantiate(wallHorizontalAngle, new Vector3(0, 4, 0), Quaternion.Euler(0, 0, 0), room.transform);
        Instantiate(wallVerticalAngle, new Vector3(-7, 0, 0), Quaternion.Euler(0, 0, 0), room.transform);
        if (angle)
            Instantiate(angleWall, new Vector3(7, -4, 0), Quaternion.Euler(0, 0, 180), room.transform);
        cameraController = Instantiate(cameraMenager, new Vector3(-4f, 2f, 0), Quaternion.Euler(0, 0, 0), room.transform) as GameObject;
        cameraController.transform.localScale = new Vector3(0.935f, 1, 1);
        cameraController.GetComponent<colliderCameraMenager>().horizontal = false;
        cameraController.GetComponent<colliderCameraMenager>().vertical = false;
        cameraController = Instantiate(cameraMenager, new Vector3(4f, 2f, 0), Quaternion.Euler(0, 0, 0), room.transform) as GameObject;
        cameraController.transform.localScale = new Vector3(0.935f, 1, 1);
        cameraController.GetComponent<colliderCameraMenager>().horizontal = true;
        cameraController.GetComponent<colliderCameraMenager>().vertical = false;
        cameraController = Instantiate(cameraMenager, new Vector3(-4f, -3f, 0), Quaternion.Euler(0, 0, 0), room.transform) as GameObject;
        cameraController.transform.localScale = new Vector3(0.935f, 0.75f, 1);
        cameraController.GetComponent<colliderCameraMenager>().horizontal = false;
        cameraController.GetComponent<colliderCameraMenager>().vertical = true;
        cameraController = Instantiate(cameraMenager, new Vector3(4f, -3f, 0), Quaternion.Euler(0, 0, 0), room.transform) as GameObject;
        cameraController.transform.localScale = new Vector3(0.935f, 0.75f, 1);
        cameraController.GetComponent<colliderCameraMenager>().horizontal = true;
        cameraController.GetComponent<colliderCameraMenager>().vertical = true;

        return room;
    }

    private GameObject threeEnterHorizontalRoom(bool angleLeft, bool angleRight)
    {
        GameObject room = new GameObject();
        GameObject cameraController;

        Instantiate(wallHorizontal, new Vector3(0, 4, 0), Quaternion.Euler(0, 0, 0), room.transform);
        if (angleLeft)
            Instantiate(angleWall, new Vector3(-7, -4, 0), Quaternion.Euler(0, 0, 90), room.transform);
        if (angleRight)
            Instantiate(angleWall, new Vector3(7, -4, 0), Quaternion.Euler(0, 0, 180), room.transform);
        cameraController = Instantiate(cameraMenager, new Vector3(0, 2, 0), Quaternion.Euler(0, 0, 0), room.transform) as GameObject;
        cameraController.transform.localScale = new Vector3(2, 1, 1);
        cameraController.GetComponent<colliderCameraMenager>().horizontal = true;
        cameraController.GetComponent<colliderCameraMenager>().vertical = false;
        cameraController = Instantiate(cameraMenager, new Vector3(0, -3, 0), Quaternion.Euler(0, 0, 0), room.transform) as GameObject;
        cameraController.transform.localScale = new Vector3(2, 0.75f, 1);
        cameraController.GetComponent<colliderCameraMenager>().horizontal = true;
        cameraController.GetComponent<colliderCameraMenager>().vertical = true;

        return room;
    }

    private GameObject threeEnterVerticalRoom(bool angleUp, bool angleDown)
    {
        GameObject room = new GameObject();
        GameObject cameraController;

        Instantiate(wallVertical, new Vector3(7, 0, 0), Quaternion.Euler(0, 0, 180), room.transform);
        if (angleUp)
            Instantiate(angleWall, new Vector3(-7, 4, 0), Quaternion.Euler(0, 0, 0), room.transform);
        if (angleDown)
            Instantiate(angleWall, new Vector3(-7, -4, 0), Quaternion.Euler(0, 0, 90), room.transform);
        cameraController = Instantiate(cameraMenager, new Vector3(4f, 0, 0), Quaternion.Euler(0, 0, 0), room.transform) as GameObject;
        cameraController.transform.localScale = new Vector3(0.935f, 2, 1);
        cameraController.GetComponent<colliderCameraMenager>().horizontal = false;
        cameraController.GetComponent<colliderCameraMenager>().vertical = true;
        cameraController = Instantiate(cameraMenager, new Vector3(-4f, 0, 0), Quaternion.Euler(0, 0, 0), room.transform) as GameObject;
        cameraController.transform.localScale = new Vector3(0.935f, 2, 1);
        cameraController.GetComponent<colliderCameraMenager>().horizontal = true;
        cameraController.GetComponent<colliderCameraMenager>().vertical = true;

        return room;
    }

    private GameObject straightHorizontalRoom()
    {
        GameObject room = new GameObject();
        GameObject cameraController;

        Instantiate(wallHorizontal, new Vector3(0, 4, 0), Quaternion.Euler(0, 0, 0), room.transform);
        Instantiate(wallHorizontal, new Vector3(0, -4, 0), Quaternion.Euler(0, 0, 180), room.transform);
        cameraController = Instantiate(cameraMenager, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), room.transform) as GameObject;
        cameraController.transform.localScale = new Vector3(2, 2, 1);
        cameraController.GetComponent<colliderCameraMenager>().horizontal = true;
        cameraController.GetComponent<colliderCameraMenager>().vertical = false;

        return room;
    }

    private GameObject straightVerticalRoom()
    {
        GameObject room = new GameObject();
        GameObject cameraController;

        Instantiate(wallVertical, new Vector3(7, 0, 0), Quaternion.Euler(0, 0, 180), room.transform);
        Instantiate(wallVertical, new Vector3(-7, 0, 0), Quaternion.Euler(0, 0, 0), room.transform);
        cameraController = Instantiate(cameraMenager, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), room.transform) as GameObject;
        cameraController.transform.localScale = new Vector3(2, 2, 1);
        cameraController.GetComponent<colliderCameraMenager>().horizontal = false;
        cameraController.GetComponent<colliderCameraMenager>().vertical = true;

        return room;
    }

    protected abstract GameObject generateObstacle();

    protected GameObject generateEnemies()
    {
        GameObject enemies = Instantiate(miniMapActivator, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;

        int i, j, x, y, activX, activY;
        bool full, stop;

        i = x = Random.Range(0, roomStructure.GetLength(0));
        j = y = Random.Range(0, roomStructure.GetLength(1));
        full = stop = false;

        int difficulty = Random.Range(1, 10);
        if (Random.Range(0, 3) == 0)
        {
            while (roomStructure[i, j] != 0 && !full)
            {
                i++;
                if (i == roomStructure.GetLength(0))
                {
                    i = 0;
                    j++;
                    if (j == roomStructure.GetLength(1))
                        j = 0;
                }
                if (i == x && j == y)
                    full = true;
            }
            if (!full)
            {
                roomStructure[i, j] = 3;
                activX = i;
                activY = j;
                GameObject activator = getChest();
                activator.GetComponent<chestEnemiesActivator>().addItemOnChest(enemyObjectCollection);
                activator.transform.position = new Vector3(activX - 6, activY - 3, 0);
                activator.transform.parent = enemies.transform;
                while (difficulty > 0 && !stop && !full)
                {
                    i = x = Random.Range(0, roomStructure.GetLength(0));
                    j = y = Random.Range(0, roomStructure.GetLength(1));
                    full = false;
                    while (!(roomStructure[i, j] == 0 && (Mathf.Abs(activX - i) > 2 || Mathf.Abs(activY - j) > 2) && !full))
                    {
                        i++;
                        if (i == roomStructure.GetLength(0))
                        {
                            i = 0;
                            j++;
                            if (j == roomStructure.GetLength(1))
                                j = 0;
                        }
                        if (i == x && j == y)
                            full = true;
                    }

                    if (!full)
                    {
                        roomStructure[i, j] = 2;

                        GameObject enemy = getEnemy(difficulty);
                        difficulty -= enemy.GetComponent<tempStatsEnemy>().difficulty;
                        enemy.transform.position = new Vector3(i - 6, j - 3, 0);
                        enemy.transform.parent = enemies.transform;
                        activator.GetComponent<chestEnemiesActivator>().addChild(enemy);
                        enemy.SetActive(false);
                    }
                    if (Random.Range(0, 2) == 0)
                        stop = true;
                }
            }
        }
        while (difficulty > 0 && !full)
        {
            i = x = Random.Range(0, roomStructure.GetLength(0));
            j = y = Random.Range(0, roomStructure.GetLength(1));
            full = false;
            while (!(roomStructure[i, j] == 0 && !full))
            {
                i++;
                if (i == roomStructure.GetLength(0))
                {
                    i = 0;
                    j++;
                    if (j == roomStructure.GetLength(1))
                        j = 0;
                }
                if (i == x && j == y)
                    full = true;
            }

            if (!full)
            {
                roomStructure[i, j] = 2;
                GameObject enemy = getEnemy(difficulty);
                difficulty -= enemy.GetComponent<tempStatsEnemy>().difficulty;
                enemy.transform.position = new Vector3(i - 6, j - 3, 0);
                enemy.transform.parent = enemies.transform;
            }
        }
        return enemies;
    }

    protected abstract GameObject getEnemy(int difficulty);
    protected abstract GameObject getChest();

    protected bool validPoint(int x, int y)
    {
        if (x >= 0 && x < roomStructure.GetLength(0) && y >= 0 && y < roomStructure.GetLength(1))
        {
            if (roomStructure[x, y] > 0)
                return false;
            if (x - 1 >= 0)
                if (roomStructure[x - 1, y] > 0)
                    return false;
            if (x + 1 < roomStructure.GetLength(0))
                if (roomStructure[x + 1, y] > 0)
                    return false;
            if (y - 1 >= 0)
                if (roomStructure[x, y - 1] > 0)
                    return false;
            if (y + 1 < roomStructure.GetLength(1))
                if (roomStructure[x, y + 1] > 0)
                    return false;
            if (x - 1 >= 0 && y - 1 >= 0)
                if (roomStructure[x - 1, y - 1] > 0)
                    return false;
            if (x - 1 >= 0 && y + 1 < roomStructure.GetLength(1))
                if (roomStructure[x - 1, y + 1] > 0)
                    return false;
            if (x + 1 < roomStructure.GetLength(0) && y - 1 >= 0)
                if (roomStructure[x + 1, y - 1] > 0)
                    return false;
            if (x + 1 < roomStructure.GetLength(0) && y + 1 < roomStructure.GetLength(1))
                if (roomStructure[x + 1, y + 1] > 0)
                    return false;

        }
        else return false;
        return true;
    }

    protected void addObstacles(GameObject obstacles)
    {
        for (int i = 0; i < roomStructure.GetLength(0); i++)
            for (int j = 0; j < roomStructure.GetLength(1); j++)
                if (roomStructure[i, j] == 1)
                    Instantiate(obstacleObject, new Vector3(i - 6, j - 3, 0), Quaternion.Euler(0, 0, 0), obstacles.transform);
    }
}