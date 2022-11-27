using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AssetGenerator))]
public class AssetGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        AssetGenerator assGen = (AssetGenerator)target;

        if (DrawDefaultInspector())
        {
            if (assGen.autoUpdate)
            {
                assGen.GenerateAssets();
            }
        }

        if (GUILayout.Button("Generate"))
        {
            assGen.GenerateAssets();
        }
    }
}
