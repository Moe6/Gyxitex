Imports System.Net.Mail
Imports SpeedyCat
Imports Microsoft.Office.Interop
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
                If vld.UseValidDays(30) Then
                    'get  pass from config file
                    SendMail(_pass)
                Else
                    log.WriteToTemp("Validaiton failed", "MailConsole")
                End If
            Else
                log.WriteToTemp("Diff Validaiton failed", "MailConsole")
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
        Dim canSend As Boolean
        'Dim path As String = "C:\SYSPRO61\BASE\ESSBASE\"

        'convert to CSV and save 
        filename1 = ConvertToCSV(filename1)
        filename2 = ConvertToCSV(filename2)

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
            If filename1 IsNot Nothing Then
                attachment = New Attachment(filename1) 'file path
                If attachment IsNot Nothing Then
                    mail.Attachments.Add(attachment) 'attachment
                    canSend = True
                End If
            End If
            If filename2 IsNot Nothing Then
                attachment2 = New Attachment(filename2)
                If attachment2 IsNot Nothing Then
                    mail.Attachments.Add(attachment2) 'attachment
                    canSend = True
                End If
            End If

            If canSend Then
                Console.WriteLine("Sending mail...")
                SmtpServer.Send(mail)
                Console.WriteLine("Mail Sent.")
            Else
                log.WriteToTemp("Files could not be attached", "MailConsole")
            End If

        Catch ex As Exception
            log.WriteToTemp(ex.Message, "MailConsole")
        End Try
    End Sub

    Private Sub MailTo2()
        Process.Start("mailto:mtengwaa@gmail.com?subject=Please see attachment&body=Please  see the attachment")
    End Sub

    Private Function ConvertToCSV(file1 As String) As String
        Dim ogFile As String = file1
        Dim newFile As String = ""
        'Creat file path name for new file
        If ogFile.EndsWith(".xlsm") Then
            newFile = Replace(ogFile, ".xlsm", ".csv")
        End If
        If ogFile.EndsWith(".xls") Then
            newFile = Replace(ogFile, ".xls", ".csv")
        End If
        If Not DoExcelConversion(ogFile, newFile) Then
            newFile = Nothing
        End If
        Return newFile
    End Function

    Private Function DoExcelConversion(originalFile As String, newFilePath As String) As Boolean
        Dim xlApp As New Excel.Application
        Dim xWorkbook As Excel.Workbook
        xWorkbook = Nothing
        Try
            With xlApp
                .Visible = False
                .DisplayAlerts = False
            End With

            xWorkbook = xlApp.Workbooks.Open(originalFile)
            'get first worksheet
            Dim workSheet As Excel.Worksheet = xWorkbook.Worksheets(1)

            If workSheet IsNot Nothing Then
                'Convert worksheet and save
                workSheet.SaveAs(newFilePath, Excel.XlFileFormat.xlCSV)
                If FileIO.FileSystem.FileExists(newFilePath) Then
                    Return True
                End If
            End If

        Catch ex As Exception
            log.WriteToTemp(ex.Message, "MailConsole")
        Finally
            xWorkbook.Close()
            xlApp.Quit()
            ReleaseOjects(xWorkbook)
            ReleaseOjects(xlApp)
        End Try
        Return False
    End Function
    Private Sub ReleaseOjects(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

End Module
