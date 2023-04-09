using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace LKHGames
{
    [CustomEditor(typeof(MeshTogether))]
    public class MeshTogether_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            MeshTogether meshTogether = (MeshTogether)target;

            #region Properties
            if (GUILayout.Toggle(meshTogether.includeInactiveChildren, new GUIContent(" Include Inactive-Children", 
            "Does the combination include inactive children.")))
            {
                meshTogether.includeInactiveChildren = true;
            }else{meshTogether.includeInactiveChildren = false;}

            if (GUILayout.Toggle(meshTogether.deactivateCombinedChildren, new GUIContent(" Deactivate Combined-Children", 
            "Will the children be deactivated after the combination.")))
            {
                meshTogether.deactivateCombinedChildren = true;
            }else{meshTogether.deactivateCombinedChildren = false;}

            if (GUILayout.Toggle(meshTogether.deactivateCombinedChildrenMeshRenderers, new GUIContent(" Deactivate Combined-Children MeshRenderers", 
            "Will the children's MeshRenderers be deactivated after the combination.")))
            {
                meshTogether.deactivateCombinedChildrenMeshRenderers = true;
            }else{meshTogether.deactivateCombinedChildrenMeshRenderers = false;}

            if (GUILayout.Toggle(meshTogether.destroyCombinedChildren, new GUIContent(" Destroy Combined-Children", 
            "Will the children be destoryed after the combination.")))
            {
                meshTogether.destroyCombinedChildren = true;
            }else{meshTogether.destroyCombinedChildren = false;}
            #endregion

            #region Button
            if (GUILayout.Button("Combine Meshes"))
            {
                meshTogether.CombineFunction();
                AssetDatabase.Refresh();
            }

            if (GUILayout.Button("Save Meshes"))
            {
                meshTogether.SaveMeshToAsset();
                AssetDatabase.Refresh();
            }

            if (GUILayout.Button("Check Unique Material Quantity"))
            {
                meshTogether.CheckUniqueMaterial();
                AssetDatabase.Refresh();
            }
            
            EditorGUILayout.Space();
            EditorGUILayout.HelpBox(
                "The Reset button will set the Mesh Filter & Mesh Renderer's Materials back to empty."
                + " And re-activate those children that are temporarily saved in your last combination."
                + " So the re-activatation might not happen in some situatuion and return a null error. "
                + " Please notice that delete object cannot reset."
            , MessageType.Warning);

            if (GUILayout.Button("Reset & Re-activate Children"))
            {
                meshTogether.ResetToEmpty();
                AssetDatabase.Refresh();
            }
            #endregion
        }
    }
}

