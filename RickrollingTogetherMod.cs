using System.IO;
using BepInEx;
using Rewired;
using UnityEngine;

namespace RickrollingTogether;

[BepInPlugin("com.pituivan.rickrollingtogether", "Rickrolling Together", "1.0.0")]
public class RickrollingTogetherMod : BaseUnityPlugin
{
    // ----- Private Fields

    private const string logFilePath =
        @"C:\Users\Pituivan\Documents\Projects\Repos\Rickrolling Together\TestLog.txt";

    private Player player;

    // ----- Unity Callbacks

    void Update()
    {
        player ??= ReInput.players.GetPlayer(0);
        if (player.controllers.Keyboard.GetKeyDown(KeyCode.N))
        {
            LogGameObjects();
        }
    }

    // ----- Private Methods

    private void LogGameObjects()
    {
        var supermarket = GameObject.Find("Level_Supermarket");

        using var writer = new StreamWriter(logFilePath, false);
        LogGameObjectChildrenRecursively(supermarket, writer);
    }

    private void LogGameObjectChildrenRecursively(GameObject gameObject, StreamWriter writer, int indentLvl = 0)
    {
        string indent = new(' ', 2 * indentLvl);
        writer.WriteLine(indent + gameObject.name);

        foreach (Transform child in gameObject.transform)
        {
            LogGameObjectChildrenRecursively(child.gameObject, writer, ++indentLvl);
        }
    }
}