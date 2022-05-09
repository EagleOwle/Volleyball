using System;
using System.Collections.Generic;
using UnityEngine;

public class WindCreator : MonoBehaviour
{
	/*
	 * Script variables
	 */

	private Mesh treeMesh;
	private List<Material> mats = new List<Material>();
	private bool barkMatFirst;

	/*
	 * Vertex colors variables
	 */

	[SerializeField]
	[Tooltip("Allow the script to overwrite the values of this channel")]
	private bool overwriteColorsRed = true;

	[SerializeField]
	[Tooltip("The type of gradient that will be applied to the vertex red color")]
	private GradientType colorGradientType = GradientType.Plain;

	[SerializeField]
	[Tooltip("How much the red value of the vertex colors should be painted?")]
	private float colorPercent = 1f;

	[SerializeField]
	[Tooltip("Allow the script to overwrite the values of this channel")]
	private bool overwriteColorsGreen = true;

	[SerializeField]
	[Tooltip("Put the bark material of your tree here")]
	private Material barkMat;

	[SerializeField]
	[Tooltip("Put the leaves material of your tree here")]
	private Material leafMat;

	[SerializeField] private bool paintAllAsBark = false;

	/*
	 * UV2 variables
	 */

	[SerializeField]
	[Tooltip("Allow the script to overwrite the values of this channel")]
	private bool overwriteUv2Red = true;

	[SerializeField]
	[Tooltip("The type of gradient that will be apllied to the red value")]
	private GradientType uv2RedGradientType = GradientType.Spherical;

	[SerializeField]
	[Tooltip("How much the red value of the uv2 should be painted?")]
	private float uv2RedPercent = 1f;

	[SerializeField]
	[Tooltip("Allow the script to overwrite the values of this channel")]
	private bool overwriteUv2Green = true;

	[SerializeField]
	[Tooltip("The type of gradient that will be apllied to the green value")]
	private GradientType uv2GreenGradientType = GradientType.Spherical;

	[SerializeField]
	[Tooltip("How much the green value of the uv2 should be painted?")]
	private float uv2GreenPercent = 0.5f;

