using Godot;
using System;

public partial class Enemy : CharacterBody3D
{
	[Export] public float speed = 1.0f;
	[Export] public float jumpVelocity = 5f;
	[Export] public int health;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

	public void RecieveDamage()
	{
		health -= 100;
		GD.Print(health);
		if (health <= 0)
		{
			QueueFree();
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity.Y -= gravity * (float)delta;
		}
	
		Velocity = velocity;
		MoveAndSlide();
	}

	public void Initialize(Vector3 startPosition, Vector3 playerPosition)
	{
		// We position the mob by placing it at startPosition
		// and rotate it towards playerPosition, so it looks at the player.
		LookAtFromPosition(startPosition, playerPosition, Vector3.Up);
		// Rotate this mob randomly within range of -45 and +45 degrees,
		// so that it doesn't move directly towards the player.
		RotateY((float)GD.RandRange(-Mathf.Pi / 4.0, Mathf.Pi / 4.0));
	}
}
