'Модуль содержит необходимые константы для работы лексического анализатора
Namespace My
   Public Module модКонст
      ' для разбиения на слова
      Public Const мультилит = 0
      Public Const дилитера = 2
      Public Const монолитера = 1
      ' типы оберона
      Public Const iLong = 16
      ' Служебные определения
      Public Const кнц = -1000000 ' конец текста
      ' Символы
      Public Const iNuber = 34
      Public Const iIdent = 37
      Public Const iEnd = 40
      Public Const iModule = 63
   End Module
End Namespace