using Godot;
using System;

public partial class Pause : Node3D
{
	[Export] private PanelContainer pauseMenu;
	public bool pauseMenuUp = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	public void _on_resume_button_pressed()
	{
		pauseMenu.Hide();
		//GetTree().Paused = false;
		Godot.Input.MouseMode = Godot.Input.MouseModeEnum.Captured;
		pauseMenuUp = false;
	}

	public void _on_load_button_pressed()
	{
		
	}

	public void _on_settings_button_pressed()
	{
		
	}

	public void _on_main_menu_button_pressed()
	{
		Game g = (Game)GetTree().Root.GetNode("Game");
		g.backToStart();
		
	}

	public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("esc"))
		{
			if (pauseMenuUp)
			{
				pauseMenu.Hide();
				//GetTree().Paused = false;
				Godot.Input.MouseMode = Godot.Input.MouseModeEnum.Captured;
				pauseMenuUp = false;
			} else
			{
				pauseMenu.Show();
				//GetTree().Paused = true;
				Godot.Input.MouseMode = Godot.Input.MouseModeEnum.Visible;
				pauseMenuUp = true;
				//GetNode<Player>("Player");
			}
			
		}
    }
}
