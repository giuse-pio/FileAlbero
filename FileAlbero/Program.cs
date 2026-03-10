using System;
using System.Collections.Generic;
using FileAlbero;

namespace FileAlbero
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Fondamentale per vedere il simbolo ∞ correttamente
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== 1. ALBERO STANDARD ===");
            TreeNode<string> albero = TreeNode<string>.AlberoDaFile();
            if (albero != null) albero.StampaAlbero(albero, 0);



            Console.WriteLine("\n=== 2. ALBERO CON TRATTINI ===");
            try
            {
                TreeNode<string> albero2 = TreeNode<string>.AlberoDaFile2();
                if (albero2 != null) albero2.StampaAlbero2(albero2, 0);
            }
            catch { Console.WriteLine("Errore nel caricamento Albero 2."); }



            Console.WriteLine("\n=== 3. MATRICE GRAFO ===");
            string[] nodiMatrice = { "A", "B", "C" };
            TreeNode<string>.GeneraMatriceGrafo(nodiMatrice);



            Console.WriteLine("\n=== 4. RICERCA NEL GRAFO (BFS & DFS) ===");
            TreeNode<string> grafo = TreeNode<string>.GrafoDaFile();

            if (grafo == null)
            {
                Console.WriteLine("ERRORE: Grafo non caricato.");
            }
            else
            {
                // Questi sono i posti dove SI MANGIA
                List<string> postiDoveMangiare = new List<string> { "Pizzeria", "Sushi", "Paninoteca" };

                Console.WriteLine("\n--- Inizio BFS ---");
                grafo.BFS_Mangiare(postiDoveMangiare);

                Console.WriteLine("\n--- Inizio DFS ---");
                grafo.DFS_Mangiare(postiDoveMangiare, new HashSet<TreeNode<string>>());
            }
        }
    }
}