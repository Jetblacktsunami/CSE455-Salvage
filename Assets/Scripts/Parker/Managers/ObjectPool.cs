using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
	public int minPoolAmount  = 300;
	public int maxPoolAmount = 1000;
	private int totalPooledObjects = 0;

	private List<GameObject> pooledObjects = new List<GameObject>();
	private List<GameObject> usedPooledObjects = new List<GameObject>();

	//specific to this game
	public List<WorldCell> cells = new List<WorldCell>();
	public List<WorldCell> ActiveCells = new List<WorldCell>();

	private static ObjectPool instance;
	public static ObjectPool Pool
	{
		get
		{
			if(instance)
			{
				return instance;
			}

			instance = new GameObject("ObjectPool").AddComponent<ObjectPool>();
			return instance;
		}
	}

	void Awake()
	{
		if(!instance)
		{
			instance = this;
		}
		else 
		{
			Destroy(this);
		}

		DontDestroyOnLoad (this.gameObject);
	}
	
	public void AddCell(WorldCell cell)
	{
		if(!cells.Contains(cell))
		{
			cells.Add (cell);
		}
	}

	public void LinkCells()
	{

		for(float i = 0; i < cells.Count; i++)
		{
			int x = (int)i;
			int x2 = (int)i;

			//x + 1
			//x2 + 1
			x = x + 1; 
			x2 = x + 2;
			if( x < cells.Count && x >= 0 && cells[x])
			{
				if(!cells[(int)i].neighbors.Contains(cells[x]))
				{
					cells[(int)i].neighbors.Add(cells[x]);
				}
				if(x2 < cells.Count && x2 >= 0 && cells[x2])
				{
					if(!cells[(int)i].neighbors.Contains(cells[x2]))
					{
						cells[(int)i].neighbors.Add(cells[x2]);
					}				
				}
			}
		
			//x - 1
			//x2 - 2
			x = x - 2;
			x2 = x - 1;
			if( x < cells.Count && x >= 0 && cells[x])
			{
				if(!cells[(int)i].neighbors.Contains(cells[x]))
				{
					cells[(int)i].neighbors.Add(cells[x]);
				}
				if(x2 < cells.Count && x2 >= 0 && cells[x2])
				{
					if(!cells[(int)i].neighbors.Contains(cells[x2]))
					{
						cells[(int)i].neighbors.Add(cells[x2]);
					}				
				}

			}

			//x + dimension
			x = (x + 1) + WorldGenerator.worldspec.subdivisions;
			x2 = x + WorldGenerator.worldspec.subdivisions;

			if( x < cells.Count && x >= 0 && cells[x])
			{
				if(!cells[(int)i].neighbors.Contains(cells[x]))
				{
					cells[(int)i].neighbors.Add(cells[x]);
				}
				if(x2 < cells.Count && x2 >= 0 && cells[x2])
				{
					if(!cells[(int)i].neighbors.Contains(cells[x2]))
					{
						cells[(int)i].neighbors.Add(cells[x2]);
					}				
				}
			}
			
			//x + dimension + 1
			//x2 + dimension + 2
			x = x + 1; 
			x2 = x2 + 1;
			if( x < cells.Count && x >= 0 && cells[x])
			{
				if(!cells[(int)i].neighbors.Contains(cells[x]))
				{
					cells[(int)i].neighbors.Add(cells[x]);
				}
				if(x2 < cells.Count && x2 >= 0 && cells[x2])
				{
					if(!cells[(int)i].neighbors.Contains(cells[x2]))
					{
						cells[(int)i].neighbors.Add(cells[x2]);
					}				
				}
			}
			
			//x + dimension - 1
			//x2 + dimension - 2
			x = x - 2;
			x2 = x2 - 2;
			if( x < cells.Count && x >= 0 && cells[x])
			{
				if(!cells[(int)i].neighbors.Contains(cells[x]))
				{
					cells[(int)i].neighbors.Add(cells[x]);
				}
				if(x2 < cells.Count && x2 >= 0 && cells[x2])
				{
					if(!cells[(int)i].neighbors.Contains(cells[x2]))
					{
						cells[(int)i].neighbors.Add(cells[x2]);
					}				
				}
			}
			
			//x - dimension
			x = (x + 1) - (2 * WorldGenerator.worldspec.subdivisions);
			x2 = x - (2 * WorldGenerator.worldspec.subdivisions);
			if( x < cells.Count && x >= 0 && cells[x])
			{
				if(!cells[(int)i].neighbors.Contains(cells[x]))
				{
					cells[(int)i].neighbors.Add(cells[x]);
				}
				if(x2 < cells.Count && x2 >= 0 && cells[x2])
				{
					if(!cells[(int)i].neighbors.Contains(cells[x2]))
					{
						cells[(int)i].neighbors.Add(cells[x2]);
					}				
				}
			}

			//x + dimesion + 1
			x = x + 1;
			x2 = x2 + 1;
			if( x < cells.Count && x >= 0 && cells[x])
			{
				if(!cells[(int)i].neighbors.Contains(cells[x]))
				{
					cells[(int)i].neighbors.Add(cells[x]);
				}
				if(x2 < cells.Count && x2 >= 0 && cells[x2])
				{
					if(!cells[(int)i].neighbors.Contains(cells[x2]))
					{
						cells[(int)i].neighbors.Add(cells[x2]);
					}				
				}
			}

			//x + dimenstion - 1
			x = x - 2;
			x2 = x2 - 2;
			if( x < cells.Count && x >= 0 && cells[x])
			{
				if(!cells[(int)i].neighbors.Contains(cells[x]))
				{
					cells[(int)i].neighbors.Add(cells[x]);
				}
				if(x2 < cells.Count && x2 >= 0 && cells[x2])
				{
					if(!cells[(int)i].neighbors.Contains(cells[x2]))
					{
						cells[(int)i].neighbors.Add(cells[x2]);
					}				
				}
			}
		}
	}

	public bool CanPoolMore()
	{
		return (maxPoolAmount - totalPooledObjects > 0) ? true : false;
	}

	public void Register(GameObject obj)
	{
		usedPooledObjects.Add (obj);
		totalPooledObjects++;
	}

	public void Unregister(GameObject obj)
	{
		usedPooledObjects.Remove(obj);
	}

	public void MarkUnused(GameObject gameObj)
	{
		if(usedPooledObjects.Contains(gameObj))
		{
			int index = usedPooledObjects.FindIndex( delegate(GameObject obj) 
			{
				return obj == gameObj;
			});
			pooledObjects.Add(usedPooledObjects[index]);
			usedPooledObjects.RemoveAt(index);
		}
		else if(totalPooledObjects < maxPoolAmount)
		{
			pooledObjects.Add(gameObj);
			totalPooledObjects++;
		}
	}

	public Vector2 Redirect(List<Vector2> positions, List<float> perlinValues, WorldCell cell)
	{
		if(ActiveCells.Count > 0)
		{
			for(int i = ActiveCells.Count - 1; i >= 0; i--)
			{
				if(ActiveCells[i] != cell)
				{
					ActiveCells[i].CheckPlayer();
				}
			}
		}
		if(pooledObjects.Count > 0)
		{
			for(int i = 0, j = 0; i < positions.Count && j < perlinValues.Count; i++, j++)
			{
				if(i >= pooledObjects.Count)
				{
					pooledObjects.RemoveRange(0, i);
					return new Vector2(i,j);
				}

				pooledObjects[i].gameObject.transform.position = new Vector3(positions[i].x, positions[i].y, Random.Range(-1, 2));
				pooledObjects[i].gameObject.transform.localScale = new Vector3(perlinValues[j]/10.0f ,perlinValues[j]/10.0f, 1.0f);
				pooledObjects[i].gameObject.transform.parent = cell.parent.transform;
				Asteroid pooledAsteroid = pooledObjects[i].GetComponent<Asteroid>();
				pooledAsteroid.assignedPosition = positions[i];
				pooledAsteroid.parentCell = cell;

				cell.children.Add(pooledObjects[i].gameObject);
				usedPooledObjects.Add(pooledObjects[i].gameObject);
			}
			pooledObjects.RemoveRange(0, positions.Count);
			return new Vector2 (-1, -1);
		}
		return new Vector2(0, 0);
	}
	
	private void OnLevelWasLoaded()
	{
		if(Application.loadedLevelName == "MainMenu")
		{
			Clear ();
		}
	}

	private void Clear()
	{
		pooledObjects.Clear ();
		usedPooledObjects.Clear ();
		cells.Clear ();
		totalPooledObjects = 0;
	}

	public void OnEnable()
	{
		GameManager.gameManagerCalls += GameManagerEventHandler;
	}
	
	//Called when disabled
	public void OnDisable()
	{
		GameManager.gameManagerCalls -= GameManagerEventHandler;
	}

	public void GameManagerEventHandler(GameManager.FunctionCallType type)
	{
		if(ActiveCells.Count > 0)
		{
			foreach(WorldCell cell in ActiveCells)
			{
				if(type == GameManager.FunctionCallType.save)
				{
					cell.Save();
				}
			}
		}
	}
}
