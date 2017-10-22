Namespace пиОк

   Public Class clsWinMain
    
   End Class
   
   Module модОк

      Sub Main()
         Console.Clear()
         Console.Title = "Oberon-07 Compiler"
         модКокон.Сплэш_Печать()
         модАрг.Парам_Получ()
         модФайл.Init()'создаёт необходимые структуры в модуле
         модФайл.о7_Загрузить()
         модКомпиль.Компилировать()
         Console.Write("Нажмите любую клавишу для выхода")
         Console.Read()
      end sub
   End module
End Namespace
