using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SaveMesh))]
public class SaveMeshEditor : Editor
{
	private string path;

	public override void OnInspectorGUI()
	{
		SaveMesh saveMesh = (SaveMesh)target;
		saveMesh.optimizeMesh = EditorGUILayout.ToggleLeft("Optimize Mesh", saveMesh.optimizeMesh);

		if (GUILayout.Button("Save mesh to project"))
		{
			if (!saveMesh.meshFilter || !saveMesh.meshFilter.sharedMesh)
			{
				Debug.LogWarning("No mesh filter found OR mesh is null!", target);
				return;
			}

			path = EditorUtility.SaveFilePanelInProject("Mesh path", saveMesh.gameObject.name, "asset", "Save mesh to...");
			if (string.IsNullOrWhiteSpace(path)) return;

			if (saveMesh.optimizeMesh) MeshUtility.Optimize(saveMesh.meshFilter.sharedMesh);

			AssetDatabase.CreateAsset(saveMesh.meshFilter.sharedMesh, path);
			AssetDatabase.SaveAssets();
		}
	}
}