using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace AdvancedSoftwareEngineeringPart2
{
    public partial class MainForm : Form
    {
        public List<CommandParser> commandParsers = new List<CommandParser>();
        public List<List<string>> programCommandsList = new List<List<string>>();
        public Mutex mutex = new Mutex();
        public Graphics[] graphicsArray = new Graphics[2]; // Array to hold separate Graphics objects for each program

        public MainForm()
        {
            InitializeComponent();
            InitializeCommandParsers();
    
        }

        public void InitializeCommandParsers()
        {
            graphicsArray[0] = panel1.CreateGraphics(); // Graphics object for program 1
            graphicsArray[1] = panel1.CreateGraphics(); // Graphics object for program 2

            commandParsers.Add(new CommandParser(graphicsArray[0], panel1.Size)); // Use graphicsArray[0] for program 1
            commandParsers.Add(new CommandParser(graphicsArray[1], panel1.Size)); // Use graphicsArray[1] for program 2

            foreach (var parser in commandParsers)
            {
                programCommandsList.Add(new List<string>());
            }
        }

        public void SyntaxCheck(List<string> programCommands, CommandParser commandParser)
        {
            foreach (string command in programCommands)
            {
                commandParser.SyntaxCheck(command);
            }
        }

        public void UpdateProgramTextBox(List<string> programCommands, TextBox textBox)
        {
            if (textBox.InvokeRequired)
            {
                textBox.Invoke(new Action(() =>
                {
                    textBox.Text = string.Join(Environment.NewLine, programCommands);
                }));
            }
            else
            {
                textBox.Text = string.Join(Environment.NewLine, programCommands);
            }
        }

        public void button1_Click_1(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllLines(saveFileDialog.FileName, programCommandsList[0]);
            }
        }
        
        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {
            // Update program commands list associated with program 2
            programCommandsList[1] = new List<string>(textBox3.Lines);
        }

        public void button2_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                programCommandsList[0].Clear();
                programCommandsList[0].AddRange(File.ReadAllLines(openFileDialog.FileName));
                UpdateProgramTextBox(programCommandsList[0], ProgramTextBox);
                UpdateProgramTextBox(programCommandsList[0], textBox3);
            }
        }

        public void button3_Click_1(object sender, EventArgs e)
        {
            string command = CommandTextBox.Text;
            programCommandsList[0].Add(command);
            programCommandsList[1].Add(command);
            UpdateProgramTextBox(programCommandsList[0], ProgramTextBox);
            UpdateProgramTextBox(programCommandsList[1], textBox3);
            CommandTextBox.Clear();
        }

        public void button4_Click_1(object sender, EventArgs e)
        {
            try
            {
                SyntaxCheck(programCommandsList[0], commandParsers[0]);
                MessageBox.Show("Syntax check passed successfully for Program 1.", "Syntax Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SyntaxCheck(programCommandsList[1], commandParsers[1]);
                MessageBox.Show("Syntax check passed successfully for Program 2.", "Syntax Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Syntax check failed: {ex.Message}", "Syntax Check", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void RunButton_Click(object sender, EventArgs e)
        {
            try
            {
                Thread thread1 = new Thread(new ThreadStart(() => RunProgram(programCommandsList[0], commandParsers[0])));
                thread1.Start();

                Thread thread2 = new Thread(new ThreadStart(() => RunProgram(programCommandsList[1], commandParsers[1])));
                thread2.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void RunProgram(List<string> programCommands, CommandParser commandParser)
        {
            try
            {
                mutex.WaitOne();

                // Execute commands for program 1 using commands from ProgramTextBox
                commandParser.ExecuteProgram(programCommandsList[0]);
                Thread.Sleep(1000); // Add a delay for visualization

                // Draw a separator line between program 1 and program 2
                graphicsArray[commandParsers.IndexOf(commandParser)].DrawLine(Pens.Black, 0, panel1.Height / 2, panel1.Width, panel1.Height / 2);

                // Clear panel1 after program 1 execution
                graphicsArray[commandParsers.IndexOf(commandParser)].Clear(Color.White);

                // Execute commands for program 2 using commands from textBox3
                commandParser.ExecuteProgram(programCommandsList[1]);

                mutex.ReleaseMutex();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                panel1.BackColor = colorDialog.Color;
            }
        }

     
        private void button6_Click_1(object sender, EventArgs e)
        {
            // Change the text color of ProgramTextBox
            ChangeTextColor(ProgramTextBox);

            // Change the text color of textBox3
            ChangeTextColor(textBox3);
        }

        private void ChangeTextColor(TextBox textBox)
        {
            // Show color dialog to let the user select a new text color
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                // Set the selected color as the text color of the TextBox
                textBox.ForeColor = colorDialog.Color;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ChangeLineWidth(2.0f);
        }
        private void ChangeLineWidth(float width)
        {
            // Update the line width for both command parsers
            foreach (CommandParser parser in commandParsers)
            {
                parser.pen.Width = width;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // Reset the line width to default
            ChangeLineWidth(1.5f);
        }
    }


    public class CommandParser
    {
        public Graphics graphics;
        public Pen pen;
        public Size canvasSize;
        public bool fillEnabled = false;

        public CommandParser(Graphics graphics, Size canvasSize)
        {
            this.graphics = graphics;
            this.canvasSize = canvasSize;
            pen = new Pen(Color.Black); // Default pen color
        }

        public void ExecuteProgram(List<string> commands)
        {
            foreach (string command in commands)
            {
                ExecuteCommand(command);
            }
        }

        public void ExecuteCommand(string command)
        {
            string[] parts = command.Split(' ');
            string cmd = parts[0].ToLower();

            switch (cmd)
            {
                case "position":
                    if (parts.Length < 3)
                        throw new ArgumentException("Invalid number of parameters for position command");
                    int x = int.Parse(parts[1]);
                    int y = int.Parse(parts[2]);
                    graphics.ResetTransform();
                    graphics.TranslateTransform(x, y);
                    break;

                case "pen":
                    if (parts.Length < 2)
                        throw new ArgumentException("Invalid number of parameters for pen command");
                    SetPenColor(parts[1]);
                    break;

                case "draw":
                    if (parts.Length < 3)
                        throw new ArgumentException("Invalid number of parameters for draw command");
                    int endX = int.Parse(parts[1]);
                    int endY = int.Parse(parts[2]);
                    if (fillEnabled)
                        graphics.FillRectangle(pen.Brush, endX, endY, 1, 1);
                    else
                        graphics.DrawLine(pen, 0, 0, endX, endY);
                    break;

                case "clear":
                    graphics.Clear(Color.White);
                    break;

                case "reset":
                    graphics.ResetTransform();
                    break;

                case "rectangle":
                    if (parts.Length < 3)
                        throw new ArgumentException("Invalid number of parameters for rectangle command");
                    int width = int.Parse(parts[1]);
                    int height = int.Parse(parts[2]);
                    if (fillEnabled)
                        graphics.FillRectangle(pen.Brush, 0, 0, width, height);
                    else
                        graphics.DrawRectangle(pen, 0, 0, width, height);
                    break;

                case "circle":
                    if (parts.Length < 2)
                        throw new ArgumentException("Invalid number of parameters for circle command");
                    int radius = int.Parse(parts[1]);
                    int diameter = radius * 2;
                    if (fillEnabled)
                        graphics.FillEllipse(pen.Brush, 0, 0, diameter, diameter);
                    else
                        graphics.DrawEllipse(pen, 0, 0, diameter, diameter);
                    break;

                case "triangle":
                    if (parts.Length < 7)
                        throw new ArgumentException("Invalid number of parameters for triangle command");
                    Point[] trianglePoints = new Point[3];
                    trianglePoints[0] = new Point(int.Parse(parts[1]), int.Parse(parts[2]));
                    trianglePoints[1] = new Point(int.Parse(parts[3]), int.Parse(parts[4]));
                    trianglePoints[2] = new Point(int.Parse(parts[5]), int.Parse(parts[6]));
                    if (fillEnabled)
                        graphics.FillPolygon(pen.Brush, trianglePoints);
                    else
                        graphics.DrawPolygon(pen, trianglePoints);
                    break;

                case "fill":
                    if (parts.Length < 2)
                        throw new ArgumentException("Invalid number of parameters for fill command");
                    if (parts[1].ToLower() == "on")
                        fillEnabled = true;
                    else if (parts[1].ToLower() == "off")
                        fillEnabled = false;
                    break;

                default:
                    throw new ArgumentException($"Invalid command: {cmd}");
            }
        }

        public void SyntaxCheck(string command)
        {
            // Check for valid commands
            if (!Regex.IsMatch(command, @"^(position|pen|draw|clear|reset|rectangle|circle|triangle|fill)\s+(\w+\s*)*$", RegexOptions.IgnoreCase))
                throw new ArgumentException($"Invalid command syntax: {command}");

            string[] parts = command.Split(' ');
            string cmd = parts[0].ToLower();

            // Check for valid parameters
            switch (cmd)
            {
                case "position":
                    if (parts.Length < 3 || !int.TryParse(parts[1], out _) || !int.TryParse(parts[2], out _))
                        throw new ArgumentException($"Invalid parameters for position command: {command}");
                    break;

                case "pen":
                    if (parts.Length < 2 || !IsValidColor(parts[1]))
                        throw new ArgumentException($"Invalid parameters for pen command: {command}");
                    break;

                case "draw":
                    if (parts.Length < 3 || !int.TryParse(parts[1], out _) || !int.TryParse(parts[2], out _))
                        throw new ArgumentException($"Invalid parameters for draw command: {command}");
                    break;

                case "rectangle":
                    if (parts.Length < 3 || !int.TryParse(parts[1], out _) || !int.TryParse(parts[2], out _))
                        throw new ArgumentException($"Invalid parameters for rectangle command: {command}");
                    break;

                case "circle":
                    if (parts.Length < 2 || !int.TryParse(parts[1], out _))
                        throw new ArgumentException($"Invalid parameters for circle command: {command}");
                    break;

                case "fill":
                    if (parts.Length < 2 || (parts[1].ToLower() != "on" && parts[1].ToLower() != "off"))
                        throw new ArgumentException($"Invalid parameters for fill command: {command}");
                    break;

                case "triangle":
                    if (parts.Length < 7 || !int.TryParse(parts[1], out _) || !int.TryParse(parts[2], out _)
                        || !int.TryParse(parts[3], out _) || !int.TryParse(parts[4], out _) || !int.TryParse(parts[5], out _)
                        || !int.TryParse(parts[6], out _))
                        throw new ArgumentException($"Invalid parameters for triangle command: {command}");
                    break;

                default:
                    break;
            }
        }

        public void SetPenColor(string color)
        {
            switch (color.ToLower())
            {
                case "red":
                    pen.Color = Color.Red;
                    break;
                case "green":
                    pen.Color = Color.Green;
                    break;
                case "blue":
                    pen.Color = Color.Blue;
                    break;
                case "black":
                    pen.Color = Color.Black;
                    break;
                default:
                    throw new ArgumentException($"Invalid pen color: {color}");
            }
        }

        public bool IsValidColor(string color)
        {
            switch (color.ToLower())
            {
                case "red":
                case "green":
                case "blue":
                case "black":
                    return true;
                default:
                    return false;
            }
        }
    }
}
