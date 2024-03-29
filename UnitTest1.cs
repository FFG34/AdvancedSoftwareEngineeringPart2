using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using AdvancedSoftwareEngineeringPart2;

namespace TestProjectpart2
{
    public class Tests
    {
        MainForm mainForm;

        [SetUp]
        public void Setup()
        {
            // Initialize MainForm
            mainForm = new MainForm();
        }

        [Test]
        public void TestSyntaxCheck()
        {
            // Arrange
            CommandParser commandParser = new CommandParser(Graphics.FromImage(new Bitmap(1, 1)), new Size(100, 100));
            List<string> commands = new List<string> { "position 10 10", "pen red", "draw 50 50", "circle 20", "fill on" };

            // Act & Assert
            Assert.DoesNotThrow(() => mainForm.SyntaxCheck(commands, commandParser));
        }

        [Test]
        public void TestUpdateProgramTextBox()
        {
            // Arrange
            TextBox textBox = new TextBox();
            List<string> commands = new List<string> { "position 10 10", "pen red", "draw 50 50", "circle 20", "fill on" };

            // Act
            mainForm.UpdateProgramTextBox(commands, textBox);

            // Assert
            Assert.AreEqual(string.Join(Environment.NewLine, commands), textBox.Text);
        }

        [Test]
        public void TestSetPenColor()
        {
            // Arrange
            CommandParser commandParser = new CommandParser(Graphics.FromImage(new Bitmap(1, 1)), new Size(100, 100));

            // Act & Assert
            Assert.DoesNotThrow(() => commandParser.SetPenColor("red"));
        }

        [Test]
        public void TestIsValidColor()
        {
            // Arrange
            CommandParser commandParser = new CommandParser(Graphics.FromImage(new Bitmap(1, 1)), new Size(100, 100));

            // Act & Assert
            Assert.IsTrue(commandParser.IsValidColor("red"));
            Assert.IsTrue(commandParser.IsValidColor("green"));
            Assert.IsTrue(commandParser.IsValidColor("blue"));
            Assert.IsTrue(commandParser.IsValidColor("black"));
            Assert.IsFalse(commandParser.IsValidColor("yellow"));
        }

        [Test]
        public void TestExecuteCommand_Position()
        {
            // Arrange
            CommandParser commandParser = new CommandParser(Graphics.FromImage(new Bitmap(1, 1)), new Size(100, 100));
            string command = "position 10 20";

            // Act & Assert
            Assert.DoesNotThrow(() => commandParser.ExecuteCommand(command));
        }

        [Test]
        public void TestExecuteCommand_Draw()
        {
            // Arrange
            CommandParser commandParser = new CommandParser(Graphics.FromImage(new Bitmap(1, 1)), new Size(100, 100));
            string command = "draw 50 50";

            // Act & Assert
            Assert.DoesNotThrow(() => commandParser.ExecuteCommand(command));
        }

        [Test]
        public void TestExecuteCommand_Clear()
        {
            // Arrange
            CommandParser commandParser = new CommandParser(Graphics.FromImage(new Bitmap(1, 1)), new Size(100, 100));
            string command = "clear";

            // Act & Assert
            Assert.DoesNotThrow(() => commandParser.ExecuteCommand(command));
        }

        [Test]
        public void TestExecuteProgram()
        {
            // Arrange
            CommandParser commandParser = new CommandParser(Graphics.FromImage(new Bitmap(1, 1)), new Size(100, 100));
            List<string> commands = new List<string> { "position 10 10", "pen red", "draw 50 50", "circle 20", "fill on" };

            // Act & Assert
            Assert.DoesNotThrow(() => commandParser.ExecuteProgram(commands));
        }

        [Test]
        public void TestRunProgram()
        {
            // Arrange
            List<string> commands = new List<string> { "position 10 10", "pen red", "draw 50 50", "circle 20", "fill on" };
            CommandParser commandParser = new CommandParser(Graphics.FromImage(new Bitmap(1, 1)), new Size(100, 100));

            // Act & Assert
            Assert.DoesNotThrow(() => mainForm.RunProgram(commands, commandParser));
        }

        [Test]
        public void TestRunButton_Click()
        {
            // Arrange
            Button button = new Button();
            List<string> commands1 = new List<string> { "position 10 10", "pen red", "draw 50 50", "circle 20", "fill on" };
            List<string> commands2 = new List<string> { "position 50 50", "pen blue", "draw 20 20", "rectangle 30 30", "fill off" };

            // Act & Assert
            Assert.DoesNotThrow(() =>
            {
                mainForm.RunButton_Click(button, System.EventArgs.Empty);
                Thread.Sleep(2000); // Allow time for execution
            });
        }

