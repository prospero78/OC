'Модуль содержит необходимые константы для работы лексического анализатора
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
   ' Цвета расцветки
   Public Const iDefault = 0
   Public Const iKeyWord = 1
   Public Const iKeyWord2 = 2
End Module