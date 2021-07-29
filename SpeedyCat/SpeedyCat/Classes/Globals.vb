Namespace SpeedyCat
    Public Class MyGlobals
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

        Public Class DateFormats

            Public Sub New()

            End Sub
        End Class

    End Class

End Namespace
