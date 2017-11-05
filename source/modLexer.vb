' Модуль разбивает исходник на лексемы

Namespace пиОк
   Public Class туИсходник
      Dim _стрИсходника As String = "" ' одиночная текущая строка исходника
      Dim _мСтроки() As String ' массив строк исходного текста
      Default Public ReadOnly Property строки(i As Integer) As String ' массив строк исходников
         Get
            modUtil.ASSERT(IsNothing(Me._мСтроки(i)), "Пустая строка!")
            Return Me._мСтроки(i)
         End Get
      End Property
      Public ReadOnly Property txt As String ' исходный код оперативный 
         Get
            Return Me._txt_current
         End Get
      End Property
      Public ReadOnly Property origin As String
         Get
            Return Me._txt_origin
         End Get
      End Property
      Public коорд As clsCoord
      Dim _lit As String = ""
      Public ReadOnly Property lit As String ' возвращает очередную литеру
         Get
            If Me._txt_current.Length > 0 Then
               Me._lit = Mid(Me._txt_current, 1, 1)
               Me._txt_current = Mid(Me._txt_current, 2)
               If Me._lit = vbLf Then
                  Me.коорд.iStr += 1
                  Me.коорд.iPos = 0
                  If IsNothing(Me._мСтроки) Then
                     ReDim Me._мСтроки(0)
                  Else
                     ReDim Preserve Me._мСтроки(Me._мСтроки.Length)
                  End If
                  Me._мСтроки(Me._мСтроки.Length - 1) = Me._стрИсходника
                  Console.WriteLine("clsTxtSource._strSource: " + Me._стрИсходника)
                  Me._стрИсходника = ""
               Else
                  Me.коорд.iPos += 1
                  Me._стрИсходника += Me._lit
               End If
            End If
            Return Me._lit
         End Get
      End Property
      Public ReadOnly Property lit2 As String
         Get
            Return Mid(Me._txt_current, 1, 1)
         End Get
      End Property
      Public ReadOnly Property len As Integer ' длина текста
         Get
            Return Me._txt_current.Length
         End Get
      End Property
      Dim _txt_origin As String ' исходный код оригинальный
      Dim _txt_current As String ' исходный код текущий
      Public Sub New(_txt As String)
         Me.коорд = New clsCoord()
         Me._txt_origin = _txt + vbCrLf + vbCrLf
         Me._txt_current = _txt + vbCrLf + vbCrLf
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
            res = modUtil.ЕслиИмя(value)
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
   Public Class clsCoord
      '''<summary>
      '''Позиция тега в строке
      '''</summary>
      Dim _pos As Integer = 0
      '''<summary>
      '''Номер строки в исходном тексте
      '''</summary>
      Dim _str As Integer = 0
      '''<summary>
      '''Позиция тега в строке
      '''</summary>
      Public Property iPos As Integer
         Get
            Return Me._pos
         End Get
         Set(value As Integer)
            Me._pos = value
         End Set
      End Property
      '''<summary>
      '''Номер строки в исходном тексте, содержащий текущий тег
      '''</summary>
      Public Property iStr As Integer
         Get
            Return Me._str
         End Get
         Set(value As Integer)
            Me._str = value
         End Set
      End Property
      Public Sub New(Optional str_ As Integer = 0, Optional pos_ As Integer = 0)
         If _pos < 0 Then
            модКокон.Ошибка("Позиция в строке не может быть отрицательной val=" + _pos.ToString())
            Environment.Exit(1)
         Else
            Me._pos = pos_
         End If
         If _str < 0 Then
            модКокон.Ошибка("Номер строки не может быть отрицательной val=" + _str.ToString())
            Environment.Exit(1)
         Else
            Me._str = str_
         End If
      End Sub
      '''<summary>
      '''По литере определяет налчие новой строки. В любом случае, обновляет координаты
      '''</summary>
      Public Sub Coord_Update(str_ As Integer, pos_ As Integer)
         Me._pos = pos_
         Me._str += str_
      End Sub
   End Class
   Public Class clsTag
      ' Стили именования
      ' хранит в себе последовательно кусочек нераспознанного кода с координатами
      Public coord As clsCoord
      Public strTag As String = ""
      Public Sub New(_strTag As String, _coord As clsCoord)
         Me.coord = _coord
         Me.strTag = _strTag
      End Sub
   End Class
   Public Class тЦепьЛекс
      Public tags() As clsTag
      Public Function ЕслиОтсев(lit As String) As Boolean
         Dim res As Boolean = False
         If lit <= " " Then
            res = True
         End If
         Return res
      End Function
      Public Sub Add(_lit As String, _coord As clsCoord) 'Создать новый тэг
         Dim tag As clsTag = New clsTag(_lit, _coord)
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
            modUtil.ASSERT(Not IsNothing(Me.tags(i)), "clsTaggerList.strTag: Элемент списка пустой! i=" + Str(i))
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
         ElseIf modUtil.ЕслиЦифра(lit) Or modUtil.ЕслиБуква(lit) Or lit = "_" Then
            res = modConst.multitag
         End If
         Return res
      End Function
      Public Sub Tagging(_src As туИсходник)
         Dim гсТег As String = ""
         Dim lit As String = ""
         Dim lit2 As String = ""
         Dim _tmp As String = "" ' перменная для правильного пропуска doubletag
         lit = txtSrc.lit
         Do While _src.len > 1 ' общий цикл с правильными литерами
            If Me.ЕслиОтсев(lit) Then ' отбросим мусор
               ' Пропуск символов-мусора
               lit = txtSrc.lit
            End If
            If Me.ClassTag(lit) = modConst.singletag Then ' если тег-одиночка
               'Console.WriteLine("singletag-1")
               Me.Add(lit, txtSrc.коорд)
               lit = txtSrc.lit
            End If
            If Me.ClassTag(lit) = modConst.doubletag Then ' если сложный тег
               lit2 = txtSrc.lit2
               If InStr(":><", lit) > 0 And lit2 = "=" Then
                  'Console.WriteLine("doubletag-1")
                  Me.Add(lit + lit2, txtSrc.коорд)
                  _tmp = txtSrc.lit
               ElseIf lit = "(" And lit2 = "*" Then
                  'Console.WriteLine("doubletag-2")
                  Me.Add(lit + lit2, txtSrc.коорд)
                  _tmp = txtSrc.lit
               ElseIf lit = "*" And lit2 = ")" Then
                  'Console.WriteLine("doubletag-3")
                  Me.Add(lit + lit2, txtSrc.коорд)
                  _tmp = txtSrc.lit
               End If
               lit = txtSrc.lit
            End If
            If Me.ClassTag(lit) = modConst.multitag Then
               Do While Me.ClassTag(lit) = modConst.multitag ' если многосимвольный тэг
                  гсТег += lit
                  lit = txtSrc.lit
               Loop
               Me.Add(гсТег, txtSrc.коорд)
               гсТег = ""
            End If
         Loop
      End Sub
   End Class
   Public Module модЛексер
      ' попытка сделать по уму
      Public tags As тЦепьЛекс
      Public txtSrc As туИсходник ' исходник разбитый построчно
      ' =================== ТЕГИРОВАНИЕ =======================
      Public Sub Тег_Разметить()
         tags = New тЦепьЛекс()
         txtSrc = New туИсходник(модФайл.txtFileO7 + "  ") ' хвост нужен, чтобы гарантированно не обрезать тег
         tags.Tagging(txtSrc)
      End Sub
   End Module
End Namespace
