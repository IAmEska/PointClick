using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(LineRenderer))]

public class RopeScript : MonoBehaviour
{
   
    public Transform target;
    public float resolution = 0.5f;                          
    
    private Vector2[] segmentPos;           
    private GameObject[] joints;            
    private LineRenderer line;                       
    private int segments = 0;                  
    private bool rope = false;                       



    void Awake()
    {
        BuildRope();
    }

  
    void LateUpdate()
    {
        if (rope)
        {
            for (int i = 0; i < segments; i++)
            {
                if (i == 0)
                {
                    line.SetPosition(i, transform.position);
                }
                else
                if (i == segments - 1)
                {
                    line.SetPosition(i, target.transform.position);
                }
                else
                {
                    line.SetPosition(i, joints[i].transform.position);
                }
            }
            line.enabled = true;
        }
        else
        {
            line.enabled = false;
        }
    }



    void BuildRope()
    {
        line = gameObject.GetComponent<LineRenderer>();

        // Find the amount of segments based on the distance and resolution
        // Example: [resolution of 1.0 = 1 joint per unit of distance]
        segments = (int)(Vector2.Distance(transform.position, target.position) * resolution);
        Debug.Log(segments);
        line.SetVertexCount(segments);
        segmentPos = new Vector2[segments];
        joints = new GameObject[segments];
        segmentPos[0] = transform.position;
        segmentPos[segments - 1] = target.position;

        // Find the distance between each segment
        var segs = segments - 1;
        var seperation = ((target.position - transform.position) / segs);

        for (int s = 1; s < segments; s++)
        {
            // Find the each segments position using the slope from above
            Vector3 vector = (seperation * s) + transform.position;
            segmentPos[s] = vector;

            //Add Physics to the segments
            AddJointPhysics(s);
        }

        // Attach the joints to the target object and parent it to this object	
        HingeJoint2D end = target.gameObject.AddComponent<HingeJoint2D>();
      //  end.autoConfigureConnectedAnchor = true;
        end.connectedBody = joints[joints.Length - 1].GetComponent<Rigidbody2D>();
        end.anchor = new Vector2(0,0.5f);
        end.autoConfigureConnectedAnchor = false;
        end.connectedAnchor = new Vector2(0, 0f);

        var rigibody = target.GetComponent<Rigidbody2D>();
        rigibody.mass = 10;
        rigibody.angularDrag = 0.25f;
        rigibody.gravityScale = 1;

        // Rope = true, The rope now exists in the scene!
        rope = true;
    }

    void AddJointPhysics(int n)
    {
        joints[n] = new GameObject("Joint_" + n);
        joints[n].transform.parent = transform;
        joints[n].AddComponent<Rigidbody2D>();
        HingeJoint2D ph = joints[n].AddComponent<HingeJoint2D>();
        ph.autoConfigureConnectedAnchor = true;


        joints[n].transform.position = segmentPos[n];


        if (n == 1)
        {
            ph.connectedBody = transform.GetComponent<Rigidbody2D>();
        }
        else
        {
            ph.connectedBody = joints[n - 1].GetComponent<Rigidbody2D>();
        }

    }

}