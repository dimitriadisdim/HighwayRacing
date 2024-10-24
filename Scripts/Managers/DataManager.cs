using Godot;
using System;


public partial class DataManager: Node
{
    public static readonly float[] Lanes = { 180, 300, 415, 535};
    public static readonly int LevelCount = 2;
    public static readonly string Password = "dksadk39012skdskldskdasxc239";
    public enum LevelState{
        Unlocked,
        Locked,
        Error
    };
}