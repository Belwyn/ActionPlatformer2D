using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Belwyn.Editor.Build;

namespace Belwyn.Editor.Build {
 

    // This class is only for displaying an additional button at the bottom when inspecting a BuildConfiguration asset
    // So you can also apply a configuration directly through the asset!

    [CustomEditor(typeof(BuildConfiguration))]
    public class BuildConfigurationEditor : UnityEditor.Editor {

        public override void OnInspectorGUI() {

            base.OnInspectorGUI();

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            // Click this button to apply the configuration
            if (GUILayout.Button("Apply this configuration")) {
                BuildConfiguration config = target as BuildConfiguration;
                if (config != null)
                    BuildSetup.Setup(config);
            }

        }

    }

}