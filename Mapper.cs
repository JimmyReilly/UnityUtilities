using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//TODO: Enumerator implementation https://dotnetcodr.com/2017/11/15/implementing-an-enumerator-for-a-custom-object-in-net-c-3/
//TODO: Other implementations https://stackoverflow.com/questions/10966331/two-way-bidirectional-dictionary-in-c
public class Mapper<T, U>
{
	Dictionary<T, U> _itemControllerMap;
	Dictionary<U, T> _controllerItemMap;
	
	public Mapper()
	{
		_itemControllerMap = new Dictionary<T, U>();
		_controllerItemMap = new Dictionary<U, T>();
	}

	public U Get(T item)
	{
		if (!_itemControllerMap.ContainsKey(item))
		{
			Debug.LogError("Item not found in map");
			return default(U);
		}
		return _itemControllerMap[item];
	}
	
	public T Get(U controller)
	{
		if (!_controllerItemMap.ContainsKey(controller))
		{
			Debug.LogError("Item not found in map");
			return default(T);
		}
		
		return _controllerItemMap[controller];
	}

	public List<T> GetItems()
	{
		return _itemControllerMap.Keys.ToList();
	}

	public List<U> GetControllers()
	{
		return _controllerItemMap.Keys.ToList();
	}
	
	public void Add(T item, U controller)
	{
		if (_itemControllerMap.ContainsKey(item) || _controllerItemMap.ContainsKey(controller))
		{
			Debug.LogError("Trying to add a map that already exists");
			return;
		}
		_itemControllerMap.Add(item, controller);
		_controllerItemMap.Add(controller, item);
	}

	public void Add(U controller, T item)
	{
		if (_itemControllerMap.ContainsKey(item) || _controllerItemMap.ContainsKey(controller))
		{
			Debug.LogError("Trying to add a map that already exists");
			return;
		}
		_itemControllerMap.Add(item, controller);
		_controllerItemMap.Add(controller, item);
	}

	public void Remove(U controller)
	{
		if (!_controllerItemMap.ContainsKey(controller) || !_itemControllerMap.ContainsValue(controller))
		{
			Debug.LogError("Trying to remove an object missing from the map");
			return;
		}
		
		var key = _controllerItemMap[controller];
		_itemControllerMap.Remove(key);
		_controllerItemMap.Remove(controller);
	}

	public void Remove(T item)
	{
		if (!_controllerItemMap.ContainsValue(item) || !_itemControllerMap.ContainsKey(item))
		{
			Debug.LogError("Trying to remove an object missing from the map");
			return;
		}

		var val = _itemControllerMap[item];
		_controllerItemMap.Remove(val);
		_itemControllerMap.Remove(item);
	}

	public bool Contains(U controller)
	{
		return _itemControllerMap.ContainsValue(controller) || _controllerItemMap.ContainsKey(controller);
	}

	public bool Contains(T item)
	{
		return _itemControllerMap.ContainsKey(item) || _controllerItemMap.ContainsValue(item);
	}
}