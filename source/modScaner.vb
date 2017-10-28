' Модуль для сканирования исходного текста
Namespace пиОк
   Public Class clsAnaliz
      Public txtTokens() As String
   End Class
   Public Module модСканер
      Dim txtOb As String = "" ' Исходный код на обероне
      Dim lit As Char ' Текущая литера из потока
      Dim txtLex() As String ' Набор лексем после разделения текста
      Dim iSym As Integer 'Распознанный символ в исходном "сыром" тексте
      Sub Symb_Get(iSym As Integer) ' Попытка повторить сканер Вирта
         Do While (Len(txtOb) > 0 And lit <= " ")
            txtOb = Mid(txtOb, 2)
         Loop
         If Len(txtOb) = 0 Then
            iSym = modConst.eot
         End If
      End Sub
      Public Sub Сканировать()
         txtOb = модФайл.txtFileO7
         модКокон.Инфо(vbCrLf + "   Исходник " + модАрг.argMod.val + ":" + vbCrLf)
         Console.WriteLine(txtFileO7 + vbCrLf)
         Symb_Get(iSym)
      End Sub
   End Module
End Namespace