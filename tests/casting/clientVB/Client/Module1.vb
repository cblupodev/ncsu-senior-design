Imports SDK
Module Module1

    Sub Main()
        Dim obj = CType(New Sample1, Sample1)
        Console.WriteLine(obj.GetType())
        Console.WriteLine(Sample1.add(1, 2))
    End Sub

End Module
