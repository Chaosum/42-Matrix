using System;
using models.Vectors;
using models.Matrices;
using controller.Ex00;
using controller.Ex01;
using controller.Ex02;
using controller.Ex03;
using controller.Ex04;
using controller.Ex05;
using controller.Ex06;
using controller.Ex07;
using controller.Ex08;
using controller.Ex09;
using controller.Ex10;
using controller.Ex11;
 using controller.Ex12;
// using controller.Ex13;
// using controller.Ex14;

namespace models
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                // Si aucun argument, on lance tous les exercices
                RunAllTests();
            }
            else if (args.Length == 1)
            {
                // Si un seul argument, on lance l'exercice correspondant
                RunSingleTest(args[0]);
            }
            else
            {
                // Si plusieurs arguments, on affiche une erreur
                Console.WriteLine("Erreur : Veuillez fournir un seul argument correspondant à l'exercice (ex00, ex01, etc.), ou aucun argument pour tout exécuter.");
            }
        }

        static void RunSingleTest(string testName)
        {
            switch (testName)
            {
                case "ex00":
                    Ex00.Run();
                    break;
                case "ex01":
                    Ex01.Run();
                    break;
                case "ex02":
                    Ex02.Run();
                    break;
                case "ex03":
                    Ex03.Run();
                    break;
                case "ex04":
                    Ex04.Run();
                    break;
                case "ex05":
                    Ex05.Run();
                    break;
                case "ex06":
                    Ex06.Run();
                    break;
                case "ex07":
                    Ex07.Run();
                    break;
                case "ex08":
                    Ex08.Run();
                    break;
                case "ex09":
                    Ex09.Run();
                    break;
                case "ex10":
                    Ex10.Run();
                    break;
                case "ex11":
                    Ex11.Run();
                    break;
                case "ex12":
                    Ex12.Run();
                    break;
                default:
                    Console.WriteLine($"Erreur : L'exercice '{testName}' n'existe pas.");
                    break;
            }
        }

        static void RunAllTests()
        {
            Console.WriteLine("Exécution de tous les exercices :");
            Ex00.Run();
            Ex01.Run();
            Ex02.Run();
            Ex03.Run();
            Ex04.Run();
            Ex05.Run();
            Ex06.Run();
            Ex07.Run();
            Ex08.Run();
            Ex09.Run();
            Ex10.Run();
            Ex11.Run();
            Ex12.Run();
        }
    }
}
