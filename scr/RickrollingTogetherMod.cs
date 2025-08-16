using System;
using System.IO;
using System.Linq;
using BepInEx;
using Rewired;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace RickrollingTogether;

[BepInPlugin("com.pituivan.rickrollingtogether", "Rickrolling Together", "1.0.0")]
public class RickrollingTogetherMod : BaseUnityPlugin
{
    // ----- Private Fields

    private readonly Lazy<string> modPath = new(() =>
        Paths.PluginPath + "\\Rickrolling Together");
    
    private readonly Lazy<RenderTexture> renderTexture = new(() =>
        new RenderTexture(640, 360, 0));
    
    // ----- Properties
    
    
    // ----- Unity Callbacks
    
    void Start()
    {
        Log("This is a test");
        
        SceneManager.sceneLoaded += (scene, _) =>
        {
            Log($"Scene {scene.buildIndex} loaded");
            
            const string storeSceneName = "B_Main";
            if (scene.name == storeSceneName) OnStoreSceneLoaded();
        };
    }

    void Update()
    {
        if (ReInput.players.GetPlayer(0).controllers.Keyboard.GetKeyDown(KeyCode.N))
        {
            var tvRends =
                from rend in FindObjectsByType<Renderer>(FindObjectsInactive.Include, FindObjectsSortMode.None)
                where rend.material.mainTexture is RenderTexture
                select rend;

            foreach (Renderer rend in tvRends)
                SetUpRickroll(rend);
        }
    }

    // ----- Private Methods

    private void OnStoreSceneLoaded()
    {
        Log("Store Scene Loaded");
        
        var tvRends =
            from rend in FindObjectsByType<Renderer>(FindObjectsInactive.Include, FindObjectsSortMode.None)
            where rend.material.mainTexture is RenderTexture
            select rend;

        foreach (Renderer rend in tvRends)
            SetUpRickroll(rend);
    }

    private void SetUpRickroll(Renderer target)
    {
        var videoPlayer = target.gameObject.AddComponent<VideoPlayer>();
        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = modPath.Value + "\\Rickroll.mp4";
        videoPlayer.isLooping = true;
        
        var audioSource = target.gameObject.AddComponent<AudioSource>();
        videoPlayer.SetTargetAudioSource(0, audioSource);

        videoPlayer.targetTexture = renderTexture.Value;
        target.material.mainTexture = renderTexture.Value;

        videoPlayer.Play();
        
        Log("Rickroll set up");
    }

    private void Log(string msg)
    {
        using var writer = new StreamWriter(
            path: @"C:\Users\Pituivan\Documents\Projects\Repos\Rickrolling Together\Logs.txt", 
            append: true
            );
        
        writer.WriteLine(msg);
    }
}