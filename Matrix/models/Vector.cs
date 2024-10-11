using System;
using System.Collections.Generic;
using models.Vectors;
using models.Matrices;

namespace models.Vectors
{
    public class Vector<K>
    {
        private List<K> data;

#region constructors
        public Vector(List<K> data)
        {
            this.data = data;
        }

        public Vector(Vector<K> data)
        {
            this.data = new List<K>(data.data);
        }
#endregion constructors

#region utilities
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
        // Surcharge de l'opérateur []
        public K this[int i]
        {
            get
            {
                return GetElement(i);
            }
        }
        
#endregion utilities

#region add sub scale
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
#endregion add sub scale
        
#region LinearCombination
        public static Vector<K> LinearCombination<K>(List<Vector<K>> vectors, List<K> coefs)
        {
            if (vectors.Count != coefs.Count)
            {
                throw new ArgumentException("The number of vectors must match the number of coefficients");
            }

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
#endregion LinearCombination

#region Lerp
        public static Vector<K> Lerp(Vector<K> u, Vector<K> v, float t)
        {
            if (u.Size() != v.Size())
            {
                throw new ArgumentException("Vectors must be of the same length");
            }
            List<K> listResult = new List<K>();
            for (int i =0; i < u.Size(); i++)
            {
                K lerp = (1 - t) * (dynamic)u.GetElement(i) + t * (dynamic)v.GetElement(i);
                listResult.Add(lerp);
            }
            return new Vector<K>(listResult);
        }
#endregion Lerp
        
#region dot product (produit scalaire)
        private K GetDotProduct(K element1, K element2)
        {
            if (element1 is Complex complexElement1 && element2 is Complex complexElement2)
            {
                // Si ce sont deux nombres complexes, utiliser le conjugué du premier
                return (K)(dynamic)(complexElement1.Conjugate() * complexElement2);
            }
            else
            {
               return (K)((dynamic)element1 * (dynamic)element2);
            }
        }

        public K dot(Vector<K> v)
        {
            if (this.Size() != v.Size())
            {
                throw new ArgumentException("Les vecteurs doivent être de la même taille.");
            }

            dynamic result = default(K);  // Initialisation du résultat
            for (int i = 0; i < this.Size(); i++)
            {
                // Utilisation de la méthode GetDotProduct pour chaque paire d'éléments
                result += GetDotProduct(this.GetElement(i), v.GetElement(i));
            }
            return result;
        }
#endregion dot product (produit scalaire)
        
#region norm
        private float GetMagnitude(dynamic element)
        {
            if (element is Complex)
            {
                return ((Complex)(dynamic)element).Abs(); // Module pour un nombre complexe
            }
            else
            {
                return Math.Abs((float)(dynamic)element); // Valeur absolue pour un réel
            }
        }

        public float Norm_1() {
            float result = 0f;
            for (int i = 0; i < this.Size(); i++)
            {               
                result += GetMagnitude(this.GetElement(i));
            }
            return result;
        }

        public float Norm()
        {
            dynamic result = 0f;
            for (int i = 0; i < this.Size(); i++)
            {
                result += Math.Pow(GetMagnitude(this.GetElement(i)), 2);
            }
            return (float)Math.Sqrt((float)result);
        }

        public float Norm_inf()
        {
            float result = 0f;
            for (int i = 0; i < this.Size(); i++)
            {
                float current = GetMagnitude(this.GetElement(i));
                if (result < current)
                {
                    result = current;
                }
            }
            return result;
        }
#endregion norm

#region Angle_cos
        public static float Angle_cos(Vector<K> u, Vector<K> v)
        {
            if (u.Size() != v.Size())
            {
                throw new ArgumentException("Vectors must be of the same length");
            }
            dynamic dotProduct = u.dot(v);
            float normU = u.Norm();
            float normV = v.Norm();
            if (normU == 0 || normV == 0)
            {
                throw new ArgumentException("La norme d'un des vecteurs est nulle.");
            }
            if (dotProduct is Complex)
            {
                dotProduct = dotProduct.Real;
            }
            return (float)(dotProduct/ (normU * normV));
        }
#endregion Angle_cos

#region cross product
        public static Vector<K> Cross_product(Vector<K> u, Vector<K> v)
        {
            if (u.Size() != 3 || v.Size() != 3)
            {
                throw new ArgumentException("the cross product is only defined for vector of size 3.");
            }
            List<K> ListResult = new List<K>();
            dynamic x = (dynamic)u[2] * v[3] - (dynamic)u[3] - v[2];
            dynamic y = (dynamic)u[3] * v[1] - (dynamic)u[1] * v[3];
            dynamic z = (dynamic)u[1] * v[2] - (dynamic)u[2] * v[1];
            ListResult.Add(x);
            ListResult.Add(y);
            ListResult.Add(z);
            Vector<K> result = new Vector<K>(ListResult);
            return result;
        }
#endregion cross product

    }
}
