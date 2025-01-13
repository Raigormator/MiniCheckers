using Godot;
using System;
using System.Collections.Generic;

namespace MiniCheckers {
	public partial class PieceService : Node
	{
		
		public static PieceService pieceService;

		private PackedScene pieceScene;

		private static Texture2D whiteTexturePiece;
		private static Texture2D blackTexturePiece;

		public static PieceService Instantiate() {
			if (pieceService == null) {
				whiteTexturePiece = (Texture2D) GD.Load("res://Textures/WhitePiece_texture.png");
				blackTexturePiece = (Texture2D) GD.Load("res://Textures/BlackPiece_texture.png");

				pieceService = new PieceService();
			}

			return pieceService;
		}

		private PieceService() {
			pieceScene = GD.Load<PackedScene>("res://Scenes/Piece.tscn");
		}

		public void LayPieces(SortedDictionary<string, Placement> placementsByCode, int boardSize) {
			int row = boardSize;

			GenerateWhitePieces(placementsByCode);

			GenerateBlackPieces(placementsByCode);
		}

		private void GenerateBlackPieces(SortedDictionary<string, Placement> placementsByCode) {
			BoardUtility.ResetCharacter();

			int zStrt = 2;

            for (int z = 3; 1 <= z; z--) {
				for (int x = zStrt; 0 <= x; x--) {
                    string placementCode = BoardUtility.GetPlacementCode(x, z);
					Console.WriteLine("Black Pieces: " + placementCode);

                    Placement placement = placementsByCode.GetValueOrDefault(placementCode, null);
					if (placement == null) {
						throw new Exception("Placement is not found in: " + placementCode);
					}

                    var pieceNode = (Piece) pieceScene.Instantiate();
					ApplyMeshTexture(pieceNode, "BLACK");

					placement.AddChild(pieceNode);

					placement.Piece = pieceNode;

					pieceNode.Position += new Vector3(0, 0.60f, 0); //Vector3.Up;
				}
				zStrt--;
 			}
		}
	

		private void GenerateWhitePieces(SortedDictionary<string, Placement> placementsByCode) {

			BoardUtility.ResetCharacter();

			int zStrt = 2;

            for (int x = 3; 1 <= x; x--) {
				for (int z = zStrt; 0 <= z; z--) {
                    string placementCode = BoardUtility.GetPlacementCode(x, z);
					Console.WriteLine("White Pieces: " + placementCode);

                    Placement placement = placementsByCode.GetValueOrDefault(placementCode, null);
					if (placement == null) {
						throw new Exception("Placement is not found in: " + placementCode);
					}

                    var pieceNode = (Piece) pieceScene.Instantiate();
					ApplyMeshTexture(pieceNode, "WHITE");

					placement.AddChild(pieceNode);

					placement.Piece = pieceNode;

					pieceNode.Position += new Vector3(0, 0.60f, 0); //Vector3.Up;
				}
				zStrt--;
			}
		}

		private void ApplyMeshTexture(Piece pieceNode, string textureCode) {
			var pieceNodeMeshInstance = (MeshInstance3D) pieceNode.FindChild("MeshInstance3D");
			var pieceMaterial = new StandardMaterial3D();
			AssignTexture(pieceMaterial, textureCode);
			pieceNodeMeshInstance.SetSurfaceOverrideMaterial(0, pieceMaterial);
		}

		private void AssignTexture(StandardMaterial3D pieceMaterial, string textureCode) {
			if ("BLACK".Equals(textureCode)) {
				pieceMaterial.AlbedoTexture = blackTexturePiece;
			}
			
			if ("WHITE".Equals(textureCode)) {
				pieceMaterial.AlbedoTexture = whiteTexturePiece;
			}
			
			if (pieceMaterial.AlbedoTexture == null) {
				throw new Exception("Specified Texture Code is not supported: " + textureCode);
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