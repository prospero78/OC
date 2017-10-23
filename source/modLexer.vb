' Модуль разбивает исходник на лексемы
Namespace пиОк
   Public Class clsLexem ' Этот класс содержит две позиции: номер строки и сама строка
   End Class
   Public Module модЛексер
      Public Dim txtSourceClean as String
      Public Sub Исх_Очистить() ' Удаляет всякую фигню, типа литер новых строк и двойных пробелов.
      Dim tmpSource As String
      Dim lit As String
         tmpSource = модФайл.txtFileO7
         
         ' Пропуск всех ненужных символов
         Do While Len(tmpSource) >0
            lit = Mid(tmpSource,1,1)
            tmpSource = Mid(tmpSource,2)
            If lit>=" " Then
               txtSourceClean += lit
            End IF
         Loop
         
         ' Выводим что получилось
         Dim count As Integer = 1
         For i As Integer = 1 To Len(txtSourceClean)
            Console.Write(Mid(txtSourceClean,i,1))
            If count = 50 Then
               Console.Write(vbCrLf)
               count=1
            End If
         Next
         Console.Write(vbCrLf)
      End Sub
   End Module
End Namespace
