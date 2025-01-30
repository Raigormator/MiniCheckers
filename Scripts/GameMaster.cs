using Godot;
using System;
using System.Collections.Generic;

namespace MiniCheckers {
	public partial class GameMaster : Node3D
	{	

		public RigidBody3D grabbedObject = null;
		public float grabDistance = 10;
		public Vector2 mouse;
		public const float DIST = 1000;
		public StaticBody3D boardPlacement = null;
		public Placement placement = null;
		private PlacementService placementService;

		public override void _Ready()
		{
			placementService = PlacementService.Instantiate();
		}

		public override void _Process(double delta)
		{
			if (grabbedObject == null) {
				return;
			}

			if (grabbedObject is RigidBody3D) {
				ShowGhostPiece(boardPlacement);
			} else {
				grabbedObject.Position = GetGrabPosition();
			}
		}

		public override void _Input(InputEvent @event) {
			if (@event is InputEventMouseMotion) {
				mouse = ((InputEventMouseMotion) @event).Position;
				
				GetMouseWorldPos(mouse);
			}

			if (@event is InputEventMouseButton) {
				InputEventMouseButton eventMouseButton = (InputEventMouseButton) @event;  
				
				if (eventMouseButton.IsPressed() == false && eventMouseButton.ButtonIndex == MouseButton.Left) {
					GetMouseWorldPos(mouse);
				} else if (eventMouseButton.IsPressed() == false && eventMouseButton.ButtonIndex == MouseButton.Right) {
					grabbedObject = null;
				}
			}
		}

		private void GetMouseWorldPos(Vector2 mouse) {
			var space = GetWorld3D().DirectSpaceState;
			var start = GetViewport().GetCamera3D().ProjectRayOrigin(mouse);
			var end = GetViewport().GetCamera3D().ProjectPosition(mouse, DIST);
			var query = new PhysicsRayQueryParameters3D();
			query.CollideWithBodies = true;
			query.From = start;
			query.To = end;

			var result = space.IntersectRay(query);
			foreach (KeyValuePair<Variant, Variant> variant in result) {
				if (((string) variant.Key).Equals("collider") && variant.Value.Obj is RigidBody3D) {
					grabbedObject = (RigidBody3D) variant.Value.Obj;	
				}

				if (((string) variant.Key).Equals("collider") && variant.Value.Obj is StaticBody3D) {
					boardPlacement = (StaticBody3D) variant.Value.Obj;
				}
			}
		}

		private Vector3 GetGrabPosition() {
			return GetViewport().GetCamera3D().ProjectPosition(mouse, grabDistance);
		}

		private void ShowGhostPiece(StaticBody3D boardPlacement) {
			if (!(boardPlacement.GetParent() is Placement)) {
				return;
			}

			Placement varPlacement = (Placement) boardPlacement.GetParent();

			if (placement != null && placement.PlacementCode == varPlacement.PlacementCode) {
				return; 
			}

			placement = boardPlacement.GetParent() as Placement;

			Console.WriteLine("Placement is Valid: " + placement);
		}

		// private void LiftItem(RigidBody3D item, Vector3 targetPosition, float delta) {
		// 	float influence = 500.0f;
		// 	float stiffness = 20.0f;
		// 	targetPosition.Y = 2f;
		// 	var position = targetPosition - item.GlobalPosition;
		// 	var mass = item.Mass;
		// 	var velocity = item.LinearVelocity;
		// 	var impulse = (influence * position) - (stiffness * mass * velocity);
		// 	item.ApplyCentralImpulse(impulse * delta);
		// }
	}
}