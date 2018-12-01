using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(MapCreator2))]
public class MapCreatorEditor2 : Editor {

	public override void OnInspectorGUI() {

		DrawDefaultInspector();
		GUILayout.Space(10);

		MapCreator2 myTarget = (MapCreator2)target;

		if (GUILayout.Button("Generate map links")) {
			// myTarget.GenerateLinks();
			EditorUtility.SetDirty(myTarget);
			EditorSceneManager.MarkSceneDirty(myTarget.gameObject.scene);
		}
	}
	
}
