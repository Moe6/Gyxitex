Imports System.IO
Imports System.Text

Public Class Log

    Public Sub WriteToTemp(message As String, appName As String)
        Try
            Dim _fileContent As New StringBuilder
            Dim _filePath As String = My.Application.Info.DirectoryPath & formatDateAndTime() & "_" & appName & ".txt"
            _fileContent.AppendLine(Now.ToString)
            _fileContent.AppendLine(message)
            File.WriteAllText(_filePath, _fileContent.ToString)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Console.ReadLine()
        End Try
    End Sub
    Private Function formatDateAndTime() As String
        Dim h As String = Now.Hour
        Dim m As String = Now.Minute
        Dim d As String = Now.Day
        Dim mt As String = Now.Month
        Dim yr As String = Now.Year
        Dim sc As String = Now.Second
        Dim rnr As String = yr & mt & d & h & m & sc
        Return rnr
    End Function
    Public Function GetFullMessage(ex As Exception) As String
        Dim strBuild As New System.Text.StringBuilder
        If ex IsNot Nothing Then
            strBuild.Append("Message:" & Environment.NewLine & ex.Message & Environment.NewLine & Environment.NewLine)
            'Get Inner Exception
            If ex.InnerException IsNot Nothing Then
                If ex.InnerException.Message IsNot Nothing Then
                    strBuild.Append("Inner Exception:" & Environment.NewLine & ex.InnerException.Message _
                                    & Environment.NewLine & Environment.NewLine)
                End If
                If ex.InnerException.StackTrace IsNot Nothing Then
                    strBuild.Append("Inner Exception Stack:" & Environment.NewLine & ex.InnerException.StackTrace _
                                    & Environment.NewLine & Environment.NewLine)
                ElseIf ex.StackTrace IsNot Nothing Then
                    strBuild.Append("Initial Stack Trace:" & Environment.NewLine & ex.StackTrace)
                End If
                'Get main stack trace
            ElseIf ex.StackTrace IsNot Nothing Then
                strBuild.Append("Initial Stack Trace:" & Environment.NewLine & ex.StackTrace)

            End If
        End If

        Return strBuild.ToString
    End Function
End Class
