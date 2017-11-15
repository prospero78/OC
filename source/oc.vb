Namespace My
   Module модОк

      Sub Main()
         Console.Clear()
         Console.Title = "Oberon-07 Compiler"
         модКокон.Сплэш_Печать()
         модАрг.Парам_Получ()
         If модАрг.sRes <> "err" Then
            модФайл.Init()  'создаёт необходимые структуры в модуле
            модФайл.Исх_Загрузить()
         End If
         If модФайл.sRes <> "err" Then
            модКомпиль2.Компилировать()
            Console.Write("Для просмотра результат запустите ""out.exe"" ")
         End If
         Console.Read()
      End Sub
   End Module
End Namespace
