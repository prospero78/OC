' Главный модуль компилятора
Namespace пиОк
   Public Module модКомпиль
      Dim литАнализ As String ' Look литера для анализа
      
      Sub Лит_Получ() 'GetChar получение символа из входного потока
         литАнализ = Str(Console.Read())
      End Sub
      
      Sub Ошибка(ByRef msg As String) ' Error вывод сообщения об ошибке
         Console.WriteLine()
         модКокон.Ошибка("   Ошибка: " + msg)
      End Sub
      
      Sub Прервать(ByRef msg As String) '  Abort Прерывание компиляции
         Ошибка(msg)
         Exit Sub 
      End Sub
      
      Sub Ожидалось(ByRef msg As String) ' Expected
         Прервать(msg + " ожидалось")
      End Sub
      
      Sub Совпадение(lit As String) '  Math
         If литАнализ = lit Then
            Лит_Получ()
         Else
            Ожидалось("'" + lit + "'")
         End If
      End Sub
      
      Function ЕслиБуква(lit As String) As Boolean '  IsAlpha
         Dim res As Boolean = False
         lit = UCase(lit)
         If InStr("ABCDEFGHIJKLMNOPQRSTUVWXYZ", lit) <> 0 Then
            res = True
         End If
         If InStr("АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ", lit)<>0 Then
            res = True
         End If
         Return res
      End Function
      
      Function ЕслиЦифра(lit As String) As Boolean '  IsDigit
         Dim res As Boolean = False
         If InStr("0123456789", lit)<>0 Then
            res = True
         End If
         Return res
      End Function
      
      Function Имя_Получ() As String ' GetName
         If Not ЕслиБуква(литАнализ) Then
            Ожидалось("Имя")
         End If
         Dim lit As String = UCase(литАнализ)
         Лит_Получ()
         Return lit
      End Function
      
      Sub Вывод(ByVal txt As String) ' Emit
         Console.Write(vbTab, txt)
      End Sub
      
      Sub Настр() ' Init
         Лит_Получ()
      End Sub
      
      Public Sub Компилировать()
         Настр()
      End Sub
   End Module
End Namespace