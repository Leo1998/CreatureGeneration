using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{

    private GameObject parent;
    private GameObject left;
    private GameObject right;

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

        // Skeleton 
        parent = new GameObject();
        left = new GameObject();
        right = new GameObject();

        parent.name = "Creature (Parent)";
        left.name = "left";
        right.name = "right";

        // set parent
        left.transform.parent = parent.transform;
        right.transform.parent = parent.transform;


        int elements = Random.Range(10, 50);

        // loop to generate creature parts
        for (int i = 0; i < elements; i++)
        {
            int randShape = Random.Range(0, shapes.Count);
            float scale = Random.Range(0.25f, 1.25f);
            float randRot = Random.Range(-360.00f, 360.00f);

            GameObject leftGo = Instantiate(shapes[randShape], new Vector3(Random.Range(0.35f, 3.0f), Random.Range(-1.0f, 3.0f), Random.Range(-1.0f, 3.0f)), Quaternion.identity);
            GameObject rightGo = Instantiate(shapes[randShape], new Vector3(-leftGo.transform.position.x, leftGo.transform.position.y, leftGo.transform.position.z), Quaternion.identity);

            leftGo.transform.localScale = new Vector3(scale, scale, scale);
            rightGo.transform.localScale = new Vector3(-leftGo.transform.localScale.x, leftGo.transform.localScale.y, leftGo.transform.localScale.z);

            leftGo.transform.rotation = Quaternion.Euler(0, 0, randRot);
            rightGo.transform.rotation = Quaternion.Inverse(leftGo.transform.rotation);

            // add color
            leftGo.GetComponent<Renderer>().material = material;
            rightGo.GetComponent<Renderer>().material = material;
         
            leftGo.transform.parent = left.transform;
            rightGo.transform.parent = right.transform;

            leftGo.SetActive(true);
            rightGo.SetActive(true);
           
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
