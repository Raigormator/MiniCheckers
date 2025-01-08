using Godot;
using System;
using System.Collections.Generic;

namespace MiniCheckers {
	public partial class Board : Node3D
	{

		SortedDictionary<string, Placement> placementsByCode = new SortedDictionary<string, Placement>();

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{	
			BoardService boardService = BoardService.Instantiate();
			PieceService pieceService = PieceService.Instantiate();

			int boardSize = 4;
			boardService.CreateBoard(boardSize, this);
			pieceService.LayPieces(placementsByCode, boardSize);
		}

		public void AddChild(string placementCode, Placement placement) {
			placement.PlacementCode = placementCode;
			placementsByCode.Add(placementCode, placement);
			AddChild(placement);
		}

		public Placement GetPlacementByCode(string placementCode) {
			Placement placement = placementsByCode.GetValueOrDefault(placementCode, null);
			
			if (placement == null) {
				throw new Exception("ERROR: placementCode not found: " + placementCode);
			}

			return placement;
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			
		}
	}
}
