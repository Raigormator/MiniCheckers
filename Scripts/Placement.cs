using Godot;
using System;

namespace MiniCheckers
{
	public partial class Placement : Node3D
	{

		private string _placementCode;

		private Piece _piece;

		private PlacementLabel _placementLabel;

		public string PlacementCode {
			get { return _placementCode; }
			set { _placementCode = value; }
		}

		public Piece Piece {
			get { return _piece; }
			set { _piece = value; }
		}

		public PlacementLabel PlacementLabel {
			get { return _placementLabel; }
			set { _placementLabel = value; }
		}

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}
	}
}

