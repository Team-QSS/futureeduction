using System;
using UnityEngine;

namespace ColorSelector.Script
{
	[RequireComponent(typeof(ColorSelector))]
	public class ColorSelector : MonoBehaviour {
		public Camera refCamera;
		public GameObject selectorImage, outerCursor, innerCursor;
		public SpriteRenderer finalColorSprite;

		private Color _finalColor, _selectedColor;
		private float _selectorAngle;
		private Vector2 _innerDelta = new(0.17f, -0.1f);
		private static ColorSelector _instance;
		private static readonly int Color1 = Shader.PropertyToID("_Color");

		private void Awake () {
			_instance = this;
			_finalColor = Color.black;
		}

		private void Start () {
			if (!refCamera) refCamera = Camera.main;
			SelectOuterColor(new Vector2(0, 0.2252683f));
			finalColorSprite.color = _finalColor;
		}

		private void Update () {
			if (Input.GetMouseButton(0)) UserInputUpdate();
		}

		private void UserInputUpdate(){
			var cursorPos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, (transform.position.z - refCamera.transform.position.z));
			var cursorRay = refCamera.ScreenPointToRay(cursorPos);
			if (!Physics.Raycast(cursorRay, out var hit)) return;
			var localPosition=transform.InverseTransformPoint(hit.point);
			var dist=Vector2.Distance(Vector2.zero,localPosition);
			if(dist>0.22) SelectOuterColor(localPosition);
			else SelectInnerColor(localPosition);
		}

		private void SelectInnerColor(Vector2 delta){
			float v=0.0f, w=0.0f, u=0.0f;
			Barycentric (delta,ref v,ref w,ref u);
			if (!(v >= 0.15f) || !(w >= -0.15f) || !(u >= -0.15f)) return;
			var colorVector = new Vector3(_selectedColor.r, _selectedColor.g, _selectedColor.b);
			var finalColorVector = v * colorVector + u * new Vector3(0.0f, 0.0f, 0.0f) + w * new Vector3(1.0f, 1.0f, 1.0f);
			_finalColor = new Color (finalColorVector.x, finalColorVector.y, finalColorVector.z);

			finalColorSprite.color=_finalColor;
			innerCursor.transform.localPosition =delta;
			_innerDelta = delta;
		}

		private static Vector3 ClampPosToCircle(Vector3 pos){
			var newPos = Vector3.zero;
			const float dist = 0.225f;
			var angle = Mathf.Atan2(pos.x, pos.y);

			newPos.x = dist * Mathf.Sin( angle ) ;
			newPos.y = dist * Mathf.Cos( angle ) ;
			newPos.z = pos.z;
			return newPos;
		}


		private static void Barycentric(Vector2 point, ref float u, ref float v, ref float w){
			if (u < 0) throw new ArgumentOutOfRangeException(nameof(u));

			var a = new Vector2 (0.0f, 0.125f);
			var b = new Vector2 (-0.145f, -0.145f);
			var c = new Vector2 (0.145f, -0.145f);

			Vector2 v0 = b - a, v1 = c - a, v2 = point - a;
			var d00 = Vector2.Dot(v0, v0); 
			var d01 = Vector2.Dot(v0, v1);
			var d11 = Vector2.Dot(v1, v1);
			var d20 = Vector2.Dot(v2, v0);
			var d21 = Vector2.Dot(v2, v1);
			var den = d00 * d11 - d01 * d01;
			v = (d11 * d20 - d01 * d21) / den;
			w = (d00 * d21 - d01 * d20) / den;
			u = 1.0f - v - w;
		}


		private void SelectOuterColor(Vector2 delta){
			var angle= Mathf.Atan2(delta.x, delta.y);
			var angleGrad=angle*57.2957795f;
			if(angleGrad<0.0f)
				angleGrad=360+angleGrad;
			_selectorAngle=angleGrad/360;
			_selectedColor=HSVToRGB(_selectorAngle,1.0f,1.0f);
			selectorImage.GetComponent<Renderer>().material.SetColor(Color1, _selectedColor);
			outerCursor.transform.localPosition = ClampPosToCircle (delta);
			SelectInnerColor (_innerDelta);
		}

		private static Color HSVToRGB(float h, float s, float v)
		{
			if (s == 0f)
				return new Color(v,v,v);
			if (v == 0f)
				return Color.black;
			var col = Color.black;
			var hVal = h * 6f;
			var sel = Mathf.FloorToInt(hVal);
			var mod = hVal - sel;
			var v1 = v * (1f - s);
			var v2 = v * (1f - s * mod);
			var v3 = v * (1f - s * (1f - mod));
			switch (sel + 1)
			{
				case 0:
					col.r = v;
					col.g = v1;
					col.b = v2;
					break;
				case 1:
					col.r = v;
					col.g = v3;
					col.b = v1;
					break;
				case 2:
					col.r = v2;
					col.g = v;
					col.b = v1;
					break;
				case 3:
					col.r = v1;
					col.g = v;
					col.b = v3;
					break;
				case 4:
					col.r = v1;
					col.g = v2;
					col.b = v;
					break;
				case 5:
					col.r = v3;
					col.g = v1;
					col.b = v;
					break;
				case 6:
					col.r = v;
					col.g = v1;
					col.b = v2;
					break;
				case 7:
					col.r = v;
					col.g = v3;
					col.b = v1;
					break;
			}
			col.r = Mathf.Clamp(col.r, 0f, 1f);
			col.g = Mathf.Clamp(col.g, 0f, 1f);
			col.b = Mathf.Clamp(col.b, 0f, 1f);
			return col;
		}
		
		public static Color GetColor(){
			return _instance._finalColor;
		}
	}
}
