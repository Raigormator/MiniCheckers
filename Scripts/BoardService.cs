using Godot;
using System;

namespace MiniCheckers {
	public partial class BoardService : Node
	{

		private PlacementService placementService;

		private static BoardService boardService;

		public static BoardService Instantiate() {
			if (boardService == null) {
				boardService = new BoardService();
			}

			return boardService;
		}

		private BoardService() {
			placementService = PlacementService.Instantiate();
		}

		public void CreateBoard(int boardSize, Board board) {
			var xPos = board.Position.X;
			var zPos = board.Position.Z;

			BoardUtility.ResetCharacter();

			for (int x = 0; x < boardSize; x++) {
				for (int z = 0; z < boardSize; z++) {
					Placement placementNode;
					string placementCode = BoardUtility.CreatePlacementCode(x, z);

					if ((x + z) % 2 == 0) {
						placementNode = placementService.SetupPlacement("DARK", new Vector3(xPos, 0, zPos), placementCode);
					} else {
						placementNode = placementService.SetupPlacement("LIGHT", new Vector3(xPos, 0, zPos), placementCode);
					}
					board.AddChild(placementCode, placementNode);

					zPos += 1;
				}
				zPos = 0;
				xPos += 1;
			}
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}
	}
}