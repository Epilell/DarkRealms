using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

namespace LKHGames
{
	[RequireComponent(typeof(MeshFilter))]
	[RequireComponent(typeof(MeshRenderer))]
	public class MeshTogether : MonoBehaviour
	{	
		public string savingPath = "/LKHGames/MeshTogether/CombinedMeshes/";

		[HideInInspector] public bool includeInactiveChildren = false;
		[HideInInspector] public bool deactivateCombinedChildren = true;
		[HideInInspector] public bool deactivateCombinedChildrenMeshRenderers = false;
		[HideInInspector] public bool destroyCombinedChildren = false;

		//private const int Mesh16BitBufferVertexLimit = 65535;
		[SerializeField] private MeshFilter[] _meshFiltersToSkip = new MeshFilter[0];

		private MeshFilter[] _meshFilterArray;
		private MeshRenderer[] _meshRendererArray;
		private List<Material> _uniqueMaterialsList;

		public void CombineFunction()
		{
			GetList_UniqueMaterial();
			CombineMeshes();
		}
		///////////////////////////////////////////////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////////////////////////////////////////////

		private void GetArray_MeshFiltersToCombine()
		{
			// Get all MeshFilters belongs to this GameObject and its children
			_meshFilterArray = GetComponentsInChildren<MeshFilter>(includeInactiveChildren);

			// Delete first MeshFilter belongs to this GameObject in meshFiltersToSkip array
			_meshFiltersToSkip = _meshFiltersToSkip.Where((meshFilter) => meshFilter != _meshFilterArray[0]).ToArray();

			// Delete null values in array
			_meshFiltersToSkip = _meshFiltersToSkip.Where((meshFilter) => meshFilter != null).ToArray();

			for(int i = 0; i < _meshFiltersToSkip.Length; i++)
			{
				_meshFilterArray = _meshFilterArray.Where((meshFilter) => meshFilter != _meshFiltersToSkip[i]).ToArray();
			}
		}

		private void GetList_UniqueMaterial()
		{
			#region Get MeshFilters, MeshRenderers and unique Materials from all children
			GetArray_MeshFiltersToCombine();
			_meshRendererArray = new MeshRenderer[_meshFilterArray.Length];
			_meshRendererArray[0] = GetComponent<MeshRenderer>();
			_uniqueMaterialsList = new List<Material>();

			for(int i = 0; i < _meshFilterArray.Length-1; i++)
			{
				_meshRendererArray[i+1] = _meshFilterArray[i+1].GetComponent<MeshRenderer>();
				if(_meshRendererArray[i+1] != null)
				{
					Material[] materials = _meshRendererArray[i+1].sharedMaterials; 
					for(int j = 0; j < materials.Length; j++)
					{
						if(!_uniqueMaterialsList.Contains(materials[j])) 
						{
							_uniqueMaterialsList.Add(materials[j]);
						}
					}
				}
			}
			#endregion
		}

		///////////////////////////////////////////////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////////////////////////////////////////////

		private void CombineMeshes()
		{
			#region Combine same material children meshes to create submeshes for final Mesh
			List<CombineInstance> finalMeshCombineInstancesList = new List<CombineInstance>();

			for(int i = 0; i < _uniqueMaterialsList.Count; i++) // Create submesh with the same Material.
			{
				List<CombineInstance> sameMaterialSubmesh_CombineInstancesList = new List<CombineInstance>();

				for(int j = 0; j < _meshFilterArray.Length-1; j++) // Get only childeren Meshes
				{
					if(_meshRendererArray[j+1] != null)
					{
						Material[] submeshMaterialArray = _meshRendererArray[j+1].sharedMaterials; // Get all Materials from child Mesh.

						for(int k = 0; k < submeshMaterialArray.Length; k++)
						{
							// If Materials are equal, combine Mesh from this child:
							if(_uniqueMaterialsList[i] == submeshMaterialArray[k])
							{
								CombineInstance combineInstance = new CombineInstance();
								combineInstance.subMeshIndex = k;
								combineInstance.mesh = _meshFilterArray[j+1].sharedMesh;
								combineInstance.transform = _meshFilterArray[j+1].transform.localToWorldMatrix;
								sameMaterialSubmesh_CombineInstancesList.Add(combineInstance);
							}
						}
					}
				}

				// Create new Mesh (submesh) from Meshes with the same Material:
				Mesh temp_Submesh = new Mesh();
				temp_Submesh.CombineMeshes(sameMaterialSubmesh_CombineInstancesList.ToArray(), true);

				CombineInstance finalTemp_CombineInstance = new CombineInstance();
				finalTemp_CombineInstance.subMeshIndex = 0;
				finalTemp_CombineInstance.mesh = temp_Submesh;
				finalTemp_CombineInstance.transform = Matrix4x4.identity;

				finalMeshCombineInstancesList.Add(finalTemp_CombineInstance);
			}
			#endregion

			#region combine materials & submeshes

			_meshRendererArray[0].sharedMaterials = _uniqueMaterialsList.ToArray();

			transform.GetComponent<MeshFilter>().mesh = new Mesh();
			transform.GetComponent<MeshFilter>().sharedMesh.name = name;
			transform.GetComponent<MeshFilter>().sharedMesh.CombineMeshes(finalMeshCombineInstancesList.ToArray(), false);
			
			//GenerateUV(combinedMesh);
			Deactivate_Destory_CombinedGameObjects(_meshFilterArray);
			#endregion

			Debug.Log("<color=#00FF00><b> Mesh Combined Successfully. " + _uniqueMaterialsList.Count + "  submesh combined.</b></color>");
			
		}
		///////////////////////////////////////////////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////////////////////////////////////////////
		
