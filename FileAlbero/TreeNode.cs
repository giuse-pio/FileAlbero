using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileAlbero
{
    public class TreeNode<T>
    {
        public T Value { get; set; }
        public List<TreeNode<T>> Nodes { get; set; } = new();

        public void StampaAlbero(TreeNode<T> radice)
        {
            Console.WriteLine(radice.Value);
            foreach (var nodi in radice.Nodes)
            {
                StampaAlbero(nodi);
            }

        }


        public void AlberoDaFile()
        {
            StreamReader reader = new("FileAlbero.txt");
            TreeNode<T> radice = new();
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                
            }
        }
        //public void StampaAlberoDaFile(TreeNode<T> radice, string filePath)
        //{
        //    using (StreamWriter writer = new StreamWriter(filePath))
        //    {
        //        StampaAlberoDaFileHelper(radice, writer, 0);
        //    }
        //}
        //private void StampaAlberoDaFileHelper(TreeNode<T> nodo, StreamWriter writer, int livello)
        //{
        //    writer.WriteLine(new string(' ', livello * 2) + nodo.Value);
        //    foreach (var nodi in nodo.Nodes)
        //    {
        //        StampaAlberoDaFileHelper(nodi, writer, livello + 1);
        //    }
        //}
    }
}
