Imports SDK.FirstSpace
Imports SDK.SecondSpace
Module Module1

    Sub Main()
        Console.WriteLine(Sample1.add(1, 2))
        Module2.Run()
        Module3.Run()
    End Sub

End Module

Module Module3
    Sub Run()
        Console.WriteLine(Sample4.add5(1, 2, 3, 4, 5))
    End Sub
End Module