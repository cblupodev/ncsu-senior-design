Imports SDK
Module Module1

    Sub Main()
        Console.WriteLine(Sample1.add(1, 2))
        Console.WriteLine(GetSDKClass().mul(3, 5))
    End Sub

    Function GetSDKClass()
        Return New Sample1()
    End Function

End Module
