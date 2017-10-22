'Модуль парсит параметры командной строки
Namespace пиОк
   Public Class clsArgs'Описывает структуру аргументов
      Public Dim arg As String=""
      Public Dim val As String=""
   End Class
   Public Module модАрг
      Public arg As String = System.Environment.CommandLine
      Public args() As String = Split(arg, " ")
      Public argOc As clsArgs  'Указатель на путь к компилятору
      Public argMod As clsArgs 'Указатель на модуль для компиляции
      Public optOc As Integer = 0'Число распознанных опций компилятора
      
      Public Sub Парам_Получ()
         Static NumArg As Integer = 0
         Dim i As Integer = 0
         argOc = New clsArgs()
         argMod = New clsArgs()
         'Перечисление списrа парметров
         NumArg = args.Length
         i = 0
         Do While i<NumArg
            If Len(args(i))>0 Then
               If i = 0 Then 'Указатель на сам компилятор
                  argOc.arg = "path_oc"
                  argOc.val = args(i)
               End If
               If Mid(args(i), 1, 3) = "/m:" Then
                  argMod.arg = "module"
                  argMod.val = Mid(args(i), 4)
               End If
               OptOc += 1
            End IF
            i += 1
         Loop
         If optOc < 2 Then ' аргументов недостаточно
            модКокон.Ошибка("Нет параметров для работы")
            модСправка.Справа_Показать()
         Else
            Console.WriteLine("Командная строка:  <" + arg + ">")
            Console.WriteLine("Число аргументов: <" + Str(args.Length) + ">")
            Console.WriteLine(argOc.arg+": "+argOc.val)
            Console.WriteLine(argMod.arg+":  "+argMod.val)
         End If
      End Sub
   End Module
End Namespace