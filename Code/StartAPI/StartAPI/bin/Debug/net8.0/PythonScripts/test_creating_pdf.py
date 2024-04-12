from reportlab.lib.pagesizes import letter
from reportlab.pdfgen import canvas
from reportlab.platypus import Table, TableStyle


def create_report(pdf_file):
    # Create a canvas object
    c = canvas.Canvas(pdf_file, pagesize=letter)

    # Draw some text
    c.setFont("Helvetica", 12)
    c.drawString(100, 750, "This is a test PDF file")
    c.drawString(100, 550, "This is a test PDF file")
    c.drawString(100, 450, "This is a test PDF file")
    c.drawString(100, 350, "This is a test PDF file")

    # Draw a table
    data = [['Name', 'Age', 'Gender'],
            ['John Doe', '30', 'Male'],
            ['Jane Smith', '25', 'Female']]

    table_style = TableStyle([('BACKGROUND', (0, 0), (-1, 0), (0.8, 0.8, 0.8)),  # Header background color
                              ('TEXTCOLOR', (0, 0), (-1, 0), (0, 0, 0)),  # Header text color
                              ('ALIGN', (0, 0), (-1, -1), 'CENTER'),  # Center align text
                              ('FONTNAME', (0, 0), (-1, 0), 'Helvetica-Bold')])  # Header font style

    table = Table(data)
    table.setStyle(table_style)
    table.wrapOn(c, 0, 0)
    table.drawOn(c, 100, 600)

    # Draw a line
    c.line(50, 680, 550, 680)

    # Close the canvas
    c.save()


# Usage
create_report("./example_report.pdf")
