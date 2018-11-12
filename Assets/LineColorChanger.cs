using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineColorChanger : MonoBehaviour
{
	private ClickableRegion _clickableRegion;
	private LineRenderer _lineRenderer;
	
	private void Start()
	{
		_clickableRegion = GetComponent<ClickableRegion>();
		_lineRenderer = GetComponent<LineRenderer>();
	}

	void Update () {
		if (_clickableRegion.Site == ClickableRegion.ActiveSite)
		{
			_lineRenderer.startColor = Color.red;
			_lineRenderer.endColor = Color.red;
			_lineRenderer.sortingOrder = 1;
			return;
		}
		
		_lineRenderer.startColor = Color.white;
		_lineRenderer.endColor = Color.white;
		_lineRenderer.sortingOrder = 0;
	}
}
