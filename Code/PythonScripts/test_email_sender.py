from email.message import EmailMessage
import ssl
import smtplib
import os

email_sender = "produktion.purolex@gmail.com"
email_password = "edlq jfvs mtcc tgfz"
email_receiver = "kaliuzhnyiv190154@sus.htl-grieskirchen.at"

subject = "this is a testmail"
body = """
This is the body of the test Email!
"""
em = EmailMessage()
em["From"] = email_sender
em["To"] = email_receiver
em["Subject"] = subject
em.set_content(body)

# Attach the hello.pdf file to the email

file_path = os.path.join(os.path.dirname(os.path.abspath(__file__)), "output.pdf")

with open(file_path, "rb") as f:
    file_data = f.read()
    file_name = f.name
    em.add_attachment(file_data, maintype="application", subtype="octet-stream", filename=file_name)

context = ssl.create_default_context()

with smtplib.SMTP_SSL("smtp.gmail.com", 465, context=context) as smtp:
    smtp.login(email_sender, email_password)
    smtp.sendmail(email_sender, email_receiver, em.as_string())

#%%
