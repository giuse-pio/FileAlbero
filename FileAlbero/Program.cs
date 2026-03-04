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
                Console.WriteLine("funzia");
            }

           
        }
    }
}
