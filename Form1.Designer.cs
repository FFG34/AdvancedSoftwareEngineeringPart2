namespace GraphicalProgrammingLanguage
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Panel drawingPanel1;
            this.addButton = new System.Windows.Forms.Button();
            this.commandTextBox = new System.Windows.Forms.TextBox();
            this.runButton = new System.Windows.Forms.Button();
            drawingPanel1 = new System.Windows.Forms.Panel();
            drawingPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // drawingPanel1
            // 
            drawingPanel1.Controls.Add(this.runButton);
            drawingPanel1.Controls.Add(this.commandTextBox);
            drawingPanel1.Controls.Add(this.addButton);
            drawingPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            drawingPanel1.Location = new System.Drawing.Point(0, 0);
            drawingPanel1.Name = "drawingPanel1";
            drawingPanel1.Size = new System.Drawing.Size(856, 414);
            drawingPanel1.TabIndex = 3;
            drawingPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(320, 10);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(137, 22);
            this.addButton.TabIndex = 2;
            this.addButton.Text = "Add Command";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.button3_Click);
            // 
            // commandTextBox
            // 
            this.commandTextBox.Location = new System.Drawing.Point(10, 10);
            this.commandTextBox.Name = "commandTextBox";
            this.commandTextBox.Size = new System.Drawing.Size(300, 22);
            this.commandTextBox.TabIndex = 3;
            this.commandTextBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(127, 142);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(75, 23);
            this.runButton.TabIndex = 4;
            this.runButton.Text = "Run";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(856, 414);
            this.Controls.Add(drawingPanel1);
            this.Name = "MainForm";
            drawingPanel1.ResumeLayout(false);
            drawingPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel drawingPanel;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.TextBox commandTextBox;
        private System.Windows.Forms.Button runButton;

    }
}
