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

        public TreeNode(T value)
        {
            Value = value;
        }
        public void StampaAlbero(TreeNode<T> radice, int livello)
        {
            if (radice == null)
                return;
            string indentazione = new string(' ', livello * 4);
            Console.WriteLine(indentazione + radice.Value);

            foreach (var figlio in radice.Nodes)
            {
                StampaAlbero(figlio, livello + 1);
            }

        }

        public static TreeNode<T> CercaNodo(TreeNode<T> nodo, T valore)
        {
            TreeNode<T> nodoTrovato = null;
            if (EqualityComparer<T>.Default.Equals(nodo.Value, valore))
                return nodo;
            foreach (var n in nodo.Nodes)
            {
                nodoTrovato = CercaNodo(n, valore);
                if (nodoTrovato != null)
                    return nodoTrovato;
            }
            return null;
        }


        public static TreeNode<T> AlberoDaFile()
        {
            TreeNode<T> radice = null;
            string[] righe = File.ReadAllLines("AlberoFile.txt");
            foreach (var r in righe)
            {
                string[] parti = r.Split(' ');
                string figlio = parti[0];
                string padre = parti[1];

                if (padre == figlio)
                {
                    radice = new TreeNode<T>((T)Convert.ChangeType(padre, typeof(T)));
                    continue;
                }

                TreeNode<T> nodoPadre = CercaNodo(radice, (T)Convert.ChangeType(padre, typeof(T)));
                if (nodoPadre != null)
                {
                    nodoPadre.Nodes.Add(new TreeNode<T>((T)Convert.ChangeType(figlio, typeof(T))));
                }


            }

            return radice;
        }


        public bool FileDaAlbero(TreeNode<T> root)
        {
            if (root == null) return false;
            foreach (var n in root.Nodes)
            {
                File.AppendAllText("AlberoFile2.txt", $"{n.Value} {root.Value}\n");
                FileDaAlbero(n);
            }
            return true;
        }

        
    }
}

