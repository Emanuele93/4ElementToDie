﻿using UnityEngine;
using System.Collections;

public class upMap : superMap
{

    void Start()
    {
        marginX = -80;
        marginY = +126;

        map = new int[,]
        {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0},
            { 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0},
            { 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0},
            { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            { 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0},
            { 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0},
            { 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
        };

        generateMap(10, 5);

        insertWall();
    }

    private void resetMap()
    {
        clearMap();
        map = new int[,]
        {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0},
            { 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0},
            { 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0},
            { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            { 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0},
            { 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0},
            { 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
        };

        generateMap(10, 5);

        insertWall();
    }
}
