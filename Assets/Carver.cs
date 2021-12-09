using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carver : MonoBehaviour
{
    [SerializeField] ProceduralCylinder cyliner;
    public List<ProceduralCylinder.MapKey> outLine;
    private void FixedUpdate()
    {/*
        //calculate out shape
        List<ProceduralCylinder.MapKey> ot = new List<ProceduralCylinder.MapKey>();
        ot.Add(new ProceduralCylinder.MapKey(transform.position.x, transform.position.y - 0.5f));
        for (int i = 0; i < outLine.Count; i++)
        {
            ProceduralCylinder.MapKey key = new ProceduralCylinder.MapKey();
            key.radius = transform.position.x - outLine[i].radius;
            key.height += transform.position.y;
            ot.Add(key);
        }
        ot.Add(new ProceduralCylinder.MapKey(transform.position.x, transform.position.y + 0.5f));

        //detect colliding points
        List<ProceduralCylinder.MapKey> map = cyliner.GetMap();
        int low =0;
        for (int i = 0; i < map.Count; i++)
        {
            if (map[i].height < ot[1].height && map[i].radius<ot[1].radius)
            {
                low = i;
                break;
            }
        }
        if(low>0)
        {
            cyliner.InsertShape(ot);
        }
        //overwrite colliding points as outline
        */
    }

    private void OnCollisionStay(Collision collision)
    {
        ContactPoint[] cps = new ContactPoint[collision.contactCount];
        collision.GetContacts(cps);
        foreach (ContactPoint cp in cps)
            cyliner.Dent(cp.point);
    }
}
