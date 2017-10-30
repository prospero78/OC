Namespace пиОк
   ''' <summary>
   '''  Класс описывает еденицу типа
   ''' </summary>
   Public Class clsType
      Inherits clsLex
      Public strBase As String = "_none_" ' базовый класс
      Public strName As String = "_none_" ' основа типа (ARRAY, POINTER, RECORD)
      Public strOf As String = "_none_" ' из чег осостоит (если массив)
      Public lenAr As Integer = 0 ' размер типа если это массив
      Public Sub New(_strTag As String, _coord As clsCoord)
         MyBase.New(_strTag, _coord)
      End Sub
      ''' <summary>
      ''' Ошибка в разделителе между именем типа и описанием типа
      ''' </summary>
      ''' <param name="txtLine">Строка с описанием типа</param>
      ''' <param name="_mLex">Лекссема с ошибкой</param>
      Public Sub ErrorTerminal(txtLine As String, _mLex As clsLex)
         модКокон.Ошибка("Крд: " + Str(_mLex.coord.iStr) + " -" + Str(_mLex.coord.iPos))
         Console.WriteLine(txtLine)
         Console.WriteLine(Смещ(_mLex.coord.iPos))
         модКокон.Ошибка("Ошибочный разделитель между именем типа и его описателем")
         Environment.Exit(1)
      End Sub
      ''' <summary>
      '''  Сообщение, при неверном определении типа
      ''' </summary>
      ''' <param name="txtLine">Строка с неверным определением</param>
      ''' <param name="_mLex">Ошибочная лексема</param>
      Public Sub ErrorKeywordType(txtLine As String, _mLex As clsLex)
         модКокон.Ошибка("Крд: " + Str(_mLex.coord.iStr) + " -" + Str(_mLex.coord.iPos))
         Console.WriteLine(txtLine)
         Console.WriteLine(Смещ(_mLex.coord.iPos))
         модКокон.Ошибка("Ошибочный описатель типа")
         Environment.Exit(1)
      End Sub
   End Class
   ''' <summary>
   ''' Модуль предоставляет процедуры и кассы для обработки типов
   ''' </summary>
   Public Module modType
      ''' <summary>
      ''' Выводит ошибку, что для опеределения типа-массива должно использоваться целое число
      ''' </summary>
      ''' <param name="txtLine"></param>
      ''' <param name="_lex"></param>
      Public Sub ErrorNumArray(txtLine As String, _lex As clsLex)
         модКокон.Ошибка("Крд: " + Str(_lex.coord.iStr) + " -" + Str(_lex.coord.iPos))
         Console.WriteLine(txtLine)
         Console.WriteLine(Смещ(_lex.coord.iPos))
         модКокон.Ошибка("Размерность типа-массива должна быть целым числом")
         Environment.Exit(1)
      End Sub
      ''' <summary>
      '''  Выводит сообщение, что после размерности массива должно идти кючевое слово ТО
      ''' </summary>
      ''' <param name="txtLine"></param>
      ''' <param name="_lex"></param>
      Public Sub ErrorOfArray(txtLine As String, _lex As clsLex)
         модКокон.Ошибка("Крд: " + Str(_lex.coord.iStr) + " -" + Str(_lex.coord.iPos))
         Console.WriteLine(txtLine)
         Console.WriteLine(Смещ(_lex.coord.iPos))
         модКокон.Ошибка("Должно быть OF после целого числа")
         Environment.Exit(1)
      End Sub
      ''' <summary>
      '''  Выводит сообщение, что элементы типа-массива должно быть именем
      ''' </summary>
      ''' <param name="txtLine"></param>
      ''' <param name="_lex"></param>
      Public Sub ErrorToElArray(txtLine As String, _lex As clsLex)
         модКокон.Ошибка("Крд: " + Str(_lex.coord.iStr) + " -" + Str(_lex.coord.iPos))
         Console.WriteLine(txtLine)
         Console.WriteLine(Смещ(_lex.coord.iPos))
         модКокон.Ошибка("Элементы массива должны быть типом")
         Environment.Exit(1)
      End Sub
      ''' <summary>
      '''  Выводит сообщение, что тип-массив должен заанчиваться разделителем
      ''' </summary>
      ''' <param name="txtLine"></param>
      ''' <param name="_lex"></param>
      Public Sub ErrorEndArray(txtLine As String, _lex As clsLex)
         модКокон.Ошибка("Крд: " + Str(_lex.coord.iStr) + " -" + Str(_lex.coord.iPos))
         Console.WriteLine(txtLine)
         Console.WriteLine(Смещ(_lex.coord.iPos))
         модКокон.Ошибка("Тип-массив должен заканчиваться разделителем <;>")
         Environment.Exit(1)
      End Sub
      ''' <summary>
      '''  Выбирает из базовых типов, и если не подходит возвращает _not_
      ''' </summary>
      ''' <param name="_str"></param>
      Public Function SelectType(_str As String) As String
         Dim _res As String = ""
         If _str = "BOOLEAN" Then
            _res = "BOOLEAN"
         ElseIf _str = "CHAR" Then
            _res = "CHAR"
         ElseIf _str = "INTEGER" Then
            _res = "INTEGER"
         ElseIf _str = "REAL" Then
            _res = "REAL"
         ElseIf _str = "BYTE" Then
            _res = "BYTE"
         ElseIf _str = "SET" Then
            _res = "SET"
         ElseIf modUtil.ЕслиИмя(_str) = "_name_" Then
            _res = _str
         Else
            ' такого имени типа быть не может!!
            _res = "_not_"
         End If
         Return _res
      End Function
   End Module
End Namespace