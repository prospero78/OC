Namespace пиОк
   Module модОк

      Sub Main()
         Console.Clear()
         Console.Title = "Oberon-07 Compiler"
         модКокон.Сплэш_Печать()
         модАрг.Парам_Получ()
         модФайл.Init() 'создаёт необходимые структуры в модуле
         модФайл.Исх_Загрузить()
         модКомпиль2.Компилировать()
         Console.Write("Для просмотра результат запустите ""out.exe"" ")
         'Console.Read()
      end sub
   End module
End Namespace
