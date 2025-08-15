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
            foreach (var rend in FindObjectsByType<Renderer>(FindObjectsSortMode.None))
            {
                if (rend.material.mainTexture is RenderTexture)
                {
                    using var writer = new StreamWriter(logFilePath, append: false);
                    writer.WriteLine(rend.name);
                }
            }
        }
    }
}