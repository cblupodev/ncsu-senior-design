Imports SDK
Module Module1

    Public item As Sample1

    Sub Main()
        Console.WriteLine(Sample1.add(1, 2))
        item = New Sample1()
        Console.WriteLine(item.mul(3, 5))
        Dim item2 As Sample1 = New Sample1()
        Console.WriteLine(item.mul(4, 2))
    End Sub

End Module
