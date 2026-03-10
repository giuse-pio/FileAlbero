using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileAlbero
{
    public class TreeNode<T>
    {
        public T Value { get; set; }
        public List<TreeNode<T>> Nodes { get; set; } = new List<TreeNode<T>>();

        public TreeNode(T value) { Value = value; }


      
        public void StampaAlbero(TreeNode<T> node, int livello)
        {
            Console.WriteLine(new string(' ', livello) + node.Value);
            foreach (var n in node.Nodes) StampaAlbero(n, livello + 1);
        }

        public void StampaAlbero2(TreeNode<T> node, int livello)
        {
            Console.WriteLine(new string('-', livello) + node.Value);
            foreach (var n in node.Nodes) StampaAlbero2(n, livello + 1);
        }

        public static TreeNode<T> CercaNodo(TreeNode<T> node, T value)
        {
            if (node == null) return null;
            if (EqualityComparer<T>.Default.Equals(node.Value, value)) return node;
            foreach (var n in node.Nodes)
            {
                var t = CercaNodo(n, value);
                if (t != null) return t;
            }
            return null;
        }

        public static TreeNode<T> AlberoDaFile()
        {
            TreeNode<T> root = null;
            foreach (var r in File.ReadAllLines("AlberoFile.txt"))
            {
                string[] p = r.Split(' ');
                if (p[1] == p[0]) root = new TreeNode<T>((T)Convert.ChangeType(p[1].Trim(), typeof(T)));
                else if (root != null)
                {
                    TreeNode<T> padre = CercaNodo(root, (T)Convert.ChangeType(p[1].Trim(), typeof(T)));
                    if (padre != null) padre.Nodes.Add(new TreeNode<T>((T)Convert.ChangeType(p[0].Trim(), typeof(T))));
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
            TreeNode<T> root = new TreeNode<T>((T)Convert.ChangeType(righe[0].Trim(), typeof(T)));
            List<TreeNode<T>> ultimi = new List<TreeNode<T>> { root };
            foreach (var r in righe)
            {
                int liv = r.TakeWhile(c => c == '-').Count();
                TreeNode<T> nodo = new TreeNode<T>((T)Convert.ChangeType(r.Substring(liv).Trim(), typeof(T)));
                if (liv > 0 && liv <= ultimi.Count)
                {
                    ultimi[liv - 1].Nodes.Add(nodo);
                    if (liv < ultimi.Count) ultimi[liv] = nodo; else ultimi.Add(nodo);
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
            righe.Add(new string('-', livello) + nodo.Value.ToString());
            foreach (var n in nodo.Nodes) CreaLivelli(n, livello + 1, righe);
        }
  public static void GeneraMatriceGrafo(string[] nodi)
        {
            float infinito = float.PositiveInfinity;
            int n = nodi.Length;
            float[,] matrice = new float[n, n];
            Dictionary<string, int> mappaNodi = new Dictionary<string, int>();
            for (int r = 0; r < n; r++)
            {
                mappaNodi[nodi[r]] = r;
                for (int c = 0; c < n; c++) matrice[r, c] = (r == c) ? 0 : infinito;
            }
            if (!File.Exists("AlberoFile3.txt")) return;
            foreach (var riga in File.ReadAllLines("AlberoFile3.txt"))
            {
                string[] p = riga.Split(' ');
                if (p.Length < 3) continue;
                if (mappaNodi.ContainsKey(p[0]) && mappaNodi.ContainsKey(p[1]))
                    matrice[mappaNodi[p[0]], mappaNodi[p[1]]] = float.Parse(p[2]);
            }
            Console.WriteLine("\nMatrice:");
            for (int r = 0; r < n; r++)
            {
                Console.Write(nodi[r] + " ");
                for (int c = 0; c < n; c++)
                    Console.Write((double.IsPositiveInfinity(matrice[r, c]) ? "∞" : matrice[r, c].ToString()).PadLeft(6));
                Console.WriteLine();
            }
        }

        public static TreeNode<T> GrafoDaFile()
        {
            if (!File.Exists("grafofile.txt"))
                return null;

            string[] righe = File.ReadAllLines("grafofile.txt");
            List<TreeNode<T>> listaNodi = new List<TreeNode<T>>();

            foreach (string r in righe)
            {
                string[] parti = r.Split(' ');
                if (parti.Length < 2)
                    continue;

                string nomeFiglio = parti[0].Trim();
                string nomePadre = parti[1].Trim();

                TreeNode<T> figlio = null;
                foreach (TreeNode<T> nodo in listaNodi)
                {
                    if (nodo.Value.ToString() == nomeFiglio)
                    {
                        figlio = nodo;
                        break;
                    }
                }

                if (figlio == null)
                {
                    figlio = new TreeNode<T>((T)Convert.ChangeType(nomeFiglio, typeof(T)));
                    listaNodi.Add(figlio);
                }

                TreeNode<T> padre = null;
                foreach (TreeNode<T> nodo in listaNodi)
                {
                    if (nodo.Value.ToString() == nomePadre)
                    {
                        padre = nodo;
                        break;
                    }
                }

                if (padre == null)
                {
                    padre = new TreeNode<T>((T)Convert.ChangeType(nomePadre, typeof(T)));
                    listaNodi.Add(padre);
                }

                if (!padre.Nodes.Contains(figlio))
                {
                    padre.Nodes.Add(figlio);
                }
            }

            foreach (TreeNode<T> nodo in listaNodi)
            {
                if (nodo.Value.ToString() == "Casa")
                    return nodo;
            }

            return null;
        }


        // BFS che si ferma se trova uno dei posti dove mangiare
        public void BFS_Mangiare(List<string> postiDoveMangiare)
        {
            var visited = new HashSet<TreeNode<T>>();
            var queue = new Queue<TreeNode<T>>();
            queue.Enqueue(this);
            visited.Add(this);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                Console.WriteLine("Visitato: " + current.Value);

                if (postiDoveMangiare.Contains(current.Value.ToString()))
                {
                    Console.WriteLine("trovato, puoi mangiare in " + current.Value);
                    return;
                }

                foreach (var neighbor in current.Nodes)
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        queue.Enqueue(neighbor);
                    }
                }
            }
        }

        // DFS che si ferma se trova uno dei posti dove mangiare
        public bool DFS_Mangiare(List<string> postiDoveMangiare, HashSet<TreeNode<T>> visited)
        {
            if (visited.Contains(this))
            {
                return false;
            }
            Console.WriteLine("Visitato: " + this.Value);
            visited.Add(this);

            if (postiDoveMangiare.Contains(this.Value.ToString()))
            {
                Console.WriteLine("trovato, puoi mangiare in " + this.Value);
                return true;
            }

            foreach (var neighbor in Nodes)
            {
                if (neighbor.DFS_Mangiare(postiDoveMangiare, visited))
                {
                    return true;
                }
            }
            return false;
        }
    }
}