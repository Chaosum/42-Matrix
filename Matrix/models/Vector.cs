using System;
using System.Collections.Generic;
using MatrixExercises.Vectors;
using MatrixExercises.Matrices;

namespace MatrixExercises.Vectors
{
    public class Vector<K>
    {
        private List<K> data;

        public Vector(List<K> data)
        {
            this.data = data;
        }

        public Vector(Vector<K> data)
        {
            this.data = new List<K>(data.data);
        }

        // Retourne la taille du vecteur
        public int Size()
        {
            return data.Count;
        }

        // Imprime le vecteur
        public void Print()
        {
            Console.WriteLine("( " + string.Join(", ", data) + " )");
        }

        // Fonction pour transformer le vecteur en une matrice
        public Matrix<K> ReshapeToMatrix(int rows, int cols)
        {
            if (rows * cols != data.Count)
            {
                throw new ArgumentException("La taille du vecteur ne correspond pas à la forme demandée.");
            }

            List<List<K>> matrixData = new List<List<K>>();
            for (int i = 0; i < rows; i++)
            {
                matrixData.Add(new List<K>());
                for (int j = 0; j < cols; j++)
                {
                    matrixData[i].Add(data[i * cols + j]);
                }
            }

            return new Matrix<K>(matrixData);
        }

        // Accéder à un élément du vecteur
        public K GetElement(int index)
        {
            if (index >= 0 && index < data.Count)
            {
                return data[index];
            }
            else
            {
                throw new ArgumentOutOfRangeException("Index hors limites");
            }
        }

        // Mettre à jour un élément du vecteur
        public void SetElement(int index, K value)
        {
            if (index >= 0 && index < data.Count)
            {
                data[index] = value;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Index hors limites");
            }
        }

        // Affichage
        public override string ToString()
        {
            return "[" + string.Join(", ", data) + "]";
        }

        public void add(Vector<K> v)
        {
            if (v.data.Count != data.Count)
                throw new ArgumentException("Vectors must be of the same length");

            for (int i = 0; i < data.Count; i++)
            {
                data[i]  = (dynamic)data[i] + (dynamic)v.data[i];
            }
        }

        // Soustraction
        public void sub(Vector<K> v)
        {
            if (v.data.Count != data.Count)
                throw new ArgumentException("Vectors must be of the same length");

            for (int i = 0; i < data.Count; i++)
            {
                data[i] -= (dynamic)v.data[i];
            }
        }

        // Scaling
        public void scale(K scalar)
        {
            for (int i = 0; i < data.Count; i++)
            {
                data[i] = (dynamic)data[i] * scalar;
            }
        }
        public static Vector<K> LinearCombination<K>(List<Vector<K>> vectors, List<K> coefs)
        {
            if (vectors.Count != coefs.Count)
                throw new ArgumentException("The number of vectors must match the number of coefficients");

            Vector<K> result = new (vectors[0]);
            result.scale(coefs[0]);
            // Additionner les vecteurs multipliés par les coefficients
            for (int i = 1; i < vectors.Count; i++)
            {
                Vector<K> tempVector = new Vector<K>(vectors[i]);
                tempVector.scale(coefs[i]);
                result.add(tempVector);
            }
            return result;
        }

    }
}
