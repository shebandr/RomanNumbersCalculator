using Avalonia.Controls;
using Avalonia.VisualTree;
using RomanNumbersCalculator.Views;
using static System.Net.Mime.MediaTypeNames;

namespace UITestsForRomanNumbersCalculator
{
    public class UnitTests
    {
        private static Dictionary<string, Button> initializeMainWindowButtons(MainWindow mainWindow)
        {

            var buttonI = mainWindow.GetVisualDescendants().OfType<Button>().First(b => b.Name == "I");
            var buttonV = mainWindow.GetVisualDescendants().OfType<Button>().First(b => b.Name == "V");
            var buttonX = mainWindow.GetVisualDescendants().OfType<Button>().First(b => b.Name == "X");
            var buttonL = mainWindow.GetVisualDescendants().OfType<Button>().First(b => b.Name == "L");
            var buttonC = mainWindow.GetVisualDescendants().OfType<Button>().First(b => b.Name == "C");
            var buttonD = mainWindow.GetVisualDescendants().OfType<Button>().First(b => b.Name == "D");
            var buttonM = mainWindow.GetVisualDescendants().OfType<Button>().First(b => b.Name == "M");
            var buttonPlus = mainWindow.GetVisualDescendants().OfType<Button>().First(b => b.Name == "Plus");
            var buttonMinus = mainWindow.GetVisualDescendants().OfType<Button>().First(b => b.Name == "Minus");
            var buttonMultiply = mainWindow.GetVisualDescendants().OfType<Button>().First(b => b.Name == "Multiply");
            var buttonDivide = mainWindow.GetVisualDescendants().OfType<Button>().First(b => b.Name == "Divide");
            var buttonEqual = mainWindow.GetVisualDescendants().OfType<Button>().First(b => b.Name == "Equal");
            var buttonCE = mainWindow.GetVisualDescendants().OfType<Button>().First(b => b.Name == "CE");

            Dictionary<string, Button> buttons = new Dictionary<string, Button>
            {
                { "I", buttonI },
                { "V", buttonV },
                { "X", buttonX },
                { "L", buttonL },
                { "C", buttonC },
                { "D", buttonD },
                { "M", buttonM },
                { "CE", buttonCE },
                { "+", buttonPlus },
                { "-", buttonMinus },
                { "*", buttonMultiply },
                { "/", buttonDivide },
                { "=", buttonEqual },
            };
            return buttons;
        }

        private static string getErrorMessage(string expectedValue, string resultValue) => String.Format("Expected '{0}', however got '{1}'", expectedValue, resultValue);



        [Fact]
        public async void TestSubstract()
        {
            var app = AvaloniaApp.GetApp();
            var mainWindow = AvaloniaApp.GetMainWindow();

            await Task.Delay(100);

            var resultTextBox = mainWindow.GetVisualDescendants().OfType<TextBlock>().First(textBox => textBox.Name == "CalRTB");
            Dictionary<string, Button> buttons = initializeMainWindowButtons(mainWindow);

            
            buttons["C"].Command.Execute(buttons["C"].CommandParameter); // C - V = XCV
            buttons["-"].Command.Execute(buttons["-"].CommandParameter);
            buttons["V"].Command.Execute(buttons["V"].CommandParameter);
            buttons["="].Command.Execute(buttons["="].CommandParameter);

            await Task.Delay(50);
            string text = resultTextBox.Text;
            buttons["CE"].Command.Execute(buttons["CE"].CommandParameter);
            Assert.True(text == "XCV", getErrorMessage("XCV", text));

            
            buttons["C"].Command.Execute(buttons["C"].CommandParameter); // C - XC = X
            buttons["-"].Command.Execute(buttons["-"].CommandParameter);
            buttons["X"].Command.Execute(buttons["X"].CommandParameter);
            buttons["C"].Command.Execute(buttons["C"].CommandParameter);
            buttons["="].Command.Execute(buttons["="].CommandParameter);

            await Task.Delay(50);
            text = resultTextBox.Text;
            buttons["CE"].Command.Execute(buttons["CE"].CommandParameter);
            Assert.True(text == "X", getErrorMessage("X", text));
        }

