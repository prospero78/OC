' Модуль разбивает исходник на лексемы

Namespace пиОк
   Public Class клсТег
      ' хранит в себе последовательно кусочек нераспознанного кода с координатами
      Public цСтр As Integer = 0
      Public цПоз As Integer = 0
      Public стрТег As String = ""
   End Class
   Public Module модТеггер
      ' попытка сделать по уму
      Const multitag = 0
      Const doubletag = 2
      Const singletag = 1
      Public теги() As клсТег
      Dim txtSrc As String = "" ' текст исходника
      Public txtLine() As String ' исходник разбитый построчно
      Public гцСтр As Integer = 0
      Dim гцПоз As Integer = 0
      ' =================== ТЕГИРОВАНИЕ =======================
      Sub Поз_Получ(lit As String)
         Static srcLine As String = "" ' очередная строка исходного кода
         If lit = vbLf Then

            гцПоз = 0
            ' Добавить строку в массив исходников
            If IsNothing(txtLine) Then
               ReDim txtLine(0)
            Else
               ReDim Preserve txtLine(гцСтр + 1)
            End If
            txtLine(гцСтр) = srcLine
            гцСтр += 1
            srcLine = ""
         Else
            гцПоз += 1
            srcLine += lit
         End If
      End Sub
      Function ЕслиОтсев(lit As String) As Boolean
         Dim res As Boolean = False
         If lit <= " " Then
            res = True
         End If
         Return res
      End Function
      Sub Тег_Добавить(lit As String)
         'Создать новый тэг
         Dim tag As клсТег = New клсТег With {
             .стрТег = lit,
             .цСтр = гцСтр,
             .цПоз = гцПоз
         }
         If IsNothing(теги) Then
            ReDim Preserve теги(0)
         Else
            ReDim Preserve теги(теги.Length)
         End If
         теги(теги.Length - 1) = tag
      End Sub
      Function ЕслиВнутрТег(lit As String) As Integer
         Dim res As Integer = -1
         If InStr(",(;)*-+.[""]'=", lit) > 0 Then
            res = singletag
         ElseIf InStr(":<>", lit) > 0 Then
            res = doubletag
         ElseIf InStr("0123456789_", lit) > 0 Or
              InStr("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvw", lit) > 0 Or
              InStr("АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя", lit) > 0 Then
            res = multitag
         End If
         Return res
      End Function
      Public Sub Тег_Разметить()
         Dim lit, lit2 As String
         Dim гсТег As String = "" ' глобальный текущий тэг
         txtSrc = модФайл.txtFileO7 + "  " ' хвост нужен, чтобы гарантированно не обрезать тег
         lit = Mid(txtSrc, 1, 1)
         Поз_Получ(lit)
         Do While Len(txtSrc) > 1 ' общий цикл с правильными литерами
            If ЕслиОтсев(lit) Then ' отбросим мусор
               ' Пропуск символов-мусора
               txtSrc = Mid(txtSrc, 2)
               lit = Mid(txtSrc, 1, 1)
               Поз_Получ(lit)
            End If
            If ЕслиВнутрТег(lit) = singletag Then ' если тег-одиночка
               Тег_Добавить(lit)

               txtSrc = Mid(txtSrc, 2)
               lit = Mid(txtSrc, 1, 1)
               Поз_Получ(lit)
            End If
            If ЕслиВнутрТег(lit) = doubletag Then ' если сложный тег
               lit2 = Mid(txtSrc, 2, 1)
               If InStr("=", lit2) > 0 Then
                  txtSrc = Mid(txtSrc, 2)
                  Поз_Получ(lit2)
                  Тег_Добавить(lit + lit2)
               Else
                  Тег_Добавить(lit)
               End If
               txtSrc = Mid(txtSrc, 2)
               lit = Mid(txtSrc, 1, 1)
               Поз_Получ(lit)
            End If
            If ЕслиВнутрТег(lit) = multitag Then

               Do While ЕслиВнутрТег(lit) = multitag ' если многосимвольный тэг
                  гсТег += lit
                  txtSrc = Mid(txtSrc, 2)
                  lit = Mid(txtSrc, 1, 1)
                  Поз_Получ(lit)
               Loop
               Тег_Добавить(гсТег)
               гсТег = ""
            End If
         Loop

         'If Not IsNothing(теги) Then
         '   Console.WriteLine("Вывод лексера:")
         '   Console.WriteLine("Len(теги)" + Str(теги.Count))
         '   Dim i As Integer = 0
         '   Do While i < теги.Count
         '      Console.WriteLine(Str(i) + ":" + теги(i).стрТег)
         '      i += 1
         '   Loop
         'End If
      End Sub
   End Module
End Namespace
