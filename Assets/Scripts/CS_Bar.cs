using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Bar : MonoBehaviour
{
	[SerializeField] RectTransform myFill;

	public void SetValue(float g_value)
	{
		myFill.localScale = new Vector3(g_value, 1, 1);
	}
}
