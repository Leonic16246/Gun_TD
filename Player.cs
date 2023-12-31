using Godot;
using System;

public partial class Player : CharacterBody3D
{
	public float Speed;
	public float WalkSpeed = 5.0f;
	public float SprintSpeed = 10.0f;
	public const float JumpVelocity = 5.0f;
	public float sensitivity = 0.1f;
	[Export] private Node3D head;
	[Export] private AnimationPlayer animation;

	private Game game;

	

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

	public override void _Ready() {
        Godot.Input.MouseMode = Godot.Input.MouseModeEnum.Captured;
		game = (Game)GetParent();
		
	}

	public override void _Input(InputEvent @event)
	{
		GD.Print(game.gameMenuUp);

		if (!game.gameMenuUp)
		{
			if (@event is InputEventMouseMotion mouseMotion)
			{
				RotateY(Mathf.DegToRad(-mouseMotion.Relative.X * sensitivity));
				head.RotateX(Mathf.DegToRad(-mouseMotion.Relative.Y * sensitivity));
				Vector3 rotate = head.Rotation;
				rotate.X = Mathf.Clamp(head.Rotation.X, -Mathf.Pi/2, Mathf.Pi/2);
				head.Rotation = rotate;
			}
			if (Input.IsActionJustPressed("shoot"))
			{
				
				animation.Stop(); 
				animation.Play("shoot");
			}
		}
		
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		if (Godot.Input.IsActionPressed("sprint"))
		{
            Speed = SprintSpeed;
		} else
		{
            Speed = WalkSpeed;
		}

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y -= gravity * (float)delta;

		// Handle Jump.
		if (Godot.Input.IsActionJustPressed("ui_accept") && IsOnFloor())
			velocity.Y = JumpVelocity;

        // Get the input direction and handle the movement/deceleration.
        // As good practice, you should replace UI actions with custom gameplay actions.
        Vector2 inputDir = Godot.Input.GetVector("move_left", "move_right", "move_forward", "move_back");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
			if (animation.CurrentAnimation != "shoot")
			{
				animation.Play("move");
			}
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
			if (animation.CurrentAnimation != "shoot")
			{
				animation.Play("idle");
			}
			
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
