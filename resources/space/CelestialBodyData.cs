using Godot;
using System;
using System.Reflection.Metadata.Ecma335;

public partial class CelestialBodyData : Resource
{
    [Export] public float Mass { get; private set; } = 1;

    // 3.986004418E14 * (8.6400E4)^2 / (3.84400E8)^3
    // lunar distance ^ 3 / (seconds in a day)^2 / earth mass
    public float StandardGravitationalParameter => Mass * 0.05238598613f;

    public CelestialBodyData()
    {
    }


}
