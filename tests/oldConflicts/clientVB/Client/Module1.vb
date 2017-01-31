Imports SDK
Module Module1
    Class Sample1
        Shared Function minus(a, b)
            Return a - b
        End Function
    End Class
    Sub Main()
        Console.WriteLine(SDK.Sample1.add(1, 2))
        Console.WriteLine(Sample1.minus(3, 2))
    End Sub

End Module
