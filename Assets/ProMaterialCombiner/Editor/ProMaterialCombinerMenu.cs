/*
  Created by:
  Juan Sebastian Munoz Arango
  naruse@gmail.com
  All rights reserved
 */

namespace ProMaterialCombiner {
	using UnityEngine;
	using UnityEditor;
	using System;
	using System.IO;
	using System.Collections;
	using System.Collections.Generic;

	public sealed class ProMaterialCombinerMenu : EditorWindow {
	    private static GUIStyle smallTextStyle;
	    private static GUIStyle smallTextErrorStyle;
		private static GUIStyle smallTextWarningStyle;

        private bool flagObjNull = false;

        private bool generatePrefab = true;
        private bool autoSelect = true;

	    private static string customAtlasName = "";
        private static CombinableObject combObj;

        private Vector2 scrollPos = Vector2.zero;

	    private static ProMaterialCombinerMenu window;
	    [MenuItem("Window/Pro Material Combiner")]
	    private static void Init() {
	        smallTextStyle = new GUIStyle();
	        smallTextStyle.fontSize = 9;

	        smallTextErrorStyle = new GUIStyle();
	        smallTextErrorStyle.normal.textColor = Color.red;
	        smallTextErrorStyle.fontSize = 9;

			smallTextWarningStyle = new GUIStyle();
			smallTextWarningStyle.normal.textColor = new Color(0.7725f, 0.5255f, 0);//~ dark yellow
			smallTextWarningStyle.fontSize = 9;


	        window = (ProMaterialCombinerMenu) EditorWindow.GetWindow(typeof(ProMaterialCombinerMenu));
	        window.minSize = new Vector2(475, 200);
	        window.Show();

            combObj = new CombinableObject();
	    }

        private static void ReloadDataStructures() {
            Init();
            customAtlasName = "";
        }
        private bool NeedToReload() {
            if(window == null)
                return true;
            else
                return false;
        }
        GameObject objToCombine = null;
	    void OnGUI() {
            if(NeedToReload())
                ReloadDataStructures();
            GUILayout.BeginHorizontal();
                GUILayout.BeginVertical();
                    GUILayout.Label("Atlas name(Optional)", GUILayout.Width(window.position.width/4));
                    customAtlasName = GUILayout.TextField(customAtlasName, GUILayout.Width(window.position.width/4));
                GUILayout.EndVertical();
                GUILayout.BeginVertical();
                    autoSelect = GUILayout.Toggle(autoSelect, "Auto select");
                    generatePrefab = GUILayout.Toggle(generatePrefab, "Generate prefab");
                GUILayout.EndVertical();

                GUILayout.Space(window.position.width/4);

                Vector2 size = GUI.skin.GetStyle("Button").CalcSize(new GUIContent(""));
                GUILayout.BeginVertical();
                    GUI.enabled = !autoSelect;
                    if(GUILayout.Button("Add selected\nobject", GUILayout.Width(window.position.width/4*0.95f), GUILayout.Height(size.y * 1.80f)) || autoSelect)
                        objToCombine = Selection.activeGameObject;
                    GUI.enabled = true;
                GUILayout.EndVertical();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
                if(autoSelect) {
                    objToCombine = Selection.activeGameObject;
                    GUILayout.Label("Auto selected object: " + ((objToCombine == null) ? "none." : objToCombine.name));
                } else {
                    objToCombine = EditorGUILayout.ObjectField("GameObject to combine:", objToCombine, typeof(GameObject), true,GUILayout.Width(window.position.width/2)) as GameObject;
                }
                // assign only once the combObj.ObjectToCombine as this is a render loop
                if(objToCombine != null) {
                    if(combObj.ObjectToCombine == null || objToCombine.GetInstanceID() != combObj.ObjectToCombine.GetInstanceID()) {
                        combObj.ObjectToCombine = objToCombine;
                        flagObjNull = false;
                    }
                } else if(!flagObjNull) {
                    combObj.ObjectToCombine = objToCombine;
                    flagObjNull = true;
                }
                /**************************************************************************/
                GUILayout.BeginVertical();
                    if(!combObj.IsCorrectlyAssembled) {
                        GUILayout.Label(combObj.IntegrityLog[0], smallTextErrorStyle);
                        GUILayout.Label(combObj.IntegrityLog[1], smallTextErrorStyle);
                    }
                GUILayout.EndVertical();
            GUILayout.EndHorizontal();

            Rect combinableObjectGUIRect = new Rect(3, 75, window.position.width-6, window.position.height-40-78);
            GUILayout.BeginArea(combinableObjectGUIRect);
                scrollPos = GUILayout.BeginScrollView(scrollPos, GUILayout.Width(window.position.width - 6), GUILayout.Height(window.position.height-40-78));
                    combObj.DrawGUI();
                GUI.EndScrollView();
            GUILayout.EndArea();

            GUI.enabled = combObj.IsCorrectlyAssembled;
            GUILayout.BeginArea(new Rect(3, window.position.height - 40, window.position.width-6, 40));
                if(GUILayout.Button("Combine", GUILayout.Height(38))) {
                    combObj.CombineObject(customAtlasName, generatePrefab);
                    combObj.ObjectToCombine.SetActive(false);
                }
            GUILayout.EndArea();
            GUI.enabled = true;
	    }

	    void OnInspectorUpdate() {
	        Repaint();
	    }
	}
}