        [Test]
        public void TestButton1_Click_1()
        {
            // Arrange
            Button button = new Button();

            // Act & Assert
            Assert.DoesNotThrow(() => mainForm.button1_Click_1(button, System.EventArgs.Empty));
        }

        [Test]
        public void TestButton2_Click_1()
        {
            // Arrange
            Button button = new Button();

            // Act & Assert
            Assert.DoesNotThrow(() => mainForm.button2_Click_1(button, System.EventArgs.Empty));
        }

        [Test]
        public void TestButton3_Click_1()
        {
            // Arrange
            Button button = new Button();
            mainForm.CommandTextBox.Text = "position 10 10";

            // Act & Assert
            Assert.DoesNotThrow(() => mainForm.button3_Click_1(button, System.EventArgs.Empty));
        }

        [Test]
        public void TestButton4_Click_1()
        {
            // Arrange
            Button button = new Button();

            // Act & Assert
            Assert.DoesNotThrow(() => mainForm.button4_Click_1(button, System.EventArgs.Empty));
        }
        [Test]
        public void TestExpressions()
        {
            // Arrange
            CommandParser commandParser = new CommandParser(Graphics.FromImage(new Bitmap(1, 1)), new Size(100, 100));

            // Act
            int result = commandParser.EvaluateExpression("5 * 10");

            // Assert
            Assert.AreEqual(50, result);
        }

        [Test]
        public void TestLoopWithVariable()
        {
            // Arrange
            CommandParser commandParser = new CommandParser(Graphics.FromImage(new Bitmap(1, 1)), new Size(100, 100));
            List<string> commands = new List<string> { "pen red", "draw 10 10" };
            int count = 3;

            // Act & Assert
            Assert.DoesNotThrow(() =>
            {
                commandParser.ExecuteLoopWithVariable(commands, count);
            });
        }

        [Test]
        public void TestMethodsWithMultipleParameters()
        {
            // Arrange
            CommandParser commandParser = new CommandParser(Graphics.FromImage(new Bitmap(1, 1)), new Size(100, 100));
            int x = 10;
            int y = 20;
            int radius = 30;

            // Act & Assert
            Assert.DoesNotThrow(() =>
            {
                commandParser.DrawCircle(x, y, radius);
            });
        }

        [Test]
        public void TestSyntaxCheck()
        {
            // Arrange
            CommandParser commandParser = new CommandParser(Graphics.FromImage(new Bitmap(1, 1)), new Size(100, 100));
            List<string> commands = new List<string> { "position 10 10", "pen red", "draw 50 50", "circle 20", "fill on" };

            // Act & Assert
            Assert.DoesNotThrow(() => mainForm.SyntaxCheck(commands, commandParser));
        }

        [Test]
        public void TestIfStatement()
        {
            // Arrange
            CommandParser commandParser = new CommandParser(Graphics.FromImage(new Bitmap(1, 1)), new Size(100, 100));
            string condition = "5 > 3";
            List<string> ifBlock = new List<string> { "pen red", "draw 50 50" };
            List<string> elseBlock = new List<string> { "pen blue", "draw 20 20" };

            // Act & Assert
            Assert.DoesNotThrow(() =>
            {
                commandParser.ExecuteIfStatement(condition, ifBlock, elseBlock);
            });
        }

        [Test]
        public void TestIfStatementWithCodeBlock()
        {
            // Arrange
            CommandParser commandParser = new CommandParser(Graphics.FromImage(new Bitmap(1, 1)), new Size(100, 100));
            string condition = "5 > 3";
            List<string> codeBlock = new List<string> { "pen red", "draw 50 50" };

            // Act & Assert
            Assert.DoesNotThrow(() =>
            {
                commandParser.ExecuteIfStatementWithCodeBlock(condition, codeBlock);
            });
        }

        [Test]
        public void TestLoopWithVariable()
        {
            // Arrange
            CommandParser commandParser = new CommandParser(Graphics.FromImage(new Bitmap(1, 1)), new Size(100, 100));
            List<string> commands = new List<string> { "pen red", "draw 10 10" };
            int count = 3;

            // Act & Assert
            Assert.DoesNotThrow(() =>
            {
                commandParser.ExecuteLoopWithVariable(commands, count);
            });
        }

        [Test]
        public void TestExpressions()
        {
            // Arrange
            CommandParser commandParser = new CommandParser(Graphics.FromImage(new Bitmap(1, 1)), new Size(100, 100));

            // Act
            int result = commandParser.EvaluateExpression("5 * 10");

            // Assert
            Assert.AreEqual(50, result);
        }
    }
}
   
