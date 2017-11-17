' Автогенерация текста Visual Basic
' Данные для компиляции
Public Module modOut

   Dim рег0 As Integer = 0
   Dim рег1 As Integer = 0
   Dim head As Integer = 0
   Dim стек(1000) As Integer ' программный стек
   Dim sp As Integer = 0 'указатель стека

   Sub push(arg As Integer)
      If (sp + 1) < 1000 Then
         sp += 1
      Else
         Console.WriteLine("ВНИМАНИЕ! Стек переполнен!!!")
      End If
      стек(sp) = arg
   End Sub

   Sub pop(ByRef arg As Integer)
      arg = стек(sp)
      If (sp - 1) >= 0 Then
         sp -= 1
      Else
         Console.WriteLine("ВНИМАНИЕ! Стек пустой!!!")
      End If
   End Sub

   Dim W As Integer = 0
   Sub Main()
      рег0 = 2
      push(рег0)
      рег0 = 3
      pop(head)
      рег0 += head
      W = рег0
      Console.WriteLine("Result: " + Str(рег0))
   End Sub
End Module