using UnityEngine;
using System.Collections.Generic;
using Delaunay;
using Delaunay.Geo;

public class VoronoiDemo : MonoBehaviour
{
	public GameObject polygonPrefab;
	public int RelaxationInterations;
	[SerializeField]
	private int m_pointCount = 300;
	private List<Vector2> m_points;
	private float m_mapWidth = 100;
	private float m_mapHeight = 50;
	private Voronoi _voronoi;
	private List<List<Vector2>> _regions;
	private List<LineSegment> m_edges = null;
	private List<GameObject> polys;

	void Awake ()
	{
		polys = new List<GameObject>();
		Demo ();
	}

	public void Demo ()
	{
		ClearGameObjects();
		List<uint> colors = new List<uint> ();
		m_points = new List<Vector2> ();
			
		for (int i = 0; i < m_pointCount; i++) {
			colors.Add (0);
			m_points.Add (new Vector2 (
					UnityEngine.Random.Range (0, m_mapWidth),
					UnityEngine.Random.Range (0, m_mapHeight))
			);
		}
		_voronoi = new Voronoi (m_points, colors, new Rect (0, 0, m_mapWidth, m_mapHeight), RelaxationInterations);
		m_edges = _voronoi.VoronoiDiagram ();
		
		CreatePolygonsForRegions();
	}

	private void ClearGameObjects()
	{
		if (polys.Count == 0) return;

		foreach (var poly in polys)
		{
			Destroy(poly);
		}
		
		polys = new List<GameObject>();
	}

	private void CreatePolygonsForRegions()
	{
		ClickableRegion.ActiveSite = _voronoi.SitesList.Sites[0];
		
		foreach (var site in _voronoi.SitesList.Sites)
		{
			GameObject polyGameObject = Instantiate(polygonPrefab);
			polyGameObject.GetComponent<PolygonCollider2D>().SetPath(0, site.Region.ToArray());
			polyGameObject.GetComponent<ClickableRegion>().Site = site;
			polyGameObject.GetComponent<AreaFiller>().FillArea(site);
			LineRenderer lr = polyGameObject.GetComponent<LineRenderer>();
			lr.positionCount = site.Region.Count;
			for(int i = 0; i < site.Region.Count; i++)
			{
				lr.SetPosition(i, site.Region[i]);
			}
			polys.Add(polyGameObject);
		}
	}

	void OnDrawGizmos ()
	{
		Gizmos.color = Color.red;
		if (m_points != null) {
			for (int i = 0; i < m_points.Count; i++) {
				Gizmos.DrawSphere (m_points [i], 0.2f);
			}
		}

		if (m_edges != null) {
			Gizmos.color = Color.white;
			for (int i = 0; i< m_edges.Count; i++) {
				Vector2 left = (Vector2)m_edges [i].p0;
				Vector2 right = (Vector2)m_edges [i].p1;
				Gizmos.DrawLine ((Vector3)left, (Vector3)right);
			}
		}
		/*

		Gizmos.color = Color.magenta;
		if (m_delaunayTriangulation != null) {
			for (int i = 0; i< m_delaunayTriangulation.Count; i++) {
				Vector2 left = (Vector2)m_delaunayTriangulation [i].p0;
				Vector2 right = (Vector2)m_delaunayTriangulation [i].p1;
				Gizmos.DrawLine ((Vector3)left, (Vector3)right);
			}
		}

		if (m_spanningTree != null) {
			Gizmos.color = Color.green;
			for (int i = 0; i< m_spanningTree.Count; i++) {
				LineSegment seg = m_spanningTree [i];				
				Vector2 left = (Vector2)seg.p0;
				Vector2 right = (Vector2)seg.p1;
				Gizmos.DrawLine ((Vector3)left, (Vector3)right);
			}
		}*/

		Gizmos.color = Color.yellow;
		Gizmos.DrawLine (new Vector2 (0, 0), new Vector2 (0, m_mapHeight));
		Gizmos.DrawLine (new Vector2 (0, 0), new Vector2 (m_mapWidth, 0));
		Gizmos.DrawLine (new Vector2 (m_mapWidth, 0), new Vector2 (m_mapWidth, m_mapHeight));
		Gizmos.DrawLine (new Vector2 (0, m_mapHeight), new Vector2 (m_mapWidth, m_mapHeight));
	}
}