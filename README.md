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

