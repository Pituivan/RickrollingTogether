using System.IO;
using System.Linq;
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

    private void Start()
    {
        using var tempWriter = new StreamWriter(logFilePath, false);
        tempWriter.WriteLine("This is a test");
    }

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
        var parentGameObjects =
            from gameObject in FindObjectsOfType<GameObject>(true)
            where !gameObject.transform.parent
            select gameObject;

        var activeGameObjects =
            from gameObject in parentGameObjects
            where gameObject.activeInHierarchy
            select gameObject;
            
        var nonActiveGameObjects = parentGameObjects.Except(activeGameObjects);

        using var writer = new StreamWriter(logFilePath, false);

        writer.WriteLine("----- ACTIVE GAMEOBJECTS:");
        foreach (var gameObject in activeGameObjects)
            writer.WriteLine(gameObject.name);

        writer.WriteLine("----- NON-ACTIVE GAMEOBJECTS:");
        foreach (var gameObject in nonActiveGameObjects)
            writer.WriteLine(gameObject.name);
    }
}