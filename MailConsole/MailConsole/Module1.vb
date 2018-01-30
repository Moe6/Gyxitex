Imports System.Net.Mail
Imports SpeedyCat
Module Module1
    Private log As New SpeedyCat.Log

    Sub Main()
        Try
            'Check if config file values are not tempered with
            Dim _pass As String
            Dim crpt As New SpeedyCat.Crypto
            Dim sDt = Configuration.ConfigurationSettings.AppSettings.Item("vdu")
            Dim eDt = Configuration.ConfigurationSettings.AppSettings.Item("appstat")
            Dim oDt = Configuration.ConfigurationSettings.AppSettings.Item("dvu")
            Dim xDt = Configuration.ConfigurationSettings.AppSettings.Item("terminate")
            _pass = Configuration.ConfigurationSettings.AppSettings.Item("tip")

            Dim d1 As Date = sDt
            Dim d2 As Date = eDt
            Dim diff = DateDiff(DateInterval.Day, d1, d2)
            'compare date diff value as per crypto expected value
            If crpt.ValidateNumberInput(diff) Then
                'Validate app days/period
                Dim vld As New SpeedyCat.ValidateUse(Configuration.ConfigurationSettings.AppSettings.Item("vdu")) 'mm-dd-yyyy
                If vld.UseValidDays(25) Then
                    'get  pass from config file
                    SendMail(_pass)
                Else
                    log.WriteToTemp("Validaiton failed", "MailConsole")
                End If
            End If
        Catch ex As Exception
            log.WriteToTemp(ex.Message, "MailConsole")
        End Try

    End Sub

    Private Sub SendMail(_pwd As String)
        Dim SmtpServer As New SmtpClient()
        Dim mail As New MailMessage()
        Dim attachment As Attachment
        Dim attachment2 As Attachment
        Dim filename1 As String = Configuration.ConfigurationSettings.AppSettings.Item("file1")
        Dim filename2 As String = Configuration.ConfigurationSettings.AppSettings.Item("file2")

        'Dim path As String = "C:\SYSPRO61\BASE\ESSBASE\"
        Try
            SmtpServer.EnableSsl = True
            SmtpServer.Credentials = New Net.NetworkCredential("chlorox.reports@gmail.com", _pwd)
            SmtpServer.Port = 587
            SmtpServer.Host = "smtp.gmail.com"
            mail = New MailMessage()
            mail.From = New MailAddress("chlorox.reports@gmail.com")
            mail.To.Add("Kapil.Agarwal@clorox.com")
            mail.To.Add("Keri.Dobson@clorox.com")
            mail.To.Add("CTS.ESSBase.Support@clorox.com")
            mail.To.Add("bradley.saunders@clorox.com")
            mail.To.Add("sithuli.khumalo@gmail.com")
            mail.Subject = "Attachment"
            mail.Body = "Please see file as at " & Now

            'Attachment
            attachment = New Attachment(filename1) 'file path
            attachment2 = New Attachment(filename2)
            If attachment IsNot Nothing Then
                mail.Attachments.Add(attachment) 'attachment
            End If
            If attachment2 IsNot Nothing Then
                mail.Attachments.Add(attachment2) 'attachment
            End If
            Console.WriteLine("Sending mail...")
            SmtpServer.Send(mail)
            Console.WriteLine("Mail Sent.")
        Catch ex As Exception
            log.WriteToTemp(ex.Message, "MailConsole")
        End Try
    End Sub

    Private Sub MailTo2()
        Process.Start("mailto:mtengwaa@gmail.com?subject=Please see attachment&body=Please  see the attachment")
    End Sub



End Module
