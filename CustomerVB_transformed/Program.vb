
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports DBConnector
'test
' test 
Imports testAlias = FujitsuSDKOld
Imports oldNamespace


Namespace Customer
    <ModelIdentifier("26A993C4-65C6-4168-B045-9FABB2A1526D")>
    Class Program
        Inherits oldNamespace.OldClass

        Private a As New testAlias.Sample()
        'this is a comment to test keeping track of trivia
        Private b As New oldNamespace.OldClass()

        Public Shared Sub Main(args As String())
            Dim s As New FujitsuSDKOld.Sample()
            ' this is also a test comment
            Dim t As New oldNamespace.OldClass()

            Dim cast As OldClass = DirectCast(New Casting(), OldClass)
        End Sub

        Public Function testMethod(a As OldClass) As OldClass
            Dim test As Dictionary(Of [String], oldNamespace.OldClass) = New Dictionary(Of String, OldClass)()
            Dim b As New OldClass()
            Return b
        End Function
    End Class
End Namespace

'=======================================================
'Service provided by Telerik (www.telerik.com)
'Conversion powered by NRefactory.
'Twitter: @telerik
'Facebook: facebook.com/telerik
'=======================================================
