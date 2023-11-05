using Godot;
using System;

public partial class Difficulty : Node3D
{
	[Export] public PackedScene enemy1Scene;
	[Export] public PackedScene enemy2Scene;

	public PathFollow3D spawnLocation;

	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	public void GetSpawnLocation(PathFollow3D spawn)
	{
		spawnLocation = spawn;
		GD.Print("spawn yes");
	}

	public void _on_timer_timeout()
	{
		// Create a new instance of the Mob scene.
		Enemy enemy1 = enemy1Scene.Instantiate<Enemy>();
		Enemy enemy2 = enemy2Scene.Instantiate<Enemy>();
		// Choose a random location on the SpawnPath.
		// We store the reference to the SpawnLocation node.

		Vector3 playerPosition = GetParent().GetNode<Player>("Player").Position;
		// And give it a random offset.
		//spawnLocation.ProgressRatio = GD.Randf();
		enemy1.Initialize(spawnLocation.Position, playerPosition);

		
		enemy2.Initialize(spawnLocation.Position, playerPosition);

		// Spawn the mob by adding it to the Main scene.
		AddChild(enemy1);
		GD.Print("enemy spawned");

		AddChild(enemy2);
		GD.Print("minion spawned");
	}
}
