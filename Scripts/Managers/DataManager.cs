using System;
using Godot;


public class DataManager: Node
{
    private float[] _lanes = { 180, 300, 415, 535};

    public float[] GetLanes(){
        return _lanes;
    }
}