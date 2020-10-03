using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Belwyn.Editor.Build {

	// This class applies the corresponding scripting symbols to each target given a BuildConfiguration, or just one of the two default configurations
	public static class BuildSetup {


		// This two symbols are core for some other utilities of this project.
		// They will be added if a configuration is marked as shipping or testing
		// --If you change their value, beware to change de directives in the code!--
		public const string testSymbols = "TEST_BUILD";
		public const string shippingSymbols = "SHIPPING_BUILD";
		


		// You can apply both default configurations if you don't need any other symbols

		[MenuItem("Build/Build Setups/Default Shipping", priority = 101)]
		public static void DefaultShippingSetup() {
			PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, shippingSymbols);
			PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, shippingSymbols);
			PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, shippingSymbols);

			// Apply to more platforms!

			Debug.Log("------ Applied default shipping configuration ------");
		}

		[MenuItem("Build/Build Setups/Default Testing", priority = 102)]
		public static void DefaultTestingSetup() {
			PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, testSymbols);
			PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, testSymbols);
			PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, testSymbols);

			// Apply to more platforms!

			Debug.Log("------ Applied default testing configuration ------");
		}




		// This checks the configuration of a given BuildConfiguration asset, and set the corresponding symbols for each group
		// The override boolean is checked for every other platform to decide wheter to apply the standalone symbols or an especific ones for each platform
		public static void Setup(BuildConfiguration config) {

			string symbols = string.Join(";", config.standaloneConfig.defineSymbols);

			symbols += config.standaloneConfig.isTestingBuild ? $";{testSymbols}" : "";
			symbols += config.standaloneConfig.isShippingBuild ? $";{shippingSymbols}" : "";


			// Standalone

			PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, symbols);


			// Android

			if (config.overrideAndroid) {
				string androidSymbols = string.Join(";", config.androidConfig.defineSymbols);
				PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, androidSymbols);
			} 
			else {
				PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, symbols);
			}


			// iOS

			if (config.overrideIOS) {
				string iosSymbols = string.Join(";", config.iosConfig.defineSymbols);
				PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, iosSymbols);
			} 
			else {
				PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, symbols);
			}


			//
			// Add more platforms here!
			//

			Debug.Log($"------ Applied configuration from {config.name} ------");
		}

	}

}