using Godot;
using System;

public partial class Game : Node3D
{
	[Export] private PanelContainer gameMenu;
	[Export] public PackedScene playerScene;
	[Export] public PackedScene enemyScene { get; set; }

	[Export] public PathFollow3D spawnLocation;
	public bool gameMenuUp = false;


	public bool getMainMenuUp()
	{
		return gameMenuUp;//
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// adding player
		if (playerScene != null)
		{
			Player player = playerScene.Instantiate<Player>();
			AddChild(player);
			GD.Print("new game");
		} else
		{
			GD.Print("error loading player scene");
		}

		// spawning enemy
		// Node enemyInstance = enemyScene.Instantiate();
		// AddChild(enemyInstance);
		// GD.Print("enemy spawned");
	}

	public void _on_resume_button_pressed()
	{
		gameMenu.Hide();
		//GetTree().Paused = false;
		Godot.Input.MouseMode = Godot.Input.MouseModeEnum.Captured;
		gameMenuUp = false;
	}

	public void _on_load_button_pressed()
	{
		
	}

	public void _on_settings_button_pressed()
	{
		
	}

	public void _on_main_menu_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://Main.tscn");
	}

	public void _on_timer_timeout()
	{
		// Create a new instance of the Mob scene.
		Enemy enemy = enemyScene.Instantiate<Enemy>();

		// Choose a random location on the SpawnPath.
		// We store the reference to the SpawnLocation node.

		// And give it a random offset.
		spawnLocation.ProgressRatio = GD.Randf();

		Vector3 playerPosition = GetNode<Player>("Player").Position;
		enemy.Initialize(spawnLocation.Position, playerPosition);

		// Spawn the mob by adding it to the Main scene.
		AddChild(enemy);
		GD.Print("enemy spawned");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{

	}

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("esc"))
		{
			if (gameMenuUp)
			{
				gameMenu.Hide();
				//GetTree().Paused = false;
				Godot.Input.MouseMode = Godot.Input.MouseModeEnum.Captured;
				gameMenuUp = false;
			} else
			{
				gameMenu.Show();
				//GetTree().Paused = true;
				Godot.Input.MouseMode = Godot.Input.MouseModeEnum.Visible;
				gameMenuUp = true;
				//GetNode<Player>("Player");
			}
			
		}
    }

}
