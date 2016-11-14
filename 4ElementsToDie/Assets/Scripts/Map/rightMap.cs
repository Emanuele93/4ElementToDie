﻿using UnityEngine;
using System.Collections;

public class rightMap : superMap
{

    void Start()
    {
        marginX = 13;
        marginY = 40;

        map = new int[,]
        {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0},
            { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0},
            { 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            { 0, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            { 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0},
            { 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
        };

        generateMap(4, 1);

        insertWall();
    }

    private void resetMap()
    {
        clearMap();
        map = new int[,]
        {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0},
            { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0},
            { 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            { 0, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            { 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0},
            { 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
        };

        generateMap(4, 1);

        insertWall();
    }
}

