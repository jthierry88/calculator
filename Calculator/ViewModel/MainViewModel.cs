using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Input;

namespace Calculator.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private string _outputText;
        private Operation _currentOperation;
        private bool _inOperation;
        private string _outputText2;
        private bool _isErrorState;

        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                OutputText = "Testing...";
            }

            OutputText = "0";
            EquationSolution = double.NaN;
        }

        public Operation CurrentOperation
        {
            get => _currentOperation; set
            {
                _currentOperation = value;
                RaisePropertyChanged();

                InOperation = value != Operation.None;

                if (InOperation)
                {
                    UpdateOutputText2();
                }
            }
        }

        private void UpdateOutputText2()
        {
            var output = UserInput1.ToString();

            switch (_currentOperation)
            {
                case Operation.Divide:
                    {
                        output += @" \";
                    }
                    break;

                case Operation.Multiply:
                    {
                        output += @" X";
                    }
                    break;

                case Operation.Subtract:
                    {
                        output += @" -";
                    }
                    break;

                case Operation.Add:
                    {
                        output += @" +";
                    }
                    break;
            }
            OutputText2 = output;
        }

        private double UserInput1
        {
            get; set;
        }

        private double UserInput2
        {
            get; set;
        }

        private double EquationSolution
        {
            get; set;
        }

        public string OutputText
        {
            get
            {
                return _outputText;
            }

            set
            {
                _outputText = value;
                RaisePropertyChanged();
            }
        }

        public string OutputText2
        {
            get => _outputText2; set
            {
                _outputText2 = value;
                RaisePropertyChanged();
            }
        }

        public bool InOperation
        {
            get => _inOperation; set
            {
                _inOperation = value;
                RaisePropertyChanged();
            }
        }
        public bool IsErrorState
        {
            get => _isErrorState; set
            {
                _isErrorState = value;
                RaisePropertyChanged();
            }
        }


        public RelayCommand<string> CommandButtonPressed => new RelayCommand<string>(ButtonPressed);

        private void PerformCalculation()
        {
            var newValue = double.NaN;

            switch (CurrentOperation)
            {
                case Operation.Divide:
                    {
                        {
                            // Check for division by zero
                            if (UserInput2 == 0)
                            {
                                OutputText = "Cannot divide by zero";
                                // Set the error state
                                IsErrorState = true;
                                // Simply exit to avoid division by zero
                                return;
                            }
                        }
                        newValue = UserInput1 / UserInput2;
                    }
                    break;

                case Operation.Multiply:
                    {
                        newValue = UserInput1 * UserInput2;
                    }
                    break;

                case Operation.Subtract:
                    {
                        newValue = UserInput1 - UserInput2;
                    }
                    break;

                case Operation.Add:
                    {
                        newValue = UserInput1 + UserInput2;
                    }
                    break;
            }

            var roundedValue = Math.Round(newValue, 4);

            EquationSolution = roundedValue;

            OutputText = roundedValue.ToString();
        }

        private void ClearCalculator(bool clearOutput)
        {
            if (clearOutput)
            {
                OutputText = "0";
            }

            CurrentOperation = Operation.None;
            UserInput1 = double.NaN;
            UserInput2 = double.NaN;
        }

        public void ButtonPressed(string button)
        {
            switch (button)
            {
                case "Divide":
                case "Multiply":
                case "Subtract":
                case "Add":
                    HandleOperationButton(button);
                    break;

                case "Equals":
                    HandleEqualsButton(button);
                    break;

                case "Clear":
                    ClearCalculator(true);
                    break;

                case "Backspace":
                    HandleBackspaceButton();
                    break;

                case ".":
                    HandleDecimalButton();
                    break;

                default:
                    HandleNumberButton(button);
                    break;
            }
        }

        private void HandleOperationButton(string button)
        {
            // Don't allow operation if in error state
            if (IsErrorState)
            {
                return;
            }

            // Ensure first operand is captured
            if (CurrentOperation == Operation.None)
            {
                UserInput1 = Convert.ToDouble(OutputText);
                CurrentOperation = GetOperationFromString(button);
                // Update display to show the operation
                UpdateOutputText2();
            }
            else
            {
                if (CurrentOperation != Operation.None && !double.IsNaN(UserInput2))
                {
                    // If operation already in progress, perform calculation first
                    PerformCalculation();

                    // Set result of calculation as the first operand for the next operation
                    UserInput1 = EquationSolution;
                }
                else if (CurrentOperation == Operation.None)
                {
                    // If no operation in progress, set the first operand
                    UserInput1 = Convert.ToDouble(OutputText);
                }

                // In both cases, set the new operation and reset the second operand
                CurrentOperation = GetOperationFromString(button);
                UserInput2 = double.NaN;
                UpdateOutputText2(); // Update the display
            }
        }

        private void HandleEqualsButton(string button)
        {
            if (CurrentOperation == Operation.None || double.IsNaN(UserInput2))
            {
                return;
            }

            PerformCalculation();

            ClearCalculator(false);
        }

        private void HandleBackspaceButton()
        {
            if (CurrentOperation == Operation.None)
            {
                // First Input
                if (OutputText.Length == 1)
                {
                    OutputText = "0";
                }
                else
                {
                    var trimmedOutput = OutputText.Substring(0, OutputText.Length - 1);
                    OutputText = trimmedOutput;
                }

                UserInput1 = Convert.ToDouble(OutputText);
            }
            else
            {
                if (Convert.ToDouble(OutputText) == UserInput1)
                {
                    // First entry in User Input 2
                    OutputText = "0";
                    UserInput2 = Convert.ToDouble(OutputText);
                }
            }
        }

        private void HandleDecimalButton()
        {
            if (CurrentOperation == Operation.None)
            {
                if (OutputText.Contains("."))
                {
                    // Decimal already exists
                    return;
                }
                OutputText += ".";
            }
            else
            {
                if (Convert.ToDouble(OutputText) == UserInput1)
                {
                    // First entry in User Input 2
                    OutputText = "0.";
                    UserInput2 = Convert.ToDouble(OutputText);
                }
                else
                {
                    if (OutputText.Contains("."))
                    {
                        // Decimal already exists
                        return;
                    }
                    OutputText += ".";
                }
            }
        }

        private void HandleNumberButton(string button)
        {
            // Reset calculator if in error state
            if (IsErrorState)
            {
                ClearCalculator(true);
                OutputText = button; // Set the new number
                IsErrorState = false; // Reset the error state
                return;
            }

            // A number was pressed

            if (CurrentOperation == Operation.None)
            {
                // First number input, or first input after equation completed

                if (OutputText == "0" || Convert.ToDouble(OutputText) == EquationSolution)
                {
                    OutputText = button;
                }
                else
                {
                    OutputText += button;
                }

                UserInput1 = Convert.ToDouble(OutputText);
            }
            else
            {
                if (OutputText == "0" || OutputText == UserInput1.ToString())
                {
                    OutputText = button;
                }
                else
                {
                    OutputText += button;
                }

                UserInput2 = Convert.ToDouble(OutputText);
            }
        }

        private Operation GetOperationFromString(string operationString)
        {
            switch (operationString)
            {
                case "Divide":
                    return Operation.Divide;

                case "Multiply":
                    return Operation.Multiply;

                case "Subtract":
                    return Operation.Subtract;

                case "Add":
                    return Operation.Add;

                default:
                    return Operation.None;
            }
        }

        internal void HandleKey(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Delete:
                    {
                        ButtonPressed("Clear");
                    }
                    break;

                case Key.Back:
                    {
                        ButtonPressed("Backspace");
                    }
                    break;

                case Key.Enter:
                    {
                        ButtonPressed("Equals");
                    }
                    break;


                // Numbers
                case Key.NumPad0:
                case Key.D0:
                    {
                        ButtonPressed("0");
                    }
                    break;

                case Key.NumPad1:
                case Key.D1:
                    {
                        ButtonPressed("1");
                    }
                    break;

                case Key.NumPad2:
                case Key.D2:
                    {
                        ButtonPressed("2");
                    }
                    break;

                case Key.NumPad3:
                case Key.D3:
                    {
                        ButtonPressed("3");
                    }
                    break;

                case Key.NumPad4:
                case Key.D4:
                    {
                        ButtonPressed("4");
                    }
                    break;

                case Key.NumPad5:
                case Key.D5:
                    {
                        ButtonPressed("5");
                    }
                    break;

                case Key.NumPad6:
                case Key.D6:
                    {
                        ButtonPressed("6");
                    }
                    break;

                case Key.NumPad7:
                case Key.D7:
                    {
                        ButtonPressed("7");
                    }
                    break;

                case Key.NumPad8:
                case Key.D8:
                    {
                        if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                        {
                            ButtonPressed("Multiply");
                        }
                        else
                        {
                            ButtonPressed("8");
                        }
                    }
                    break;

                case Key.NumPad9:
                case Key.D9:
                    {
                        ButtonPressed("9");
                    }
                    break;


                // Operations
                case Key.OemPlus:
                    {
                        if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                        {
                            ButtonPressed("Add");
                        }
                        else
                        {
                            ButtonPressed("Equals");
                        }
                    }
                    break;

                case Key.Multiply:
                    {
                        ButtonPressed("Multiply");
                    }
                    break;

                case Key.Add:
                    {

                        ButtonPressed("Add");
                    }
                    break;

                case Key.OemMinus:
                case Key.Subtract:
                    {
                        ButtonPressed("Subtract");
                    }
                    break;

                case Key.OemPeriod:
                case Key.Decimal:
                    {
                        ButtonPressed(".");
                    }
                    break;

                // Backslash and Forwardslash
                case Key.Oem5:
                case Key.OemQuestion:
                case Key.Divide:
                    {

                        ButtonPressed("Divide");
                    }
                    break;

            }
        }
    }

    public enum Operation
    {
        None,
        Divide,
        Multiply,
        Subtract,
        Add
    }
}