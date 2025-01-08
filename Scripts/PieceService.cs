using Godot;
using System;
using System.Collections.Generic;

namespace MiniCheckers {
	public partial class PieceService : Node
	{
		
		public static PieceService pieceService;

		private PackedScene pieceScene;

		public static PieceService Instantiate() {
			if (pieceService == null) {
				pieceService = new PieceService();
			}

			return pieceService;
		}

		private PieceService() {
			pieceScene = GD.Load<PackedScene>("res://Scenes/Piece.tscn");
		}

		public void LayPieces(SortedDictionary<string, Placement> placementsByCode, int boardSize) {
			int row = boardSize;

			BoardUtility.ResetCharacter();
			
			int zStrt = 2;

            for (int x = 3; 1 <= x; x--) {
				for (int z = zStrt; 0 <= z; z--) {
                    string placementCode = BoardUtility.GetPlacementCode(x, z);
					Console.WriteLine("Placement: " + placementCode);

                    Placement placement = placementsByCode.GetValueOrDefault(placementCode, null);
					if (placement == null) {
						throw new Exception("Placement is not found in: " + placementCode);
					}

                    var pieceNode = (Piece) pieceScene.Instantiate();

					placement.AddChild(pieceNode);

					placement.Piece = pieceNode;

					pieceNode.Position += Vector3.Up;
				}
				zStrt--;
			}

			for (int x = 4; x <  4; x++) {
				for (int z = 4; z < row; z++) {
					
				}
				row--;
			}
		}

	}
}

// Placement: D3
// Placement: D2
// Placement: D1
// Placement: D0
// Placement: C2
// Placement: C1
// Placement: C0
// Placement: B1

// Placement: D3
// Placement: D2
// Placement: D1
// Placement: C2
// Placement: C1
// Placement: B1

// Placement: C2
// Placement: C1
// Placement: C0
// Placement: B1
// Placement: B0
// Placement: A0

// Placement: B2
// Placement: B1
// Placement: B0
// Placement: C1
// Placement: C0
// Placement: D0