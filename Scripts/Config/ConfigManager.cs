using System;
using Godot;

public class ConfigManager
{
    private static ConfigFile _config;
    private static readonly string _configPath = "user://data.cfg";
    
    private static int Init(){
        try{
            _config = new ConfigFile();
            var err = _config.LoadEncryptedPass(_configPath, DataManager.Password);
            //Check if file could load
            if(err != Error.Ok){
                GD.PrintErr("Failed to load config file, creating...");
                CreateDefault(_config);
                _config.SaveEncryptedPass(_configPath, DataManager.Password);
            }
            //All went smoothly
            return 0;
        }catch(Exception e){
            GD.Print(e.Message);
            return -1;
        }
    }

    private static void CreateDefault(ConfigFile _config)
    {
        //Set all levels except the first one to locked
        for(int i=0; i<DataManager.LevelCount; i++){
            if(i == 0){
                _config.SetValue("Levels", i.ToString(), DataManager.LevelState.Unlocked);
                continue;
            }
            _config.SetValue("Levels", i.ToString(), DataManager.LevelState.Locked);
        }
        _config.SaveEncryptedPass(_configPath, DataManager.Password);
    }

#region Score
    public static Int64 GetScore(){
        if(_config == null)
            if(Init() == -1)
                return -1;
        //Config exists or created successfully
        var bestScore = _config.GetValue("Score", LevelPicker.CurrentLevel.ToString(), 0);
        return (int)bestScore;
    }

    public static void WriteScore(Int64 score){
        if(_config == null)
            if(Init() == -1)
                return;
        //Config exists or created successfully
        _config.SetValue("Score", LevelPicker.CurrentLevel.ToString(), score);
        _config.Save(_configPath);
    }
#endregion
#region Levels

    public static void SetLevelState(DataManager.LevelState state, int level)
    {
        if(_config == null)
            if(Init() == -1)
                return;

        _config.SetValue("Levels", level.ToString(), state);
    }

    public static DataManager.LevelState GetLevelState(int level)
    {
        if(_config == null)
            if(Init() == -1)
                return DataManager.LevelState.Error;
        
        var s = _config.GetValue("Levels", level.ToString(), DataManager.LevelState.Locked);
        return (DataManager.LevelState)s;
    }
#endregion
}