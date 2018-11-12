using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Delaunay;
using UnityEngine;

public class ClickableRegion : MonoBehaviour
{
	public Site Site;
	public static Site ActiveSite;

	private void OnMouseUpAsButton()
	{
		Debug.Log("Region Clicked: " + Site.Index);
		bool isAdjacent = Site.NeighborSites().Contains(ActiveSite);

		if (isAdjacent)
		{
			ActiveSite = Site;
		}
		else
		{
			Debug.Log("Site " + Site.Index +" is not adjacent to active site: " + ActiveSite.Index);
		}
	}
}
