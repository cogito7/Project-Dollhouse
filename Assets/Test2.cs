using JetBrains.Annotations;
using System.IO.IsolatedStorage;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class DesertScene : MonoBehaviour
{
    public GameObject[,] forestGrid;
    public int rowsofForest;
    public int columnsofForest;
    public int pyramidSize;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializeVariables();
        CreateGround();
        CreateRandomForest();
        CreatePyramid();
    }

    void InitializeVariables()
    {
        rowsofForest = 5;//initializes rows of trees (x axis)
        columnsofForest = 5;//initializes columns of trees (z axis)
        forestGrid = new GameObject[rowsofForest, columnsofForest];//initializes 2D array for trees
        pyramidSize = 5;//initializes size of a pyramid row
    }

    void CreateGround()
    {
        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.transform.localScale = new Vector3(2, 1, 2); //enlarges plane

        Renderer renderer = plane.GetComponent<Renderer>();
        //renderer.material = new Material(Shader.Find("Standard"));
        renderer.material.color = Color.green;
    }
    void CreateRandomForest()
    {
        GameObject cylinderParent = new GameObject("CylinderParent");


        for (int x = 0; x < rowsofForest; x++)
        {
            for (int z = 0; z < columnsofForest; z++)
            {
                GameObject randomcylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                randomcylinder.transform.parent = cylinderParent.transform;
                randomcylinder.GetComponent<Renderer>().material.color = Color.green;
                randomcylinder.transform.parent = randomcylinder.transform;

                //randomize scale and position of trees
                float scaleX = UnityEngine.Random.Range(0.1f, 2.0f);
                float scaleY = UnityEngine.Random.Range(0.5f, 2.0f);
                float scaleZ = UnityEngine.Random.Range(0.1f, 2.0f);
                randomcylinder.transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
                float positionX = UnityEngine.Random.Range(2.0f, 7.0f);
                float positionZ = UnityEngine.Random.Range(-2.0f, 7.0f);
                randomcylinder.transform.position = new Vector3(positionX, scaleY, positionZ);//keeps y value from falling through plane

                forestGrid[x, z] = randomcylinder;//stores cylinder trees into 2D array

            }
        }

    }
    void CreatePyramid()

    {
        GameObject cubeParent = new GameObject("CubeParent");
        float cubeSize = 1.0f;
        float spacing = 1.1f;
        float pyramidBaseHeight = cubeSize / 2; //keep cubes above the plane
        float xShift = -2.0f; //shifts pyramid slightly to right for room for forest

        for (int y = 0; y < pyramidSize; y++)
        {
            int cubesInLayer = pyramidSize - y; //reduce cubes on layer value as layers rise up in the loop
            for (int x = 0; x < cubesInLayer; x++)
            {
                for (int z = 0; z < cubesInLayer; z++)
                {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.parent = cubeParent.transform;

                    float xOffset = (cubesInLayer - 1) * 0.5f * spacing; //for centering cubes along x axis
                    float zOffset = (cubesInLayer - 1) * 0.5f * spacing; //for centering cubes along z axis

                    cube.transform.position = new Vector3(
                        x * spacing - xOffset + xShift, //adjusts lef and right (along x axis)
                        pyramidBaseHeight + y * spacing, //keep cubes above plane level           
                        z * spacing - zOffset //adjusts forward and back (along z axis)
                    );
                    if (cubesInLayer == 5)
                    {
                        cube.GetComponent<Renderer>().material.color = Color.black;
                    }

                    if (cubesInLayer == 4)
                    {
                        cube.GetComponent<Renderer>().material.color = Color.white;
                    }

                    if (cubesInLayer == 3)
                    {
                        cube.GetComponent<Renderer>().material.color = Color.red;
                    }

                    if (cubesInLayer == 2)
                    {
                        cube.GetComponent<Renderer>().material.color = Color.blue;
                    }

                    if (cubesInLayer == 1)
                    {
                        cube.GetComponent<Renderer>().material.color = Color.green;
                    }
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

}