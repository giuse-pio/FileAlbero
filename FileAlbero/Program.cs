namespace FileAlbero

{
    internal class Program
    {
        static void Main(string[] args)
        {
            TreeNode<string> albero = TreeNode<string>.AlberoDaFile();
            albero.StampaAlbero(albero, 0);
            if (albero.FileDaAlbero(albero))
            {
                Console.WriteLine("funziona (file-->albero)");
            }

            string[] nodi = { "A", "B", "C" };
            TreeNode<string>.GeneraMatriceGrafo(nodi);
            Console.WriteLine();
            Console.WriteLine();


        }
    }
}
