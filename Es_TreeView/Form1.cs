using TreeLibrary;
namespace Es_TreeView
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static void TreeViewFile1(FileAlbero.TreeNode<string> nodoLibreria, System.Windows.Forms.TreeNodeCollection collezione)
        {
            if (nodoLibreria == null) return;

            var nuovoNodo = new System.Windows.Forms.TreeNode(nodoLibreria.Value);

            collezione.Add(nuovoNodo);

            foreach (var figlioLib in nodoLibreria.Nodes)
            {
                TreeViewFile1(figlioLib, nuovoNodo.Nodes);
            }
        }

        public static void TreeViewFile2(string percorso, TreeView treeView)
        {
            string[] righe = File.ReadAllLines(percorso);
            treeView.Nodes.Clear();
            List<TreeNode> livelli = new();
            foreach (string riga in righe)
            {
                int livello = 0;
                while (livello < riga.Length && riga[livello] == '-')
                {
                    livello++;
                }
                string valore = riga.Substring(livello).Trim();
                var nuovoNodo = new TreeNode(valore);
                if (livello == 0)
                {
                    treeView.Nodes.Add(nuovoNodo);
                }
                else
                {
                    var padre = livelli[livello - 1];
                    padre.Nodes.Add(nuovoNodo);
                }
                if (livello < livelli.Count)
                {
                    livelli[livello] = nuovoNodo;
                }
                else
                {
                    livelli.Add(nuovoNodo);
                }
            }
            treeView.ExpandAll();
        }

        private void btnCaricaFile1_Click(object sender, EventArgs e)
        {
            FileAlbero.TreeNode<string> radice = FileAlbero.TreeNode<string>.AlberoDaFile();

            treeView1.Nodes.Clear();

            TreeViewFile1(radice, treeView1.Nodes);

            treeView1.ExpandAll();
            lblCaricamento.Text = "Caricamento con successo";

        }

        private void btnCaricaFile2_Click(object sender, EventArgs e)
        {
            TreeViewFile2("AlberoFile2.txt", treeView1);

        }

        private void btnPulisciAlbero_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            lblCaricamento.Text = "";
        }
    }
}
