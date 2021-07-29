Imports System.IO
Imports System.Text
Public Class Helper

    Public Sub WriteToTemp(message As String, appName As String)
        Dim fPath As String = $"C:\Temp\{appName}"
        Try
            If CheckDirExistsAndCreate($"{fPath}\LOG") Then
                Dim _fileContent As New StringBuilder
                Dim _filePath As String = $"{fPath}\LOG\{formatDateAndTime()}_{appName}.txt"
                _fileContent.AppendLine(Now.ToString)
                _fileContent.AppendLine(message)
                File.WriteAllText(_filePath, _fileContent.ToString)
            End If

        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Console.ReadLine()
        End Try
    End Sub

    Public Sub WriteToLog(message As String, appName As String)
        Dim fPath As String = $"C:\Temp\{appName}"
        Try
            If CheckDirExistsAndCreate($"{fPath}\LOG") Then
                Dim _fileContent As New StringBuilder
                Dim _filePath As String = $"{fPath}\LOG\{appName}_LOG.txt"
                With _fileContent
                    .AppendLine(Now.ToString)
                    .AppendLine(message)
                    .AppendLine("")
                    .AppendLine("-------------------------------------------------------------------------------------")
                    .AppendLine("")
                End With
                File.AppendAllText(_filePath, _fileContent.ToString)
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Console.ReadLine()
        End Try
    End Sub

    Public Function formatDateAndTime() As String
        Dim h As String = Now.Hour
        Dim m As String = Now.Minute
        Dim d As String = Now.Day
        Dim mt As String = Now.Month
        Dim yr As String = Now.Year
        Dim sc As String = Now.Second
        Dim rnr As String = yr & mt & d & h & m & sc
        Return rnr
    End Function

    Public Function FormatDate(theDate As Date) As String
        Dim _day As String = theDate.Day
        Dim _month As String = theDate.Month
        _day = _day.PadLeft(2, "0")
        _month = _month.PadLeft(2, "0")
        Return $"{theDate.Year}{_month}{_day}"
    End Function

    Public Function FormatDate2(theDate As Date) As String
        Dim _day As String = theDate.Day
        Dim _month As String = theDate.Month
        _day = _day.PadLeft(2, "0")
        _month = _month.PadLeft(2, "0")
        Return $"{theDate.Year}/{_month}/{_day}"
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

    Public Function QuoteString(toQuote As String) As String
        Dim qt As String = $"{""""}"
        Return $"{qt}{toQuote}{qt}"
    End Function

    Public Function CheckDirExistsAndCreate(dirPath As String) As Boolean
        Try
            If Not Directory.Exists(dirPath) Then
                Directory.CreateDirectory(dirPath)
                If Directory.Exists(dirPath) Then
                    Return True
                End If
            Else
                Return True
            End If
        Catch ex As Exception
            WriteToTemp(GetFullMessage(ex), "CreateDir")
        End Try
        Return False
    End Function

    Public Function GetDomainPassword(pword As String) As Security.SecureString
        Dim scurity As New Security.SecureString
        Dim len As Integer = pword.Length
        For i As Integer = 0 To len - 1
            scurity.AppendChar(pword.Chars(i))
        Next
        Return scurity
    End Function

    Public Class GetGlobals
        Public Class NumberFormats
            Public ReadOnly Property ListSeparator As String
                Get
                    Return lstSeparator
                End Get
            End Property
            Private Property lstSeparator As String

            Public ReadOnly Property DecimalSymbol As String
                Get
                    Return decSymbol
                End Get
            End Property
            Private Property decSymbol As String

            Public ReadOnly Property DigitGroupingSymbol As String
                Get
                    Return dgtGrpSymbol
                End Get
            End Property
            Private Property dgtGrpSymbol As String

            Public Sub New()
                lstSeparator = Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator
                decSymbol = Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator
                dgtGrpSymbol = Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator
            End Sub

        End Class

        Public Class CurrencyFormats

        End Class

    End Class

End Class
