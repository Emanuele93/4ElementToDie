using UnityEngine;
using System.Collections;

public class roomFactoryDown : roomFactory
{
    protected override GameObject generateObstacle()
    {
        GameObject obstacles = new GameObject();
        obstacles.name = "obstacles";

        int i, j, x, y, k, lenght, numObstacles;
        bool full;

        for (i = 0; i < roomStructure.GetLength(0); i++)
            for (j = 0; j < roomStructure.GetLength(1); j++)
                roomStructure[i, j] = 0;

        numObstacles = Random.Range(1, 5);

        for (k = 0; k < numObstacles; k++)
        {
            lenght = Random.Range(1, 4);
            i = x = Random.Range(0, roomStructure.GetLength(0));
            j = y = Random.Range(0, roomStructure.GetLength(1));
            full = false;
            while (!valid(i, j, lenght) && !full)
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
                x = i;
                for (y = j + 1; y < j + lenght + 1; y++)
                    roomStructure[x, y] = 1;
                x = i + lenght + 1;
                for (y = j + 1; y < j + lenght + 1; y++)
                    roomStructure[x, y] = 1;
                y = j;
                for (x = i + 1; x < i + lenght + 1; x++)
                    roomStructure[x, y] = 1;
                y = j + lenght + 1;
                for (x = i + 1; x < i + lenght + 1; x++)
                    roomStructure[x, y] = 1;
            }
        }
        addObstacles(obstacles);
        return obstacles;
    }

    private bool valid(int i, int j, int lenght)
    {
        int x, y;
        if (i + lenght + 1 >= roomStructure.GetLength(0) || j + lenght + 1 >= roomStructure.GetLength(1))
            return false;
        else
        {
            x = i;
            for (y = j; y < j + lenght + 1; y++)
                if (roomStructure[x, y] > 0)
                    return false;
            x = i + lenght + 1;
            for (y = j; y < j + lenght + 1; y++)
                if (roomStructure[x, y] > 0)
                    return false;
            y = j;
            for (x = i; x < i + lenght + 1; x++)
                if (roomStructure[x, y] > 0)
                    return false;
            y = j + lenght + 1;
            for (x = i; x < i + lenght + 1; x++)
                if (roomStructure[x, y] > 0)
                    return false;
            x = i - 1;
            if (x >= 0)
                for (y = j; y < j + lenght + 2; y++)
                    if (roomStructure[x, y] > 0)
                        return false;
            x = i + lenght + 2;
            if (x < roomStructure.GetLength(0))
                for (y = j; y < j + lenght + 2; y++)
                    if (roomStructure[x, y] > 0)
                        return false;
            y = j - 1;
            if (y >= 0)
                for (x = i; x < i + lenght + 2; x++)
                    if (roomStructure[x, y] > 0)
                        return false;
            y = j + lenght + 2;
            if (y < roomStructure.GetLength(1))
                for (x = i; x < i + lenght + 2; x++)
                    if (roomStructure[x, y] > 0)
                        return false;
        }
        return true;
    }

    protected override GameObject getEnemy(int difficulty)
    {
        return enemyObjectCollection.GetComponent<EnemyObjectCollection>().getWaterEnemy(difficulty);
    }

    protected override GameObject getChest()
    {
        if (Random.Range(0, 10) == 0)
        {
            switch (Random.Range(0, 3))
            {
                case 0:
                    return enemyObjectCollection.GetComponent<EnemyObjectCollection>().getAirChest();
                case 1:
                    return enemyObjectCollection.GetComponent<EnemyObjectCollection>().getFireChest();
            }
            return enemyObjectCollection.GetComponent<EnemyObjectCollection>().getEarthChest();
        }
        else
            return enemyObjectCollection.GetComponent<EnemyObjectCollection>().getWaterChest();
    }

    protected override int getDifficulty()
    {
        return gameplayManager.getNoKilledBosses((int)(ElementType.Water));
    }

    public override void getBossEnemy(Vector3 pos, Transform parent)
    {
        GameObject go = enemyObjectCollection.GetComponent<EnemyObjectCollection>().getWaterEnemy(4);
        go.transform.parent = parent;
        go.transform.position = pos;
        go.tag = "Boss";
    }
}