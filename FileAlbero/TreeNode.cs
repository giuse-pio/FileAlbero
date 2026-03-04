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

        public static void GeneraMatriceGrafo(string[] nodi)
        {
            float infinito = float.PositiveInfinity;
            int n = nodi.Length;
            float[,] matrice = new float[n, n];

            Dictionary<string, int> mappaNodi = new Dictionary<string, int>();
            for (int i = 0; i < n; i++)
            {
                mappaNodi[nodi[i]] = i;
                for (int j = 0; j < n; j++)
                {
                    if (i == j)
                    {
                        matrice[i, j] = 0;
                    }
                    else
                    {
                        matrice[i, j] = infinito;
                    }
                }
            }
            if (!File.Exists("AlberoFile3.txt"))
            {
                return;
            }
            string[] righe = File.ReadAllLines("AlberoFile3.txt");

            foreach (var r in righe)
            {
                string[] parti = r.Split(' ');

                if (parti.Length < 3)
                    continue;

                string partenza = parti[0];
                string arrivo = parti[1];
                string peso = parti[2];

                if (mappaNodi.ContainsKey(partenza) && mappaNodi.ContainsKey(arrivo))
                {
                    int riga = mappaNodi[partenza];
                    int colonna = mappaNodi[arrivo];
                    matrice[riga, colonna] = float.Parse(peso);
                }
            }
            Console.WriteLine("\nMatrice:");
            Console.WriteLine("       A     B     C");
            for (int i = 0; i < n; i++)
            {
                Console.Write(nodi[i] + " ");
                for (int j = 0; j < n; j++)
                {
                    string v;
                    if (double.IsPositiveInfinity(matrice[i, j]))
                    {
                        v = "∞";
                    }
                    else
                    {
                        v = matrice[i, j].ToString();
                    }
                    Console.Write(v.PadLeft(6));
                }
                Console.WriteLine();

            }
        }






    }
}

