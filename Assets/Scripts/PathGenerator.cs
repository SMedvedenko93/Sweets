using System;
using System.Collections.Generic;
using UnityEngine;

class PathGenerator
{
    public List<EnemyAction> path;

    public List<CharacterCell> characterCell;
    public int[,] CharacterCellArray;
    public List<string> moveList = new List<string>();
    public float startPositionX, startPositionZ;

    private string currentDirection, skipDirection, swapDirection, oldDirection;
    private bool skipLine, swapLine;

    private float currentPositionX, currentPositionZ, swapPositionX, swapPositionZ;
    private string orientation = "right";

    private List<EnemyAction> originalActionList = new List<EnemyAction>();
    private List<EnemyAction> finalActionList = new List<EnemyAction>();

    private float currentPosX, currentPosZ;

    public List<EnemyAction> getPath()
    {
        currentPosX = startPositionX;
        currentPosZ = startPositionZ;
        var newPosX = currentPosX;
        var newPosZ = currentPosZ;
        var move = "";

        foreach (string moveItem in moveList)
        {
            move = moveItem;

            if (skipLine == true)
            {
                if (move == skipDirection)
                {
                    continue;
                }
                else
                {
                    skipLine = false;
                    skipDirection = "";
                }
            }

            currentDirection = moveItem;
            if (swapLine == true)
            {
                if (oldDirection == currentDirection || oldDirection == "")
                {
                    switch (moveItem)
                    {
                        case "xm":
                            move = "xp";
                            break;
                        case "xp":
                            move = "xm";
                            break;
                        case "ym":
                            move = "yp";
                            break;
                        case "yp":
                            move = "ym";
                            break;
                    }
                }
                else
                {
                    swapLine = false;
                }
            }
            oldDirection = moveItem;

            switch (move)
            {
                case "xm":
                    orientation = "left";
                    newPosX = currentPosX - 1;
                    newPosZ = currentPosZ;
                    break;
                case "xp":
                    orientation = "right";
                    newPosX = currentPosX + 1;
                    newPosZ = currentPosZ;
                    break;
                case "ym":
                    orientation = "down";
                    newPosX = currentPosX;
                    newPosZ = currentPosZ - 1;
                    break;
                case "yp":
                    orientation = "up";
                    newPosX = currentPosX;
                    newPosZ = currentPosZ + 1;
                    break;
            }

            if (newPosX >= 0 && newPosX <= 5 && newPosZ >= 0 && newPosZ <= 9)
            {
                if (CharacterCellArray[(int)newPosX, (int)newPosZ] != 0)
                {
                    var index = CharacterCellArray[(int)newPosX, (int)newPosZ];

                    switch (characterCell[index - 1].name)
                    {
                        case "Skipcake":
                            SkipcakeAction(newPosX, newPosZ, move);
                            break;
                        case "Hypnopie":
                            if (characterCell[index - 1].x == newPosX && characterCell[index - 1].z == newPosZ)
                            {
                                SkipcakeAction(newPosX, newPosZ, move);
                            }
                            else
                            {
                                if (Enemy.CheckOrient(orientation, characterCell[index - 1].orientation))
                                { 
                                    HypnopieAction(characterCell[index - 1], move, newPosX, newPosZ, orientation);
                                }
                            }
                            break;
                        case "Swapple":
                            SwappleAction(newPosX, newPosZ, orientation);
                            break;
                        case "Jellyjump":
                            JellyJumpAction(newPosX, newPosZ, orientation);
                            break;
                        case "Magneticecream":
                            Debug.Log("Magneticecream       =   " + newPosX + " - " + newPosZ);
                            if (characterCell[index - 1].x == newPosX && characterCell[index - 1].z == newPosZ)
                            {
                                SkipcakeAction(newPosX, newPosZ, move);
                            }
                            else
                            {
                                MagneticeCreamAction(characterCell[index - 1], move, newPosX, newPosZ, orientation);
                            }
                            break;
                        case "LolyPortal":
                            LolyPortalAction();
                            break;
                        case "Crossant":
                            CrossantAction();
                            break;
                        case "DonutPunch":
                            DonutPunchAction();
                            break;
                    }
                }
                else
                {
                    AddToActionList("move", newPosX, newPosZ, orientation);
                }

                //if (skipLine == false)
                //{
                //    currentPosX = newPosX;
                //    currentPosX = newPosZ;
                //}
            }

        }

        foreach (var action in finalActionList)
        {
            //Debug.Log(action.type + " to " + action.pozX + " : " + action.pozZ);
        }


        return finalActionList;
    }