        [Fact]
        public async void TestMultiplyError()
        {
            var app = AvaloniaApp.GetApp();
            var mainWindow = AvaloniaApp.GetMainWindow();

            await Task.Delay(100);

            var resultTextBox = mainWindow.GetVisualDescendants().OfType<TextBlock>().First(textBox => textBox.Name == "CalRTB");
            Dictionary<string, Button> buttons = initializeMainWindowButtons(mainWindow);

            // M * M = #ERROR
            buttons["M"].Command.Execute(buttons["M"].CommandParameter);
            buttons["*"].Command.Execute(buttons["*"].CommandParameter);
            buttons["M"].Command.Execute(buttons["M"].CommandParameter);
            buttons["="].Command.Execute(buttons["="].CommandParameter);

            await Task.Delay(50);
            string text = resultTextBox.Text;
            buttons["CE"].Command.Execute(buttons["CE"].CommandParameter);
            Assert.True(text == "#ERROR", getErrorMessage("#ERROR", text));
        }


        [Fact]
        public async void TestDivide()
        {
            var app = AvaloniaApp.GetApp();
            var mainWindow = AvaloniaApp.GetMainWindow();

            await Task.Delay(100);

            var resultTextBox = mainWindow.GetVisualDescendants().OfType<TextBlock>().First(textBox => textBox.Name == "CalRTB");
            Dictionary<string, Button> buttons = initializeMainWindowButtons(mainWindow);


            buttons["C"].Command.Execute(buttons["C"].CommandParameter); // C / V = XX
            buttons["/"].Command.Execute(buttons["/"].CommandParameter);
            buttons["V"].Command.Execute(buttons["V"].CommandParameter);
            buttons["="].Command.Execute(buttons["="].CommandParameter);

            await Task.Delay(50);
            string text = resultTextBox.Text;
            buttons["CE"].Command.Execute(buttons["CE"].CommandParameter);
            Assert.True(text == "XX", getErrorMessage("XX", text));


            buttons["C"].Command.Execute(buttons["C"].CommandParameter); // CV / III = XXXV
            buttons["V"].Command.Execute(buttons["V"].CommandParameter);
            buttons["/"].Command.Execute(buttons["/"].CommandParameter);
            buttons["I"].Command.Execute(buttons["I"].CommandParameter);
            buttons["I"].Command.Execute(buttons["I"].CommandParameter);
            buttons["I"].Command.Execute(buttons["I"].CommandParameter);
            buttons["="].Command.Execute(buttons["="].CommandParameter);

            await Task.Delay(50);
            text = resultTextBox.Text;
            buttons["CE"].Command.Execute(buttons["CE"].CommandParameter);
            Assert.True(text == "XXXV", getErrorMessage("XXXV", text));
        }

        [Fact]
        public async void TestAdd()
        {
            var app = AvaloniaApp.GetApp();
            var mainWindow = AvaloniaApp.GetMainWindow();

            await Task.Delay(100);

            var resultTextBox = mainWindow.GetVisualDescendants().OfType<TextBlock>().First(textBox => textBox.Name == "CalRTB");
            Dictionary<string, Button> buttons = initializeMainWindowButtons(mainWindow);


            buttons["I"].Command.Execute(buttons["I"].CommandParameter);  // I + V = VI
            buttons["+"].Command.Execute(buttons["+"].CommandParameter);
            buttons["V"].Command.Execute(buttons["V"].CommandParameter);
            buttons["="].Command.Execute(buttons["="].CommandParameter);

            await Task.Delay(50);
            string text = resultTextBox.Text;
            buttons["CE"].Command.Execute(buttons["CE"].CommandParameter);
            Assert.True(text == "VI", getErrorMessage("VI", text));


            buttons["M"].Command.Execute(buttons["M"].CommandParameter); // M + M = MM
            buttons["+"].Command.Execute(buttons["+"].CommandParameter);
            buttons["M"].Command.Execute(buttons["M"].CommandParameter);
            buttons["="].Command.Execute(buttons["="].CommandParameter);

            await Task.Delay(50);
            text = resultTextBox.Text;
            buttons["CE"].Command.Execute(buttons["CE"].CommandParameter);
            Assert.True(text == "MM", getErrorMessage("MM", text));
        }

