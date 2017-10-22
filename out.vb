' Автогенерация текста Visual Basic
' Данные для компиляции
Namespace Oberon07
Public Module modOut
Dim рег0 As Integer = 0
Dim рег1 As Integer = 0
Sub Main()
рег0 = 6
рег1 = рег0
рег0 = 5
рег0 = рег1 - рег0
Console.WriteLine("Result: " + Str(рег0))
End Sub
End Module
End Namespace