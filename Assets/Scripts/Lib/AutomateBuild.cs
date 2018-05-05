using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;

public static class AutomateBuild {

#if UNITY_EDITOR
	public static void BuildIOS() {
		BuildPipeline.BuildPlayer (SceneNames (), "Build/iOS", BuildTarget.iOS, BuildOptions.None);
	}

	private static string[] SceneNames() {
		List<string> scenes = new List<string> ();

		for (int i = 0; i < EditorBuildSettings.scenes.Length; i++) {
			EditorBuildSettingsScene scene = EditorBuildSettings.scenes[i];

			if(scene.enabled) {
				scenes.Add (scene.path);
			}
		}
		return scenes.ToArray();
	}
#endif
}