    private void AddToActionList(string type, float newPosX, float newPosZ, string orientation)
    {
        finalActionList.Add(new EnemyAction()
        {
            type = type,
            pozX = newPosX,
            pozZ = newPosZ,
            orientation = orientation
        });
        currentPosX = newPosX;
        currentPosZ = newPosZ;
    }

    private void SkipcakeAction(float newPosX, float newPosZ, string move)
    {
        skipLine = true;
        skipDirection = move;
        //currentPosX = newPosX;
        //currentPosZ = newPosZ;
    }

    private void SwappleAction(float newPosX, float newPosZ, string orientation)
    {
        swapLine = true;
        oldDirection = "";
        AddToActionList("move", newPosX, newPosZ, orientation);
    }

    private void HypnopieAction(CharacterCell characterCell, string move, float newPosX, float newPosZ, string orientation)
    {
        skipLine = true;
        skipDirection = move;

        var hypnoPosX = currentPosX;
        var hypnoPosZ = currentPosZ;

        switch (characterCell.orientation)
        {
            case "right":
                hypnoPosX = characterCell.x + 1;
                break;
            case "left":
                hypnoPosX = characterCell.x - 1;
                break;
            case "up":
                hypnoPosZ = characterCell.z + 1;
                break;
            case "down":
                hypnoPosZ = characterCell.z - 1;
                break;
        }

        AddToActionList("move", newPosX, newPosZ, orientation);
        AddToActionList("hypno", hypnoPosX, hypnoPosZ, orientation);
    }

    private void JellyJumpAction(float newPosX, float newPosZ, string orientation)
    {
        AddToActionList("move", newPosX, newPosZ, orientation);
        switch (orientation)
        {
            case "left":
                newPosX = newPosX - 3;
                break;
            case "right":
                newPosX = newPosX + 3;
                break;
            case "down":
                newPosZ = newPosZ - 3;
                break;
            case "up":
                newPosZ = newPosZ + 3;
                break;
        }

        AddToActionList("jump", newPosX, newPosZ, orientation);
    }

    private void MagneticeCreamAction(CharacterCell characterCell, string move, float newPosX, float newPosZ, string orientation)
    {
        var magnetPosX = currentPosX;
        var magnetPosZ = currentPosZ;

        Debug.Log("characterCell TO " + characterCell.x + " : " + newPosX);

        if (characterCell.x == newPosX)
        {
            if (characterCell.z > newPosZ)
            {
                magnetPosZ = characterCell.z - 1;
            } else
            {
                magnetPosZ = characterCell.z + 1;
            }
            magnetPosX = characterCell.x;
        }
        else if(characterCell.z == newPosZ)
        {
            if (characterCell.x > newPosX)
            {
                magnetPosX = characterCell.x - 1;
            }
            else
            {
                magnetPosX = characterCell.x + 1;
            }
            magnetPosZ = characterCell.z;
        }

        AddToActionList("move", newPosX, newPosZ, orientation);
        AddToActionList("hypno", magnetPosX, magnetPosZ, orientation);
    }

    private void DonutPunchAction()
    {

    }

    private void CrossantAction()
    {

    }

    private void LolyPortalAction()
    {

    }




}