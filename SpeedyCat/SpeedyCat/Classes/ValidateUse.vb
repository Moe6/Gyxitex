Public Class ValidateUse
    Public Property sDate As Date
    Private _trnMsg As String

    Public ReadOnly Property mesasge As String
        Get
            Return _trnMsg
        End Get
    End Property

    Public Sub New(startDate As Date)
        sDate = startDate
        _trnMsg = "App stat Null."
    End Sub

    Public Function UseValid(lapseDate As Date) As Boolean
        'Compares the lapse date specified and now. 
        'MM-dd-YY
        If Now <= lapseDate Then
            _trnMsg = "valid"
            Return True
        End If
        Return False
    End Function

    Public Function UseValidMonth(periodValidMonths As Integer)
        'Pass the number of months app is valid
        'Compares if the period has not elapsed
        Dim diff = DateDiff(DateInterval.Month, sDate, Now)
        If diff <= periodValidMonths Then
            _trnMsg = "valid"
            Return True
        End If
        Return False
    End Function

    Public Function UseValidDays(val As Integer) As Boolean
        'Pass the number of days valid from start date to now
        'Compares the days lapsed from the start date specified to now.
        Dim diff = DateDiff(DateInterval.Day, sDate, Now)
        If diff <= val Then
            _trnMsg = "valid"
            Return True
        End If
        Return False
    End Function


End Class
