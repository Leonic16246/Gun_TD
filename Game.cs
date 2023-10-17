using Godot;
using System;

public partial class Game : Node3D
{
	[Export] private PanelContainer gameMenu;
	[Export] public PackedScene playerScene;
	[Export] public PackedScene enemyScene;
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
			Node playerInstance = playerScene.Instantiate();
			AddChild(playerInstance);
			GD.Print("new game");
		} else
		{
			GD.Print("error loading player scene");
		}

		// spawning enemy
		Node enemyInstance = enemyScene.Instantiate();
		AddChild(enemyInstance);
		GD.Print("enemy spawned");
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
