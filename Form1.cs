using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GraphicalProgrammingLanguage
{
    public partial class MainForm : Form
    {
        private List<string> programCommands = new List<string>();
        private Graphics graphics;
        private Pen pen = new Pen(Color.Black);
        private PointF currentPosition = PointF.Empty;
        private bool fillShape = false;

        public MainForm()
        {
            InitializeComponent();
            graphics = drawingPanel.CreateGraphics();
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            ExecuteProgram();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveProgramToFile();
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            LoadProgramFromFile();
        }

        private void ExecuteProgram()
        {
            ClearDrawingArea();
            currentPosition = PointF.Empty;

            foreach (string cmd in programCommands)
            {
                try
                {
                    ParseAndExecuteCommand(cmd);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ParseAndExecuteCommand(string command)
        {
            string[] parts = command.Split(' ');

            string keyword = parts[0].ToLower();
            switch (keyword)
            {
                case "position":
                    MoveTo(parts);
                    break;
                case "pen":
                    SetPenColor(parts);
                    break;
                case "draw":
                    DrawTo(parts);
                    break;
                case "rectangle":
                    DrawRectangle(parts);
                    break;
                case "circle":
                    DrawCircle(parts);
                    break;
                case "triangle":
                    DrawTriangle();
                    break;
                case "clear":
                    ClearDrawingArea();
                    break;
                case "reset":
                    ResetPenPosition();
                    break;
                case "fill":
                    SetFillMode(parts);
                    break;
                default:
                    throw new ArgumentException($"Invalid command: {command}");
            }
        }

        private void MoveTo(string[] parts)
        {
            if (parts.Length != 3)
                throw new ArgumentException("Invalid position command");

            float x = float.Parse(parts[1]);
            float y = float.Parse(parts[2]);

            currentPosition = new PointF(x, y);
        }

        private void SetPenColor(string[] parts)
        {
            if (parts.Length != 2)
                throw new ArgumentException("Invalid pen command");

            string colorName = parts[1];
            pen.Color = Color.FromName(colorName);
        }

        private void DrawTo(string[] parts)
        {
            // Implement drawTo functionality (not shown for simplicity)
        }

        private void DrawRectangle(string[] parts)
        {
            if (parts.Length != 3)
                throw new ArgumentException("Invalid rectangle command");

            int width = int.Parse(parts[1]);
            int height = int.Parse(parts[2]);

            if (fillShape)
                graphics.FillRectangle(new SolidBrush(pen.Color), currentPosition.X, currentPosition.Y, width, height);
            else
                graphics.DrawRectangle(pen, currentPosition.X, currentPosition.Y, width, height);
        }

        private void DrawCircle(string[] parts)
        {
            if (parts.Length != 2)
                throw new ArgumentException("Invalid circle command");

            int radius = int.Parse(parts[1]);

            if (fillShape)
                graphics.FillEllipse(new SolidBrush(pen.Color), currentPosition.X - radius, currentPosition.Y - radius, radius * 2, radius * 2);
            else
                graphics.DrawEllipse(pen, currentPosition.X - radius, currentPosition.Y - radius, radius * 2, radius * 2);
        }

        private void DrawTriangle()
        {
            // Implement draw triangle functionality
            PointF[] points = new PointF[]
            {
                new PointF(currentPosition.X, currentPosition.Y),
                new PointF(currentPosition.X + 50, currentPosition.Y + 100),
                new PointF(currentPosition.X - 50, currentPosition.Y + 100)
            };

            if (fillShape)
                graphics.FillPolygon(new SolidBrush(pen.Color), points);
            else
                graphics.DrawPolygon(pen, points);
        }

        private void ClearDrawingArea()
        {
            graphics.Clear(Color.White);
        }

        private void ResetPenPosition()
        {
            currentPosition = PointF.Empty;
        }

        private void SetFillMode(string[] parts)
        {
            if (parts.Length != 2)
                throw new ArgumentException("Invalid fill command");

            fillShape = parts[1].ToLower() == "on";
        }

        private void SaveProgramToFile()
        {
            using (StreamWriter writer = new StreamWriter("program.txt"))
            {
                foreach (string cmd in programCommands)
                {
                    writer.WriteLine(cmd);
                }
            }

            MessageBox.Show("Program saved to program.txt", "Save Program", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LoadProgramFromFile()
        {
            programCommands.Clear();
            using (StreamReader reader = new StreamReader("program.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    programCommands.Add(line);
                }
            }

            MessageBox.Show("Program loaded from program.txt", "Load Program", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Button click event for adding commands to the program
        private void addButton_Click(object sender, EventArgs e)
        {
            string command = commandTextBox.Text;
            programCommands.Add(command);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
