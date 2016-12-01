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

    public GameObject cameraMenager;

    public GameObject obstacleObject;

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

        if (!door)
            generateObstacle().transform.parent = room.transform;

        room.transform.position = new Vector3(x, y, 0);
        room.name = "room";
        return room;
    }

    private GameObject fourEnterRoom(bool ur, bool ul, bool dr, bool dl)
    {
        GameObject room = new GameObject();
        GameObject wall;
        GameObject cameraController;
        if (!ur)
        {
            wall = Instantiate(angleWall, new Vector3(7, 4, 0), Quaternion.Euler(0, 0, -90)) as GameObject;
            wall.transform.parent = room.transform;
        }
        if (!dr)
        {
            wall = Instantiate(angleWall, new Vector3(7, -4, 0), Quaternion.Euler(0, 0, 180)) as GameObject;
            wall.transform.parent = room.transform;
        }
        if (!ul)
        {
            wall = Instantiate(angleWall, new Vector3(-7, 4, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
            wall.transform.parent = room.transform;
        }
        if (!dl)
        {
            wall = Instantiate(angleWall, new Vector3(-7, -4, 0), Quaternion.Euler(0, 0, 90)) as GameObject;
            wall.transform.parent = room.transform;
        }
        cameraController = Instantiate(cameraMenager, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        cameraController.transform.localScale = new Vector3(2, 2, 1);
        cameraController.GetComponent<colliderCameraMenager>().horizontal = true;
        cameraController.GetComponent<colliderCameraMenager>().vertical = true;
        cameraController.transform.parent = room.transform;
        return room;
    }

    private GameObject oneEnterHorizontalRoom()
    {
        GameObject room = new GameObject();
        GameObject wall;
        GameObject cameraController;

        wall = Instantiate(wallVerticalDoubleAngle, new Vector3(-7, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        wall.transform.parent = room.transform;
        wall = Instantiate(wallHorizontalAngle, new Vector3(0, 4, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        wall.transform.parent = room.transform;
        wall = Instantiate(wallHorizontalAngle, new Vector3(0, -4, 0), Quaternion.Euler(180, 0, 0)) as GameObject;
        wall.transform.parent = room.transform;
        cameraController = Instantiate(cameraMenager, new Vector3(4, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        cameraController.transform.localScale = new Vector3(0.935f, 2, 1);
        cameraController.GetComponent<colliderCameraMenager>().horizontal = true;
        cameraController.GetComponent<colliderCameraMenager>().vertical = false;
        cameraController.transform.parent = room.transform;
        cameraController = Instantiate(cameraMenager, new Vector3(-4f, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        cameraController.transform.localScale = new Vector3(0.935f, 2, 1);
        cameraController.GetComponent<colliderCameraMenager>().horizontal = false;
        cameraController.GetComponent<colliderCameraMenager>().vertical = false;
        cameraController.transform.parent = room.transform;

        return room;
    }

    private GameObject oneEnterVerticalRoom()
    {
        GameObject room = new GameObject();
        GameObject wall;
        GameObject cameraController;

        wall = Instantiate(wallHorizontalDoubleAngle, new Vector3(0, 4, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        wall.transform.parent = room.transform;
        wall = Instantiate(wallVerticalAngle, new Vector3(7, 0, 0), Quaternion.Euler(0, 180, 0)) as GameObject;
        wall.transform.parent = room.transform;
        wall = Instantiate(wallVerticalAngle, new Vector3(-7, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        wall.transform.parent = room.transform;
        cameraController = Instantiate(cameraMenager, new Vector3(0, 2f, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        cameraController.transform.localScale = new Vector3(2, 1, 1);
        cameraController.GetComponent<colliderCameraMenager>().horizontal = false;
        cameraController.GetComponent<colliderCameraMenager>().vertical = false;
        cameraController.transform.parent = room.transform;
        cameraController = Instantiate(cameraMenager, new Vector3(0, -3f, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        cameraController.transform.localScale = new Vector3(2, 0.75f, 1);
        cameraController.GetComponent<colliderCameraMenager>().horizontal = false;
        cameraController.GetComponent<colliderCameraMenager>().vertical = true;
        cameraController.transform.parent = room.transform;

        return room;
    }

    private GameObject angleRoom(bool angle)
    {
        GameObject room = new GameObject();
        GameObject wall;
        GameObject cameraController;

        wall = Instantiate(wallHorizontalAngle, new Vector3(0, 4, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        wall.transform.parent = room.transform;
        wall = Instantiate(wallVerticalAngle, new Vector3(-7, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        wall.transform.parent = room.transform;
        if (angle)
        {
            wall = Instantiate(angleWall, new Vector3(7, -4, 0), Quaternion.Euler(0, 0, 180)) as GameObject;
            wall.transform.parent = room.transform;
        }
        cameraController = Instantiate(cameraMenager, new Vector3(-4f, 2f, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        cameraController.transform.localScale = new Vector3(0.935f, 1, 1);
        cameraController.GetComponent<colliderCameraMenager>().horizontal = false;
        cameraController.GetComponent<colliderCameraMenager>().vertical = false;
        cameraController.transform.parent = room.transform;
        cameraController = Instantiate(cameraMenager, new Vector3(4f, 2f, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        cameraController.transform.localScale = new Vector3(0.935f, 1, 1);
        cameraController.GetComponent<colliderCameraMenager>().horizontal = true;
        cameraController.GetComponent<colliderCameraMenager>().vertical = false;
        cameraController.transform.parent = room.transform;
        cameraController = Instantiate(cameraMenager, new Vector3(-4f, -3f, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        cameraController.transform.localScale = new Vector3(0.935f, 0.75f, 1);
        cameraController.GetComponent<colliderCameraMenager>().horizontal = false;
        cameraController.GetComponent<colliderCameraMenager>().vertical = true;
        cameraController.transform.parent = room.transform;
        cameraController = Instantiate(cameraMenager, new Vector3(4f, -3f, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        cameraController.transform.localScale = new Vector3(0.935f, 0.75f, 1);
        cameraController.GetComponent<colliderCameraMenager>().horizontal = true;
        cameraController.GetComponent<colliderCameraMenager>().vertical = true;
        cameraController.transform.parent = room.transform;

        return room;
    }

    private GameObject threeEnterHorizontalRoom(bool angleLeft, bool angleRight)
    {
        GameObject room = new GameObject();
        GameObject wall;
        GameObject cameraController;

        wall = Instantiate(wallHorizontal, new Vector3(0, 4, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        wall.transform.parent = room.transform;
        if (angleLeft)
        {
            wall = Instantiate(angleWall, new Vector3(-7, -4, 0), Quaternion.Euler(0, 0, 90)) as GameObject;
            wall.transform.parent = room.transform;
        }
        if (angleRight)
        {
            wall = Instantiate(angleWall, new Vector3(7, -4, 0), Quaternion.Euler(0, 0, 180)) as GameObject;
            wall.transform.parent = room.transform;
        }
        cameraController = Instantiate(cameraMenager, new Vector3(0, 2, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        cameraController.transform.localScale = new Vector3(2, 1, 1);
        cameraController.GetComponent<colliderCameraMenager>().horizontal = true;
        cameraController.GetComponent<colliderCameraMenager>().vertical = false;
        cameraController.transform.parent = room.transform;
        cameraController = Instantiate(cameraMenager, new Vector3(0, -3, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        cameraController.transform.localScale = new Vector3(2, 0.75f, 1);
        cameraController.GetComponent<colliderCameraMenager>().horizontal = true;
        cameraController.GetComponent<colliderCameraMenager>().vertical = true;
        cameraController.transform.parent = room.transform;

        return room;
    }

    private GameObject threeEnterVerticalRoom(bool angleUp, bool angleDown)
    {
        GameObject room = new GameObject();
        GameObject wall;
        GameObject cameraController;

        wall = Instantiate(wallVertical, new Vector3(7, 0, 0), Quaternion.Euler(0, 0, 180)) as GameObject;
        wall.transform.parent = room.transform;
        if (angleUp)
        {
            wall = Instantiate(angleWall, new Vector3(-7, 4, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
            wall.transform.parent = room.transform;
        }
        if (angleDown)
        {
            wall = Instantiate(angleWall, new Vector3(-7, -4, 0), Quaternion.Euler(0, 0, 90)) as GameObject;
            wall.transform.parent = room.transform;
        }
        cameraController = Instantiate(cameraMenager, new Vector3(4f, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        cameraController.transform.localScale = new Vector3(0.935f, 2, 1);
        cameraController.GetComponent<colliderCameraMenager>().horizontal = false;
        cameraController.GetComponent<colliderCameraMenager>().vertical = true;
        cameraController.transform.parent = room.transform;
        cameraController = Instantiate(cameraMenager, new Vector3(-4f, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        cameraController.transform.localScale = new Vector3(0.935f, 2, 1);
        cameraController.GetComponent<colliderCameraMenager>().horizontal = true;
        cameraController.GetComponent<colliderCameraMenager>().vertical = true;
        cameraController.transform.parent = room.transform;

        return room;
    }

    private GameObject straightHorizontalRoom()
    {
        GameObject room = new GameObject();
        GameObject wall;
        GameObject cameraController;

        wall = Instantiate(wallHorizontal, new Vector3(0, 4, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        wall.transform.parent = room.transform;
        wall = Instantiate(wallHorizontal, new Vector3(0, -4, 0), Quaternion.Euler(0, 0, 180)) as GameObject;
        wall.transform.parent = room.transform;
        cameraController = Instantiate(cameraMenager, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        cameraController.transform.localScale = new Vector3(2, 2, 1);
        cameraController.GetComponent<colliderCameraMenager>().horizontal = true;
        cameraController.GetComponent<colliderCameraMenager>().vertical = false;
        cameraController.transform.parent = room.transform;

        return room;
    }

    private GameObject straightVerticalRoom()
    {
        GameObject room = new GameObject();
        GameObject wall;
        GameObject cameraController;

        wall = Instantiate(wallVertical, new Vector3(7, 0, 0), Quaternion.Euler(0, 0, 180)) as GameObject;
        wall.transform.parent = room.transform;
        wall = Instantiate(wallVertical, new Vector3(-7, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        wall.transform.parent = room.transform;
        cameraController = Instantiate(cameraMenager, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        cameraController.transform.localScale = new Vector3(2, 2, 1);
        cameraController.GetComponent<colliderCameraMenager>().horizontal = false;
        cameraController.GetComponent<colliderCameraMenager>().vertical = true;
        cameraController.transform.parent = room.transform;

        return room;
    }

    protected abstract GameObject generateObstacle();

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
        GameObject obstacle;
        for (int i = 0; i < roomStructure.GetLength(0); i++)
            for (int j = 0; j < roomStructure.GetLength(1); j++)
                if (roomStructure[i, j] > 0)
                {
                    obstacle = Instantiate(obstacleObject, new Vector3(i - 6, j - 3, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
                    obstacle.transform.parent = obstacles.transform;
                }
    }
}
