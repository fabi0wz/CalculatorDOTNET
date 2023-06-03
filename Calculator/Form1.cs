using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace Calculator
{
    public partial class Calculator : Form
    {
        bool negativeCheck = false;
        bool IsCalulating = false;
        bool isDecimal = false;
        public Calculator()
        {
            InitializeComponent();
            typedisplay.Enabled= false;
            this.KeyPreview = true; // By setting KeyPreview to true, the form will receive key events before the focused control does, allowing your event handler to capture the key presses.
        }

        private void Calculator_Load(object sender, EventArgs e)
        {

        }
        private void btn0_Click(object sender, EventArgs e)
        {
            Update_Label("0");
        }
        private void btn1_Click(object sender, EventArgs e)
        {
            Update_Label("1");
        }
        private void btn2_Click(object sender, EventArgs e)
        {
            Update_Label("2");
        }
        private void btn3_Click(object sender, EventArgs e)
        {
            Update_Label("3");
        }
        private void btn4_Click(object sender, EventArgs e)
        {
            Update_Label("4");
        }
        private void btn5_Click(object sender, EventArgs e)
        {
            Update_Label("5");
        }
        private void btn6_Click(object sender, EventArgs e)
        {
            Update_Label("6");
        }
        private void btn7_Click(object sender, EventArgs e)
        {
            Update_Label("7");
        }
        private void btn8_Click(object sender, EventArgs e)
        {
            Update_Label("8");
        }
        private void btn9_Click(object sender, EventArgs e)
        {
            Update_Label("9");
        }

        private void btnnegative_Click(object sender, EventArgs e)
        {
            negativeUpdate();
        }

        private void negativeUpdate()
        {
            negativeCheck = !negativeCheck;
            typedisplay.Text = negativeCheck ? "-" + typedisplay.Text : typedisplay.Text.TrimStart('-');
        }

        private void btnequal_Click(object sender, EventArgs e)
        {
            equalcalculation();
        }

        private void equalcalculation()
        {
            if (!String.IsNullOrEmpty(calclabel.Text) && !String.IsNullOrEmpty(typedisplay.Text))
            {
                calculate();
                typedisplay.Text = calclabel.Text;
                calclabel.Text = "";
                IsCalulating = Convert.ToDouble(typedisplay.Text) % 2 == 0 ? false : true;
            }
            else if (String.IsNullOrEmpty(typedisplay.Text) && !String.IsNullOrEmpty(calclabel.Text))
            {
                typedisplay.Text = calclabel.Text;
                calclabel.Text = "";
                IsCalulating = Convert.ToInt32(typedisplay.Text) % 2 == 0 ? false : true;
            }
        }

        private void btnsum_Click(object sender, EventArgs e)
        {
            operatorUpdate('+');
        }

        private void btnsubtract_Click(object sender, EventArgs e)
        {
            operatorUpdate('-');
        }

        private void btnmultiply_Click(object sender, EventArgs e)
        {
            operatorUpdate('*');
        }

        private void btndivide_Click(object sender, EventArgs e)
        {
            operatorUpdate('/');
        }

        private void operatorUpdate(char math_operator)
        {
            string[] operators = { "+", "-", "*", "/" };
            if (String.IsNullOrEmpty(calclabel.Text) && (!String.IsNullOrEmpty(typedisplay.Text) && typedisplay.Text != ","))
            {
                Update_Calculo_Label(typedisplay.Text + " " + math_operator);
                Update_Label(null);
                IsCalulating = !IsCalulating;
                isDecimal = false;
            }
            else if (IsCalulating)
            {
                if (typedisplay.Text == "" || typedisplay.Text == ",")
                {
                    calclabel.Text = calclabel.Text.Substring(0, calclabel.Text.Length - 2);
                    Update_Calculo_Label(calclabel.Text + " " + math_operator);
                }
                else if (operators.Any(x => calclabel.Text.EndsWith(x)))
                {
                    calculate();
                    Update_Label(null);
                    calclabel.Text = calclabel.Text + " " + math_operator;
                }
                isDecimal = !isDecimal;
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            delete();
        }

        private void delete() 
        {
            if (!string.IsNullOrEmpty(typedisplay.Text))
            {
                if (typedisplay.Text.EndsWith(','))
                {
                    isDecimal = !isDecimal;
                }
                typedisplay.Text = typedisplay.Text.Substring(0, typedisplay.Text.Length - 1);
            }
            else if (!string.IsNullOrEmpty(calclabel.Text))
            {
                typedisplay.Text = calclabel.Text.Substring(0, calclabel.Text.Length - 2);
                calclabel.Text = "";
            }
        }

        private void Update_Label(string value)
        {
            if(value == null)
            {
                typedisplay.Text = "";
                return;
            }
            typedisplay.Text = typedisplay.Text == "0" ? "" : typedisplay.Text;
            typedisplay.Text += value;
        }

        private void Update_Calculo_Label(string value)
        {
            calclabel.Text = value;
        }

        private void calculate()
        {
            char value = Convert.ToChar(calclabel.Text.Substring(calclabel.Text.Length - 1, 1));
            
            switch (value) { 
                case '+':
                        calclabel.Text = (Double.Parse(calclabel.Text.Substring(0, calclabel.Text.Length-2)) + Double.Parse(typedisplay.Text)).ToString();
                break;

                case '-':
                    calclabel.Text = (Double.Parse(calclabel.Text.Substring(0, calclabel.Text.Length-2)) - Double.Parse(typedisplay.Text)).ToString();
                    break;

                case '*':
                    calclabel.Text = (Double.Parse(calclabel.Text.Substring(0, calclabel.Text.Length-2)) * Double.Parse(typedisplay.Text)).ToString();
                    break;

                case '/':
                    calclabel.Text = (Double.Parse(calclabel.Text.Substring(0, calclabel.Text.Length-2)) / Double.Parse(typedisplay.Text)).ToString();
                    break;
            }
        }
        private void btncomma_Click(object sender, EventArgs e)
        {
            decimalUpdate();
        }

        private void decimalUpdate()
        {
            if (!isDecimal)
            {
                Update_Label(",");
                isDecimal = !isDecimal;
            }
        }


        private void Calculator_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '+')
            {
                operatorUpdate('+');
            }
            else if (e.KeyChar == '-')
            {
                operatorUpdate('-');
            }
            else if (e.KeyChar == '/')
            {
                operatorUpdate('/');
            }
            else if (e.KeyChar == '*')
            {
                operatorUpdate('*');
            }
            else if (e.KeyChar == ',')
            {
                decimalUpdate();
            }
            else if (e.KeyChar == '1')
            {
                Update_Label("1");
            }
            else if (e.KeyChar == '2')
            {
                Update_Label("2");
            }
            else if (e.KeyChar == '3')
            {
                Update_Label("3");
            }
            else if (e.KeyChar == '4')
            {
                Update_Label("4");
            }
            else if (e.KeyChar == '5')
            {
                Update_Label("5");
            }
            else if (e.KeyChar == '6')
            {
                Update_Label("6");
            }
            else if (e.KeyChar == '7')
            {
                Update_Label("7");
            }
            else if (e.KeyChar == '8')
            {
                Update_Label("8");
            }
            else if (e.KeyChar == '9')
            {
                Update_Label("9");
            }
            else if (e.KeyChar == '0')
            {
                Update_Label("0");
            }
            else if (e.KeyChar == '\b')
            {
                delete();
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            typedisplay.Text = "";
            calclabel.Text = "";
            negativeCheck = false;
            IsCalulating = false;
            isDecimal = false;
        }
    }
}