using UnityEngine;
using System.Collections;

public class roomFactoryRight : roomFactory
{
    protected override GameObject generateObstacle()
    {
        GameObject obstacles = new GameObject();
        obstacles.name = "obstacles";

        int i, j, x, y, k, lenght, numObstacles;
        bool full, vertical;

        for (i = 0; i < roomStructure.GetLength(0); i++)
            for (j = 0; j < roomStructure.GetLength(1); j++)
                roomStructure[i, j] = 0;

        numObstacles = Random.Range(1, 5);

        for (k = 0; k < numObstacles; k++)
        {
            lenght = Random.Range(1, 6);
            if (Random.Range(0, 2) == 0)
                lenght = -lenght;
            i = x = Random.Range(0, roomStructure.GetLength(0));
            j = y = Random.Range(0, roomStructure.GetLength(1));
            full = false;
            vertical = Random.Range(0, 2) == 0;
            while (!valid(i, j, lenght, vertical) && !full)
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
                if (vertical)
                {
                    x = i;
                    if (lenght > 0)
                    {
                        for (y = j; y < j + lenght + 1; y++)
                            roomStructure[x, y] = 1;
                    }
                    else
                    {
                        for (y = j; y > j + lenght - 1; y--)
                            roomStructure[x, y] = 1;
                    }
                }
                else
                {
                    y = j;
                    if (lenght > 0)
                    {
                        for (x = i; x < i + lenght + 1; x++)
                            roomStructure[x, y] = 1;
                    }
                    else
                    {
                        for (x = i; x > i + lenght - 1; x--)
                            roomStructure[x, y] = 1;
                    }
                }
            }
        }
        addObstacles(obstacles);
        return obstacles;
    }


    private bool valid(int i, int j, int lenght, bool vertical)
    {
        int x, y;
        if (vertical)
        {
            x = i;
            if (lenght > 0)
            {
                if (j + lenght + 1 >= roomStructure.GetLength(1))
                    return false;
                for (y = j; y < j + lenght + 1; y++)
                    if (!validPoint(x, y))
                        return false;
            }
            else
            {
                if (j + lenght - 1 < 0)
                    return false;
                for (y = j; y > j + lenght - 1; y--)
                    if (!validPoint(x, y))
                        return false;
            }
        }
        else
        {
            y = j;
            if (lenght > 0)
            {
                if (i + lenght + 1 >= roomStructure.GetLength(0))
                    return false;
                for (x = i; x < i + lenght + 1; x++)
                    if (!validPoint(x, y))
                        return false;
            }
            else
            {
                if (i + lenght - 1 < 0)
                    return false;
                for (x = i; x > i + lenght - 1; x--)
                    if (!validPoint(x, y))
                        return false;
            }
        }
        return true;
    }

    protected override GameObject getEnemy(int difficulty)
    {
        return enemyObjectCollection.GetComponent<EnemyObjectCollection>().getAirEnemy(difficulty);
    }

    protected override GameObject getChest()
    {
        if (Random.Range(0, 10) == 0)
        {
            switch (Random.Range(0, 3))
            {
                case 0:
                    return enemyObjectCollection.GetComponent<EnemyObjectCollection>().getWaterChest();
                case 1:
                    return enemyObjectCollection.GetComponent<EnemyObjectCollection>().getFireChest();
            }
            return enemyObjectCollection.GetComponent<EnemyObjectCollection>().getEarthChest();
        }
        else
            return enemyObjectCollection.GetComponent<EnemyObjectCollection>().getAirChest();
    }

    protected override int getDifficulty()
    {
        return gameplayManager.getNoKilledBosses((int)(ElementType.Air));
    }

    public override void getBossEnemy(Vector3 pos, Transform parent)
    {
        GameObject go = enemyObjectCollection.GetComponent<EnemyObjectCollection>().getAirEnemy(4);
        go.transform.parent = parent;
        go.transform.position = pos;
        go.tag = "Boss";
    }
}