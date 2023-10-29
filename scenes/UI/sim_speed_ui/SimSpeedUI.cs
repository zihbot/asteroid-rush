using Godot;
using System;

public partial class SimSpeedUI : Control
{
    private Button s1;
    private Button s2;
    private Button s3;
    private Button s4;
    private Button s5;
    private Button play;
    private Button pause;
    private Label time;

    private double sec = 0;
    private long deltaTime = 0;


	public override void _Ready()
	{
        s1 = GetNode<Button>("Grid/S1");
        s2 = GetNode<Button>("Grid/S2");
        s3 = GetNode<Button>("Grid/S3");
        s4 = GetNode<Button>("Grid/S4");
        s5 = GetNode<Button>("Grid/S5");
        play = GetNode<Button>("Grid/Play");
        pause = GetNode<Button>("Grid/Pause");
        time = GetNode<Label>("Grid/Time");

        s1.Disabled = true;
        s2.Disabled = true;
        s3.Disabled = true;
        s4.Disabled = true;
        s5.Disabled = true;

        pause.Pressed += () => Engine.TimeScale = 0;
        play.Pressed += () => Engine.TimeScale = 1;
	}

	public override void _Process(double delta)
	{
        sec += delta;
        deltaTime += Mathf.FloorToInt(sec);
        sec -= Mathf.Floor(sec);
        time.Text = $"{deltaTime}s";
	}
}