        [Fact]
        public async void TestMultiply()
        {
            var app = AvaloniaApp.GetApp();
            var mainWindow = AvaloniaApp.GetMainWindow();

            await Task.Delay(100);

            var resultTextBox = mainWindow.GetVisualDescendants().OfType<TextBlock>().First(textBox => textBox.Name == "CalRTB");
            Dictionary<string, Button> buttons = initializeMainWindowButtons(mainWindow);

           
            buttons["C"].Command.Execute(buttons["C"].CommandParameter);  // C * V = D
            buttons["*"].Command.Execute(buttons["*"].CommandParameter);
            buttons["V"].Command.Execute(buttons["V"].CommandParameter);
            buttons["="].Command.Execute(buttons["="].CommandParameter);

            await Task.Delay(50);
            string text = resultTextBox.Text;
            buttons["CE"].Command.Execute(buttons["CE"].CommandParameter);
            Assert.True(text == "D", getErrorMessage("D", text));

            
            buttons["C"].Command.Execute(buttons["C"].CommandParameter); // C * IX = CM
            buttons["*"].Command.Execute(buttons["*"].CommandParameter);
            buttons["I"].Command.Execute(buttons["I"].CommandParameter);
            buttons["X"].Command.Execute(buttons["X"].CommandParameter);
            buttons["="].Command.Execute(buttons["="].CommandParameter);

            await Task.Delay(50);
            text = resultTextBox.Text;
            buttons["CE"].Command.Execute(buttons["CE"].CommandParameter);
            Assert.True(text == "CM", getErrorMessage("CM", text));
        }


        [Fact]
        public async void TestAddError() // MM + MM = #ERROR
        {
            var app = AvaloniaApp.GetApp();
            var mainWindow = AvaloniaApp.GetMainWindow();

            await Task.Delay(100);

            var resultTextBox = mainWindow.GetVisualDescendants().OfType<TextBlock>().First(textBox => textBox.Name == "CalRTB");
            Dictionary<string, Button> buttons = initializeMainWindowButtons(mainWindow);

            
            buttons["M"].Command.Execute(buttons["M"].CommandParameter);
            buttons["M"].Command.Execute(buttons["M"].CommandParameter);
            buttons["+"].Command.Execute(buttons["+"].CommandParameter);
            buttons["M"].Command.Execute(buttons["M"].CommandParameter);
            buttons["M"].Command.Execute(buttons["M"].CommandParameter);
            buttons["="].Command.Execute(buttons["="].CommandParameter);

            await Task.Delay(50);
            string text = resultTextBox.Text;
            buttons["CE"].Command.Execute(buttons["CE"].CommandParameter);
            Assert.True(text == "#ERROR", getErrorMessage("#ERROR", text));
        }
        
        [Fact]
        public async void TestSubstractError() // MMM - MM = M
        {
            var app = AvaloniaApp.GetApp();
            var mainWindow = AvaloniaApp.GetMainWindow();

            await Task.Delay(100);

            var resultTextBox = mainWindow.GetVisualDescendants().OfType<TextBlock>().First(textBox => textBox.Name == "CalRTB");
            Dictionary<string, Button> buttons = initializeMainWindowButtons(mainWindow);

            
            buttons["M"].Command.Execute(buttons["M"].CommandParameter);
            buttons["M"].Command.Execute(buttons["M"].CommandParameter);
            buttons["M"].Command.Execute(buttons["M"].CommandParameter);
            buttons["-"].Command.Execute(buttons["-"].CommandParameter);
            buttons["M"].Command.Execute(buttons["M"].CommandParameter);
            buttons["M"].Command.Execute(buttons["M"].CommandParameter);
            buttons["="].Command.Execute(buttons["="].CommandParameter);

            await Task.Delay(50);
            string text = resultTextBox.Text;
            buttons["CE"].Command.Execute(buttons["CE"].CommandParameter);
            Assert.True(text == "M", getErrorMessage("M", text));
        }


        [Fact]
        public async void TestDivideError()
        {
            var app = AvaloniaApp.GetApp();
            var mainWindow = AvaloniaApp.GetMainWindow();

            await Task.Delay(100);

            var resultTextBox = mainWindow.GetVisualDescendants().OfType<TextBlock>().First(textBox => textBox.Name == "CalRTB");
            Dictionary<string, Button> buttons = initializeMainWindowButtons(mainWindow);

            // C / M = #ERROR
            buttons["C"].Command.Execute(buttons["C"].CommandParameter);
            buttons["/"].Command.Execute(buttons["/"].CommandParameter);
            buttons["M"].Command.Execute(buttons["M"].CommandParameter);
            buttons["="].Command.Execute(buttons["="].CommandParameter);

            await Task.Delay(50);
            string text = resultTextBox.Text;
            buttons["CE"].Command.Execute(buttons["CE"].CommandParameter);
            Assert.True(text == "#ERROR", getErrorMessage("#ERROR", text));
        }

