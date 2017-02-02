Imports SDK
Module Module1

    Sub Main()
        Dim obj = New Sample1()
        Console.WriteLine(TypeOf obj Is Sample1)
        Console.WriteLine(Sample1.add(1, 2))
    End Sub

End Module
