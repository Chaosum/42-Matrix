using System;
using System.Collections.Generic;
using System.Linq;
using models.Vectors;
using models.Matrices;

namespace models.Matrices
{
    public class Matrix<K>
    {
        private List<List<K>> data;

#region constructeur
        public Matrix(List<List<K>> data)
        {
            this.data = data;
        }
#endregion constructeur

#region utilities
        /// <summary>
        /// Return the shape of the matrix (row, column)
        /// </summary>
        /// <returns></returns>
        public (int, int) Shape()
        {
            int rows = data.Count;
            int cols = rows > 0 ? data[0].Count : 0;
            return (rows, cols);
        }

        /// <summary>
        /// Check if the matrix a square
        /// </summary>
        /// <returns></returns>
        public bool IsSquare()
        {
            var (rows, cols) = Shape();
            return rows == cols;
        }

        /// <summary>
        /// Display the matrix
        /// </summary>
        public void Print()
        {
            Console.WriteLine("Matrix:");
            foreach (var row in data)
            {
                Console.WriteLine(string.Join(", ", row));
            }
        }

        /// <summary>
        /// Transform the matrix in vector
        /// the vector will be of size (1, n) where n is the number of element in the matrix
        /// </summary>
        /// <returns></returns>
        public Vector<K> FlattenToVector()
        {
            List<K> flattenedData = data.SelectMany(row => row).ToList();
            return new Vector<K>(flattenedData);
        }

        /// <summary>
        /// Access an element of the matrix
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public K GetElement(int row, int col)
        {
            if (row >= 0 && row < data.Count && col >= 0 && col < data[row].Count)
            {
                return data[row][col];
            }
            else
            {
                throw new ArgumentOutOfRangeException("Index hors limites");
            }
        }

        /// <summary>
        /// Update an element of the matrix
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="value"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void SetElement(int row, int col, K value)
        {
            if (row >= 0 && row < data.Count && col >= 0 && col < data[row].Count)
            {
                data[row][col] = value;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Index hors limites");
            }
        }

        public List<K> this[int i]
        {
            get
            {
                if (i < 0 || i >= data.Count)
                {
                    throw new IndexOutOfRangeException("L'indice de la ligne est hors limites.");
                }
                return data[i];
            }
        }
#endregion utilities

#region add sub scale
        /// <summary>
        /// Add a matric to the matrix
        /// both matrix MUST have the same size
        /// </summary>
        /// <param name="m"></param>
        /// <exception cref="ArgumentException"></exception>
        public void add(Matrix<K> m)
        {
            if (this.data.Count != m.data.Count || this.data[0].Count != m.data[0].Count)
            {
                throw new ArgumentException("Matrices must have the same dimensions for addition.");
            }
            // Parcours de chaque élément de la matrice pour effectuer la soustraction
            for (int i = 0; i < this.data.Count; i++)  // Parcours des lignes
            {
                for (int j = 0; j < this.data[i].Count; j++)  // Parcours des colonnes
                {
                    // Additionne les éléments de la matrice courante et de la matrice m
                    this.data[i][j] = (dynamic)this.data[i][j] + (dynamic)m.data[i][j];
                }
            }
        }
        /// <summary>
        /// substract a matrix to the matrix
        /// both matrix MUST have the same size
        /// </summary>
        /// <param name="m"></param>
        /// <exception cref="ArgumentException"></exception>
        public void sub(Matrix<K> m)
        {
            if (this.data.Count != m.data.Count || this.data[0].Count != m.data[0].Count)
            {
                throw new ArgumentException("Matrices must have the same dimensions for substraction.");
            }
            // Parcours de chaque élément de la matrice pour effectuer la soustraction
            for (int i = 0; i < this.data.Count; i++)  // Parcours des lignes
            {
                for (int j = 0; j < this.data[i].Count; j++)  // Parcours des colonnes
                {
                    // Additionne les éléments de la matrice courante et de la matrice m
                    this.data[i][j] = (dynamic)this.data[i][j] -(dynamic)m.data[i][j];
                }
            }
        }
        /// <summary>
        /// multiply a matrix by a factor
        /// </summary>
        /// <param name="factor"></param>
        public void scale( K factor )
        {
            // Parcours de chaque élément de la matrice pour effectuer la multipication
            for (int i = 0; i < this.data.Count; i++)  // Parcours des lignes
            {
                for (int j = 0; j < this.data[i].Count; j++)  // Parcours des colonnes
                {
                    // Multiplie les éléments de la matrice courante par m
                    this.data[i][j] = (dynamic)this.data[i][j] * factor;
                }
            }
        }
#endregion add sub scale

#region linear map matrix multiplication 
        public Matrix<K> mul_mat(Matrix<K> mat)
        {
            (int rowA,int colA) = this.Shape();
            (int rowB, int colB) = mat.Shape();
            if (colA != rowB){
                throw new ArgumentException("the number of col in the first matrix must be equel to the number of lines of the second matrix.");
            }
            List<List<K>> resultList = new List<List<K>>();
            for (int i = 0; i < rowA; i++)
            {
                List<K> lineResult = new List<K>();
                for (int j = 0; j < colB; j++)
                {
                    dynamic current = default(K);
                    for (int k = 0; k < colA; k++)
                    {
                        current += (dynamic)this[i][k] * (dynamic)mat[k][j];
                    }
                    lineResult.Add(current);
                }
                resultList.Add(lineResult);
            }
            Matrix<K> result = new Matrix<K> (resultList);
            return result;
        }
#endregion linear map matrix multiplication    
    }
}
