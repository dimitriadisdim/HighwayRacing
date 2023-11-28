using System;
using Godot;

public class ConfigManager
{
    private static ConfigFile config;
    private static readonly string filePath = "user://score.cfg";

    
    private static int Init(){
        try{
            config = new ConfigFile();
            var err = config.Load(filePath);
            //Check if file could load
            if(err != Error.Ok){
                GD.PrintErr("Failed to load config file, creating...");
                config.Save(filePath);
            }
            //All went smoothly
            return 0;
        }catch(Exception e){
            GD.Print(e.Message);
            return -1;
        }
    }

    public static Int64 GetScore(){
        if(config == null)
            if(Init() == -1)
                return 0;
        //Config exists or created successfully
        var bestScore = config.GetValue("Score", "Best", 0);
        return (int)bestScore;
    }

    public static void WriteScore(Int64 score){
        if(config == null)
            if(Init() == -1)
                return;
        //Config exists or created successfully
        config.SetValue("Score", "Best", score);
        config.Save(filePath);
    }
}