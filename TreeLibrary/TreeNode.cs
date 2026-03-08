using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileAlbero
{
    public class TreeNode<T>
    {

        public static void GeneraMatriceGrafo(string[] nodi)
        {
            float infinito = float.PositiveInfinity;
            int n = nodi.Length;
            float[,] matrice = new float[n, n];

            Dictionary<string, int> mappaNodi = new Dictionary<string, int>();
            for (int riga = 0; riga < n; riga++)
            {
                mappaNodi[nodi[riga]] = riga;
                for (int colonna = 0; colonna < n; colonna++)
                {
                    if (riga == colonna)
                    {
                        matrice[riga, colonna] = 0;
                    }
                    else
                    {
                        matrice[riga, colonna] = infinito;
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
            for (int riga = 0; riga < n; riga++)
            {
                Console.Write(nodi[riga] + " ");
                for (int colonna = 0; colonna < n; colonna++)
                {
                    string v;
                    if (double.IsPositiveInfinity(matrice[riga, colonna]))
                    {
                        v = "∞";
                    }
                    else
                    {
                        v = matrice[riga, colonna].ToString();
                    }
                    Console.Write(v.PadLeft(6));
                }
                Console.WriteLine();

            }
        }




        
        public T Value { get; set; }
        public List<TreeNode<T>> Nodes { get; set; } = new List<TreeNode<T>>();
        public TreeNode(T value)
        {
            Value = value;
        }
        public void StampaAlbero(TreeNode<T> node, int livello)
        {

            Console.WriteLine(new string(' ', livello) + node.Value);

            foreach (var nodes in node.Nodes)
            {
                StampaAlbero(nodes, livello + 1);
            }
        }

        public void StampaAlbero2(TreeNode<T> node, int livello)
        {

            Console.WriteLine(new string('-', livello) + node.Value);

            foreach (var nodes in node.Nodes)
            {
                StampaAlbero2(nodes, livello + 1);
            }
        }
        public static TreeNode<T> CercaNodo(TreeNode<T> node, T value)
        {
            if (EqualityComparer<T>.Default.Equals(node.Value, value))
            {
                return node;
            }
            foreach (var n in node.Nodes)
            {
                var nodoTrovato = CercaNodo(n, value);
                if (nodoTrovato != null) return nodoTrovato;
            }
            return null;
        }

        public static TreeNode<T> AlberoDaFile()
        {
            TreeNode<T> root = null;
            string[] righe = File.ReadAllLines("AlberoFile.txt");
            foreach (var r in righe)
            {
                string[] parti = r.Split(' ');
                string child = parti[0].Trim();
                string father = parti[1].Trim();

                if (father == child)
                {
                    root = new TreeNode<T>((T)Convert.ChangeType(father, typeof(T)));
                    continue;
                }
                if (root != null)
                {
                    TreeNode<T> nodoPadre = CercaNodo(root, (T)Convert.ChangeType(father, typeof(T)));
                    if (nodoPadre != null)
                    {
                        nodoPadre.Nodes.Add(new TreeNode<T>((T)Convert.ChangeType(child, typeof(T))));
                    }
                }
            }
            return root;
        }

        public bool FileDaAlbero(TreeNode<T> root)
        {
            if (root == null) return false;
            foreach (var n in root.Nodes)
            {
                File.AppendAllText("FileAlbero.txt", $"{n.Value} {root.Value}\n");
                FileDaAlbero(n);
            }
            return true;
        }

        public static TreeNode<T> AlberoDaFile2()
        {
            string[] righe = File.ReadAllLines("AlberoFile2.txt");
            string valoreRadice = righe[0].Trim();
            TreeNode<T> root = new TreeNode<T>((T)Convert.ChangeType(valoreRadice, typeof(T)));
            List<TreeNode<T>> ultimiNodiPerLivello = new List<TreeNode<T>>();
            ultimiNodiPerLivello.Add(root);
            foreach (var r in righe)
            {
                int livello = 0;
                while (livello < r.Length && r[livello] == '-')
                {
                    livello++;
                }
                string valore = r.Substring(livello).Trim();
                TreeNode<T> nodo = new TreeNode<T>((T)Convert.ChangeType(valore, typeof(T)));
                if (livello > 0 && livello <= ultimiNodiPerLivello.Count)
                {
                    TreeNode<T> padre = ultimiNodiPerLivello[livello - 1];
                    padre.Nodes.Add(nodo);
                    if (livello < ultimiNodiPerLivello.Count)
                    {
                        ultimiNodiPerLivello[livello] = nodo;
                    }
                    else
                    {
                        ultimiNodiPerLivello.Add(nodo);
                    }
                }
            }
            return root;
        }

        public bool FileDaAlbero2(TreeNode<T> root)
        {
            if (root == null) return false;
            List<string> righe = new List<string>();
            CreaLivelli(root, 0, righe);
            File.WriteAllLines("FileAlbero2.txt", righe);
            return true;
        }
        public void CreaLivelli(TreeNode<T> nodo, int livello, List<string> righe)
        {
            if (nodo == null) return;
            string trattini = new string('-', livello);
            righe.Add(trattini + nodo.Value.ToString());
            foreach (var n in nodo.Nodes)
            {
                CreaLivelli(n, livello + 1, righe);
            }
        }

    }
}

