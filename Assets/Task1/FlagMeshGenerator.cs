using UnityEngine;

namespace DefaultNamespace
{
	[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
	public class FlagMeshGenerator : MonoBehaviour
	{
		[SerializeField] private Vector2 size;
		[SerializeField] private Vector2Int pointsCount;

		private MeshFilter _mf;

		private void Awake()
		{
			_mf = GetComponent<MeshFilter>();
		}

		private void Start()
		{
			_mf.sharedMesh = GenerateFlagMesh(transform.position, size, pointsCount);
		}

		private static Mesh GenerateFlagMesh(in Vector3 flagRoot,
			in Vector2 size,
			in Vector2Int pointsCount)
		{
			var resultMesh = new Mesh();
			resultMesh.name = "Procedural Flag";

			// Generate vertices
			var segSize = new Vector2(size.x / pointsCount.x, size.y / pointsCount.y);
			var vertices = new Vector3[pointsCount.x * pointsCount.y];
			for (var y = 0; y < pointsCount.y; y++)
			{
				for (var x = 0; x < pointsCount.x; x++)
				{
					vertices[y * pointsCount.x + x] = new Vector3(
						flagRoot.x + x * segSize.x,
						flagRoot.y + y * segSize.y,
						flagRoot.z);
				}
			}
			resultMesh.SetVertices(vertices);

			// Generate triangles for face
			var triangles = new int[6 * vertices.Length];
			for (var y = 0; y < pointsCount.y - 1; y++)
			{
				for (var x = 0; x < pointsCount.x - 1; x++)
				{
					var i = y * pointsCount.x + x;
					var offset = 6 * i;
					triangles[offset] = i;
					triangles[offset + 1] = i + pointsCount.x;
					triangles[offset + 2] = i + 1;
					triangles[offset + 3] = i + 1;
					triangles[offset + 4] = i + pointsCount.x;
					triangles[offset + 5] = i + pointsCount.x + 1;
				}
			}
			resultMesh.SetTriangles(triangles, 0);

			// Generate UV
			var uv = new Vector2[vertices.Length];
			for (var i = 0; i < vertices.Length; i++)
			{
				uv[i] = new Vector2(
					(vertices[i].x - flagRoot.x) / size.x,
					(vertices[i].y - flagRoot.y) / size.y);
			}
			resultMesh.SetUVs(0, uv);

			resultMesh.MarkDynamic();
			resultMesh.RecalculateNormals();
			resultMesh.RecalculateTangents();
			resultMesh.RecalculateBounds();

			return resultMesh;
		}
	}
}