using Godot;
using System;

namespace MiniCheckers {

	public partial class PlacementService : Node
	{

		private static PlacementService placementService;

		private PackedScene placementScene;

		private static Texture2D lightTexturePlacement;
		private static Texture2D darkTexturePlacement;

		public static PlacementService Instantiate() {
			if (placementService == null) {
				lightTexturePlacement = (Texture2D) GD.Load("res://Textures/LightPlacement_texture.png");
				darkTexturePlacement = (Texture2D) GD.Load("res://Textures/DarkPlacement_texture.png");

				placementService = new PlacementService();
			}

			return placementService;
		} 

		private PlacementService() {
			placementScene = GD.Load<PackedScene>("res://Scenes/Placement.tscn");
		}

		public Placement SetupPlacement(string placementTextureCode, Vector3 position) {
			var placementNode = (Placement) placementScene.Instantiate();

			placementNode.Position = position;

			var placementNodeMeshInstance = (MeshInstance3D) placementNode.FindChild("MeshInstance3D");
			var placementMaterial = new StandardMaterial3D();
			AssignTexture(placementMaterial, placementTextureCode);
			placementNodeMeshInstance.SetSurfaceOverrideMaterial(0, placementMaterial);

			return placementNode;
		}

		private void AssignTexture(StandardMaterial3D placementMaterial, string textureCode) {
			if ("DARK".Equals(textureCode)) {
				placementMaterial.AlbedoTexture = darkTexturePlacement;
			}
			
			if ("LIGHT".Equals(textureCode)) {
				placementMaterial.AlbedoTexture = lightTexturePlacement;
			}
			
			if (placementMaterial.AlbedoTexture == null) {
				throw new Exception("Specified Texture Code is not supported: " + textureCode);
			}
		}
	}

}

