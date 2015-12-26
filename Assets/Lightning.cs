using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Lightning : MonoBehaviour {

	public Transform    start;
	public Transform    end;
    public float        maximumOffset = 1;
    public float        speed = 1f;

    public AnimationCurve   lightningColorOverTime;
    public float            lightningDuration = 1f;
    public Material glow;
    public ParticleSystem particles;

    [Range(1, 10)]
    public int iterations = 1;

    [Range(1, 90)]
    public int angle = 10;
    int[] angleGroup;

    List<Segment> segments;
	
    
	float offsetAmount;

    
	void Start () 
	{
        angleGroup = new int[2];
        offsetAmount = maximumOffset;
        GenerateLightningSegments();
        GetComponent<GenerateQuad>().GenerateMeshFromSegments(segments);
        StartCoroutine(dimLightning());
        particles.Play(); 
	}
	

    IEnumerator dimLightning()
    {
        Material mat = GetComponent<MeshRenderer>().material;
        int glowStrength = Shader.PropertyToID("_GlowStrength");
        glow.SetFloat(glowStrength, 10);
        
        Color normalColor = mat.color;
        float time = 0;
        while( time < lightningDuration)
        {
            mat.color = normalColor * lightningColorOverTime.Evaluate(Mathf.InverseLerp(0, lightningDuration, time));
            float newGlowStrength = Mathf.Lerp(0, 10, (lightningColorOverTime.Evaluate(Mathf.InverseLerp(0, lightningDuration, time))));
            glow.SetFloat(glowStrength, newGlowStrength);
            time += Time.deltaTime;
            yield return null;
        }
        mat.color = normalColor;
        Start();
        
    }

    void GenerateLightningSegments()
    {
        segments = new List<Segment>();
        segments.Add(new Segment(start.position, end.position));

        for (int i = 0; i < iterations; i++)
        {

            int segment_length = segments.Count;
            for (int j = 0; j < segment_length; j++)
            {
                Segment currentSegment = segments[0];
                segments.RemoveAt(0);

                Vector3 midPoint = currentSegment.start + (currentSegment.end - currentSegment.start) * 0.5f;

                Vector3 midPointOffset = (currentSegment.end - currentSegment.start).normalized * Random.Range(-offsetAmount, offsetAmount);
                midPointOffset = new Vector3(-midPointOffset.y, midPointOffset.x, midPointOffset.z);
                midPoint += midPointOffset;

                if (Random.Range(0, 10) >6)
                {
                   
                    Vector3 someDirection = midPoint - currentSegment.start;
                    angleGroup[0] = Random.Range(30, angle);
                    angleGroup[1] = Random.Range(-30, -angle);
                    float deg_angle = angleGroup[Random.Range(0,2)] * Mathf.Deg2Rad;
                    float splitEnd_X = someDirection.x * Mathf.Cos(deg_angle) - someDirection.y * Mathf.Sin(deg_angle);
                    float splitEnd_Y = someDirection.x * Mathf.Sin(deg_angle) + someDirection.y * Mathf.Cos(deg_angle);


                    Vector3 splitEnd = new Vector3(splitEnd_X, splitEnd_Y, 0) * Random.Range(0.7f, 1f) + midPoint; // lengthScale is, for best results, < 1.  0.7 is a good value.
                    segments.Add(new Segment(midPoint, splitEnd, true));
                }


                segments.Add(new Segment(currentSegment.start, midPoint,currentSegment.isBranch));
                segments.Add(new Segment(midPoint, currentSegment.end, currentSegment.isBranch));


            }
            offsetAmount /= 2;
        }
    }
}

public struct Segment
{
	public Vector3 start;
	public Vector3 end;
    public bool isBranch;

	public Segment(Vector3 start, Vector3 end)
	{
		this.start=start;
		this.end=end;
        isBranch = false;
	}

    public Segment(Vector3 start, Vector3 end, bool isBranch)
    {
        this.start = start;
        this.end = end;
        this.isBranch = isBranch;
    }
}
