using MatrixExercises.Vectors;
using MatrixExercises.Matrices;

namespace MatrixExercises.Ex06
{
    public class Ex06
    {
        public static void Run()
        {
            Console.WriteLine("Exécution de l'exercice 06");

            // Création d'un vecteur
            var vector = new Vector<float>(new List<float> { 1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 6.0f });
            vector.Print();
            Console.WriteLine($"Taille du vecteur : {vector.Size()}");

            // Reshape du vecteur en une matrice 2x3
            var matrix = vector.ReshapeToMatrix(2, 3);
            matrix.Print(); // Affichera une matrice 2x3

            // Reshape du vecteur en une matrice 3x2
            var matrix2 = vector.ReshapeToMatrix(3, 2);
            matrix2.Print(); // Affichera une matrice 3x2
        }
    }
}
