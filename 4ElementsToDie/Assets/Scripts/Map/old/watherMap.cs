using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class watherMap : MonoBehaviour {

    private int[,] map;
    private int marginX, marginY;

    public GameObject wallHori;
    public GameObject wallVert;
    public GameObject wallHoriDoubleAngle;
    public GameObject wallVertDoubleAngle;
    public GameObject wallHoriAngle;
    public GameObject wallVertAngle;
    public GameObject wallAngle;

    void Start()
    {
        marginX = -54;
        marginY = 116;

        map = new int[,]
        {
            { 0, 0, 1, 1, 1, 0, 0},
            { 0, 1, 1, 1, 1, 1, 0},
            { 0, 1, 1, 1, 1, 1, 0},
            { 1, 1, 1, 1, 1, 1, 1},
            { 1, 1, 1, 1, 1, 1, 1},
            { 1, 1, 1, 1, 1, 1, 1},
            { 0, 1, 1, 1, 1, 1, 0},
            { 0, 1, 1, 1, 1, 1, 0},
            { 0, 0, 1, 1, 1, 0, 0},
            { 0, 0, 0, 2, 0, 0, 0}
        };

        generateMap();

        insertWall();
    }

    private void generateMap()
    {
        int x, y, numNewPos, numPossible;
        List<Vector2> newPosition = new List<Vector2>();
        List<Vector2> possibleNewPos = new List<Vector2>();
        newPosition.Add(new Vector2(9, 3)); //must be coerent with map
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
    }

    private bool validPosition(int x, int y)
    {
        if (x < 0 || x > map.GetLength(0) - 1 || y < 0 || y > map.GetLength(1) - 1)
            return false;
        int numero = 0;
        if (map[x, y] != 1)
            return false;
        if (x > 0)
            if (map[x - 1, y] == 2)
                numero++;
        if (x < map.GetLength(0) - 1)
            if (map[x + 1, y] == 2)
                numero++;
        if (y > 0)
            if (map[x, y - 1] == 2)
                numero++;
        if (y < map.GetLength(1) - 1)
            if (map[x, y + 1] == 2)
                numero++;
        if (numero > 1)
            return false;
        return true;
    }

    private void insertWall()
    {
        int i, j;

        i = 0;
        j = 0;
        if (map[i, j] == 2)
        {
            if (map[i, j + 1] == 2 && map[i + 1, j] != 2)
            {
                wallTopAngleLeft(i, j);
                wallDownAngleLeft(i, j);
                wallLeftDoubleAngle(i, j);
            }
            else if (map[i, j + 1] != 2 && map[i + 1, j] == 2)
            {
                wallRightAngleTop(i, j);
                wallLeftAngleTop(i, j);
                wallTopDoubleAngle(i, j);
            }
            else if (map[i, j + 1] == 2 && map[i + 1, j] == 2)
            {
                angleDownRight(i, j);
                wallLeftAngleTop(i, j);
                wallTopAngleLeft(i, j);
            }
        }

        i = 0;
        j = map.GetLength(1) - 1;
        if (map[i, j] == 2)
        {
            if (map[i, j - 1] == 2 && map[i + 1, j] != 2)
            {
                wallTopAngleRight(i, j);
                wallDownAngleRight(i, j);
                wallRightDoubleAngle(i, j);
            }
            else if (map[i, j - 1] != 2 && map[i + 1, j] == 2)
            {
                wallRightAngleTop(i, j);
                wallLeftAngleTop(i, j);
                wallTopDoubleAngle(i, j);
            }
            else if (map[i, j - 1] == 2 && map[i + 1, j] == 2)
            {
                angleDownLeft(i, j);
                wallTopAngleRight(i, j);
                wallRightAngleTop(i, j);
            }
        }

        i = map.GetLength(0) - 1;
        j = 0;
        if (map[i, j] == 2)
        {
            if (map[i - 1, j] == 2 && map[i, j + 1] != 2)
            {
                wallRightAngleDown(i, j);
                wallLeftAngleDown(i, j);
                wallDownDoubleAngle(i, j);
            }
            else if (map[i - 1, j] != 2 && map[i, j + 1] == 2)
            {
                wallTopAngleLeft(i, j);
                wallDownAngleLeft(i, j);
                wallLeftDoubleAngle(i, j);
            }
            else if (map[i - 1, j] == 2 && map[i, j + 1] == 2)
            {
                angleTopRight(i, j);
                wallLeftAngleDown(i, j);
                wallDownAngleLeft(i, j);
            }
        }

        i = map.GetLength(0) - 1;
        j = map.GetLength(1) - 1;
        if (map[i, j] == 2)
        {
            if (map[i, j - 1] == 2 && map[i - 1, j] != 2)
            {
                wallTopAngleRight(i, j);
                wallDownAngleRight(i, j);
                wallRightDoubleAngle(i, j);
            }
            else if (map[i, j - 1] != 2 && map[i - 1, j] == 2)
            {
                wallRightAngleDown(i, j);
                wallLeftAngleDown(i, j);
                wallDownDoubleAngle(i, j);
            }
            else if (map[i, j - 1] == 2 && map[i - 1, j] == 2)
            {
                angleTopLeft(i, j);
                wallDownAngleRight(i, j);
                wallRightAngleDown(i, j);
            }
        }

        i = 0;
        for (j = 1; j < map.GetLength(1) - 1; j++)
        {
            if (map[i, j] == 2)
            {
                if (map[i, j - 1] == 2 && map[i, j + 1] != 2 && map[i + 1, j] != 2)
                {
                    wallTopAngleRight(i, j);
                    wallDownAngleRight(i, j);
                    wallRightDoubleAngle(i, j);
                }
                else if (map[i, j - 1] != 2 && map[i, j + 1] == 2 && map[i + 1, j] != 2)
                {
                    wallTopAngleLeft(i, j);
                    wallDownAngleLeft(i, j);
                    wallLeftDoubleAngle(i, j);
                }
                else if (map[i, j - 1] != 2 && map[i, j + 1] != 2 && map[i + 1, j] == 2)
                {
                    wallRightAngleTop(i, j);
                    wallLeftAngleTop(i, j);
                    wallTopDoubleAngle(i, j);
                }
                else if (map[i, j - 1] == 2 && map[i, j + 1] != 2 && map[i + 1, j] == 2)
                {
                    angleDownLeft(i, j);
                    wallTopAngleRight(i, j);
                    wallRightAngleTop(i, j);
                }
                else if (map[i, j - 1] != 2 && map[i, j + 1] == 2 && map[i + 1, j] == 2)
                {
                    angleDownRight(i, j);
                    wallLeftAngleTop(i, j);
                    wallTopAngleLeft(i, j);
                }
                else if (map[i, j - 1] == 2 && map[i, j + 1] == 2 && map[i + 1, j] == 2)
                {
                    wallTop(i, j);
                    angleDownLeft(i, j);
                    angleDownRight(i, j);
                }
                else if (map[i, j - 1] == 2 && map[i, j + 1] == 2 && map[i + 1, j] != 2)
                {
                    wallDown(i, j);
                    wallTop(i, j);
                }
            }

        }

        i = map.GetLength(0) - 1;
        for (j = 1; j < map.GetLength(1) - 1; j++)
        {
            if (map[i, j] == 2)
            {
                if (map[i, j - 1] == 2 && map[i - 1, j] != 2 && map[i, j + 1] != 2)
                {
                    wallTopAngleRight(i, j);
                    wallDownAngleRight(i, j);
                    wallRightDoubleAngle(i, j);
                }
                else if (map[i, j - 1] != 2 && map[i - 1, j] == 2 && map[i, j + 1] != 2)
                {
                    wallRightAngleDown(i, j);
                    wallLeftAngleDown(i, j);
                    wallDownDoubleAngle(i, j);
                }
                else if (map[i, j - 1] != 2 && map[i - 1, j] != 2 && map[i, j + 1] == 2)
                {
                    wallTopAngleLeft(i, j);
                    wallDownAngleLeft(i, j);
                    wallLeftDoubleAngle(i, j);
                }
                else if (map[i, j - 1] == 2 && map[i - 1, j] == 2 && map[i, j + 1] != 2)
                {
                    angleTopLeft(i, j);
                    wallDownAngleRight(i, j);
                    wallRightAngleDown(i, j);
                }
                else if (map[i, j - 1] != 2 && map[i - 1, j] == 2 && map[i, j + 1] == 2)
                {
                    angleTopRight(i, j);
                    wallLeftAngleDown(i, j);
                    wallDownAngleLeft(i, j);
                }
                else if (map[i, j - 1] == 2 && map[i - 1, j] == 2 && map[i, j + 1] == 2)
                {
                    angleTopLeft(i, j);
                    angleTopRight(i, j);
                    wallDown(i, j);
                }
                else if (map[i, j - 1] == 2 && map[i - 1, j] != 2 && map[i, j + 1] == 2)
                {
                    wallDown(i, j);
                    wallTop(i, j);
                }
            }
        }

        j = 0;
        for (i = 1; i < map.GetLength(0) - 1; i++)
        {
            if (map[i, j] == 2)
            {
                if (map[i - 1, j] == 2 && map[i, j + 1] != 2 && map[i + 1, j] != 2)
                {
                    wallRightAngleDown(i, j);
                    wallLeftAngleDown(i, j);
                    wallDownDoubleAngle(i, j);
                }
                else if (map[i - 1, j] != 2 && map[i, j + 1] == 2 && map[i + 1, j] != 2)
                {
                    wallTopAngleLeft(i, j);
                    wallDownAngleLeft(i, j);
                    wallLeftDoubleAngle(i, j);
                }
                else if (map[i - 1, j] != 2 && map[i, j + 1] != 2 && map[i + 1, j] == 2)
                {
                    wallRightAngleTop(i, j);
                    wallLeftAngleTop(i, j);
                    wallTopDoubleAngle(i, j);
                }
                else if (map[i - 1, j] == 2 && map[i, j + 1] == 2 && map[i + 1, j] != 2)
                {
                    angleTopRight(i, j);
                    wallLeftAngleDown(i, j);
                    wallDownAngleLeft(i, j);
                }
                else if (map[i - 1, j] != 2 && map[i, j + 1] == 2 && map[i + 1, j] == 2)
                {
                    angleDownRight(i, j);
                    wallLeftAngleTop(i, j);
                    wallTopAngleLeft(i, j);
                }
                else if (map[i - 1, j] == 2 && map[i, j + 1] == 2 && map[i + 1, j] == 2)
                {
                    wallLeft(i, j);
                    angleTopRight(i, j);
                    angleDownRight(i, j);
                }
                else if (map[i - 1, j] == 2 && map[i, j + 1] != 2 && map[i + 1, j] == 2)
                {
                    wallLeft(i, j);
                    wallRight(i, j);
                }
            }
        }

        j = map.GetLength(1) - 1;
        for (i = 1; i < map.GetLength(0) - 1; i++)
        {
            if (map[i, j] == 2)
            {
                if (map[i, j - 1] == 2 && map[i - 1, j] != 2 && map[i + 1, j] != 2)
                {
                    wallTopAngleRight(i, j);
                    wallDownAngleRight(i, j);
                    wallRightDoubleAngle(i, j);
                }
                else if (map[i, j - 1] != 2 && map[i - 1, j] == 2 && map[i + 1, j] != 2)
                {
                    wallRightAngleDown(i, j);
                    wallLeftAngleDown(i, j);
                    wallDownDoubleAngle(i, j);
                }
                else if (map[i, j - 1] != 2 && map[i - 1, j] != 2 && map[i + 1, j] == 2)
                {
                    wallRightAngleTop(i, j);
                    wallLeftAngleTop(i, j);
                    wallTopDoubleAngle(i, j);
                }
                else if (map[i, j - 1] == 2 && map[i - 1, j] == 2 && map[i + 1, j] != 2)
                {
                    angleTopLeft(i, j);
                    wallDownAngleRight(i, j);
                    wallRightAngleDown(i, j);
                }
                else if (map[i, j - 1] == 2 && map[i - 1, j] != 2 && map[i + 1, j] == 2)
                {
                    angleDownLeft(i, j);
                    wallTopAngleRight(i, j);
                    wallRightAngleTop(i, j);
                }
                else if (map[i, j - 1] == 2 && map[i - 1, j] == 2 && map[i + 1, j] == 2)
                {
                    wallRight(i, j);
                    angleTopLeft(i, j);
                    angleDownLeft(i, j);
                }
                else if (map[i, j - 1] != 2 && map[i - 1, j] == 2 && map[i + 1, j] == 2)
                {
                    wallLeft(i, j);
                    wallRight(i, j);
                }
            }
        }

        for (i = 1; i < map.GetLength(0) - 1; i++)
        {
            for (j = 1; j < map.GetLength(1) - 1; j++)
            {
                if (map[i, j] == 2)
                {
                    if (map[i, j - 1] == 2 && map[i - 1, j] == 2 && map[i, j + 1] == 2 && map[i + 1, j] == 2)
                    {
                        angleTopRight(i, j);
                        angleTopLeft(i, j);
                        angleDownLeft(i, j);
                        angleDownRight(i, j);
                    }
                    else if (map[i, j - 1] == 2 && map[i - 1, j] != 2 && map[i, j + 1] != 2 && map[i + 1, j] != 2)
                    {
                        wallTopAngleRight(i, j);
                        wallDownAngleRight(i, j);
                        wallRightDoubleAngle(i, j);
                    }
                    else if (map[i, j - 1] != 2 && map[i - 1, j] == 2 && map[i, j + 1] != 2 && map[i + 1, j] != 2)
                    {
                        wallRightAngleDown(i, j);
                        wallLeftAngleDown(i, j);
                        wallDownDoubleAngle(i, j);
                    }
                    else if (map[i, j - 1] != 2 && map[i - 1, j] != 2 && map[i, j + 1] == 2 && map[i + 1, j] != 2)
                    {
                        wallTopAngleLeft(i, j);
                        wallDownAngleLeft(i, j);
                        wallLeftDoubleAngle(i, j);
                    }
                    else if (map[i, j - 1] != 2 && map[i - 1, j] != 2 && map[i, j + 1] != 2 && map[i + 1, j] == 2)
                    {
                        wallRightAngleTop(i, j);
                        wallLeftAngleTop(i, j);
                        wallTopDoubleAngle(i, j);
                    }
                    else if (map[i, j - 1] == 2 && map[i - 1, j] == 2 && map[i, j + 1] != 2 && map[i + 1, j] != 2)
                    {
                        angleTopLeft(i, j);
                        wallDownAngleRight(i, j);
                        wallRightAngleDown(i, j);
                    }
                    else if (map[i, j - 1] == 2 && map[i - 1, j] != 2 && map[i, j + 1] != 2 && map[i + 1, j] == 2)
                    {
                        angleDownLeft(i, j);
                        wallTopAngleRight(i, j);
                        wallRightAngleTop(i, j);
                    }
                    else if (map[i, j - 1] != 2 && map[i - 1, j] == 2 && map[i, j + 1] == 2 && map[i + 1, j] != 2)
                    {
                        angleTopRight(i, j);
                        wallLeftAngleDown(i, j);
                        wallDownAngleLeft(i, j);
                    }
                    else if (map[i, j - 1] != 2 && map[i - 1, j] != 2 && map[i, j + 1] == 2 && map[i + 1, j] == 2)
                    {
                        angleDownRight(i, j);
                        wallLeftAngleTop(i, j);
                        wallTopAngleLeft(i, j);
                    }
                    else if (map[i, j - 1] == 2 && map[i - 1, j] == 2 && map[i, j + 1] == 2 && map[i + 1, j] != 2)
                    {
                        angleTopLeft(i, j);
                        angleTopRight(i, j);
                        wallDown(i, j);
                    }
                    else if (map[i, j - 1] == 2 && map[i - 1, j] != 2 && map[i, j + 1] == 2 && map[i + 1, j] == 2)
                    {
                        wallTop(i, j);
                        angleDownLeft(i, j);
                        angleDownRight(i, j);
                    }
                    else if (map[i, j - 1] == 2 && map[i - 1, j] == 2 && map[i, j + 1] != 2 && map[i + 1, j] == 2)
                    {
                        wallRight(i, j);
                        angleTopLeft(i, j);
                        angleDownLeft(i, j);
                    }
                    else if (map[i, j - 1] != 2 && map[i - 1, j] == 2 && map[i, j + 1] == 2 && map[i + 1, j] == 2)
                    {
                        wallLeft(i, j);
                        angleTopRight(i, j);
                        angleDownRight(i, j);
                    }
                    else if (map[i, j - 1] != 2 && map[i - 1, j] == 2 && map[i, j + 1] != 2 && map[i + 1, j] == 2)
                    {
                        wallLeft(i, j);
                        wallRight(i, j);
                    }
                    else if (map[i, j - 1] == 2 && map[i - 1, j] != 2 && map[i, j + 1] == 2 && map[i + 1, j] != 2)
                    {
                        wallDown(i, j);
                        wallTop(i, j);
                    }
                }
            }
        }
    }

    private void angleTopRight(int i, int j)
    {
        Instantiate(wallAngle, new Vector3(j * 18 + 8 + marginX, -i * 10 + 4 + marginY, 0), Quaternion.Euler(0, 0, -90));
    }

    private void angleTopLeft(int i, int j)
    {
        Instantiate(wallAngle, new Vector3(j * 18 - 8 + marginX, -i * 10 + 4 + marginY, 0), Quaternion.Euler(0, 0, 0));
    }

    private void angleDownRight(int i, int j)
    {
        Instantiate(wallAngle, new Vector3(j * 18 + 8 + marginX, -i * 10 - 4 + marginY, 0), Quaternion.Euler(0, 0, 180));
    }

    private void angleDownLeft(int i, int j)
    {
        Instantiate(wallAngle, new Vector3(j * 18 - 8 + marginX, -i * 10 - 4 + marginY, 0), Quaternion.Euler(0, 0, 90));
    }

    private void wallTop(int i, int j)
    {
        Instantiate(wallHori, new Vector3(j * 18 + marginX, -i * 10 + 4 + marginY, 0), Quaternion.Euler(0, 0, 0));
    }

    private void wallDown(int i, int j)
    {
        Instantiate(wallHori, new Vector3(j * 18 + marginX, -i * 10 - 4 + marginY, 0), Quaternion.Euler(180, 0, 0));
    }

    private void wallRight(int i, int j)
    {
        Instantiate(wallVert, new Vector3(j * 18 + 8 + marginX, -i * 10 + marginY, 0), Quaternion.Euler(0, 180, 0));
    }

    private void wallLeft(int i, int j)
    {
        Instantiate(wallVert, new Vector3(j * 18 - 8 + marginX, -i * 10 + marginY, 0), Quaternion.Euler(0, 0, 0));
    }

    private void wallTopAngleRight(int i, int j)
    {
        Instantiate(wallHoriAngle, new Vector3(j * 18 + marginX, -i * 10 + 4 + marginY, 0), Quaternion.Euler(0, 0, 0));
    }

    private void wallDownAngleRight(int i, int j)
    {
        Instantiate(wallHoriAngle, new Vector3(j * 18 + marginX, -i * 10 - 4 + marginY, 0), Quaternion.Euler(180, 0, 0));
    }

    private void wallTopAngleLeft(int i, int j)
    {
        Instantiate(wallHoriAngle, new Vector3(j * 18 + marginX, -i * 10 + 4 + marginY, 0), Quaternion.Euler(0, 180, 0));
    }

    private void wallDownAngleLeft(int i, int j)
    {
        Instantiate(wallHoriAngle, new Vector3(j * 18 + marginX, -i * 10 - 4 + marginY, 0), Quaternion.Euler(180, 180, 0));
    }

    private void wallLeftAngleTop(int i, int j)
    {
        Instantiate(wallVertAngle, new Vector3(j * 18 - 8 + marginX, -i * 10 + marginY, 0), Quaternion.Euler(0, 0, 0));
    }

    private void wallLeftAngleDown(int i, int j)
    {
        Instantiate(wallVertAngle, new Vector3(j * 18 - 8 + marginX, -i * 10 + marginY, 0), Quaternion.Euler(180, 0, 0));
    }

    private void wallRightAngleDown(int i, int j)
    {
        Instantiate(wallVertAngle, new Vector3(j * 18 + 8 + marginX, -i * 10 + marginY, 0), Quaternion.Euler(180, 180, 0));
    }

    private void wallRightAngleTop(int i, int j)
    {
        Instantiate(wallVertAngle, new Vector3(j * 18 + 8 + marginX, -i * 10 + marginY, 0), Quaternion.Euler(0, 180, 0));
    }

    private void wallRightDoubleAngle(int i, int j)
    {
        Instantiate(wallVertDoubleAngle, new Vector3(j * 18 + 8 + marginX, -i * 10 + marginY, 0), Quaternion.Euler(0, 180, 0));
    }

    private void wallLeftDoubleAngle(int i, int j)
    {
        Instantiate(wallVertDoubleAngle, new Vector3(j * 18 - 8 + marginX, -i * 10 + marginY, 0), Quaternion.Euler(0, 0, 0));
    }

    private void wallDownDoubleAngle(int i, int j)
    {
        Instantiate(wallHoriDoubleAngle, new Vector3(j * 18 + marginX, -i * 10 - 4 + marginY, 0), Quaternion.Euler(180, 0, 0));
    }

    private void wallTopDoubleAngle(int i, int j)
    {
        Instantiate(wallHoriDoubleAngle, new Vector3(j * 18 + marginX, -i * 10 + 4 + marginY, 0), Quaternion.Euler(0, 0, 0));
    }
}