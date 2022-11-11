using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Autors:
// David Damián Galán
// Erika Marlene García Sánchez
public class MakePyramid : MonoBehaviour
{

    private int[] triangles;

    private class MyVector3
    {
        private double x;
        private double y;
        private double z;

        public MyVector3(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public MyVector3 Translation(MyVector3 vector)
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
            return new MyVector3(resultado[0][0], resultado[1][0], resultado[2][0]);
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
    // Start is called before the first frame update
    void Start()
    {
        Console.WriteLine("Hola mundo");
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        MyVector3 myVector3 = new MyVector3(1, 2, 3);
        Console.WriteLine(myVector3.Translation(myVector3));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
