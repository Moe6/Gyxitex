Imports System.Security.Cryptography
Imports System.Text
Public Class Crypto
    Private Const kPepper As String = "UnotambaIwe"
    Public Function EncryptMe(val As String) As String
        Return HashMe(val)
    End Function

    Public Function HashCompareMe(hash1 As String, hash2 As String) As Boolean
        Return HashMe(hash1) = HashMe(hash2)
    End Function

    Private Function HashMe(data As String) As String
        Dim bytSorde() As Byte
        Dim bytHash() As Byte
        Dim rv As String

        'Create a byte arraya from source data
        bytSorde = ASCIIEncoding.ASCII.GetBytes(data & kPepper)
        'compute hassh based on data source 
        bytHash = New MD5CryptoServiceProvider().ComputeHash(bytSorde)
        rv = ByteArrayToString(bytHash)
        Return rv
    End Function

    Private Function ByteArrayToString(ByVal arrInput() As Byte) As String
        Dim i As Integer
        Dim sOUtput As New StringBuilder(arrInput.Length)
        For i = 0 To arrInput.Length - 1
            sOUtput.Append(arrInput(i).ToString("X2"))
        Next
        Return sOUtput.ToString()
    End Function


    Public Function ValidateNumberInput(n1 As Integer) As Boolean
        If HashCompareMe(n1, 6) Then
            Return True
        End If
        Return False
    End Function

    Public Function DecryptMe(gbrish As String) As String
        Dim byt As Byte()

        Return ByteArrayToString(byt)
    End Function
End Class
