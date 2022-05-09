using UnityEngine;

[ExecuteInEditMode]
public class SaveMesh : MonoBehaviour
{
	public bool optimizeMesh;
	public MeshFilter meshFilter;

	private void OnEnable()
	{
		meshFilter = GetComponent<MeshFilter>();
	}
}