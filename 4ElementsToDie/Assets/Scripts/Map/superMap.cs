using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class superMap : MonoBehaviour
{
    protected int[,] map;
    private List<GameObject> walls = new List<GameObject>();
    protected int marginX, marginY;

    public GameObject oneEnterRoomHorizontal;
    public GameObject oneEnterRoomVertical;
    public GameObject angleRoom;
    public GameObject straightRoomHorizontal;
    public GameObject straightRoomVertical;
    public GameObject threeEnterRoomHorizontal;
    public GameObject threeEnterRoomVertical;
    public GameObject angleWall;
    public GameObject door;

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

    private void addDoor()
    {
        int i, j;

        j = 0;
        for (i = 0; i < map.GetLength(0); i++)
        {
            if (map[i, j] < 0 && map[i, j + 1] >= 2)
                Instantiate(door, new Vector3((j + 1) * 16 + marginX - 7, -i * 10 + marginY, 0), Quaternion.Euler(0, 0, 90));
        }

        j = map.GetLength(1) - 1;
        for (i = 0; i < map.GetLength(0); i++)
        {
            if (map[i, j] < 0 && map[i, j - 1] >= 2)
                Instantiate(door, new Vector3((j - 1) * 16 + marginX + 7, -i * 10 + marginY, 0), Quaternion.Euler(0, 0, -90));
        }

        i = 0;
        for (j = 0; j < map.GetLength(1); j++)
        {
            if (map[i, j] < 0 && map[i + 1, j] >= 2)
                Instantiate(door, new Vector3(j * 16 + marginX, -(i + 1) * 10 + marginY + 4, 0), Quaternion.Euler(0, 0, 0));
        }

        i = map.GetLength(0) - 1;
        for (j = 0; j < map.GetLength(1); j++)
        {
            if (map[i, j] < 0 && map[i - 1, j] >= 2)
                Instantiate(door, new Vector3(j * 16 + marginX, -(i - 1) * 10 + marginY - 4, 0), Quaternion.Euler(0, 0, 180));
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
        addDoor();
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

    protected void insertWall()
    {
        bool u, d, r, l, ur, ul, dr, dl;
        for (int i = 1; i < map.GetLength(0) - 1; i++)
        {
            for (int j = 1; j < map.GetLength(1) - 1; j++)
            {
                if (map[i, j] >= 2)
                {
                    u = d = r = l = ur = ul = dr = dl = false;
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

                    if (u && r && l && d)
                    {
                        if (!ur)
                            walls.Add(Instantiate(angleWall, new Vector3(j * 16 + marginX + 7, -i * 10 + marginY + 4, 0), Quaternion.Euler(0, 0, 180)) as GameObject);
                        if (!dr)
                            walls.Add(Instantiate(angleWall, new Vector3(j * 16 + marginX + 7, -i * 10 + marginY - 4, 0), Quaternion.Euler(0, 0, 90)) as GameObject);
                        if (!ul)
                            walls.Add(Instantiate(angleWall, new Vector3(j * 16 + marginX - 7, -i * 10 + marginY + 4, 0), Quaternion.Euler(0, 0, -90)) as GameObject);
                        if (!dl)
                            walls.Add(Instantiate(angleWall, new Vector3(j * 16 + marginX - 7, -i * 10 + marginY - 4, 0), Quaternion.Euler(0, 0, 0)) as GameObject);
                    }
                    else if (l && !u && !r && !d)
                    {
                        walls.Add(Instantiate(oneEnterRoomHorizontal, new Vector3(j * 16 + marginX, -i * 10 + marginY, 0), Quaternion.Euler(0, 0, 0)) as GameObject);
                    }
                    else if (!l && u && !r && !d)
                    {
                        walls.Add(Instantiate(oneEnterRoomVertical, new Vector3(j * 16 + marginX, -i * 10 + marginY, 0), Quaternion.Euler(0, 0, 180)) as GameObject);
                    }
                    else if (!l && !u && r && !d)
                    {
                        walls.Add(Instantiate(oneEnterRoomHorizontal, new Vector3(j * 16 + marginX, -i * 10 + marginY, 0), Quaternion.Euler(0, 0, 180)) as GameObject);
                    }
                    else if (!l && !u && !r && d)
                    {
                        walls.Add(Instantiate(oneEnterRoomVertical, new Vector3(j * 16 + marginX, -i * 10 + marginY, 0), Quaternion.Euler(0, 0, 0)) as GameObject);
                    }
                    else if (l && u && !r && !d)
                    {
                        walls.Add(Instantiate(angleRoom, new Vector3(j * 16 + marginX, -i * 10 + marginY, 0), Quaternion.Euler(180, 0, 0)) as GameObject);
                        if (!ul)
                            walls.Add(Instantiate(angleWall, new Vector3(j * 16 + marginX - 7, -i * 10 + marginY + 4, 0), Quaternion.Euler(0, 0, -90)) as GameObject);
                    }
                    else if (l && !u && !r && d)
                    {
                        walls.Add(Instantiate(angleRoom, new Vector3(j * 16 + marginX, -i * 10 + marginY, 0), Quaternion.Euler(0, 0, 0)) as GameObject);
                        if (!dl)
                            walls.Add(Instantiate(angleWall, new Vector3(j * 16 + marginX - 7, -i * 10 + marginY - 4, 0), Quaternion.Euler(0, 0, 0)) as GameObject);
                    }
                    else if (!l && u && r && !d)
                    {
                        walls.Add(Instantiate(angleRoom, new Vector3(j * 16 + marginX, -i * 10 + marginY, 0), Quaternion.Euler(0, 0, 180)) as GameObject);
                        if (!ur)
                            walls.Add(Instantiate(angleWall, new Vector3(j * 16 + marginX + 7, -i * 10 + marginY + 4, 0), Quaternion.Euler(0, 0, 180)) as GameObject);
                    }
                    else if (!l && !u && r && d)
                    {
                        walls.Add(Instantiate(angleRoom, new Vector3(j * 16 + marginX, -i * 10 + marginY, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                        if (!dr)
                            walls.Add(Instantiate(angleWall, new Vector3(j * 16 + marginX + 7, -i * 10 + marginY - 4, 0), Quaternion.Euler(0, 0, 90)) as GameObject);
                    }
                    else if (l && u && r && !d)
                    {
                        walls.Add(Instantiate(threeEnterRoomHorizontal, new Vector3(j * 16 + marginX, -i * 10 + marginY, 0), Quaternion.Euler(0, 0, 180)) as GameObject);
                        if (!ul)
                            walls.Add(Instantiate(angleWall, new Vector3(j * 16 + marginX - 7, -i * 10 + marginY + 4, 0), Quaternion.Euler(0, 0, -90)) as GameObject);
                        if (!ur)
                            walls.Add(Instantiate(angleWall, new Vector3(j * 16 + marginX + 7, -i * 10 + marginY + 4, 0), Quaternion.Euler(0, 0, 180)) as GameObject);
                    }
                    else if (l && !u && r && d)
                    {
                        walls.Add(Instantiate(threeEnterRoomHorizontal, new Vector3(j * 16 + marginX, -i * 10 + marginY, 0), Quaternion.Euler(0, 0, 0)) as GameObject);
                        if (!dl)
                            walls.Add(Instantiate(angleWall, new Vector3(j * 16 + marginX - 7, -i * 10 + marginY - 4, 0), Quaternion.Euler(0, 0, 0)) as GameObject);
                        if (!dr)
                            walls.Add(Instantiate(angleWall, new Vector3(j * 16 + marginX + 7, -i * 10 + marginY - 4, 0), Quaternion.Euler(0, 0, 90)) as GameObject);
                    }
                    else if (l && u && !r && d)
                    {
                        walls.Add(Instantiate(threeEnterRoomVertical, new Vector3(j * 16 + marginX, -i * 10 + marginY, 0), Quaternion.Euler(0, 0, 0)) as GameObject);
                        if (!ul)
                            walls.Add(Instantiate(angleWall, new Vector3(j * 16 + marginX - 7, -i * 10 + marginY + 4, 0), Quaternion.Euler(0, 0, -90)) as GameObject);
                        if (!dl)
                            walls.Add(Instantiate(angleWall, new Vector3(j * 16 + marginX - 7, -i * 10 + marginY - 4, 0), Quaternion.Euler(0, 0, 0)) as GameObject);
                    }
                    else if (!l && u && r && d)
                    {
                        walls.Add(Instantiate(threeEnterRoomVertical, new Vector3(j * 16 + marginX, -i * 10 + marginY, 0), Quaternion.Euler(0, 0, 180)) as GameObject);
                        if (!ur)
                            walls.Add(Instantiate(angleWall, new Vector3(j * 16 + marginX + 7, -i * 10 + marginY + 4, 0), Quaternion.Euler(0, 0, 180)) as GameObject);
                        if (!dr)
                            walls.Add(Instantiate(angleWall, new Vector3(j * 16 + marginX + 7, -i * 10 + marginY - 4, 0), Quaternion.Euler(0, 0, 90)) as GameObject);
                    }
                    else if (!l && u && !r && d)
                    {
                        walls.Add(Instantiate(straightRoomVertical, new Vector3(j * 16 + marginX, -i * 10 + marginY, 0), Quaternion.Euler(0, 0, 0)) as GameObject);
                    }
                    else if (l && !u && r && !d)
                    {
                        walls.Add(Instantiate(straightRoomHorizontal, new Vector3(j * 16 + marginX, -i * 10 + marginY, 0), Quaternion.Euler(0, 0, 0)) as GameObject);
                    }
                }
            }
        }
        foreach(GameObject g in walls)
        {
            g.transform.parent = transform;
        }
    }

    protected void clearMap()
    {
        while(walls.Count > 0)
        {
            Destroy(walls[0]);
            walls.Remove(walls[0]);
        }
    }
}