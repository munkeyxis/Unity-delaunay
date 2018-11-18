using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using Site = Delaunay.Site;

public class AreaFiller : MonoBehaviour
{
	public GameObject SpritePrefab;
	public Sprite ForestSprite;
	public float Border;

	public void FillArea(Site site)
	{
		float lowestY = GetBottomPoint(site.Region.ToArray());
		float highestY = GetTopPoint(site.Region.ToArray());
		float lowestX = GetMostLeftPoint(site.Region.ToArray());
		float highestX = GetMostRightPoint(site.Region.ToArray());

		float horizontalDistance = highestX - lowestX;
		float vertialDistance = highestY - lowestY;

		float horizontalSpriteCount = Mathf.Floor(horizontalDistance / ForestSprite.bounds.size.x);
		float verticalSpriteCount = Mathf.Floor(vertialDistance / ForestSprite.bounds.extents.y);

		float xRow = lowestX;
		float yRow = highestY;
		
		List<Vector2> verticesWithBorder = new List<Vector2>();

		foreach (var vector2 in site.Region)
		{
			verticesWithBorder.Add(GetVertexInDirectionOfSite(site, vector2));
		}

		for (int i = 0; i <= verticalSpriteCount; i++)
		{
			for (int j = 0; j <= horizontalSpriteCount; j++)
			{
				if (PolyContainers.ContainsPoint(verticesWithBorder.ToArray(), new Vector3(xRow, yRow) ) &&
				    PolyContainers.ContainsPoint(site.Region.ToArray(), new Vector3(xRow, yRow) ))
				{
					GameObject go = Instantiate(SpritePrefab);
					go.transform.SetParent(this.transform);
					
					Vector2 position = GetRandomPosition(xRow, yRow);
					while (!PolyContainers.ContainsPoint(verticesWithBorder.ToArray(), position))
					{
						position = GetRandomPosition(xRow, yRow);
					}
					
					go.transform.position = position;
				}
				
				xRow += ForestSprite.bounds.size.x;
			}

			xRow = lowestX;
			yRow -= ForestSprite.bounds.extents.y;
		}
	}

	private Vector2 GetRandomPosition(float originX, float originY)
	{
		var minX = originX - ForestSprite.bounds.extents.x;
		var maxX = originX + ForestSprite.bounds.extents.x;
		var minY = originY - ForestSprite.bounds.extents.y;
		var maxY = originY + ForestSprite.bounds.extents.y;
		
		return new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
	}

	private Vector2 GetVertexInDirectionOfSite(Site site, Vector2 point)
	{
		return point + Border*(site.Centroid - point);
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
