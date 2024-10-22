using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

public class CustomTextBox : TextBox
{
    private int borderRadius = 10; // Ajusta el radio del borde

    public CustomTextBox()
    {
        this.BorderStyle = BorderStyle.None; // Quitar borde predeterminado
        this.Padding = new Padding(3); // Espaciado interno
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        // Establecer la región redondeada
        using (GraphicsPath path = new GraphicsPath())
        {
            path.StartFigure();
            path.AddArc(0, 0, borderRadius, borderRadius, 180, 90);
            path.AddArc(Width - borderRadius, 0, borderRadius, borderRadius, 270, 90);
            path.AddArc(Width - borderRadius, Height - borderRadius, borderRadius, borderRadius, 0, 90);
            path.AddArc(0, Height - borderRadius, borderRadius, borderRadius, 90, 90);
            path.CloseFigure();
            this.Region = new Region(path);
        }

        // Dibujar el fondo
        e.Graphics.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, Width, Height));

        // Dibujar el borde
        using (Pen borderPen = new Pen(Color.Gray, 1)) // Ajusta el color y grosor del borde
        {
            e.Graphics.DrawPath(borderPen, new GraphicsPath()); // Dibuja el borde
        }
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        this.Invalidate(); // Redibujar el control al cambiar el tamaño
    }
}
