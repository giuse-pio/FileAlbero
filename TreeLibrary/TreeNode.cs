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

        // --- ALGORITMI DI RICERCA (Sistemati per non esplodere) ---

        // BFS che si ferma se trova uno dei posti dove mangiare
        public void BFS_Mangiare(List<string> targetValues)
        {
            var visited = new HashSet<TreeNode<T>>();
            var queue = new Queue<TreeNode<T>>();
            queue.Enqueue(this);
            visited.Add(this);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                Console.WriteLine("Visitato: " + current.Value);

                // Controllo se il valore del nodo corrente è nella lista dei posti dove si mangia
                if (targetValues.Contains(current.Value.ToString()))
                {
                    Console.WriteLine(">>> TROVATO! Puoi mangiare da: " + current.Value);
                    return;
                }

                foreach (var neighbor in Nodes)
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
        public bool DFS_Mangiare(List<string> targetValues, HashSet<TreeNode<T>> visited)
        {
            if (visited.Contains(this)) return false;

            Console.WriteLine("Visitato: " + this.Value);
            visited.Add(this);

            if (targetValues.Contains(this.Value.ToString()))
            {
                Console.WriteLine(">>> TROVATO! Puoi mangiare da: " + this.Value);
                return true;
            }

            foreach (var neighbor in Nodes)
            {
                if (neighbor.DFS_Mangiare(targetValues, visited)) return true;
            }
            return false;
        }
        // --- METODI ORIGINALI ---

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

        public static TreeNode<T> GrafoDaFile()
        {
            if (!File.Exists("grafofile.txt")) return null;

            var dizionarioNodi = new Dictionary<string, TreeNode<T>>();

            foreach (var r in File.ReadAllLines("grafofile.txt"))
            {
                string[] p = r.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (p.Length < 2) continue;

                string figlioVal = p[0].Trim();
                string padreVal = p[1].Trim();

                // 1. Assicurati che esistano nel dizionario (creali se mancano)
                if (!dizionarioNodi.ContainsKey(figlioVal))
                    dizionarioNodi[figlioVal] = new TreeNode<T>((T)Convert.ChangeType(figlioVal, typeof(T)));

                if (!dizionarioNodi.ContainsKey(padreVal))
                    dizionarioNodi[padreVal] = new TreeNode<T>((T)Convert.ChangeType(padreVal, typeof(T)));

                // 2. Collega l'OGGETTO che sta nel dizionario
                var padreObj = dizionarioNodi[padreVal];
                var figlioObj = dizionarioNodi[figlioVal];

                // EVITA DUPLICATI: controlla se è già collegato
                if (!padreObj.Nodes.Contains(figlioObj))
                {
                    padreObj.Nodes.Add(figlioObj);
                }
            }

            return dizionarioNodi.ContainsKey("Casa") ? dizionarioNodi["Casa"] : null;
        }
    }
}