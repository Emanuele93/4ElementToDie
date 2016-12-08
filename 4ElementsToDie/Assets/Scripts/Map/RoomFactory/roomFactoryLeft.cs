using UnityEngine;
using System.Collections;

public class roomFactoryLeft : roomFactory
{
    protected override GameObject generateObstacle()
    {
        GameObject obstacles = new GameObject();
        obstacles.name = "obstacles";

        int i, j, x, y, k, lenghtX, lenghtY, numObstacles;
        bool full, vertical;

        for (i = 0; i < roomStructure.GetLength(0); i++)
            for (j = 0; j < roomStructure.GetLength(1); j++)
                roomStructure[i, j] = 0;

        numObstacles = Random.Range(1, 5);

        for (k = 0; k < numObstacles; k++)
        {
            lenghtX = Random.Range(2, 5);
            if (Random.Range(0, 2) == 0)
                lenghtX = -lenghtX;
            lenghtY = Random.Range(2, 5);
            if (Random.Range(0, 2) == 0)
                lenghtY = -lenghtY;
            i = x = Random.Range(0, roomStructure.GetLength(0));
            j = y = Random.Range(0, roomStructure.GetLength(1));
            full = false;
            vertical = Random.Range(0, 2) == 0;
            while (!valid(i, j, lenghtX, lenghtY, vertical) && !full)
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
                roomStructure[i, j] = 1;
                x = i;
                if (lenghtY > 0)
                    for (y = j + 1; y < j + lenghtY + 1; y++)
                        roomStructure[x, y] = 1;
                else
                    for (y = j - 1; y > j + lenghtY - 1; y--)
                        roomStructure[x, y] = 1;
                y = j;
                if (lenghtX > 0)
                    for (x = i + 1; x < i + lenghtX + 1; x++)
                        roomStructure[x, y] = 1;
                else
                    for (x = i - 1; x > i + lenghtX - 1; x--)
                        roomStructure[x, y] = 1;
                if (vertical)
                {
                    x = i + lenghtX;
                    if (lenghtY > 0)
                        for (y = j + 1; y < j + lenghtY + 1; y++)
                            roomStructure[x, y] = 1;
                    else
                        for (y = j - 1; y > j + lenghtY - 1; y--)
                            roomStructure[x, y] = 1;
                }
                else
                {
                    y = j + lenghtY;
                    if (lenghtX > 0)
                        for (x = i + 1; x < i + lenghtX + 1; x++)
                            roomStructure[x, y] = 1;
                    else
                        for (x = i - 1; x > i + lenghtX - 1; x--)
                            roomStructure[x, y] = 1;
                }
            }
        }
        addObstacles(obstacles);
        return obstacles;
    }

    private bool valid(int i, int j, int lenghtX, int lenghtY, bool vertical)
    {
        int x, y;
        if (!validPoint(i, j))
            return false;
        x = i;
        if (lenghtY > 0)
        {
            for (y = j + 1; y < j + lenghtY + 1; y++)
                if (!validPoint(x, y))
                    return false;
        }
        else
        {
            for (y = j - 1; y > j + lenghtY - 1; y--)
                if (!validPoint(x, y))
                    return false;
        }
        y = j;
        if (lenghtX > 0)
        {
            for (x = i + 1; x < i + lenghtX + 1; x++)
                if (!validPoint(x, y))
                    return false;
        }
        else
        {
            for (x = i - 1; x > i + lenghtX - 1; x--)
                if (!validPoint(x, y))
                    return false;
        }
        if (vertical)
        {
            x = i + lenghtX;
            if (lenghtY > 0)
            {
                for (y = j + 1; y < j + lenghtY + 1; y++)
                    if (!validPoint(x, y))
                        return false;
            }
            else
            {
                for (y = j - 1; y > j + lenghtY - 1; y--)
                    if (!validPoint(x, y))
                        return false;
            }
        }
        else
        {
            y = j + lenghtY;
            if (lenghtX > 0)
            {
                for (x = i + 1; x < i + lenghtX + 1; x++)
                    if (!validPoint(x, y))
                        return false;
            }
            else
            {
                for (x = i - 1; x > i + lenghtX - 1; x--)
                    if (!validPoint(x, y))
                        return false;
            }
        }

        return true;
    }

    protected override GameObject getEnemy(int difficulty)
    {
        return enemyObjectCollection.GetComponent<EnemyObjectCollection>().getEarthEnemy(difficulty);
    }
}