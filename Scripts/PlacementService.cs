using Godot;
using System;

namespace MiniCheckers {

	public partial class PlacementService : Node
	{

		private static PlacementService placementService;

		private PackedScene placementScene;
		private PackedScene placementLabelScene;

		private static Texture2D lightTexturePlacement;
		private static Texture2D darkTexturePlacement;

		public static PlacementService Instantiate() {
			if (placementService == null) {
				placementService = new PlacementService();
			}

			return placementService;
		} 

		private PlacementService() {
			lightTexturePlacement = (Texture2D) GD.Load("res://Textures/LightPlacement_texture.png");
			darkTexturePlacement = (Texture2D) GD.Load("res://Textures/DarkPlacement_texture.png");

			placementScene = GD.Load<PackedScene>("res://Scenes/Placement.tscn");
			placementLabelScene = GD.Load<PackedScene>("res://Scenes/PlacementLabel.tscn");
		}

		public Placement SetupPlacement(string placementTextureCode, Vector3 position, string placementCode) {
			var placementNode = (Placement) placementScene.Instantiate();

			placementNode.Position = position;

			var placementNodeMeshInstance = (MeshInstance3D) placementNode.FindChild("MeshInstance3D");
			var placementMaterial = new StandardMaterial3D();
			AssignTexture(placementMaterial, placementTextureCode);
			placementNodeMeshInstance.SetSurfaceOverrideMaterial(0, placementMaterial);

			var placementLabelNode = (PlacementLabel) placementLabelScene.Instantiate();
			placementLabelNode.Position += new Vector3(0, 0.60f, 0);

			var label3D = (Label3D) placementLabelNode.FindChild("Label3D");
			label3D.Text = placementCode;

			placementNode.AddChild(placementLabelNode);

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

