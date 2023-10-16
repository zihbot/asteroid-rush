using Godot;
using System;

public partial class OrbitData : Resource
{
    [Export] public float SemiMajorAxis { get; set; } = 1;
    [Export] public float Eccentricity { get; set; } = 0;
    [Export] public float Inclination { get; set; } = 0;
    [Export] public float LongitudeOfAscendingNode { get; set; } = 0;
    [Export] public float ArgumentOfPeriapsis { get; set; } = 0;
    [Export] public float MeanAnomaly { get; set; } = 0;

    public OrbitData()
    {
    }
}
