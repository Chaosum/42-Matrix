using MatrixExercises.Vectors;
using MatrixExercises.Matrices;
using System.ComponentModel.DataAnnotations;

namespace MatrixExercises.Ex00
{
    //Exercise 00 - Add, Subtract and Scale
    public class Ex00
    {
        public static void Run()
        {
            Console.WriteLine("Running Ex00 : ");

            // Creation of a vector
            var vector = new Vector<float>(new List<float> { 1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 6.0f });
            var secondVector = new Vector<float>(new List<float> { 3.0f, 17.0f, 20.0f, 11.0f, 13.0f, 18.0f });
            var allOneVector = new Vector<float>(new List<float> { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f });

            Console.WriteLine($"Size of the first vector : {vector.Size()}");
            Console.WriteLine($"vector = {vector}");
            Console.WriteLine($"Size of the second vector: {secondVector.Size()}");
            Console.WriteLine($"secondVector = {secondVector}");

            Console.WriteLine($"Test add :");
            Console.WriteLine($"Vector + allOneVector");
            vector.add(allOneVector);
            Console.WriteLine(vector);
            Console.WriteLine($"Vector - allOneVector");
            vector.sub(allOneVector);
            Console.WriteLine(vector);
            Console.WriteLine($"Vector + secondVector");
            vector.add(secondVector);
            Console.WriteLine(vector);
            Console.WriteLine($"Vector - secondVector 2 times");
            vector.sub(secondVector);
            Console.WriteLine(vector);
            vector.sub(secondVector);
            Console.WriteLine(vector);
            Console.WriteLine($"scale allOneVector by 15");
            allOneVector.scale(15);
            Console.WriteLine(allOneVector);
            Console.WriteLine($"scale vector by 2");
            vector.scale(2);
            Console.WriteLine(vector);
            Console.WriteLine($"scale vector by 0.1");
            vector.scale(0.1f);
            Console.WriteLine(vector);
        }
    }
}
