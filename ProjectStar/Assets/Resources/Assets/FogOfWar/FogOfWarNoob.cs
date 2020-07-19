using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWarNoob : MonoBehaviour
{
    public GameObject m_fogOfWarPlane;
    public Transform[] players;
    public Transform m_player;
    public Transform m_player1;
    public LayerMask m_fogLayer;
    public float m_radius = 5f;
    private float m_radiusSqr { get { return m_radius * m_radius; } }

    private Mesh m_mesh;
    private Vector3[] m_vertices;
    private Color[] m_colors;



    private List<Vector3> listaDoMeuPau = new List<Vector3>();
    private List<Color> listaDoMeuPauPintado = new List<Color>();
    // Use this for initialization
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        //for (int i = 0; i < m_vertices.Length; i++)
        //{
        //    m_colors[i].a = 1f;
        //    UpdateColor();
        //}

        Cu();

    }

    public void Cu()
    {
        lint.Clear();
        //loat.Clear();
        foreach (var player in players)
        {

            Ray r = new Ray(transform.position, player.position - transform.position);
            RaycastHit hit;
            if (Physics.Raycast(r, out hit, 1000, m_fogLayer, QueryTriggerInteraction.Collide))
            {
                for (int i = 0; i < m_vertices.Length; i++)
                {
                    m_colors[i] = Color.black;
                    m_colors[i].a = 1.0f;

                    Vector3 v = m_fogOfWarPlane.transform.TransformPoint(m_vertices[i]);
                    float dist = Vector3.SqrMagnitude(v - hit.point);
                    if (dist < m_radiusSqr)
                    {
                        lint.Add(i);
                        //float alpha = Mathf.Min(m_colors[i].a, dist / m_radiusSqr);
                        //loat.Add(alpha);
                        //m_colors[i].a = alpha;

                    }
                    //else
                    //{
                    //    m_colors[i].a = 1f;
                    //}
                }
                //UpdateColor();
            }


        }

        UpdateColor();

        //if (Input.GetKey(KeyCode.P))
        //{
        // m_mesh.colors = m_colors;
        //}

    }

    void Initialize()
    {
        m_mesh = m_fogOfWarPlane.GetComponent<MeshFilter>().mesh;
        m_vertices = m_mesh.vertices;
        //listaDoMeuPau = new List<Vector3>(m_vertices);
        m_colors = new Color[m_vertices.Length];
        colorlala = new Color[m_vertices.Length];
        for (int i = 0; i < m_colors.Length; i++)
        {
            m_colors[i] = Color.black;
            m_colors[i].a = 1.0f;
        }
        UpdateColor();
    }

    private Color[] colorlala;
    private List<int> lint = new List<int>();
    private List<float> loat = new List<float>();

    void UpdateColor()
    {
        //for (int i = 0; i < colorlala.Length; i++)
        //{
        //    colorlala[i] = Color.black;
        //    colorlala[i].a = 1.0f;
        //}

        for (int i = 0; i < m_colors.Length; i++)
        {
            m_colors[i] = Color.black;
            m_colors[i].a = .99f;
        }

        m_mesh.colors = m_colors;

        int index = 0;

        foreach (var item in lint)
        {
            //m_colors[item].a = .1f;
            m_colors[item].a = .1f;//loat[index];
            //m_colors[item].a = loat[index];
            index++;
        }

        m_mesh.colors = m_colors;

        //Debug.Log("before" + m_mesh.colors.Length);
        //m_mesh.colors = m_colors;
        //Debug.Log("after" + m_mesh.colors.Length);
    }
}
