using UnityEngine;

public class JMath {
	public const float BezierCircle = 0.55191502449351f;
    
	public static Quaternion LookAt2D(Vector3 position, Vector3 targetPosition)
	{
		Vector3 vDiff = (position - targetPosition);    
		float atan2 = Mathf.Atan2 ( vDiff.y, vDiff.x );
		float angle = atan2* Mathf.Rad2Deg + 90;
		return Quaternion.Euler(0f, 0f, angle );
	}

	public static Quaternion LookAway2D(Vector3 position, Vector3 targetPosition)
	{
		Vector3 vDiff = (position - targetPosition);    
		float atan2 = Mathf.Atan2 ( vDiff.y, vDiff.x );
		float angle = atan2* Mathf.Rad2Deg - 90;
		return Quaternion.Euler(0f, 0f, angle);
	}
	
	public static Quaternion LookAt3D(Vector3 position, Vector3 targetPosition)
	{
		Vector3 vDiff = (position - targetPosition);    
		float atan2 = Mathf.Atan2 ( vDiff.y, vDiff.z );
		float angle = atan2* Mathf.Rad2Deg + 90;
		return Quaternion.Euler(0f, angle, 0f );
	}

	public static Quaternion LookAway3D(Vector3 position, Vector3 targetPosition)
	{
		Vector3 vDiff = (position - targetPosition);    
		float atan2 = Mathf.Atan2 ( vDiff.y, vDiff.z );
		float angle = atan2* Mathf.Rad2Deg - 90;
		return Quaternion.Euler(0f, angle, 0f);
	}
    
	public static Vector2 BezierQuadratic(Vector2 a, Vector2 b, Vector2 c, float t)
	{
		Vector2 p0 = Vector2.Lerp(a, b, t);
		Vector2 p1 = Vector2.Lerp(b, c, t);
		return Vector2.Lerp(p0, p1, t);
	}

	public static Vector2 BezierCubic(Vector2 a, Vector2 b, Vector2 c, Vector2 d, float t)
	{
		Vector2 p0 = BezierQuadratic(a, b, c, t);
		Vector2 p1 = BezierQuadratic(b, c, d, t);
		return Vector2.Lerp(p0, p1, t);
	}

	public static Vector2 Vec3ToVec2(Vector3 vector3) {
		return new Vector2(vector3.x, vector3.z);
	}

	public static Vector3 Vec2ToVec3(Vector2 vector2) {
		return new Vector3(vector2.x, 0, vector2.y);
	}

	public static Vector3 Flatten(Vector3 vector) {
		return new Vector3(vector.x, 0, vector.z);
	}
	
	public static bool CircleTangents(Vector2 center, float radius, Vector2 point, ref Vector2 tanPosA, ref Vector2 tanPosB)
	{
		point -= center;

		float pointDistance = point.magnitude;

		if (pointDistance <= radius)
		{
			Vector2 direction = point;
			direction.Normalize();
			tanPosA = tanPosB = center + direction * radius;

			return true;
		}

		float area = radius * radius / pointDistance;
		float q = radius * (float)System.Math.Sqrt((pointDistance * pointDistance) - (radius * radius)) / pointDistance;

		Vector2 pN = point / pointDistance;
		Vector2 pNP = new Vector2(-pN.y, pN.x);
		Vector2 va = pN * area;

		tanPosA = va + pNP * q;
		tanPosB = va - pNP * q;

		tanPosA += center;
		tanPosB += center;

		return true;
	}
}