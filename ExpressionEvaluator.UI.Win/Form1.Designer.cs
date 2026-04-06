namespace ExpressionEvaluator.UI.Win;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        txtDisplay = new TextBox();
        SuspendLayout();

        txtDisplay.Font = new Font("Segoe UI", 18F, FontStyle.Regular);
        txtDisplay.Location = new Point(12, 12);
        txtDisplay.Size = new Size(326, 44);
        txtDisplay.ReadOnly = true;
        txtDisplay.TextAlign = HorizontalAlignment.Right;
        txtDisplay.BackColor = Color.White;

        string[][] layout =
        [
            ["(", ")", "^", "C", "←"],
            ["7", "8", "9", "/", ""],
            ["4", "5", "6", "*", ""],
            ["1", "2", "3", "-", ""],
            ["0", ".", "=", "+", ""]
        ];

        int btnW = 60, btnH = 50, gap = 6;
        int startX = 12, startY = 70;

        for (int row = 0; row < layout.Length; row++)
        {
            for (int col = 0; col < layout[row].Length; col++)
            {
                string label = layout[row][col];
                if (label == "") continue;

                var btn = new Button
                {
                    Text = label,
                    Size = new Size(btnW, btnH),
                    Location = new Point(startX + col * (btnW + gap),
                                         startY + row * (btnH + gap)),
                    Font = new Font("Segoe UI", 13F),
                    FlatStyle = FlatStyle.Flat,
                };

                btn.BackColor = label switch
                {
                    "=" => Color.FromArgb(0, 120, 215),
                    "C" or "←" => Color.FromArgb(220, 53, 69),
                    "+" or "-" or "*" or "/" or "^" => Color.FromArgb(80, 80, 80),
                    "(" or ")" => Color.FromArgb(100, 100, 120),
                    _ => Color.FromArgb(50, 50, 50)
                };
                btn.ForeColor = Color.White;
                btn.FlatAppearance.BorderSize = 0;

                if (label == "=")
                    btn.Click += BtnEquals_Click;
                else if (label == "C")
                    btn.Click += BtnClear_Click;
                else if (label == "←")
                    btn.Click += BtnBack_Click;
                else
                    btn.Click += BtnToken_Click;

                Controls.Add(btn);
            }
        }

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(350, 360);
        Controls.Add(txtDisplay);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "Form1";
        Text = "Expression Evaluator";
        BackColor = Color.FromArgb(30, 30, 30);
        ResumeLayout(false);
        PerformLayout();
    }

    private TextBox txtDisplay;
}