	/// <summary>
	/// This is the main function
	/// </summary>
	[ContextMenu("Create wind")]
	public void CreateWind()
	{
		try
		{
			treeMesh = CopyMesh(GetComponent<MeshFilter>().sharedMesh);
			mats.Clear();
			mats.AddRange(GetComponent<MeshRenderer>().sharedMaterials);
			if (!paintAllAsBark)
			{
				if (mats[0].Equals(barkMat)) barkMatFirst = true;
				else if (mats[0].Equals(leafMat)) barkMatFirst = false;
			}

			if (overwriteColorsRed || overwriteColorsGreen) PaintColors();

			if (overwriteUv2Red || overwriteUv2Green) PaintUV2();

			CreateNewMesh();
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.Message);
		}
	}

	/// <summary>
	/// This function will edit the needed vertex color channels
	/// </summary>
	private void PaintColors()
	{
		try
		{
			Vector3 range = treeMesh.bounds.max - treeMesh.bounds.min;
			List<Color> meshColors = new List<Color>();
			treeMesh.GetColors(meshColors);

			// We check if the list has the correct count
			if (meshColors.Count != treeMesh.vertexCount)
			{
				meshColors = new List<Color>(new Color[treeMesh.vertexCount]);
			}

			// Just in case the user chose the plain color, the whole will be painted with the same random color for diversity
			float power = UnityEngine.Random.Range(0f, 1f);
#if UNITY_2019_3_OR_NEWER

#else
			paintAllAsBark = true;
			Debug.LogWarning("This Unity version isn't supported (Unity 2019.3 or newer), so the tree will be fully painted as bark for the RED VERTEX COLOR to avoid errors.");
#endif
			// We iterate through each color of the mesh
			for (int i = 0; i < meshColors.Count; i++)
			{
				DoGradient(i, colorGradientType, colorPercent, range, ref power);

				float green = 0f;
				if (paintAllAsBark)
				{
					// We need to paint the grass as bark
					green = 0f;
				}
				else
				{
					//Leaf mat
					// WARNING: this code is only available for Unity 2019.3 and more!
					// The GetSubMesh function isn't available before
					if ((!barkMatFirst && treeMesh.GetSubMesh(0).firstVertex < i)
						|| (barkMatFirst && treeMesh.GetSubMesh(1).firstVertex < i))
					{
						green = 1f;
					}
				}
				meshColors[i] = new Color(overwriteColorsRed ? Mathf.Clamp01(power) : meshColors[i].r, overwriteColorsGreen ? Mathf.Clamp01(green) : meshColors[i].g, meshColors[i].b, meshColors[i].a);
			}
			// We assign back the new color list
			treeMesh.SetColors(meshColors);
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.ToString());
		}
	}

	/// <summary>
	/// This function will edit the needed UV2 channels
	/// </summary>
	private void PaintUV2()
	{
		try
		{
			Vector3 range = treeMesh.bounds.max - treeMesh.bounds.min;
			List<Vector4> uv2 = new List<Vector4>();
			treeMesh.GetUVs(1, uv2);

			// We check if the list has the correct count
			if (uv2.Count != treeMesh.vertexCount)
			{
				uv2 = new List<Vector4>(new Vector4[treeMesh.vertexCount]);
			}

			// Just in case the user chose the plain color, the whole will be painted with the same random color for diversity
			float red = UnityEngine.Random.Range(0f, 1f);
			float green = UnityEngine.Random.Range(0f, 1f);
			// We iterate through each color of the mesh
			for (int i = 0; i < uv2.Count; i++)
			{
				DoGradient(i, uv2RedGradientType, uv2RedPercent, range, ref red);

				DoGradient(i, uv2GreenGradientType, uv2GreenPercent, range, ref green);

				uv2[i] = new Vector4(overwriteUv2Red ? Mathf.Clamp01(red) : uv2[i].x, overwriteUv2Green ? Mathf.Clamp01(green) : uv2[i].y, uv2[i].z, uv2[i].w);
			}
			// We assign back the new uv2 list
			treeMesh.SetUVs(1, uv2);
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.ToString());
		}
	}

	/// <summary>
	/// This method is calculating all the gradients
	/// </summary>
	/// <param name="i">The vertex index</param>
	/// <param name="gradientType">The type of gradient to apply</param>
	/// <param name="percentage">The percentage of the gradient for this channel</param>
	/// <param name="range">The bounds of the tree</param>
	/// <param name="power">The output power to apply to this vertex</param>
	private void DoGradient(int i, GradientType gradientType, float percentage, Vector3 range, ref float power)
	{
		switch (gradientType)
		{
			// The linear gradient is kinda like a lerp applied only to the Y axis of the tree, so the bottom is perfect black, and the top perfect white
			case GradientType.Linear:
				power = treeMesh.vertices[i].y / range.y * percentage;
				break;

			// The spherical gradient is like a sphere with the center at the pivot point of the tree (black), its radius depends to the tree bounds on each axis
			case GradientType.Spherical:
				power = Mathf.Sqrt(Mathf.Pow(treeMesh.vertices[i].x / range.x, 2) + Mathf.Pow(treeMesh.vertices[i].y / range.y, 2) + Mathf.Pow(treeMesh.vertices[i].z / range.z, 2)) / Mathf.Sqrt(3) * percentage;
				break;

			// The circular gradient is like a circle applied to the Y axis, with the center as black, so the leaves should be almost white, and the trunk almost dark
			case GradientType.Circular:
				power = Mathf.Sqrt(Mathf.Pow(treeMesh.vertices[i].x / range.x, 2) + Mathf.Pow(treeMesh.vertices[i].z / range.z, 2)) / Mathf.Sqrt(2) * percentage;
				break;

			// The optimal gradient is a circular gradient on which we apply a very small linear gradient
			case GradientType.Optimal:
				power = Mathf.Lerp(
					Mathf.Sqrt(Mathf.Pow(treeMesh.vertices[i].x / range.x, 2) + Mathf.Pow(treeMesh.vertices[i].z / range.z, 2)) / Mathf.Sqrt(2),
					treeMesh.vertices[i].y / range.y * (percentage * 0.5f),
					0.5f) * percentage;
				break;

			// Just random, useful for painted the Red Color on the grass
			case GradientType.Random:
				power = UnityEngine.Random.Range(0f, 1f);
				break;

			// A randomly choosed plain color
			case GradientType.Plain:
				break;

			default:
				break;
		}
	}

	/// <summary>
	/// This function will create the new gameobject with all the needed components
	/// </summary>
	private void CreateNewMesh()
	{
		GameObject tree = new GameObject(gameObject.name + "_Wind");
		tree.transform.position = transform.position + transform.forward * 5;
		tree.AddComponent<MeshFilter>().sharedMesh = treeMesh;
		tree.AddComponent<MeshRenderer>().sharedMaterials = mats.ToArray();
		tree.AddComponent<SaveMesh>();
		tree.AddComponent<Tree>();
		Debug.Log("Wind successfully added to : " + gameObject.name, tree);
	}

	/// <summary>
	/// This function makes a copy of a Mesh, it breaks the reference
	/// So it allows us to modify the mesh data without editing the original one
	/// </summary>
	/// <param name="mesh">The Mesh to copy</param>
	/// <returns>The copied Mesh</returns>
	private Mesh CopyMesh(Mesh mesh)
	{
		return Instantiate(mesh);
	}

	/// <summary>
	/// The gradient that will be applied
	/// </summary>
	private enum GradientType
	{
		Linear,
		Spherical,
		Random,
		Plain,
		Circular,
		Optimal
	}
}