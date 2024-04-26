from datetime import datetime
from email.message import EmailMessage
import ssl
import smtplib
import os

email_sender = "produktion.purolex@gmail.com"
email_password = "edlq jfvs mtcc tgfz"
email_receiver = "mario@purolex.at"

subject = "Bericht vom "+ format(datetime.now().strftime("%d.%m.%Y"))
body = """
Guten Abend, es ist """+ format(datetime.now().strftime("%H:%M"))
em = EmailMessage()
em["From"] = email_sender
em["To"] = email_receiver
em["Subject"] = subject
em.set_content(body)

# Attach the hello.pdf file to the email
print('Send: '+os.path.join(os.path.dirname(os.path.abspath(__file__))))
file_path = os.path.join(os.path.dirname(os.path.abspath(__file__)), "output.pdf")

with open(file_path, "rb") as f:
    file_data = f.read()
    file_name = f.name
    em.add_attachment(file_data, maintype="application", subtype="octet-stream", filename="Bericht "+datetime.now().strftime("%d.%m.%Y")+".pdf")

context = ssl.create_default_context()

with smtplib.SMTP_SSL("smtp.gmail.com", 465, context=context) as smtp:
    smtp.login(email_sender, email_password)
    smtp.sendmail(email_sender, email_receiver, em.as_string())

#%%
