using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace SqlBackupConsole1
{
  public  class SqlBackupWorker : BackgroundService
    {

        private readonly ILogger<SqlBackupWorker> _logger;
        private readonly string backupDir = @"D:\Backup\";

        public SqlBackupWorker(ILogger<SqlBackupWorker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await RunBackupAsync();
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken); // Run every 5 minutes
            }
        }

        public async Task RunBackupAsync()
        {
            string connectionString = "Server=localhost;Database=Students;Integrated Security=true;Encrypt=False;";

            string databaseName = "Students";
            string folderName = DateTime.Now.ToString("yyyy-MM");
            string targetDir = Path.Combine(backupDir, folderName);

            Directory.CreateDirectory(targetDir);

            string dateSuffix = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string backupPath = Path.Combine(targetDir, $"{databaseName}_Backup_{dateSuffix}.bak");

            string backupQuery = $@"
        BACKUP DATABASE [{databaseName}]
        TO DISK = N'{backupPath}'
        WITH FORMAT, INIT, NAME = N'{databaseName} Full Backup', COMPRESSION;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(backupQuery, conn))
                {
                    conn.Open();
                    await cmd.ExecuteNonQueryAsync();
                }

                string logMessage = $"{DateTime.Now}: ✅ Backup succeeded - File: {backupPath}";
                 LogBackupResult(folderName, logMessage);
                await SendEmail("✅ Backup Successful", logMessage);
                _logger.LogInformation(logMessage);
            }
            catch (Exception ex)
            {
                string logMessage = $"{DateTime.Now}: ❌ Backup failed - Error: {ex.Message}";
                 LogBackupResult(folderName, logMessage);
                await SendEmail("❌ Backup Failed", logMessage);
                _logger.LogError(ex, logMessage);
            }
        }

        private void LogBackupResult(string folderName, string message)
        {
            string monthlyLogPath = Path.Combine(backupDir, folderName, "backup_log.txt");
            File.AppendAllText(monthlyLogPath, message + Environment.NewLine);
        }


        private async Task SendEmail(string subject, string body)
        {
            var message = new MailMessage
            {
                From = new MailAddress("kethanannam@gmail.com"),
                Subject = subject,
                Body = body
            };
            message.To.Add("kethanannam@gmail.com");

            using (var client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("kethanannam@gmail.com", "rsms yqow lqtr uaik");
                client.EnableSsl = true;
                await client.SendMailAsync(message);
            }
        }
    }
}

