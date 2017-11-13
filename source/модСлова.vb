' Модуль разбивает исходник на слова, выкидывая всё лишнее

Namespace пиОк
   '''' <summary>
   '''' Хранит одну строку исходника
   '''' </summary>
   'Public Class туЗвСтрока
   '   Inherits туЗвено
   '   Public стрСтрока As String = ""
   '   Public Sub New(Optional пуПред As туЗвено = Nothing, Optional пуСлед As туЗвено = Nothing)
   '      Me.уПред_Уст(пуПред)
   '      Me.уСлед_Уст(пуСлед)
   '   End Sub
   'End Class
   Public Class туИсход
      Dim стрИсхТекущ As String = "" ' одиночная текущая строка исходника
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
                  Me.мСтроки(Me.мСтроки.Length - 1) = Me.стрИсхТекущ
                  Console.WriteLine("туИсходник.стрИсхТекущ: " + Me.стрИсхТекущ)
                  Me.стрИсхТекущ = ""
               Else
                  Me.коорд.цПоз_Уст(Me.коорд.цПоз + 1)
                  Me.стрИсхТекущ += Me._лит
               End If
            End If
            Return Me._лит
         End Get
      End Property
      Public ReadOnly Property лит2 As String
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

      Public Sub New(Optional пцСтр As Integer = 0, Optional пцПоз As Integer = 0)
         If пцПоз < 0 Then
            модКокон.Ошибка("Позиция в строке не может быть отрицательной val=" + пцПоз.ToString())
            Environment.Exit(1)
         Else
            Me._цПоз = пцПоз
         End If
         If пцСтр < 0 Then
            модКокон.Ошибка("Номер строки не может быть отрицательной val=" + пцСтр.ToString())
            Environment.Exit(1)
         Else
            Me._цСтр = пцСтр
         End If
      End Sub
      '''<summary>
      '''По литере определяет налчие новой строки. В любом случае, обновляет координаты
      '''Похоже, этот метод не нужен
      '''</summary>
      Sub Уст(str_ As Integer, pos_ As Integer)
         Me._цПоз = pos_
         Me._цСтр += str_
      End Sub
   End Class
   Public Class туСлово
      Inherits туЗвено
      ' хранит в себе последовательно кусочек нераспознанного кода с координатами
      Public уКоорд As туКоорд
      Dim _стрСлово As String
      Public ReadOnly Property стрСлово As String
         Get
            Return Me._стрСлово
         End Get
      End Property
      Public Sub New(Optional псЛекс As String = "", Optional пуКоорд As туКоорд = Nothing)
         Me.уКоорд = пуКоорд
         Me._стрСлово = псЛекс
         Me.уСлед_Уст(Nothing)
         Me.уПред_Уст(Nothing)
      End Sub
   End Class
   Public Class тСлова
      Dim _послед As туСлово ' последняя лексема в цепи
      Dim _первая As туСлово ' первая лексема в цепи
      Dim _текущ As туСлово ' текущая лексема в цепи
      Dim _длина As Integer = 0 ' длина всей цепи
      Public ReadOnly Property уПервЛекс As туСлово
         Get
            Return Me._первая
         End Get
      End Property
      Public Function ЕслиОтсев(lit As String) As Boolean
         Dim res As Boolean = False
         If lit <= " " Then
            res = True
         End If
         Return res
      End Function
      Public Sub Добавить(_lit As String, _coord As туКоорд) 'Создать новый тэг
         Dim луЛекс As туСлово = New туСлово(_lit, _coord)
         'Console.WriteLine("clsTagList.Add(): " + _lit + " " + Str(_str) + " " + Str(_pos))
         If IsNothing(Me._первая) Then
            Me._первая = луЛекс
            'Me._послед = луЛекс
            Me._первая.уСлед_Уст(Me._послед)
            Me._послед.уПред_Уст(Me._первая)
            Me._послед.уСлед_Уст(Nothing)
         Else
            Me._послед.уСлед_Уст(луЛекс)
            луЛекс.уПред_Уст(Me._послед)
            Me._послед = луЛекс
         End If
      End Sub
      Public ReadOnly Property длина As Integer
         Get
            Return Me._длина
         End Get
      End Property
      Public Sub Дальше()
         If IsNothing(_текущ) Then
            Me._текущ = Me._первая
         Else
            Me._текущ = Me._текущ.уСлед
         End If
      End Sub
      Public Sub New()
         Me._первая = Nothing
         Me._текущ = Nothing
         Me._послед = Nothing
      End Sub
      Public Function Слово_Тип(lit As String) As Integer
         ' описывает типы тегов в зависимости от длины:
         '   синглетег -- одиночная литера
         '   дублетег -- двойная литера
         '   мультитег -- мультилитера
         Dim res As Integer = -1

         If InStr(",;-+.[""]'=", lit) > 0 Then
            res = модКонст.монолитера
         ElseIf InStr(":<>()*", lit) > 0 Then
            res = модКонст.дилитера
         ElseIf модУтиль.ЕслиЦифра(lit) Or модУтиль.ЕслиБуква(lit) Or lit = "_" Then
            res = модКонст.мультилит
         End If
         Return res
      End Function
      Public Sub Нарезать(_исходник As туИсход)
         Dim лсТег As String = ""
         Dim лит As String = ""
         Dim лит2 As String = ""
         Dim _tmp As String = "" ' перменная для правильного пропуска doubletag
         лит = txtSrc.лит
         Do While _исходник.длина > 1 ' общий цикл с правильными литерами
            If Me.ЕслиОтсев(лит) Then ' отбросим мусор
               ' Пропуск символов-мусора
               лит = txtSrc.лит
            End If
            If Me.Слово_Тип(лит) = модКонст.монолитера Then ' если монолитера
               'Console.WriteLine("singletag-1")
               Me.Добавить(лит, txtSrc.коорд)
               лит = txtSrc.лит
            End If
            If Me.Слово_Тип(лит) = модКонст.дилитера Then ' если парное слово
               лит2 = txtSrc.лит2
               If InStr(":><", лит) > 0 And лит2 = "=" Then
                  Me.Добавить(лит + лит2, txtSrc.коорд)
                  _tmp = txtSrc.лит
               ElseIf лит = "(" And лит2 = "*" Then
                  Me.Добавить(лит + лит2, txtSrc.коорд)
                  _tmp = txtSrc.лит
               ElseIf лит = "*" And лит2 = ")" Then
                  Me.Добавить(лит + лит2, txtSrc.коорд)
                  _tmp = txtSrc.лит
               End If
               лит = txtSrc.лит
            End If
            If Me.Слово_Тип(лит) = модКонст.мультилит Then
               Do While Me.Слово_Тип(лит) = модКонст.мультилит ' если многосимвольное слово
                  лсТег += лит
                  лит = txtSrc.лит
               Loop
               Me.Добавить(лсТег, txtSrc.коорд)
               лсТег = ""
            End If
         Loop
      End Sub
   End Class
   ''' <summary>
   ''' попытка сделать по уму
   ''' </summary>
   Public Module модСлова
      Public слова As тСлова = New тСлова()
      Public txtSrc As туИсход ' исходник разбитый построчно
      ' =================== Лексирование =======================
      Public Sub Лексемы_Разметить()
         слова = New тСлова()
         txtSrc = New туИсход(модФайл.txtFileO7 + "  ") ' хвост нужен, чтобы гарантированно не обрезать тег
         слова.Нарезать(txtSrc)
      End Sub
   End Module
End Namespace
