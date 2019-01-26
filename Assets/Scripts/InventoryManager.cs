﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private int currentObjectIndex;
    private Dictionary<int, string> objectPositionInInventory;

    public const string TEDDY_BEAR_GO_NAME = "TeddyBear";
    public const string PAPER_CLIP_KEY_GO_NAME = "PaperClipKey";
    public const string BOX_SHIP_GO_NAME = "BoxShip";
    public const string SWORD_RULER_GO_NAME = "SwordRuler";
    public const string RAZOR_STONE_GO_NAME = "RazorStone";
    public const string BOSS_KEY_GO_NAME = "BossKey";
    public const string TORCH_LIGHT_GO_NAME = "TorchLight";

    private UIManager uiManager;
    private EventManager eventManager;
    public bool switchAsked;

    // Start is called before the first frame update
    void Start()
    {
        currentObjectIndex = 1;
        objectPositionInInventory = new Dictionary<int, string>();

        uiManager = GameObject.FindObjectOfType<UIManager>();

        eventManager = EventManager.Instance;
        eventManager.OnItemPickUp += AddObject;

        switchAsked = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
     * Add object to inventory
     */
    public void AddObject(string name)
    {
        int objectPosition = InventoryHasObject(name);
        if (objectPosition < 1)
        {
            Debug.Log("Adding object " + name + " in inventory in position " + objectPosition);
            objectPositionInInventory.Add(currentObjectIndex, name);
            currentObjectIndex++;
            AddObjectFeedback(name);
        }
        else
        {
            Debug.LogError("InventoryManager - AddObject - Object " + name + " already in inventory - why the hell we are here?");
        }
    }

    /*
     * Return if we have the object in inventory as a boolean
     */
    public bool InventoryObjectOwned(string name)
    {
        if(InventoryHasObject(name) >= 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /*
     * Return if we have the object in inventory as its position in inventory
     */
    public int InventoryHasObject(string name)
    {
        int value = 0;
        string objectName = "";
        foreach(int currentKey in objectPositionInInventory.Keys)
        {
            objectName = "";
            objectPositionInInventory.TryGetValue(currentKey, out objectName);
            if (objectName.Equals(name))
            {
                value = currentKey;
                break;
            }
        }
        //if (objectPositionInInventory.ContainsValue(name))
        //{
        //    int value = objectPositionInInventory.Values.ToList().IndexOf(name);
        //    return value;
        //}
        //else
        //{
        //    return 0;
        //}

        return value;
    }

    /*
     * Return name of Inventory object at position if available, else return empty string
     */
    public string GetInventoryObjectName(int position)
    {
        string name = string.Empty;
        objectPositionInInventory.TryGetValue(position, out name);
        return name;
    }
    
    /*
     * Launch action for object at selected position in inventory
     */
    public void ActionInventoryManager(int inventoryPosition)
    {
        string objectName = GetInventoryObjectName(inventoryPosition);
        ActionInventoryManager( objectName);
    }

    private void ActionInventoryManager(string objectName)
    {
        if (objectName != null)
        {
            switch (objectName)
            {
                case TEDDY_BEAR_GO_NAME:
                    ActionTeddyBearClickOnInventory();
                    break;
                case PAPER_CLIP_KEY_GO_NAME:
                    ActionPaperClipKeyClickOnInventory();
                    break;
                case BOX_SHIP_GO_NAME:
                    ActionBoxShipClickOnInventory();
                    break;
                case SWORD_RULER_GO_NAME:
                    ActionSwordRulerClickOnInventory();
                    break;
                case RAZOR_STONE_GO_NAME:
                    ActionRazorStoneClickOnInventory();
                    break;
                case BOSS_KEY_GO_NAME:
                    ActionBossKeyClickOnInventory();
                    break;
                case TORCH_LIGHT_GO_NAME:
                    ActionTorchLightClickOnInventory();
                    break;
                default:
                    Debug.LogError("InventoryManager - ActionInventoryManager - Object " + name + " unknown - why the hell we are here?");
                    break;
            }
        }
        else
        {
            Debug.Log("InventoryManager - ActionInventoryManager - click on empty position ");
        }
    }

    private void AddObjectFeedback(string objectName)
    {
        if (objectName != null)
        {
            switch (objectName)
            {
                case TEDDY_BEAR_GO_NAME:
                    ActionTeddyBearOnAdd();
                    break;
                case PAPER_CLIP_KEY_GO_NAME:
                    ActionPaperClipKeyOnAdd();
                    break;
                case BOX_SHIP_GO_NAME:
                    ActionBoxShipOnAdd();
                    break;
                case SWORD_RULER_GO_NAME:
                    ActionSwordRulerOnAdd();
                    break;
                case RAZOR_STONE_GO_NAME:
                    ActionRazorStoneOnAdd();
                    break;
                case BOSS_KEY_GO_NAME:
                    ActionBossKeyOnAdd();
                    break;
                case TORCH_LIGHT_GO_NAME:
                    ActionTorchLightOnAdd();
                    break;
                default:
                    Debug.LogError("InventoryManager - ActionInventoryManager - Object " + name + " unknown - why the hell we are here?");
                    break;
            }
        }
        else
        {
            Debug.Log("InventoryManager - ActionInventoryManager - click on empty position ");
        }
    }

    #region ClickOnInventory
    private void ActionTeddyBearClickOnInventory()
    {
        switchAsked = true;
        if (GameManager.Instance.swaper.World.Equals(World.Real))
        {
            uiManager.UpdateDialog(UIManager.DialogSpeaker.TEDDY, "C'est parti pour le pays magique!");            
        }
        else
        {
            uiManager.UpdateDialog(UIManager.DialogSpeaker.TEDDY, "De retour dans le réel!");
        }
        
    }

    private void ActionPaperClipKeyClickOnInventory()
    {
        uiManager.UpdateDialog(UIManager.DialogSpeaker.PAPER_CLIP, "Voulez-vous toujours mon assistance?");
    }

    private void ActionBoxShipClickOnInventory()
    {
        uiManager.UpdateDialog(UIManager.DialogSpeaker.BOX_SHIP, "Metal Gear??!");
    }

    private void ActionSwordRulerClickOnInventory()
    {
        uiManager.UpdateDialog(UIManager.DialogSpeaker.SWORD_RULER, "Not suitable for children");
    }

    private void ActionRazorStoneClickOnInventory()
    {
        uiManager.UpdateDialog(UIManager.DialogSpeaker.RAZOR_STONE, "Stone!!! Le monde est stone!!!");
    }

    private void ActionBossKeyClickOnInventory()
    {
        uiManager.UpdateDialog(UIManager.DialogSpeaker.BOSS_KEY, "Tadaaadaaadaaa!");
    }

    private void ActionTorchLightClickOnInventory()
    {
        uiManager.UpdateDialog(UIManager.DialogSpeaker.RAZOR_STONE, "Ma maman m'a toujours dit que je n'étais pas une lumière...");
    }
    #endregion

    #region OnAdd
    private void ActionTeddyBearOnAdd()
    {
        uiManager.UpdateDialog(UIManager.DialogSpeaker.TEDDY,"Salut je m'appelle Teddy");
        int inventoryPosition = InventoryHasObject(TEDDY_BEAR_GO_NAME);
        uiManager.SetInventoryItemAtPosition(inventoryPosition, TEDDY_BEAR_GO_NAME);

    }

    private void ActionPaperClipKeyOnAdd()
    {
        uiManager.UpdateDialog(UIManager.DialogSpeaker.PAPER_CLIP, "Voulez-vous mon assistance?");
        int inventoryPosition = InventoryHasObject(PAPER_CLIP_KEY_GO_NAME);
        uiManager.SetInventoryItemAtPosition(inventoryPosition, PAPER_CLIP_KEY_GO_NAME);
    }

    private void ActionBoxShipOnAdd()
    {
        uiManager.UpdateDialog(UIManager.DialogSpeaker.BOX_SHIP, "Maman les petits bateaux qui vont sur l'eau ont ils des jambes?");
        int inventoryPosition = InventoryHasObject(BOX_SHIP_GO_NAME);
        uiManager.SetInventoryItemAtPosition(inventoryPosition, BOX_SHIP_GO_NAME);
    }

    private void ActionSwordRulerOnAdd()
    {
        uiManager.UpdateDialog(UIManager.DialogSpeaker.SWORD_RULER, "One ruler to sword them all.");
        int inventoryPosition = InventoryHasObject(SWORD_RULER_GO_NAME);
        uiManager.SetInventoryItemAtPosition(inventoryPosition, SWORD_RULER_GO_NAME);
    }

    private void ActionRazorStoneOnAdd()
    {
        uiManager.UpdateDialog(UIManager.DialogSpeaker.RAZOR_STONE, "Des cailloux, des cailloux");
        int inventoryPosition = InventoryHasObject(RAZOR_STONE_GO_NAME);
        uiManager.SetInventoryItemAtPosition(inventoryPosition, RAZOR_STONE_GO_NAME);
    }

    private void ActionBossKeyOnAdd()
    {
        uiManager.UpdateDialog(UIManager.DialogSpeaker.BOSS_KEY, "La clé!");
        int inventoryPosition = InventoryHasObject(BOSS_KEY_GO_NAME);
        uiManager.SetInventoryItemAtPosition(inventoryPosition, BOSS_KEY_GO_NAME);
    }

    private void ActionTorchLightOnAdd()
    {
        uiManager.UpdateDialog(UIManager.DialogSpeaker.RAZOR_STONE, "Le pouvoir de la lumière!");
        int inventoryPosition = InventoryHasObject(TORCH_LIGHT_GO_NAME);
        uiManager.SetInventoryItemAtPosition(inventoryPosition, TORCH_LIGHT_GO_NAME);
    }
    #endregion
    

    public void OnWorldSwap()
    {
        foreach (int currentKey in objectPositionInInventory.Keys)
        {
            string name = "";
            objectPositionInInventory.TryGetValue(currentKey, out name);
            uiManager.SetInventoryItemAtPosition(currentKey, name);
        }
    }
}