		private void Deactivate_Destory_CombinedGameObjects(MeshFilter[] meshFilters)
		{
			for(int i = 0; i < meshFilters.Length-1; i++) // Skip first MeshFilter belongs to this GameObject in this loop.
			{
				if(!destroyCombinedChildren)
				{
					if(deactivateCombinedChildren)
					{
						meshFilters[i+1].gameObject.SetActive(false);
					}
					if(deactivateCombinedChildrenMeshRenderers)
					{
						MeshRenderer meshRenderer = meshFilters[i+1].gameObject.GetComponent<MeshRenderer>();
						if(meshRenderer != null)
						{
							meshRenderer.enabled = false;
						}
					}
				}
				else
				{
					DestroyImmediate(meshFilters[i+1].gameObject);
				}
			}
		}

		public void SaveMeshToAsset()
		{
			if(transform.GetComponent<MeshFilter>().sharedMesh == null)
			{
				Debug.LogWarning("<color=#D4AC0D><b> Mesh filter is empty, Please combine meshes before you save it. </b></color>");
				return;
			}
			Mesh mesh = transform.GetComponent<MeshFilter>().sharedMesh;
			bool meshIsExist = AssetDatabase.Contains(mesh);
			
			#region Create new folder if it doesn't exist
			if(AssetDatabase.IsValidFolder("Assets/" + savingPath) == false)
			{
				string[] folderNameArray = savingPath.Split('/');
				string newfolderPath = "";

				for(int i = 0; i < folderNameArray.Length-2; i++)
				{	
					newfolderPath += folderNameArray[i];
					if(i != folderNameArray.Length-3)
					{
						newfolderPath += "/";
					}
				}
				AssetDatabase.CreateFolder("Assets" + newfolderPath, folderNameArray[folderNameArray.Length-2]);
				Debug.Log("<color=#FFFF00><b>Path saving location not found, New folder was created</b></color>");
			}
			#endregion

			#region save mesh
			if(meshIsExist == false)
			{
				string path = "Assets/" + savingPath + mesh.name + ".asset";
				int assetNumber = 1;
				
				//if same name mesh found then add numbers after name
				while(AssetDatabase.LoadAssetAtPath(path, typeof(Mesh)) != null)
				{
					path = "Assets/" + savingPath + mesh.name + " ("+assetNumber+").asset";
					assetNumber++;
				}

				AssetDatabase.CreateAsset(mesh, path);
				AssetDatabase.SaveAssets();
				EditorGUIUtility.PingObject(mesh);

				Debug.Log("<color=#00FF00><b> Mesh saved in " + path + "</b></color>");
			}
			else
			{
				Debug.LogWarning("<color=#D4AC0D><b> The mesh you trying to save is aready exist!! </b></color>");
			}
			#endregion
		}
		///////////////////////////////////////////////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////////////////////////////////////////////

		public void CheckUniqueMaterial()
		{
			GetList_UniqueMaterial();
			Debug.Log("<color=#00FF00><b>" + _uniqueMaterialsList.Count + " unique materials found.</b></color>");
		}

		public void ResetToEmpty()
		{
			transform.GetComponent<MeshFilter>().mesh = null;
			transform.GetComponent<MeshRenderer>().sharedMaterials = new Material[0];

			for(int i = 0; i < _meshFilterArray.Length-1; i++) // Skip first MeshFilter belongs to this GameObject in this loop.
			{
				_meshFilterArray[i+1].gameObject.SetActive(true);
			}

			Debug.Log("<color=#00FF00><b> Tried to reset the combination process. </b></color>");
		}
	}
}

