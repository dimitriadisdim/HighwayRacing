using Godot;
using System;
using System.Runtime.InteropServices;


public class DataManager: Node
{
    private static readonly string filePath = "user://LockedLevels.cfg";
    private readonly float[] _lanes = { 180, 300, 415, 535};
    private static readonly int _levelCount = 2;
    private static ConfigFile _config;
    public enum LevelState{
        Unlocked,
        Locked,
        Error
    };


#region Levels
    private static int Init(){
        try{
            _config = new ConfigFile();
            var err = _config.Load(filePath);
            //Check if file could load
            if(err != Error.Ok){
                GD.PrintErr("Failed to load level config file, creating...");
                CreateDefault(_config);
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
        for(int i=0; i<_levelCount; i++){
            if(i == 0){
                _config.SetValue("Levels", i.ToString(), LevelState.Unlocked);
                continue;
            }
            _config.SetValue("Levels", i.ToString(), LevelState.Locked);
            GD.Print(i.ToString());
        }
        _config.Save(filePath);
    }

    public static void SetLevelState(LevelState state, int level)
    {
        if(_config == null)
            if(Init() == -1)
                return;

        _config.SetValue("Levels", level.ToString(), state);
    }

    public static LevelState GetLevelState(int level)
    {
        if(_config == null)
            if(Init() == -1)
                return LevelState.Error;
        
        var s = _config.GetValue("Levels", level.ToString(), LevelState.Locked);
        return (LevelState)s;
    }
#endregion

    public float[] GetLanes(){
        return _lanes;
    }
}