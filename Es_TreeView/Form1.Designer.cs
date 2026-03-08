namespace Es_TreeView
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblCaricamento = new Label();
            treeView1 = new TreeView();
            btnPulisciAlbero = new Button();
            btnCaricaFile2 = new Button();
            btnCaricaFile1 = new Button();
            SuspendLayout();
            // 
            // lblCaricamento
            // 
            lblCaricamento.AutoSize = true;
            lblCaricamento.Location = new Point(376, 370);
            lblCaricamento.Name = "lblCaricamento";
            lblCaricamento.Size = new Size(0, 20);
            lblCaricamento.TabIndex = 11;
            // 
            // treeView1
            // 
            treeView1.Location = new Point(12, -6);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(294, 462);
            treeView1.TabIndex = 9;
            // 
            // btnPulisciAlbero
            // 
            btnPulisciAlbero.Location = new Point(353, 291);
            btnPulisciAlbero.Name = "btnPulisciAlbero";
            btnPulisciAlbero.Size = new Size(107, 29);
            btnPulisciAlbero.TabIndex = 8;
            btnPulisciAlbero.Text = "Pulisci Albero";
            btnPulisciAlbero.UseVisualStyleBackColor = true;
            btnPulisciAlbero.Click += btnPulisciAlbero_Click;
            // 
            // btnCaricaFile2
            // 
            btnCaricaFile2.Location = new Point(353, 191);
            btnCaricaFile2.Name = "btnCaricaFile2";
            btnCaricaFile2.Size = new Size(107, 29);
            btnCaricaFile2.TabIndex = 7;
            btnCaricaFile2.Text = "Carica File 2";
            btnCaricaFile2.UseVisualStyleBackColor = true;
            btnCaricaFile2.Click += btnCaricaFile2_Click;
            // 
            // btnCaricaFile1
            // 
            btnCaricaFile1.Location = new Point(353, 90);
            btnCaricaFile1.Name = "btnCaricaFile1";
            btnCaricaFile1.Size = new Size(107, 29);
            btnCaricaFile1.TabIndex = 6;
            btnCaricaFile1.Text = "Carica File 1";
            btnCaricaFile1.UseVisualStyleBackColor = true;
            btnCaricaFile1.Click += btnCaricaFile1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblCaricamento);
            Controls.Add(treeView1);
            Controls.Add(btnPulisciAlbero);
            Controls.Add(btnCaricaFile2);
            Controls.Add(btnCaricaFile1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblCaricamento;
        private TreeView treeView1;
        private Button btnPulisciAlbero;
        private Button btnCaricaFile2;
        private Button btnCaricaFile1;
    }
}
