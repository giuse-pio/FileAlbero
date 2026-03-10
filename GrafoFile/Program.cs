using FileAlbero;
namespace GrafoFile
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TreeNode<string> grafo = TreeNode<string>.GrafoDaFile();

            if (grafo == null)
            {
                Console.WriteLine("ERRORE: Grafo non caricato.");
            }
            else
            {
                List<string> postiDoveMangiare = new List<string> { "Pizzeria", "Sushi", "Paninoteca" };

                Console.WriteLine("\n BFS ");
                grafo.BFS_Mangiare(postiDoveMangiare);

                Console.WriteLine("\n DFS ");
                grafo.DFS_Mangiare(postiDoveMangiare, new HashSet<TreeNode<string>>());
            }
        }
    }
}
