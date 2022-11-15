using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Autors:
// David Damián Galán
// Erika Marlene García Sánchez
public class MakePyramid : MonoBehaviour
{

    private class MyVector3
    {
        public double x;
        public double y;
        public double z;

        public static MyVector3 operator -(MyVector3 vector)
        {
            return new MyVector3(-vector.x, -vector.y, -vector.z);
        }

        public MyVector3(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static explicit operator Vector3 (MyVector3 vector)
        {
            return new Vector3((float)vector.x, (float)vector.y, (float)vector.z);
        }
        public void Translation(MyVector3 vector)
        {
            double[][] matrix = new double[4][];
            matrix[0] = new double[] { 1, 0, 0, vector.x };
            matrix[1] = new double[] { 0, 1, 0, vector.y };
            matrix[2] = new double[] { 0, 0, 1, vector.z };
            matrix[3] = new double[] { 0, 0, 0, 1 };
            double[][] point = new double[4][];
            point[0] = new double[] { x };
            point[1] = new double[] { y };
            point[2] = new double[] { z };
            point[3] = new double[] { 1 };
            double[][] resultado = MatrixMultiplication(matrix, point);
            this.x = resultado[0][0];
            this.y = resultado[1][0];
            this.z = resultado[2][0];
        }
        public void Rotation(string axis, double angle, MyVector3 pivote)
        {

            double[][] matrix = new double[4][];
            angle = Mathf.Deg2Rad * angle;

            Translation(-pivote);

            if (axis == "x")
            {
                matrix[0] = new double[] { 1, 0, 0, 0 };
                matrix[1] = new double[] { 0, Mathf.Cos((float)angle), -Mathf.Sin((float)angle), 0 };
                matrix[2] = new double[] { 0, Mathf.Sin((float)angle), Mathf.Cos((float)angle), 0 };
                matrix[3] = new double[] { 0, 0, 0, 1 };
            }
            if (axis == "y")
            {
                matrix[0] = new double[] { Mathf.Cos((float)angle), 0, Mathf.Sin((float)angle), 0 };
                matrix[1] = new double[] { 0, 1, 0, 0 };
                matrix[2] = new double[] { -Mathf.Sin((float)angle), 0, Mathf.Cos((float)angle), 0 };
                matrix[3] = new double[] { 0, 0, 0, 1 };
            }
            if (axis == "z")
            {
                matrix[0] = new double[] { Mathf.Cos((float)angle), -Mathf.Sin((float)angle), 0, 0 };
                matrix[1] = new double[] { Mathf.Sin((float)angle), Mathf.Cos((float)angle), 0, 0 };
                matrix[2] = new double[] { 0, 0, 1, 0 };
                matrix[3] = new double[] { 0, 0, 0, 1 };
            }

            double[][] point = new double[4][];
            point[0] = new double[] { x };
            point[1] = new double[] { y };
            point[2] = new double[] { z };
            point[3] = new double[] { 1 };
            double[][] resultado = MatrixMultiplication(matrix, point);
            MyVector3 final = new MyVector3(resultado[0][0], resultado[1][0], resultado[2][0]);

            final.Translation(pivote);
            this.x = final.x;
            this.y = final.y;
            this.z = final.z;
        }
        private double[][] MatrixMultiplication(double[][] matrix, double[][] point)
        {

            int rows = matrix.Length;
            int cols = point[0].Length;
            double[][] resultado = new double[rows][];
            for (int r = 0; r < rows; r++)
            {
                resultado[r] = new double[cols];
                for (int c = 0; c < cols; c++)
                {
                    double suma = 0;
                    for (int k = 0; k < point.Length; k++)
                    {
                        suma += matrix[r][k] * point[k][c];
                    }

                    resultado[r][c] = suma;
                }
            }
            return resultado;
        }
    }
    private Vector3[] vertices;
    private int[] triangles;
    // Start is called before the first frame update
    void Start()
    {
        MyVector3 pivote = new MyVector3(-1.812, -6.15, 5.25);
        Debug.Log("Vertex Q");
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        MyVector3 q = new MyVector3(-3.462, -6.824, 4.294);
        q.Rotation("y", -15, pivote);
        Debug.Log(q.x);
        Debug.Log(q.y);
        Debug.Log(q.z);

        Debug.Log("Vertex P");
        MyVector3 p = new MyVector3(-0.162, -6.824, 4.294);
        p.Rotation("y", -15, pivote);
        Debug.Log(p.x);
        Debug.Log(p.y);
        Debug.Log(p.z);

        Debug.Log("Vertex R");
        MyVector3 r = new MyVector3(-1.812, -6.824, 7.151);
        r.Rotation("y", -15, pivote);
        Debug.Log(r.x);
        Debug.Log(r.y);
        Debug.Log(r.z);

        Debug.Log("Vertex S");
        MyVector3 s = new MyVector3(-1.812, -4.129, 5.247);
        s.Rotation("y", -15, pivote);
        Debug.Log(s.x);
        Debug.Log(s.y);
        Debug.Log(s.z);

        vertices = new Vector3[12];
        triangles = new int[12];


        vertices[0] = (Vector3) q;
        vertices[1] = (Vector3) r;
        vertices[2] = (Vector3) s;

        vertices[3] = (Vector3)r;
        vertices[4] = (Vector3)s;
        vertices[5] = (Vector3)p;

        vertices[6] = (Vector3)p;
        vertices[7] = (Vector3)s;
        vertices[8] = (Vector3)q;

        vertices[9] = (Vector3)q;
        vertices[10] = (Vector3)r;
        vertices[11] = (Vector3)p;


        triangles[0] = 0;
        triangles[1] = 2;
        triangles[2] = 1;

        triangles[3] = 3;
        triangles[4] = 4;
        triangles[5] = 5;

        triangles[6] = 6;
        triangles[7] = 7;
        triangles[8] = 8;

        triangles[9] = 10;
        triangles[10] = 11;
        triangles[11] = 9;



        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