        [Fact]
        public async void TestDivideError2()
        {
            var app = AvaloniaApp.GetApp();
            var mainWindow = AvaloniaApp.GetMainWindow();

            await Task.Delay(100);

            var resultTextBox = mainWindow.GetVisualDescendants().OfType<TextBlock>().First(textBox => textBox.Name == "CalRTB");
            Dictionary<string, Button> buttons = initializeMainWindowButtons(mainWindow);
            buttons["M"].Command.Execute(buttons["M"].CommandParameter);
            buttons["M"].Command.Execute(buttons["M"].CommandParameter);
            buttons["M"].Command.Execute(buttons["M"].CommandParameter);
            buttons["M"].Command.Execute(buttons["M"].CommandParameter);
            buttons["M"].Command.Execute(buttons["M"].CommandParameter);
            buttons["/"].Command.Execute(buttons["/"].CommandParameter);
            buttons["M"].Command.Execute(buttons["M"].CommandParameter);
            buttons["="].Command.Execute(buttons["="].CommandParameter);

            await Task.Delay(50);
            string text = resultTextBox.Text;
            buttons["CE"].Command.Execute(buttons["CE"].CommandParameter);
            Assert.True(text == "#ERROR", getErrorMessage("#ERROR", text));
        }

        [Fact]
        public async void TestWithChangingSigns()
        {
            var app = AvaloniaApp.GetApp();
            var mainWindow = AvaloniaApp.GetMainWindow();

            await Task.Delay(100);

            var resultTextBox = mainWindow.GetVisualDescendants().OfType<TextBlock>().First(textBox => textBox.Name == "CalRTB");
            Dictionary<string, Button> buttons = initializeMainWindowButtons(mainWindow);

            
            buttons["C"].Command.Execute(buttons["C"].CommandParameter); // Test add
            buttons["/"].Command.Execute(buttons["/"].CommandParameter);
            buttons["+"].Command.Execute(buttons["+"].CommandParameter);
            buttons["M"].Command.Execute(buttons["M"].CommandParameter);
            buttons["="].Command.Execute(buttons["="].CommandParameter);

            await Task.Delay(50);
            string text = resultTextBox.Text;
            buttons["CE"].Command.Execute(buttons["CE"].CommandParameter);
            Assert.True(text == "MC", getErrorMessage("MC", text));

            
            buttons["M"].Command.Execute(buttons["M"].CommandParameter); // Test divide
            buttons["*"].Command.Execute(buttons["*"].CommandParameter);
            buttons["/"].Command.Execute(buttons["/"].CommandParameter);
            buttons["M"].Command.Execute(buttons["M"].CommandParameter);
            buttons["="].Command.Execute(buttons["="].CommandParameter);

            await Task.Delay(50);
            text = resultTextBox.Text;
            buttons["CE"].Command.Execute(buttons["CE"].CommandParameter);
            Assert.True(text == "I", getErrorMessage("I", text));

            
            buttons["C"].Command.Execute(buttons["C"].CommandParameter); // Test multiply
            buttons["+"].Command.Execute(buttons["+"].CommandParameter);
            buttons["/"].Command.Execute(buttons["/"].CommandParameter);
            buttons["-"].Command.Execute(buttons["-"].CommandParameter);
            buttons["*"].Command.Execute(buttons["*"].CommandParameter);
            buttons["X"].Command.Execute(buttons["X"].CommandParameter);
            buttons["I"].Command.Execute(buttons["I"].CommandParameter);
            buttons["="].Command.Execute(buttons["="].CommandParameter);

            await Task.Delay(50);
            text = resultTextBox.Text;
            buttons["CE"].Command.Execute(buttons["CE"].CommandParameter);
            Assert.True(text == "MC", getErrorMessage("MC", text));

           
            buttons["M"].Command.Execute(buttons["M"].CommandParameter);  // Test substract
            buttons["+"].Command.Execute(buttons["+"].CommandParameter);
            buttons["-"].Command.Execute(buttons["-"].CommandParameter);
            buttons["X"].Command.Execute(buttons["X"].CommandParameter);
            buttons["I"].Command.Execute(buttons["I"].CommandParameter);
            buttons["="].Command.Execute(buttons["="].CommandParameter);

            await Task.Delay(50);
            text = resultTextBox.Text;
            buttons["CE"].Command.Execute(buttons["CE"].CommandParameter);
            Assert.True(text == "CMLXXXIX", getErrorMessage("CMLXXXIX", text));
        }
    }
}
