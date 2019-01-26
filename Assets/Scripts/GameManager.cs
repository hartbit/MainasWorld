﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerController player;
    public WorldSwapper swaper;
    public InventoryManager inventory;

    void Start()
    {
        Instance = this;
    }

}
