using TreeLibrary;
namespace FileAlbero

{
    internal class Program
    {
        static void Main(string[] args)
        {
            TreeNode<string> albero = TreeNode<string>.AlberoDaFile();
            //albero.StampaAlbero(albero, 0);
            if (albero.FileDaAlbero(albero))
            {
                Console.WriteLine("generazione albero con il DSB (file-->albero)");
            }
            albero.StampaAlbero(albero,0);
            
            Console.WriteLine();

            TreeNode<string> albero2 = TreeNode<string>.AlberoDaFile2();
            if (albero2.FileDaAlbero2(albero2))
            {
                Console.WriteLine("generazione albero con i meno");
            }
            albero2.StampaAlbero2(albero2,0);
            
            Console.WriteLine();
            Console.WriteLine();
            string[] nodi = { "A", "B", "C" };
            TreeNode<string>.GeneraMatriceGrafo(nodi);
            


        }
    }
}
