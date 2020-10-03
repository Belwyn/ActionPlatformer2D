using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;


namespace Belwyn.Editor.Build {

    // This is a simple window to help selecting wich BuildConfiguration asset you want to apply
    public class BuildConfigWindow : EditorWindow {

        private string[] _guids;
        private string[] _names;
        private BuildConfiguration[] _configurations;
        private int _idx;

        private const string TITLE = "Build Configuration";

        [MenuItem("Build/Build Setups/Configuration selector")]
        static void Init() {

            // Get existing open window or if none, make a new one:
            BuildConfigWindow window = (BuildConfigWindow)GetWindow(typeof(BuildConfigWindow));
            window.titleContent = new GUIContent(TITLE);
            window.Show();
        }


        
        private void Awake() {
            _idx = 0;

            // Find all BuildConfiguration assets and create a list
            _guids = AssetDatabase.FindAssets("t:" + typeof(BuildConfiguration).Name);
            _names = new string[_guids.Length];
            _configurations = new BuildConfiguration[_guids.Length];

            for (int i = 0; i < _guids.Length; i++) {
                string path = AssetDatabase.GUIDToAssetPath(_guids[i]);
                _names[i] = path.Split('/').LastOrDefault();
                _configurations[i] = AssetDatabase.LoadAssetAtPath<BuildConfiguration>(path);
            }
        }


        // Draw the window
        void OnGUI() {

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Defaults", EditorStyles.boldLabel);
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();

            // Draw two buttons for the default testing and shipping configurations
            if (GUILayout.Button(new GUIContent("Apply Default Testing"))) {
                BuildSetup.DefaultTestingSetup();
            }

            if (GUILayout.Button(new GUIContent("Apply Default Shipping"))) {
                BuildSetup.DefaultShippingSetup();
            }

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Select Configuration", EditorStyles.boldLabel);
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            // If no BuildConfiguration assets are found, no llist popup will appear
            if (_guids.Length == 0) {
                EditorGUILayout.LabelField("Create a build configuration asset first");
            }
            else {

                _idx = EditorGUILayout.Popup(_idx, _names);
                EditorGUILayout.Space();

                if (GUILayout.Button(new GUIContent("Apply Selected Configuration"))) {
                    BuildSetup.Setup(_configurations[_idx]);                    
                }
            }
            
        }

    }


}