using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using Site = Delaunay.Site;

public class AreaFiller : MonoBehaviour
{
	public GameObject SpritePrefab;
	public Sprite ForestSprite;

	public void FillArea(Site site)
	{
		float lowestY = GetBottomPoint(site.Region.ToArray());
		float highestY = GetTopPoint(site.Region.ToArray());
		float lowestX = GetMostLeftPoint(site.Region.ToArray());
		float highestX = GetMostRightPoint(site.Region.ToArray());
		
		Vector2 bottomLeftPoint = new Vector2(lowestX, lowestY);
		Vector2 topRightPoint = new Vector2(highestX, highestY);

		float horizontalDistance = highestX - lowestX;
		float vertialDistance = highestY - lowestY;

		float horizontalSpriteCount = Mathf.Floor(horizontalDistance / ForestSprite.bounds.size.x);
		float verticalSpriteCount = Mathf.Floor(vertialDistance / ForestSprite.bounds.extents.y);

		float xRow = lowestX;
		float yRow = highestY;

		for (int i = 0; i <= verticalSpriteCount; i++)
		{
			for (int j = 0; j <= horizontalSpriteCount; j++)
			{
				GameObject go = Instantiate(SpritePrefab);
				go.transform.SetParent(this.transform);
				go.transform.position = new Vector3(xRow, yRow);
				xRow += ForestSprite.bounds.size.x;
			}

			yRow += ForestSprite.bounds.extents.y;
		}
	}
	
	private float GetTopPoint(Vector2[] vectors)
	{
		float highestY = -1000000; // some high number

		foreach (var vector in vectors)
		{
			if (vector.y > highestY) highestY = vector.y;
		}

		return highestY;
	}

	private float GetBottomPoint(Vector2[] vectors)
	{
		float lowestY = 1000000; // some high number

		foreach (var vector in vectors)
		{
			if (vector.y < lowestY) lowestY = vector.y;
		}

		return lowestY;
	}
	
	private float GetMostLeftPoint(Vector2[] vectors)
	{
		float lowestX = 1000000; // some high number

		foreach (var vector in vectors)
		{
			if (vector.x < lowestX) lowestX = vector.x;
		}

		return lowestX;
	}
	
	private float GetMostRightPoint(Vector2[] vectors)
	{
		float highestX = -1000000; // some high number

		foreach (var vector in vectors)
		{
			if (vector.x > highestX) highestX = vector.x;
		}

		return highestX;
	}
}
