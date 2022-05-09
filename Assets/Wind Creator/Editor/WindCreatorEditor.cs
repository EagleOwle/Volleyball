using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WindCreator))]
public class WindCreatorEditor : Editor
{
	/*
	 * Vertex colors variables
	 */
	private SerializedProperty overwriteColorsRed;
	private SerializedProperty colorGradientType;
	private SerializedProperty colorPercent;

	private SerializedProperty overwriteColorsGreen;
	private SerializedProperty barkMat;
	private SerializedProperty leafMat;
	private SerializedProperty paintAllAsBark;

	/*
	 * UV2 variables
	 */
	private SerializedProperty overwriteUv2Red;
	private SerializedProperty uv2RedGradientType;
	private SerializedProperty uv2RedPercent;

	private SerializedProperty overwriteUv2Green;
	private SerializedProperty uv2GreenGradientType;
	private SerializedProperty uv2GreenPercent;

	/// <summary>
	/// Setting up variables
	/// </summary>
	private void OnEnable()
	{
		/*
		 * Vertex colors variables
		 */
		overwriteColorsRed = serializedObject.FindProperty("overwriteColorsRed");
		colorGradientType = serializedObject.FindProperty("colorGradientType");
		colorPercent = serializedObject.FindProperty("colorPercent");

		overwriteColorsGreen = serializedObject.FindProperty("overwriteColorsGreen");
		barkMat = serializedObject.FindProperty("barkMat");
		leafMat = serializedObject.FindProperty("leafMat");
		paintAllAsBark = serializedObject.FindProperty("paintAllAsBark");

		/*
		 * UV2 variables
		 */
		overwriteUv2Red = serializedObject.FindProperty("overwriteUv2Red");
		uv2RedGradientType = serializedObject.FindProperty("uv2RedGradientType");
		uv2RedPercent = serializedObject.FindProperty("uv2RedPercent");

		overwriteUv2Green = serializedObject.FindProperty("overwriteUv2Green");
		uv2GreenGradientType = serializedObject.FindProperty("uv2GreenGradientType");
		uv2GreenPercent = serializedObject.FindProperty("uv2GreenPercent");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		WindCreator windCreator = (WindCreator)target;

		EditorGUI.BeginChangeCheck();

		#region CustomInspector

		EditorGUILayout.HelpBox("Your mesh MUST have:\n" +
			"-> The pivot point set to the bottom of the tree/grass\n" +
			"-> A rotation of zero(0, 0, 0)\n" +
			"-> Scaled at one(1, 1, 1)\n",
			MessageType.Warning);

		/*
		 * Vertex Colors
		 */
		EditorGUILayout.Space(10);
		EditorGUILayout.LabelField("Vertex Colors", EditorStyles.boldLabel);
		EditorGUILayout.HelpBox("[Red Channel] - Branch information\nTree/Grass: Color Percent=1f", MessageType.Info);
		EditorGUILayout.PropertyField(overwriteColorsRed);
		if (overwriteColorsRed.boolValue)
		{
			EditorGUILayout.PropertyField(colorGradientType);
			EditorGUILayout.PropertyField(colorPercent);
		}
		EditorGUILayout.HelpBox("[Green Channel] - Leaf/Bark distinction\nGrass: I don't have two materials=true\nTree: I don't have two materials=false", MessageType.Info);
		EditorGUILayout.PropertyField(overwriteColorsGreen);
		if (overwriteColorsGreen.boolValue)
		{
			if (!paintAllAsBark.boolValue)
			{
				EditorGUILayout.PropertyField(barkMat);
				EditorGUILayout.PropertyField(leafMat);
			}
			paintAllAsBark.boolValue = EditorGUILayout.ToggleLeft("I don't have two disctincs materials", paintAllAsBark.boolValue);
		}

		/*
		 * UV2
		 */
		EditorGUILayout.Space(10);
		EditorGUILayout.LabelField("UV2", EditorStyles.boldLabel);
		EditorGUILayout.HelpBox("[Red Channel] - Vertex bending\nGrass: UV2 Red Percent=0.125f\nTree: UV2 Red Percent=0.5f", MessageType.Info);
		EditorGUILayout.PropertyField(overwriteUv2Red);
		if (overwriteUv2Red.boolValue)
		{
			EditorGUILayout.PropertyField(uv2RedGradientType);
			EditorGUILayout.PropertyField(uv2RedPercent);
		}
		EditorGUILayout.HelpBox("[Green Channel] - Bending relative to the wind direction\nGrass: UV2 Green Percent=0.0625f\nTree: UV2 Green Percent=0.25f", MessageType.Info);
		EditorGUILayout.PropertyField(overwriteUv2Green);
		if (overwriteUv2Green.boolValue)
		{
			EditorGUILayout.PropertyField(uv2GreenGradientType);
			EditorGUILayout.PropertyField(uv2GreenPercent);
		}

		EditorGUILayout.Space(10);
		if (GUILayout.Button("Create Wind"))
		{
			windCreator.CreateWind();
		}

		#endregion CustomInspector

		if (EditorGUI.EndChangeCheck())
		{
			Undo.RecordObject(target, "Edited values");
			EditorUtility.SetDirty(target);
		}

		serializedObject.ApplyModifiedProperties();
	}
}