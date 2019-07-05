Imports SpeedyCat.Crypto
Module Module1

    Sub Main()
        Dim cryp As New SpeedyCat.Crypto
        Dim s1 As String
        Dim s2 As String


        Console.WriteLine("Enter String 1:")
        s1 = Console.ReadLine
        Console.WriteLine("Enter String 2:")
        s2 = Console.ReadLine
        Dim h1 = cryp.EncryptMe(s1)

        h1 = cryp.DecryptMe(s1)
        Console.WriteLine(h1)

        Dim h2 = cryp.EncryptMe(s2)
        Console.WriteLine("")
        Console.WriteLine("Encryption....")

        If cryp.HashCompareMe(h1, h2) Then
            Console.WriteLine(h1)
            Console.WriteLine(h2)
            Console.WriteLine("Input matches....")
        Else
            Console.WriteLine(h1)
            Console.WriteLine(h2)
            Console.WriteLine("Input does not match....")
        End If
        h1 = cryp.DecryptMe(s1)
        Console.WriteLine(h1)
        Console.ReadLine()
    End Sub

End Module
