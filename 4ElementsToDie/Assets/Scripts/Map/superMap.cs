using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class superMap : MonoBehaviour
{
    private GameObject room;
    private List<GameObject> rooms = new List<GameObject>();
    private List<GameObject> externalObject = new List<GameObject>();

    protected int[,] map;
    protected GameObject miniMap;
    protected float marginX, marginY;

    public GameObject door;
    public GameObject centralRoomObject;
    public GameObject roomGeneratorObject;
    public GameObject bossRoom;
    public GameObject bossDoor;
    public GameObject miniMapObject;

    void Start()
    {

    }

    protected void generateMap(int startX, int startY)
    {
        int x, y, numNewPos, numPossible;
        List<Vector2> newPosition = new List<Vector2>();
        List<Vector2> possibleNewPos = new List<Vector2>();
        newPosition.Add(new Vector2(startX, startY));
        while (newPosition.Count > 0)
        {
            x = (int)newPosition[0].x;
            y = (int)newPosition[0].y;
            numNewPos = Random.Range(1, 100);

            if (numNewPos < 75) numNewPos = 1;
            else if (numNewPos < 85) numNewPos = 2;
            else if (numNewPos < 100) numNewPos = 3;
            possibleNewPos.Clear();
            if (validPosition(x, y + 1))
                possibleNewPos.Add(new Vector2(x, y + 1));
            if (validPosition(x, y - 1))
                possibleNewPos.Add(new Vector2(x, y - 1));
            if (validPosition(x + 1, y))
                possibleNewPos.Add(new Vector2(x + 1, y));
            if (validPosition(x - 1, y))
                possibleNewPos.Add(new Vector2(x - 1, y));

            while (possibleNewPos.Count > 0 && numNewPos > 0)
            {
                numPossible = Random.Range(0, possibleNewPos.Count);
                newPosition.Add(possibleNewPos[numPossible]);
                map[(int)possibleNewPos[numPossible].x, (int)possibleNewPos[numPossible].y] = 2;
                possibleNewPos.Remove(possibleNewPos[numPossible]);
                numNewPos--;
            }
            newPosition.Remove(newPosition[0]);
        }
        allSpaceFull();
    }

    private void addExternalElement()
    {
        int i, j;
        List<Vector2> possibleBossRoom = new List<Vector2>();

        for (i = 0; i < map.GetLength(0); i++)
        {
            for (j = 0; j < map.GetLength(1); j++)
            {
                if (map[i, j] < 0)
                {
                    if (map[i, j] == -1 && ((j < map.GetLength(1) - 1 && map[i, j + 1] >= 2) || (j > 0 && map[i, j - 1] >= 2) || (i < map.GetLength(0) - 1 && map[i + 1, j] >= 2) || (i > 0 && map[i - 1, j] >= 2)))
                        possibleBossRoom.Add(new Vector2(i, j));
                    else if (map[i, j] == -2)
                    {
                        GameObject doorObject = null;
                        if (j < map.GetLength(1) - 1 && map[i, j + 1] >= 2)
                            doorObject = Instantiate(door, new Vector3((j + 1) * 16 + marginX - 7.25f, -i * 10 + marginY, 0f), Quaternion.Euler(0, 0, 90)) as GameObject;
                        else if (j > 0 && map[i, j - 1] >= 2)
                            doorObject = Instantiate(door, new Vector3((j - 1) * 16 + marginX + 7.25f, -i * 10 + marginY, 0), Quaternion.Euler(0, 0, -90)) as GameObject;
                        else if (i < map.GetLength(0) - 1 && map[i + 1, j] >= 2)
                            doorObject = Instantiate(door, new Vector3(j * 16 + marginX, -(i + 1) * 10 + marginY + 4.25f, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
                        else if (i > 0 && map[i - 1, j] >= 2)
                            doorObject = Instantiate(door, new Vector3(j * 16 + marginX, -(i - 1) * 10 + marginY - 4.25f, 0), Quaternion.Euler(0, 0, 180)) as GameObject;

                        if (doorObject != null)
                        {
                            doorObject.transform.parent = transform;
                            doorObject.GetComponent<door>().where = centralRoomObject;
                            externalObject.Add(doorObject);
                        }
                    }
                }
            }
        }
        if (possibleBossRoom.Count > 0)
        {
            Vector3 position;
            Vector2 selected = possibleBossRoom[Random.Range(0, possibleBossRoom.Count)];
            GameObject boosRoomObject = null;
            GameObject doorObject = null;
            i = (int)selected.x;
            j = (int)selected.y;
            map[i, j] = -3;
            if (j < map.GetLength(1) - 1 && map[i, j + 1] >= 2)
            {
                position = new Vector3((j + 1) * 16 + marginX - 20.5f, -i * 10 + marginY, 0f);
                boosRoomObject = Instantiate(bossRoom, position, Quaternion.Euler(0, 0, 0), transform) as GameObject;
                doorObject = Instantiate(bossDoor, new Vector3((j + 1) * 16 + marginX - 7.25f, -i * 10 + marginY, 0f), Quaternion.Euler(0, 0, 90), transform) as GameObject;
                roomGeneratorObject.GetComponent<roomFactory>().getBossEnemy(position, boosRoomObject.transform);
            }
            else if (j > 0 && map[i, j - 1] >= 2)
            {
                position = new Vector3((j - 1) * 16 + marginX + 20.5f, -i * 10 + marginY, 0);
                boosRoomObject = Instantiate(bossRoom, position, Quaternion.Euler(0, 0, 0), transform) as GameObject;
                doorObject = Instantiate(bossDoor, new Vector3((j - 1) * 16 + marginX + 7.25f, -i * 10 + marginY, 0), Quaternion.Euler(0, 0, -90), transform) as GameObject;
                roomGeneratorObject.GetComponent<roomFactory>().getBossEnemy(position, boosRoomObject.transform);
            }
            else if (i < map.GetLength(0) - 1 && map[i + 1, j] >= 2)
            {
                position = new Vector3(j * 16 + marginX, -(i + 1) * 10 + marginY + 13.5f, 0);
                boosRoomObject = Instantiate(bossRoom, position, Quaternion.Euler(0, 0, 0), transform) as GameObject;
                doorObject = Instantiate(bossDoor, new Vector3(j * 16 + marginX, -(i + 1) * 10 + marginY + 4.25f, 0), Quaternion.Euler(0, 0, 0), transform) as GameObject;
                roomGeneratorObject.GetComponent<roomFactory>().getBossEnemy(position, boosRoomObject.transform);
            }
            else if (i > 0 && map[i - 1, j] >= 2)
            {
                position = new Vector3(j * 16 + marginX, -(i - 1) * 10 + marginY - 13.5f, 0);
                boosRoomObject = Instantiate(bossRoom, position, Quaternion.Euler(0, 0, 0), transform) as GameObject;
                doorObject = Instantiate(bossDoor, new Vector3(j * 16 + marginX, -(i - 1) * 10 + marginY - 4.25f, 0), Quaternion.Euler(0, 0, 180), transform) as GameObject;
                roomGeneratorObject.GetComponent<roomFactory>().getBossEnemy(position, boosRoomObject.transform);
            }
            doorObject.GetComponent<door>().where = boosRoomObject;
            externalObject.Add(boosRoomObject);
            externalObject.Add(doorObject);
        }
    }

    private void makeLoopRoom()
    {
        for (int i = 2; i < map.GetLength(0) - 2; i++)
        {
            for (int j = 2; j < map.GetLength(1) - 2; j++)
            {
                if (map[i, j] == 1 && map[i - 1, j] < 2 && map[i - 2, j] < 2 && map[i + 1, j] < 2 && map[i + 2, j] < 2 && map[i, j + 1] >= 2 && map[i, j - 1] >= 2)
                    map[i, j] = 2;
                else if (map[i, j] == 1 && map[i, j - 1] < 2 && map[i, j - 2] < 2 && map[i, j + 1] < 2 && map[i, j + 2] < 2 && map[i + 1, j] >= 2 && map[i - 1, j] >= 2)
                    map[i, j] = 2;
            }
        }
    }

    private void makeBigRoom()
    {
        int numero;
        bool u, d, r, l, ur, ul, dr, dl;
        for (int i = 1; i < map.GetLength(0) - 1; i++)
        {
            for (int j = 1; j < map.GetLength(1) - 1; j++)
            {
                if (map[i, j] == 1)
                {
                    numero = 0;
                    u = d = r = l = ur = ul = dr = dl = false;
                    if (map[i - 1, j] >= 2)
                    {
                        u = true;
                        numero++;
                    }
                    if (map[i + 1, j] >= 2)
                    {
                        d = true;
                        numero++;
                    }
                    if (map[i, j - 1] >= 2)
                    {
                        l = true;
                        numero++;
                    }
                    if (map[i, j + 1] >= 2)
                    {
                        r = true;
                        numero++;
                    }
                    if (map[i - 1, j + 1] >= 2)
                    {
                        ur = true;
                        numero++;
                    }
                    if (map[i + 1, j + 1] >= 2)
                    {
                        dr = true;
                        numero++;
                    }
                    if (map[i - 1, j - 1] >= 2)
                    {
                        ul = true;
                        numero++;
                    }
                    if (map[i + 1, j - 1] >= 2)
                    {
                        dl = true;
                        numero++;
                    }
                    if (numero == 3 || numero == 4)
                    {
                        if (u && r && ur && (numero == 3 || ul || dr))
                        {
                            map[i, j] = 3;
                            map[i - 1, j + 1] = 3;
                            map[i - 1, j] = 3;
                            map[i, j + 1] = 3;
                        }
                        else if (u && l && ul && (numero == 3 || dl || ur))
                        {
                            map[i, j] = 3;
                            map[i - 1, j - 1] = 3;
                            map[i - 1, j] = 3;
                            map[i, j - 1] = 3;
                        }
                        else if (d && r && dr && (numero == 3 || dl || ur))
                        {
                            map[i, j] = 3;
                            map[i + 1, j + 1] = 3;
                            map[i + 1, j] = 3;
                            map[i, j + 1] = 3;
                        }
                        else if (d && l && dl && (numero == 3 || ul || dr))
                        {
                            map[i, j] = 3;
                            map[i + 1, j - 1] = 3;
                            map[i + 1, j] = 3;
                            map[i, j - 1] = 3;
                        }
                    }
                }
            }
        }
    }


    private void allSpaceFull()
    {
        for (int i = 1; i < map.GetLength(0) - 1; i++)
        {
            for (int j = 1; j < map.GetLength(1) - 1; j++)
            {
                if (validPosition(i, j))
                {
                    if (map[i + 1, j] >= 2)
                    {
                        generateMap(i + 1, j);
                        return;
                    }
                    else if (map[i - 1, j] >= 2)
                    {
                        generateMap(i - 1, j);
                        return;
                    }
                    else if (map[i, j + 1] >= 2)
                    {
                        generateMap(i, j + 1);
                        return;
                    }
                    else if (map[i, j - 1] >= 2)
                    {
                        generateMap(i, j - 1);
                        return;
                    }
                }
            }
        }
        makeBigRoom();
        makeLoopRoom();
        addExternalElement();
    }

    private bool validPosition(int x, int y)
    {
        if (x <= 0 || x >= map.GetLength(0) - 1 || y <= 0 || y >= map.GetLength(1) - 1)
            return false;
        if (map[x, y] != 1)
            return false;
        int numero = 0;
        bool u, d, r, l;
        u = d = r = l = true;
        if (map[x - 1, y] >= 2)
        {
            u = false;
            numero++;
        }
        if (map[x + 1, y] >= 2)
        {
            d = false;
            numero++;
        }
        if (map[x, y - 1] >= 2)
        {
            l = false;
            numero++;
        }
        if (map[x, y + 1] >= 2)
        {
            r = false;
            numero++;
        }
        if (numero != 1)
            return false;

        if (map[x - 1, y - 1] >= 2 && u && l)
            return false;
        if (map[x - 1, y + 1] >= 2 && u && r)
            return false;
        if (map[x + 1, y - 1] >= 2 && d && l)
            return false;
        if (map[x + 1, y + 1] >= 2 && d && r)
            return false;
        return true;
    }

    protected void insertObjects()
    {
        miniMap = Instantiate(miniMapObject, transform) as GameObject;

        bool u, d, r, l, ur, ul, dr, dl, door;
        for (int i = 1; i < map.GetLength(0) - 1; i++)
        {
            for (int j = 1; j < map.GetLength(1) - 1; j++)
            {
                if (map[i, j] >= 2)
                {
                    u = d = r = l = ur = ul = dr = dl = door = false;

                    if (map[i - 1, j] >= 2)
                        u = true;
                    if (map[i + 1, j] >= 2)
                        d = true;
                    if (map[i, j - 1] >= 2)
                        l = true;
                    if (map[i, j + 1] >= 2)
                        r = true;
                    if (map[i - 1, j + 1] >= 2)
                        ur = true;
                    if (map[i + 1, j + 1] >= 2)
                        dr = true;
                    if (map[i - 1, j - 1] >= 2)
                        ul = true;
                    if (map[i + 1, j - 1] >= 2)
                        dl = true;

                    if (map[i - 1, j] < -1 || map[i + 1, j] < -1 || map[i, j - 1] < -1 || map[i, j + 1] < -1)
                        door = true;

                    room = roomGeneratorObject.GetComponent<roomFactory>().makeRoom(u, r, l, d, ur, ul, dr, dl, j * 16 + marginX, -i * 10 + marginY, door);
                    if (room != null)
                        rooms.Add(room);
                }
            }
        }
        foreach (GameObject g in rooms)
        {
            g.transform.parent = transform;
        }
    }

    protected void clearMap()
    {
        while (rooms.Count > 0)
        {
            Destroy(rooms[0]);
            rooms.Remove(rooms[0]);
        }
        while (externalObject.Count > 0)
        {
            Destroy(externalObject[0]);
            externalObject.Remove(externalObject[0]);
        }
        Destroy(miniMap);
    }

    public Vector2 getMargin()
    {
        return new Vector2(marginX, marginY);
    }

    public int[,] getMatrix()
    {
        return map;
    }

    public GameObject getMiniMap()
    {
        return miniMap;
    }
}