' Модуль разбивает исходник на лексемы

Namespace пиОк
   ''' <summary>
   ''' Хранит одну сторку исходника
   ''' </summary>
   Public Class туЗвСтрока
      Inherits туЗвено
      Public стрСтрока As String = ""
      Public Sub New(Optional пЗвПред As туЗвено = Nothing, Optional пЗвСлед As туЗвено = Nothing)
         Me.звПред = пЗвПред
         Me.звСлед = пЗвСлед
      End Sub
   End Class
   Public Class туИсходник
      Dim стрИсходникаТекущая As String = "" ' одиночная текущая строка исходника
      Public мСтроки() As String ' массив строк исходного текста
      Public коорд As туКоорд
      Dim _лит As String = ""  ' операционная литера
      Public ReadOnly Property длина As Integer ' длина текущего текста
         Get
            Return Me.тхтИсхТекущ.Length
         End Get
      End Property
      Public ReadOnly Property лит As String ' возвращает очередную литеру
         Get
            If Me.длина > 0 Then
               Me._лит = Mid(Me.тхтИсхТекущ, 1, 1)
               Me.тхтИсхТекущ = Mid(Me.тхтИсхТекущ, 2)
               If Me._лит = vbLf Then
                  Me.коорд.цСтр_Уст(Me.коорд.цСтр + 1)
                  Me.коорд.цПоз_Уст(0)
                  If IsNothing(Me.мСтроки) Then
                     ReDim Me.мСтроки(0)
                  Else
                     ReDim Preserve Me.мСтроки(Me.мСтроки.Length)
                  End If
                  Me.мСтроки(Me.мСтроки.Length - 1) = Me.стрИсходникаТекущая
                  Console.WriteLine("туИсходник.стрИсходникаТекущая: " + Me.стрИсходникаТекущая)
                  Me.стрИсходникаТекущая = ""
               Else
                  Me.коорд.цПоз_Уст(Me.коорд.цПоз + 1)
                  Me.стрИсходникаТекущая += Me._лит
               End If
            End If
            Return Me._лит
         End Get
      End Property
      Public ReadOnly Property lit2 As String
         Get
            Return Mid(Me.тхтИсхТекущ, 1, 1)
         End Get
      End Property
      Public ReadOnly тхтИсхОригин As String ' исходный код оригинальный
      Public тхтИсхТекущ As String ' исходный код текущий
      Public Sub New(_тхт As String)
         Me.коорд = New туКоорд()
         Me.тхтИсхОригин = _тхт + vbCrLf + vbCrLf
         Me.тхтИсхТекущ = _тхт + vbCrLf + vbCrLf
         'Console.WriteLine("clsTxtSource.New() " + _txt)
      End Sub
   End Class
   ''' <summary>
   ''' Содержит имя объекта и методы обработки имени
   ''' </summary>
   Public Class clsName
      Dim _name As String = ""
      ''' <summary>
      ''' Строкове обозначение имени объекта.
      ''' </summary>
      ''' <returns>возвращает строкове значение</returns>
      Public Property strVal() As String
         Get
            Return Me._name
         End Get
         Set(value As String)
            Dim res As String
            res = модУтиль.ЕслиИмя(value)
            If res = "_name_" Then
               Me._name = value
            Else
               модКокон.Ошибка(Me._name + ":" + res + " val=" + value)
               Environment.Exit(1)
            End If
         End Set
      End Property
      Public Sub New(_Name As String)
         Me._name = _Name
      End Sub
   End Class
   '''<summary>
   '''Класс хранит координаты позиции кода в исходном месте. Позволяет сканировать текст на новую строку.
   '''</summary>
   Public Class туКоорд
      '''<summary>
      '''Позиция тега в строке
      '''</summary>
      Dim _цПоз As Integer = 0
      Public ReadOnly Property цПоз As Integer
         Get
            Return Me._цПоз
         End Get
      End Property
      Public Sub цПоз_Уст(value As Integer)
         Me._цПоз = value
      End Sub
      '''<summary>
      '''Номер строки в исходном тексте, содержащий текущий тег
      '''</summary>
      Dim _цСтр As Integer = 0
      Public ReadOnly Property цСтр As Integer
         Get
            Return Me._цСтр
         End Get
      End Property

      Public Sub цСтр_Уст(value As Integer)
         Me._цСтр = value
      End Sub

      Public Sub New(Optional str_ As Integer = 0, Optional pos_ As Integer = 0)
         If pos_ < 0 Then
            модКокон.Ошибка("Позиция в строке не может быть отрицательной val=" + pos_.ToString())
            Environment.Exit(1)
         Else
            Me._цПоз = pos_
         End If
         If str_ < 0 Then
            модКокон.Ошибка("Номер строки не может быть отрицательной val=" + str_.ToString())
            Environment.Exit(1)
         Else
            Me._цСтр = str_
         End If
      End Sub
      '''<summary>
      '''По литере определяет налчие новой строки. В любом случае, обновляет координаты
      '''</summary>
      Public Sub Coord_Update(str_ As Integer, pos_ As Integer)
         Me._цПоз = pos_
         Me._цСтр += str_
      End Sub
   End Class
   Public Class туЛекс
      ' Стили именования
      ' хранит в себе последовательно кусочек нераспознанного кода с координатами
      Public уКоорд As туКоорд
      Public strTag As String = ""
      Public Sub New(_strTag As String, _coord As туКоорд)
         Me.уКоорд = _coord
         Me.strTag = _strTag
      End Sub
   End Class
   Public Class тЦепьЛекс
      Public tags() As туЛекс
      Public Function ЕслиОтсев(lit As String) As Boolean
         Dim res As Boolean = False
         If lit <= " " Then
            res = True
         End If
         Return res
      End Function
      Public Sub Add(_lit As String, _coord As туКоорд) 'Создать новый тэг
         Dim tag As туЛекс = New туЛекс(_lit, _coord)
         'Console.WriteLine("clsTagList.Add(): " + _lit + " " + Str(_str) + " " + Str(_pos))
         If IsNothing(Me.tags) Then
            ReDim Me.tags(0)
         Else
            ReDim Preserve tags(tags.Length)
         End If
         Me.tags(Me.tags.Length - 1) = tag
      End Sub
      Public ReadOnly Property len As Integer
         Get
            If IsNothing(Me.tags) Then
               ReDim Me.tags(0)
            End If
            Return Me.tags.Length
         End Get
      End Property
      Default ReadOnly Property strTag(i As Integer) As String
         Get
            модУтиль.КОНТРОЛЬ(Not IsNothing(Me.tags(i)), "clsTaggerList.strTag: Элемент списка пустой! i=" + Str(i))
            Return Me.tags(i).strTag

         End Get
      End Property
      Public Sub New()

      End Sub
      Public Function ClassTag(lit As String) As Integer
         ' описывает типы тегов в зависимости от длины:
         '   синглетег -- одиночная литера
         '   дублетег -- двойная литера
         '   мультитег -- мультилитера
         Dim res As Integer = -1

         If InStr(",;-+.[""]'=", lit) > 0 Then
            res = modConst.singletag
         ElseIf InStr(":<>()*", lit) > 0 Then
            res = modConst.doubletag
         ElseIf модУтиль.ЕслиЦифра(lit) Or модУтиль.ЕслиБуква(lit) Or lit = "_" Then
            res = modConst.multitag
         End If
         Return res
      End Function
      Public Sub Tagging(_src As туИсходник)
         Dim гсТег As String = ""
         Dim lit As String = ""
         Dim lit2 As String = ""
         Dim _tmp As String = "" ' перменная для правильного пропуска doubletag
         lit = txtSrc.лит
         Do While _src.длина > 1 ' общий цикл с правильными литерами
            If Me.ЕслиОтсев(lit) Then ' отбросим мусор
               ' Пропуск символов-мусора
               lit = txtSrc.лит
            End If
            If Me.ClassTag(lit) = modConst.singletag Then ' если тег-одиночка
               'Console.WriteLine("singletag-1")
               Me.Add(lit, txtSrc.коорд)
               lit = txtSrc.лит
            End If
            If Me.ClassTag(lit) = modConst.doubletag Then ' если сложный тег
               lit2 = txtSrc.lit2
               If InStr(":><", lit) > 0 And lit2 = "=" Then
                  'Console.WriteLine("doubletag-1")
                  Me.Add(lit + lit2, txtSrc.коорд)
                  _tmp = txtSrc.лит
               ElseIf lit = "(" And lit2 = "*" Then
                  'Console.WriteLine("doubletag-2")
                  Me.Add(lit + lit2, txtSrc.коорд)
                  _tmp = txtSrc.лит
               ElseIf lit = "*" And lit2 = ")" Then
                  'Console.WriteLine("doubletag-3")
                  Me.Add(lit + lit2, txtSrc.коорд)
                  _tmp = txtSrc.лит
               End If
               lit = txtSrc.лит
            End If
            If Me.ClassTag(lit) = modConst.multitag Then
               Do While Me.ClassTag(lit) = modConst.multitag ' если многосимвольный тэг
                  гсТег += lit
                  lit = txtSrc.лит
               Loop
               Me.Add(гсТег, txtSrc.коорд)
               гсТег = ""
            End If
         Loop
      End Sub
   End Class
   ''' <summary>
   ''' попытка сделать по уму
   ''' </summary>
   Public Module модЛексер
      Public tags As тЦепьЛекс
      Public txtSrc As туИсходник ' исходник разбитый построчно
      ' =================== Лексирование =======================
      Public Sub Лексемы_Разметить()
         tags = New тЦепьЛекс()
         txtSrc = New туИсходник(модФайл.txtFileO7 + "  ") ' хвост нужен, чтобы гарантированно не обрезать тег
         tags.Tagging(txtSrc)
      End Sub
   End Module
End Namespace
