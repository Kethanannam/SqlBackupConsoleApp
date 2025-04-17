# SqlBackupConsoleApp

This is a .NET Core console application that performs scheduled **SQL Server database backups**, stores the backup files in a specified directory, logs backup status, and can run as a **Windows Service**.

---

## 📌 Features

- 🔁 **Automatic SQL Server backups**
- 📁 Stores backup `.bak` files in `D:\Backup\`
- 🪵 Logs every backup event (success/failure) to a text file
- 📧 Sends email notifications on backup success/failure (via Gmail SMTP)
- 🖥️ Runs as a **Windows Service**, executing backups every 5 minutes
- 🧩 Simple configuration in `appsettings.json`

---

## 🛠️ Technologies Used

- C#
- .NET Core Console App
- ASP.NET Core (Web API integration optional)
- Windows Service (via `Worker` service template)
- SQL Server
- Serilog (for logging)
- MailKit or SMTP client (for email)

---

## 📁 Folder Structure

SqlBackupConsoleApp/ ├── SqlBackupConsole1/ │ ├── SqlBackupWorker.cs │ ├── Program.cs │ ├── appsettings.json │ ├── SqlBackupConsole1.csproj │ └── publish/ ├── logs/ │ └── backup-log.txt ├── packages/ ├── README.md └── .gitignore


---

🚀 How to Run as Windows Service
Publish your project:

bash
Copy
Edit
dotnet publish -c Release -o D:\IIS_Deployments\sqlconsole
Install the service:

Run PowerShell as Administrator:

powershell
Copy
Edit
sc create SqlBackupService binPath= "D:\IIS_Deployments\sqlconsole\SqlBackupConsole1.exe"
Start the service:

powershell
Copy
Edit
sc start SqlBackupService
View logs:
Check logs in D:\Backup\backup-log.txt

🔒 Permission & Authentication Notes
The Windows Service runs under the NT AUTHORITY\SYSTEM account by default.

Make sure your SQL Server allows that account or:

Use SQL authentication

Or change service account in Services.msc to a user with DB access

📧 Email Notifications
Works with Gmail SMTP (Enable "App Passwords" or allow less secure apps).

Email is triggered after each backup attempt.


## ⚙️ Configuration - `appsettings.json`

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=Students;Trusted_Connection=True;"
  },
  "BackupSettings": {
    "BackupDirectory": "D:\\Backup\\",
    "LogFilePath": "D:\\Backup\\backup-log.txt",
    "IntervalInMinutes": 5
  },
  "EmailSettings": {
    "SmtpServer": "smtp.gmail.com",
    "Port": 587,
    "SenderEmail": "your-email@gmail.com",
    "SenderPassword": "your-app-password",
    "RecipientEmail": "recipient-email@gmail.com"
  }
}







