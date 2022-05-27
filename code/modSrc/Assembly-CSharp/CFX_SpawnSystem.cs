using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000016 RID: 22
public class CFX_SpawnSystem : MonoBehaviour
{
	// Token: 0x06000069 RID: 105 RVA: 0x0001A5C4 File Offset: 0x000187C4
	public static GameObject GetNextObject(GameObject sourceObj, bool activateObject = true)
	{
		int instanceID = sourceObj.GetInstanceID();
		if (!CFX_SpawnSystem.instance.poolCursors.ContainsKey(instanceID))
		{
			Debug.LogError(string.Concat(new object[]
			{
				"[CFX_SpawnSystem.GetNextObject()] Object hasn't been preloaded: ",
				sourceObj.name,
				" (ID:",
				instanceID,
				")\n"
			}), CFX_SpawnSystem.instance);
			return null;
		}
		int num = CFX_SpawnSystem.instance.poolCursors[instanceID];
		GameObject gameObject;
		if (CFX_SpawnSystem.instance.onlyGetInactiveObjects)
		{
			int num2 = num;
			do
			{
				gameObject = CFX_SpawnSystem.instance.instantiatedObjects[instanceID][num];
				CFX_SpawnSystem.instance.increasePoolCursor(instanceID);
				num = CFX_SpawnSystem.instance.poolCursors[instanceID];
				if (gameObject != null && !gameObject.activeSelf)
				{
					goto IL_15E;
				}
			}
			while (num != num2);
			if (!CFX_SpawnSystem.instance.instantiateIfNeeded)
			{
				Debug.LogWarning("[CFX_SpawnSystem.GetNextObject()] There are no active instances available in the pool for \"" + sourceObj.name + "\"\nYou may need to increase the preloaded object count for this prefab?", CFX_SpawnSystem.instance);
				return null;
			}
			Debug.Log("[CFX_SpawnSystem.GetNextObject()] A new instance has been created for \"" + sourceObj.name + "\" because no active instance were found in the pool.\n", CFX_SpawnSystem.instance);
			CFX_SpawnSystem.PreloadObject(sourceObj, 1);
			List<GameObject> list = CFX_SpawnSystem.instance.instantiatedObjects[instanceID];
			gameObject = list[list.Count - 1];
		}
		else
		{
			gameObject = CFX_SpawnSystem.instance.instantiatedObjects[instanceID][num];
			CFX_SpawnSystem.instance.increasePoolCursor(instanceID);
		}
		IL_15E:
		if (activateObject && gameObject != null)
		{
			gameObject.SetActive(true);
		}
		return gameObject;
	}

	// Token: 0x0600006A RID: 106 RVA: 0x00002498 File Offset: 0x00000698
	public static void PreloadObject(GameObject sourceObj, int poolSize = 1)
	{
		CFX_SpawnSystem.instance.addObjectToPool(sourceObj, poolSize);
	}

	// Token: 0x0600006B RID: 107 RVA: 0x000024A6 File Offset: 0x000006A6
	public static void UnloadObjects(GameObject sourceObj)
	{
		CFX_SpawnSystem.instance.removeObjectsFromPool(sourceObj);
	}

	// Token: 0x17000007 RID: 7
	// (get) Token: 0x0600006C RID: 108 RVA: 0x000024B3 File Offset: 0x000006B3
	public static bool AllObjectsLoaded
	{
		get
		{
			return CFX_SpawnSystem.instance.allObjectsLoaded;
		}
	}

	// Token: 0x0600006D RID: 109 RVA: 0x0001A744 File Offset: 0x00018944
	private void addObjectToPool(GameObject sourceObject, int number)
	{
		int instanceID = sourceObject.GetInstanceID();
		if (!this.instantiatedObjects.ContainsKey(instanceID))
		{
			this.instantiatedObjects.Add(instanceID, new List<GameObject>());
			this.poolCursors.Add(instanceID, 0);
		}
		for (int i = 0; i < number; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(sourceObject);
			gameObject.SetActive(false);
			CFX_AutoDestructShuriken[] componentsInChildren = gameObject.GetComponentsInChildren<CFX_AutoDestructShuriken>(true);
			for (int j = 0; j < componentsInChildren.Length; j++)
			{
				componentsInChildren[j].OnlyDeactivate = true;
			}
			CFX_LightIntensityFade[] componentsInChildren2 = gameObject.GetComponentsInChildren<CFX_LightIntensityFade>(true);
			for (int j = 0; j < componentsInChildren2.Length; j++)
			{
				componentsInChildren2[j].autodestruct = false;
			}
			this.instantiatedObjects[instanceID].Add(gameObject);
			if (this.hideObjectsInHierarchy)
			{
				gameObject.hideFlags = HideFlags.HideInHierarchy;
			}
			if (this.spawnAsChildren)
			{
				gameObject.transform.parent = base.transform;
			}
		}
	}

