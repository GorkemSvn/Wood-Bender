using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralCylinder : MonoBehaviour
{
    List<MapKey> map;
    public float height = 2f;
    [Min(2)] public int segment;
    [Min(3)]public int dt;



    public void OnValidate()
    {
        map = new List<MapKey>();
        map.Add(new MapKey(0f, -0.01f));
        for (int i = 0; i < segment; i++)
        {
            map.Add(new MapKey(1f, height*i / (segment-1f)));
        }
        map.Add(new MapKey(0f, height+0.01f));
        if (map.Count>1)
            MakeSoftCylinder();
    }

    public List<MapKey> GetMap()
    {
        List<MapKey> clone = new List<MapKey>();
        clone.AddRange(map);
        return clone;
    }

    public void SetMap(List<MapKey> newMap)
    {
        if (newMap != null)
        {
            map = newMap;
            MakeSoftCylinder();
        }
    }

    public void InsertShape(List<MapKey> shape)
    {
        //this is broken for shapes bigger than cylinder
        map.Sort((x, y) => x.height.CompareTo(y.height));
        shape.Sort((x, y) => x.height.CompareTo(y.height));

        
        //index of a point on map which is higher tan lowest point of inserting shape and needs to be removed
        int minIndex = 0;
        for (int i = 0; i < map.Count; i++)
        {
            if (map[i].height > shape[0].height)
            {
                minIndex = i;
                break;
            }
        }

        //index of point on map which is lower than highest point of inserting shape and needs to be removed
        int maxIndex = 0; 
        for (int i = map.Count-1; i >= 0; i--)
        {
            if (map[i].height < shape[0].height)
            {
                maxIndex = i;
                break;
            }
        }

        //find out if shape is in range of map, if not, cut excess parts

        map.RemoveRange(minIndex , maxIndex - minIndex + 1);
        map.InsertRange(minIndex+1, shape);
        MakeSoftCylinder();
    }

    public void Dent(Vector3 point)
    {
        point = transform.InverseTransformPoint(point);
        for (int i = 0; i < map.Count-1; i++)
        {
            if(map[i].height<point.y && map[i + 1].height > point.y)
            {
                /*
                float ratio = Mathf.InverseLerp(map[i].height, map[i + 1].height, point.y);

                float bottonRatio = ratio / 0.5f;
                float topRatio = 1f - ratio;

                map[i] = new MapKey(Mathf.Lerp(map[i].radius,Mathf.Abs( point.x)*0.9f,bottonRatio), map[i].height);
                map[i+1] = new MapKey(Mathf.Lerp(map[i+1].radius, Mathf.Abs(point.x) * 0.9f, topRatio), map[i+1].height);
                */
                float ratio = Mathf.InverseLerp(map[i].height, map[i + 1].height, point.y);

                float r1 = Mathf.Lerp(map[i].radius, Mathf.Abs(point.x), 1f - ratio);
                if (map[i].radius>r1)
                    map[i] = new MapKey(r1, map[i].height);

                float r2 = Mathf.Lerp(map[i + 1].radius, Mathf.Abs(point.x), ratio);
                if (map[i+1].radius > r2)
                    map[i+1] = new MapKey(r2, map[i+1].height);
                MakeSoftCylinder();
                return;
            }
        }
    }

    void MakeSoftCylinder()
    {
        dt = Mathf.Max(3, dt);
        //first is radius, second is height

        List<Vector3> points = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        //first disc
        points.AddRange(Disc(dt, map[0].radius, map[0].height));
        for (int u = 0; u < dt; u++)
        {
            uvs.Add(new Vector2(map[0].radius, 0f));
        }

        for (int i = 1; i < map.Count; i++)
        {
            //add upper disc
            points.AddRange(Disc(dt, map[i].radius, map[i].height));

            //triangles
            for (int t = 0; t < dt-1 ; t++)
            {
                int n = (i-1) * dt+t;
                triangles.AddRange(new int[] { n, n + dt, n + 1 });
                triangles.AddRange(new int[] { n + dt, n + dt + 1, n + 1 });
            }
            //last triangle
            int lastIndex = i*dt - 1;
            triangles.AddRange(new int[] { lastIndex, lastIndex + dt, lastIndex+1-dt });
            triangles.AddRange(new int[] { lastIndex + 1 - dt, lastIndex + dt, lastIndex+1 });

            //uvs
            //uvs for higher disck
            for (int u = 0; u < dt; u++)
            {
                float x = ((float)(u)) / ((float)(dt));
                float y = ((float)(i-1)) / ((float)(map.Count-1));
               // uvs.Add(new Vector2(x, y));
                uvs.Add(new Vector2(map[i].radius, y));
            }
        }


        //mesh
        Mesh mesh = new Mesh();
        mesh.vertices = points.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }
    Mesh MakeCylinder(float radius1,float radius2, float height1,float height2,int dt)
    {
        List<Vector3> points = new List<Vector3>();
        points.AddRange(Disc(dt, radius1, height1));
        points.AddRange(Disc(dt, radius2, height2));

        List<int> triangles = new List<int>();

        //all triangles except last one
        for (int i = 0; i < dt-1; i++)
        {
            triangles.AddRange(new int[] { i, i + dt, i + 1 });
            triangles.AddRange(new int[] { i+1, i + dt, i+dt + 1 });
        }
        //last triangle
        int lastIndex = dt - 1;
        triangles.AddRange(new int[] { lastIndex, lastIndex + dt, 0 });
        triangles.AddRange(new int[] { 0, lastIndex + dt, dt });

        //uvs
        List<Vector2> uvs = new List<Vector2>();
        //uvs for lower disck
        for (int i = 0; i < dt; i++)
        {
            float x = ((float)(i)) / ((float)(dt));
            uvs.Add(new Vector2(x, 0));
        }
        //uvs for higher disck
        for (int i = 0; i < dt; i++)
        {
            float x = ((float)(i)) / ((float)(dt));
            uvs.Add(new Vector2(x, 1));
        }

        //mesh
        Mesh mesh = new Mesh();
        mesh.vertices = points.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();

        return mesh;
    }
    Vector3[] Disc(int pointCount,float radius,float height)
    {
        float deltaAngle = 2f * Mathf.PI / pointCount;

        List<Vector3> points = new List<Vector3>();

        for (int i = 0; i < pointCount; i++)
        {
            float x = Mathf.Cos(deltaAngle * i) * radius;
            float z = Mathf.Sin(deltaAngle * i) * radius;

            points.Add(new Vector3(x, height, z));
        }

        return points.ToArray();
    }

    [System.Serializable]
    public struct MapKey
    {
        public float radius, height;
        public MapKey(float r, float h)
        {
            radius = r;
            height = h;
        }
    }
}
