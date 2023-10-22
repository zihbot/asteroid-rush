using Godot;
using System;

public partial class OrbitCreator : Control
{
    private Node _target = null;
    [Export] public Node Target { get => _target; set { _target = value; UpdateConfigurationWarnings(); } }
    [Export] public PackedScene OrbitScene { get; set; } = null;

    private OrbitDrawer _orbitDrawer = null;
    private MeshInstance3D _orbitDrawerMeshInstance = null;
    private OrbitData _orbitData = new();

    private Slider _velocity;
    private Label _velocityLabel;

    public override void _Ready()
    {
        _velocity = GetNode<Slider>("Grid/Velocity");
        _velocityLabel = GetNode<Label>("Grid/VelocityLabel");
        _velocity.ValueChanged += ValueChanged;

        if (OrbitScene == null || Target == null)
            return;
        var orbitScene = OrbitScene.Instantiate();
        _orbitDrawer = orbitScene.GetNode<OrbitDrawer>("Drawer");
        _orbitDrawer.OrbitData = _orbitData;
        Target.AddChild(orbitScene);

        _velocity.Value = 50;
    }

    private void ValueChanged(double value)
    {
        _velocityLabel.Text = value.ToString();
        _orbitData.VelocityAtPeriapsis = _orbitData.VelocityAtPeriapsis.Normalized() * (float)value / 100;
    }

    public override string[] _GetConfigurationWarnings()
    {
        if (Target == null)
            return new string[] { "Target is not set" };
        return Array.Empty<string>();
    }
}
