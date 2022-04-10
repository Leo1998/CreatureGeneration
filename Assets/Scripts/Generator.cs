using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{

    private GameObject parent;

    public List<GameObject> shapes;
    private Object[] materials;

    // Start is called before the first frame update
    void Start()
    {
        // load materials from Resources folder
        materials = Resources.LoadAll("Materials", typeof(Material));

        Generate();
    }

    void Generate()
    {
        // clear for new creature
        Destroy(parent);

        // choose random material
        Material material = (Material)materials[Random.Range(0, materials.Length)];

        float bodyHeight = Random.Range(2.2f, 3.5f);
        float bodyRadius = Random.Range(0.7f, 1.5f);

        // Skeleton 
        parent = Instantiate(shapes[0], new Vector3(0, 0, 0), Quaternion.identity);
        parent.transform.localScale = new Vector3(bodyRadius, bodyHeight, bodyRadius);
        parent.GetComponent<Renderer>().material = material;
        parent.SetActive(true);

        parent.name = "Creature (Parent)";

        int parts = Random.Range(2, 6);
        for (int i = 0; i < parts; i++)
        {
            float yPos = 1.75f * i * (bodyHeight / parts) - bodyHeight * 0.75f;
            float xPos = bodyRadius / 2;
            int numJoints = Random.Range(2, 5);

            GameObject prevLeft = parent;
            GameObject prevRight = parent;
            for (int j = 0; j < numJoints; j++) {
                int randShape = Random.Range(0, shapes.Count);
                float radius = Random.Range(0.25f, 0.75f);
                float length = Random.Range(0.5f, 1.5f);
                float rot = 90.0f;

                GameObject leftGo = Instantiate(shapes[randShape], new Vector3(xPos + length * 0.5f, yPos, 0), Quaternion.identity);
                GameObject rightGo = Instantiate(shapes[randShape], new Vector3(-leftGo.transform.position.x, leftGo.transform.position.y, leftGo.transform.position.z), Quaternion.identity);

                leftGo.transform.localScale = new Vector3(radius, length * 0.5f, radius);
                rightGo.transform.localScale = new Vector3(-leftGo.transform.localScale.x, leftGo.transform.localScale.y, leftGo.transform.localScale.z);

                leftGo.transform.rotation = Quaternion.Euler(0, 0, rot);
                rightGo.transform.rotation = Quaternion.Inverse(leftGo.transform.rotation);

                // add color
                leftGo.GetComponent<Renderer>().material = material;
                rightGo.GetComponent<Renderer>().material = material;
            
                leftGo.transform.parent = prevLeft.transform;
                rightGo.transform.parent = prevRight.transform;

                prevLeft = leftGo;
                prevRight = rightGo;
                xPos += length;

                leftGo.SetActive(true);
                rightGo.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space")) {
            Generate();
        }
    }
}
