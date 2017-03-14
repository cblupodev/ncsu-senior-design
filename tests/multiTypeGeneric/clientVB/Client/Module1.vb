Imports SDK
Module Module1

    Sub Main()
        Console.WriteLine(Sample1.add(1, 2))
        Dim map = New Dictionary(Of Sample1, Sample1)
        Dim itemA = New Sample1()
        Dim itemB = New Sample1()
        map.Add(itemA, itemB)
        Console.WriteLine(map.Count)
        Console.WriteLine(If(map.Item(itemA).Equals(itemB), 1, 0))
    End Sub

End Module
