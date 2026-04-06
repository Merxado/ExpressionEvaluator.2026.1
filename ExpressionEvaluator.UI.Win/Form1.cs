namespace ExpressionEvaluator.UI.Win;
using ExpressionEvaluator.Core;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    private void BtnToken_Click(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        txtDisplay.Text += btn.Text;
    }

    private void BtnClear_Click(object sender, EventArgs e)
    {
        txtDisplay.Text = "";
    }

    private void BtnBack_Click(object sender, EventArgs e)
    {
        if (txtDisplay.Text.Length > 0)
            txtDisplay.Text = txtDisplay.Text[..^1];
    }

    private void BtnEquals_Click(object sender, EventArgs e)
    {
        try
        {
            double result = Evaluator.Evaluate(txtDisplay.Text);
            txtDisplay.Text = result.ToString(System.Globalization.CultureInfo.InvariantCulture);
        }
        catch
        {
            txtDisplay.Text = "Error";
        }
    }
}