	// Token: 0x0600006E RID: 110 RVA: 0x0001A82C File Offset: 0x00018A2C
	private void removeObjectsFromPool(GameObject sourceObject)
	{
		int instanceID = sourceObject.GetInstanceID();
		if (!this.instantiatedObjects.ContainsKey(instanceID))
		{
			Debug.LogWarning(string.Concat(new object[]
			{
				"[CFX_SpawnSystem.removeObjectsFromPool()] There aren't any preloaded object for: ",
				sourceObject.name,
				" (ID:",
				instanceID,
				")\n"
			}), base.gameObject);
			return;
		}
		for (int i = this.instantiatedObjects[instanceID].Count - 1; i >= 0; i--)
		{
			UnityEngine.Object obj = this.instantiatedObjects[instanceID][i];
			this.instantiatedObjects[instanceID].RemoveAt(i);
			UnityEngine.Object.Destroy(obj);
		}
		this.instantiatedObjects.Remove(instanceID);
		this.poolCursors.Remove(instanceID);
	}

	// Token: 0x0600006F RID: 111 RVA: 0x0001A8F0 File Offset: 0x00018AF0
	private void increasePoolCursor(int uniqueId)
	{
		Dictionary<int, int> dictionary = CFX_SpawnSystem.instance.poolCursors;
		int num = dictionary[uniqueId];
		dictionary[uniqueId] = num + 1;
		if (CFX_SpawnSystem.instance.poolCursors[uniqueId] >= CFX_SpawnSystem.instance.instantiatedObjects[uniqueId].Count)
		{
			CFX_SpawnSystem.instance.poolCursors[uniqueId] = 0;
		}
	}

	// Token: 0x06000070 RID: 112 RVA: 0x000024BF File Offset: 0x000006BF
	private void Awake()
	{
		if (CFX_SpawnSystem.instance != null)
		{
			Debug.LogWarning("CFX_SpawnSystem: There should only be one instance of CFX_SpawnSystem per Scene!\n", base.gameObject);
		}
		CFX_SpawnSystem.instance = this;
	}

	// Token: 0x06000071 RID: 113 RVA: 0x0001A954 File Offset: 0x00018B54
	private void Start()
	{
		this.allObjectsLoaded = false;
		for (int i = 0; i < this.objectsToPreload.Length; i++)
		{
			CFX_SpawnSystem.PreloadObject(this.objectsToPreload[i], this.objectsToPreloadTimes[i]);
		}
		this.allObjectsLoaded = true;
	}

	// Token: 0x0400005E RID: 94
	private static CFX_SpawnSystem instance;

	// Token: 0x0400005F RID: 95
	public GameObject[] objectsToPreload = new GameObject[0];

	// Token: 0x04000060 RID: 96
	public int[] objectsToPreloadTimes = new int[0];

	// Token: 0x04000061 RID: 97
	public bool hideObjectsInHierarchy;

	// Token: 0x04000062 RID: 98
	public bool spawnAsChildren = true;

	// Token: 0x04000063 RID: 99
	public bool onlyGetInactiveObjects;

	// Token: 0x04000064 RID: 100
	public bool instantiateIfNeeded;

	// Token: 0x04000065 RID: 101
	private bool allObjectsLoaded;

	// Token: 0x04000066 RID: 102
	private Dictionary<int, List<GameObject>> instantiatedObjects = new Dictionary<int, List<GameObject>>();

	// Token: 0x04000067 RID: 103
	private Dictionary<int, int> poolCursors = new Dictionary<int, int>();
}
