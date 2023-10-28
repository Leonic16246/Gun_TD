using Godot;
using System;

public partial class Game : Node3D
{
	[Export] public PackedScene mainScene;
	[Export] public PackedScene pauseScene;
	[Export] public PackedScene playerScene;

	Map map;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		StartGame();
	}

	public void StartGame()
	{
		if (mainScene != null)
		{
			Main main = mainScene.Instantiate<Main>();
			AddChild(main);
			GD.Print("main menu");
		} else
		{
			GD.Print("error loading main menu");
		}
	}

	public void backToStart()
	{
		if (GetChildCount() > 0)
		{
			foreach (var n in GetChildren())
			{
				RemoveChild(n);
			}
		}
		StartGame();
	}

	public void InitializeGame(String mapString, PackedScene difficultyScene)
	{
		// pause
		if (pauseScene != null)
		{
			Pause pause = pauseScene.Instantiate<Pause>();
			AddChild(pause);
		} else
		{
			GD.Print("error pause menu");
		}

		PackedScene mapScene = ResourceLoader.Load<PackedScene>(mapString);
		// map
		if (mapScene != null)
		{
			map = mapScene.Instantiate<Map>();
			AddChild(map);
			GD.Print("map:" + map + " loaded");
		} else
		{
			GD.Print("error loading map");
		}

		// spawn/difficulty
		if (mapScene != null)
		{
			
			Difficulty mode = difficultyScene.Instantiate<Difficulty>();
			mode.GetSpawnLocation(map.pathFollow);
			AddChild(mode);
			GD.Print("found mode");
		} else
		{
			GD.Print("error finding spawn/mode");
		}
	
		// adding player
		if (playerScene != null)
		{
			Player player = playerScene.Instantiate<Player>();
			AddChild(player);
			GD.Print("player loaded");
		} else
		{
			GD.Print("error loading player scene");
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{

	}

}
