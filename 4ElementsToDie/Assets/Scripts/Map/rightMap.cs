﻿using UnityEngine;
using System.Collections;

public class rightMap : superMap
{

    void Start()
    {
        marginX = 4.5f;
        marginY = 50;

        map = new int[,]
        {
            { 0, 0, 0, 0, 0, 0, 0, -1, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 1, 1, 1, 0, -1, 0, 0},
            { 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, -1, 0},
            { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0},
            { 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, -1},
            { -2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, -1},
            { 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, -1},
            { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0},
            { 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, -1, 0},
            { 0, 0, 0, 0, 0, 1, 1, 1, 0, -1, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, -1, 0, 0, 0, 0}
        };

        generateMap(5, 1);
        
        insertWall();

        gameObject.SetActive(false);
    }

    private void resetMap()
    {
        clearMap();
        map = new int[,]
        {
            { 0, 0, 0, 0, 0, 0, 0, -1, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 1, 1, 1, 0, -1, 0, 0},
            { 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, -1, 0},
            { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0},
            { 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, -1},
            { -2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, -1},
            { 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, -1},
            { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0},
            { 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, -1, 0},
            { 0, 0, 0, 0, 0, 1, 1, 1, 0, -1, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, -1, 0, 0, 0, 0}
        };

        generateMap(5, 1);

        insertWall();
    }
}

