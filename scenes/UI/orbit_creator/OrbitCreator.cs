using Godot;
using System;

public partial class OrbitCreator : Control
{
    private Node _target = null;
    [Export] public Node Target { get => _target; set { _target = value; UpdateConfigurationWarnings(); } }

    private OrbitDrawer _orbitDrawer = null;
    private MeshInstance3D _orbitDrawerMeshInstance = null;
    private OrbitData _orbitData = new();

    private Slider _velocity;
    private Label _velocityLabel;

    public override void _Ready()
    {
        _velocity = GetNode<Slider>("Grid/Velocity");
        _velocityLabel = GetNode<Label>("Grid/VelocityLabel");

        _orbitDrawer = new() { OrbitData = _orbitData };
        _orbitDrawerMeshInstance = new();
        _orbitDrawerMeshInstance.SetScript(_orbitDrawer);
        Target?.AddChild(_orbitDrawerMeshInstance);

        _velocity.ValueChanged += ValueChanged;

        _velocity.Value = 50;
    }

    private void ValueChanged(double value)
    {
        _velocityLabel.Text = value.ToString();
    }

    public override string[] _GetConfigurationWarnings()
    {
        if (Target == null)
            return new string[] { "Target is not set" };
        return Array.Empty<string>();
    }
}
