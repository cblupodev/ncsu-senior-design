Imports SDK
Module Module1

    Sub Main()
        Dim abc = New LinkedList(Of Sample1)
        abc.AddLast(New Sample1())
        Console.WriteLine(Sample1.add(1, 2))
        Console.WriteLine(abc.Count)
    End Sub

End Module
