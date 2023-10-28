using Godot;
using System;

public partial class Main : Node3D
{
	[Export] private PanelContainer mainMenu;
	[Export] private PanelContainer MapMenu;

	[Export] private PackedScene easyScene;
	private bool mainMenuUp = true;
	private bool mapSelected = false;

	Game game;
	Map map;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		game = (Game)GetParent();
	}

	public void _on_new_button_pressed()
	{
		mainMenu.Hide();
		mainMenuUp = false;
		MapMenu.Show();
		
	}

	// have difficulty selection, confirm button, pressed buttons highlighted
	public void _on_map_1_button_pressed()
	{
		game.InitializeGame("res://Map1.tscn", easyScene);
		QueueFree();
	}
	public void _on_map_2_button_pressed()
	{
		game.InitializeGame("res://Map2.tscn", easyScene);
		QueueFree();
	}
	public void _on_back_button_pressed()
	{
		MapMenu.Hide();
		mainMenu.Show();
		mainMenuUp = true;
	}
	
	public void _on_load_button_pressed()
	{
		
	}

	public void _on_settings_button_pressed()
	{
		
	}

	public void _on_quit_button_pressed()
	{
		GetTree().Quit();
	}

	public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("esc"))
		{
			if (!mainMenuUp)
			{
				MapMenu.Hide();
				//hide other menus

				mainMenu.Show();
				mainMenuUp = true;
			} else
			{
				GetTree().Quit();
			}
		}
    }
}
