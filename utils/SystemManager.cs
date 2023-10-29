using Godot;
using System;

public partial class SystemManager : Node
{
    public static readonly string TreeName = "/root/SystemManager";

    [Signal] public delegate void SpaceScaleChangedEventHandler(float spaceScale);

    private float _spaceScale = 1;
    public float SpaceScale // multiply by this to get length in units
    {
        get => _spaceScale; set { _spaceScale = value; EmitSignal(nameof(SpaceScaleChanged), value); }
    }
}
