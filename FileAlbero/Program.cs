using System;
using System.Collections.Generic;
using FileAlbero;

namespace FileAlbero
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("ALBERO STANDARD");
            TreeNode<string> albero = TreeNode<string>.AlberoDaFile();
            if (albero != null)
            {
                albero.StampaAlbero(albero, 0);
            }


            Console.WriteLine("\nALBERO CON TRATTINI");
            try
            {
                TreeNode<string> albero2 = TreeNode<string>.AlberoDaFile2();
                if (albero2 != null)
                {
                    albero2.StampaAlbero2(albero2, 0);
                }
            }
            catch { Console.WriteLine("Errore nel caricamento Albero 2."); }

            Console.WriteLine("\nMATRICE GRAFO");
            string[] nodiMatrice = { "A", "B", "C" };
            TreeNode<string>.GeneraMatriceGrafo(nodiMatrice);

        }
    }
}