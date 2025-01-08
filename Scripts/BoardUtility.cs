using Godot;
using System;
using System.Runtime.InteropServices;

namespace MiniCheckers {
	public partial class BoardUtility : Node
	{

        static int charInt = 65;

        public static string CreatePlacementCode (int x, int z) {
            x = charInt + x;
            
            char charCode = (char) x;

			string placementCode = charCode + "";

			return placementCode + z;
        }

        public static string GetPlacementCode(int x, int z) {
            x = charInt + x;
            
            char charCode = (char) x;

            string placementCode = charCode + "";

            return placementCode + z;
        }

        public static void ResetCharacter () {
            charInt = 65;
        }

        public static void IncrementCharacter () {
            charInt++;
        }



    }
}