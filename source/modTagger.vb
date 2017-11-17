''' <summary>
''' Содержит имя объекта и методы обработки имени
''' </summary>
Public Class туИмя
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
''' <summary>
'''  Предаставляет класс счётчика с полезными фишками
''' </summary>
Public Class clsCount
   Dim _count As Integer = 0
   Public ReadOnly Property val As Integer
      Get
         Return Me._count
      End Get
   End Property
   Public Sub Inc()
      Me._count += 1
   End Sub
   Public Sub Reset()
      Me._count = 0
   End Sub
   Public Sub New()
      Me._count = 0
   End Sub
End Class
Public Class clsModule ' Описывает модуль целиком
   Public tag_end As Integer = 0 'номер последнего значимого тега
   Public level As Integer = 0 ' 0 -- это главный
   Public loaded As Boolean
   Public import() As clsImport  ' Список модулей импорта
   Public const_() As clsConst 'список констант
   Public types As clsModType ' список объявленных типов всех видов в модуле
   Public proc As Integer
   Public name As String = ""
   ''' <summary>
   ''' Сообщение об отсутствии кейворда MODULE
   ''' </summary>
   ''' <param name="txtLine">Строка, где MODULE не встречено </param>
   ''' <param name="_mLex">Лексема, которая должна была содржать MODULE</param>
   Public Sub ErrorOpen(txtLine As String, _mLex As туСлово)
      модКокон.Ошибка("Крд: " + Str(_mLex.уКоорд.цСтр) + " -" + Str(_mLex.уКоорд.цПоз))
      Console.WriteLine(txtLine)
      Console.WriteLine(Смещ(_mLex.уКоорд.цПоз))
      модКокон.Ошибка("Модуль должен начинаться с ""MODULE""")
      Environment.Exit(1)
   End Sub
   ''' <summary>
   ''' Сообщение о том, что не найден разделитель имени модуля
   ''' </summary>
   ''' <param name="txtLine">Строка с именем модуля</param>
   ''' <param name="_mLex">ошибочная лексема</param>
   Public Sub ErrorEndName(txtLine As String, _mLex As туСлово)
      модКокон.Ошибка("Крд: " + Str(_mLex.уКоорд.цСтр) + " -" + Str(_mLex.уКоорд.цПоз))
      Console.WriteLine(txtLine)
      Console.WriteLine(Смещ(_mLex.уКоорд.цПоз))
      модКокон.Ошибка("Нет разделителя имени модуля")
      Environment.Exit(1)
   End Sub
   ''' <summary>
   ''' Сообщение при отсутствии маркера конца модуля
   ''' </summary>
   ''' <param name="txtLine">Последняя строка</param>
   ''' <param name="_mLex">Последняя лексема</param>
   Public Sub BadEndNModule(txtLine As String, _mLex As туСлово)
      модКокон.Ошибка("Крд: " + Str(_mLex.уКоорд.цСтр) + "-" + Str(_mLex.уКоорд.цПоз))
      Console.WriteLine(txtLine)
      Console.WriteLine(Смещ(_mLex.уКоорд.цПоз))
      модКокон.Ошибка("Модуль должен иметь ""END <NameModule.>""")
      Environment.Exit(1)
   End Sub
   ''' <summary>
   ''' Сообщение о ошибке, что MODULE не один
   ''' </summary>
   ''' <param name="txtLine">Строка в которой встречен второй MODULE</param>
   ''' <param name="_mLex">лексема запрещённый второй MODULE</param>
   Public Sub ErrorDoubleNModule(txtLine As String, _mLex As туСлово)
      модКокон.Ошибка("Крд: " + Str(_mLex.уКоорд.цСтр) + " -" + Str(_mLex.уКоорд.цПоз))
      Console.WriteLine(txtLine)
      Console.WriteLine(Смещ(_mLex.уКоорд.цПоз))
      модКокон.Ошибка("MODULE должен быть один")
      Environment.Exit(1)
   End Sub
   ''' <summary>
   ''' Сообщение о том, что имя модуля в начале, и имя модуля в конце
   ''' не совпадают.
   ''' </summary>
   ''' <param name="txtLine1">Строка содержащая имя модуля</param>
   ''' <param name="txtLine2">Строка содержащая имя модуля в конце модуля</param>
   Public Sub MissMathName(txtLine1 As String, txtLine2 As String)
      Console.WriteLine(txtLine1)
      Console.WriteLine(txtLine2)
      модКокон.Ошибка("Имя модуля несогласовано")
      Environment.Exit(1)
   End Sub
   ''' <summary>
   ''' Сообщение об ошибке, что неправильно записана инструкция
   ''' за разделителем имени модуля.
   ''' </summary>
   ''' <param name="txtLine">Строка в которой встречена запрещённая инструкция</param>
   ''' <param name="_mLex">Лексема запрещённая инструкция</param>
   Public Sub ErrorNextStatement(txtLine As String, _mLex As туСлово)
      модКокон.Ошибка("Крд: " + Str(_mLex.уКоорд.цСтр) + " -" + Str(_mLex.уКоорд.цПоз))
      Console.WriteLine(txtLine)
      Console.WriteLine(Смещ(_mLex.уКоорд.цПоз))
      модКокон.Ошибка("Запрещённая лексема в этом месте")
      Environment.Exit(1)
   End Sub
   Public Sub New()
      ReDim Me.import(0)
      ReDim Me.const_(0)
   End Sub
End Class
''' <summary>
''' Содержит список модулей для импорта
''' </summary>
Public Class clsImport ' содержит имена модулей для имопрта и их алиасы
   Public name As туИмя
   Public alias_ As туИмя
   Public Sub New(Optional _name As String = "", Optional _alias As String = "")
      Me.name = New туИмя(_name)
      Me.alias_ = New туИмя(_alias)
   End Sub
   Public Sub Import_Error(txtLine As String, _lex As туСлово)
      модКокон.Ошибка("Крд: " + Str(_lex.уКоорд.цСтр) + " -" + Str(_lex.уКоорд.цПоз))
      Console.WriteLine(txtLine)
      Console.WriteLine(Смещ(_lex.уКоорд.цПоз))
      модКокон.Ошибка("Нарушение порядка импорта>")
      Environment.Exit(1)
   End Sub
End Class
Public Class clsConst ' класс содержащий константу
   Public lex As туСлово ' содержит строкове представление и координаты
   Public exp() As туСлово ' тут может быть целое выражение!!
   Public type_ As String ' тип константы
   ''' <summary>
   ''' Выводится при отсутствии имени константы
   ''' </summary>
   ''' <param name="txtLine">Строка, в которой ошибка</param>
   ''' <param name="_lex">Неверная лексема</param>
   Public Sub ErrorMissingName(txtLine As String, _lex As туСлово)
      модКокон.Ошибка("Крд: " + Str(_lex.уКоорд.цСтр) + " -" + Str(_lex.уКоорд.цПоз))
      Console.WriteLine(txtLine)
      Console.WriteLine(Смещ(_lex.уКоорд.цПоз))
      модКокон.Ошибка("Пропущено имя константы")
      Environment.Exit(1)
   End Sub
   ''' <summary>
   ''' Сообщение об запрете пустого имени константы
   ''' </summary>
   ''' <param name="txtLine">Строка с ошибочным именем</param>
   ''' <param name="_lex">Ошибочная лексема</param>
   Public Sub ErrorEmptyName(txtLine As String, _lex As туСлово)
      модКокон.Ошибка("Крд: " + Str(_lex.уКоорд.цСтр) + " -" + Str(_lex.уКоорд.цПоз))
      Console.WriteLine(txtLine)
      Console.WriteLine(Смещ(_lex.уКоорд.цПоз))
      модКокон.Ошибка("Пустое имя константы")
      Environment.Exit(1)
   End Sub
   ''' <summary>
   '''  Выводится сообщение при отсутствии присвоения константе
   ''' </summary>
   ''' <param name="txtLine">Строка с ошибочным присвоение</param>
   ''' <param name="_lex">Ошибочная лексема на месте присвоения</param>
   Public Sub ErrorAsign(txtLine As String, _lex As туСлово)
      модКокон.Ошибка("Крд: " + Str(_lex.уКоорд.цСтр) + "-" + Str(_lex.уКоорд.цПоз))
      Console.WriteLine(txtLine)
      Console.WriteLine(Смещ(_lex.уКоорд.цПоз))
      модКокон.Ошибка("Нарушение присвоения константы")
      Environment.Exit(1)
   End Sub
   ''' <summary>
   '''  Вызывается, если достигнут конец кода, а окончания выражения константы нет
   ''' </summary>
   ''' <param name="txtLine">Строка константы с выражением</param>
   ''' <param name="_lex">Последняя просмотренная лексема</param>
   Public Sub ErrorEndSource(txtLine As String, _lex As туСлово)
      модКокон.Ошибка("Крд: " + Str(_lex.уКоорд.цСтр) + "-" + Str(_lex.уКоорд.цПоз))
      Console.WriteLine(txtLine)
      Console.WriteLine(Смещ(_lex.уКоорд.цПоз))
      модКокон.Ошибка("Нет ограничителя константы")
      Environment.Exit(1)
   End Sub
   ''' <summary>
   '''  Проверяет, если вместо имени константы встречено ключевое слово
   ''' </summary>
   ''' <param name="strTag">Строка с ключевым словом вместо имени</param>
   Public Function NextKeyword(strTag As String) As Boolean
      Dim bRes As Boolean = False
      Dim ts(9) As String
      ts(0) = "TYPE"
      ts(1) = "VAR"
      ts(2) = "PROCEDURE"
      ts(3) = "BEGIN"
      ts(5) = "END"
      ts(6) = "^"
      ts(7) = ":"
      ts(8) = ","
      Dim sr() As String
      sr = Filter(ts, strTag, True, CompareMethod.Binary)
      If sr.Length <> 0 Then
         bRes = True
      End If
      Return bRes
   End Function
   ''' <summary>
   '''  Выводит сообщение, если в этом месте выражения константы ключевое слово запрещено
   ''' </summary>
   ''' <param name="txtLine">Строка с запрещённым ключевым словом</param>
   ''' <param name="_lex">Само ключевое слово</param>
   Public Sub ErrorKeyword(txtLine As String, _lex As туСлово)
      модКокон.Ошибка("Крд: " + Str(_lex.уКоорд.цСтр) + "-" + Str(_lex.уКоорд.цПоз))
      Console.WriteLine(txtLine)
      Console.WriteLine(Смещ(_lex.уКоорд.цПоз))
      модКокон.Ошибка("В этом месте ключевое слово запрещено")
      Environment.Exit(1)
   End Sub
End Class
''' <summary>
'''  Модуль описывает клас лексера для проверки правильности построения модуля
''' </summary>
Public Module modLexer
   Dim sRes As String = "" ' результат анализа
   Dim mLex() As туСлово
   Dim prog As clsModule ' объект главного модуля есть программ
   Dim tagc As clsCount ' текущий тег на анализе
   Dim txtLine() As String ' список строк исходника
   Sub ОшибкаИмени(msg As String, t As Integer)
      модКокон.Ошибка(msg + ":" + t.ToString() + " >" + mLex(t).стрСлово + "<")
      модКокон.Ошибка("Крд: " + Str(mLex(t).уКоорд.цСтр) + " -" + Str(mLex(t).уКоорд.цПоз))
      Console.WriteLine(txtLine(mLex(t).уКоорд.цСтр))
      Console.WriteLine(Смещ(mLex(t).уКоорд.цПоз))
      модКокон.Ошибка("Имя должно начинаться с буквы или ""_""")
      Environment.Exit(1)
   End Sub
   ''' <summary>
   '''  Правило выкидывает комментарии.
   '''  Входящий ключ "comment"
   ''' </summary>
   Sub Пр_КОММЕНТАРИЙ()
      ' правило ищет комметарии и иключает их из кода
      Dim count As Integer = 0
      Dim bStrip As Boolean
      Dim tmpLex() As туСлово = Nothing
      ReDim tmpLex(0)
      Dim tag As String
      If sRes = "comment" Then
         Do While count < mLex.Length
            tag = mLex(count).стрСлово
            If tag = "(*" Then ' начало коммента
               bStrip = True
               count += 1
               Continue Do
            End If
            If bStrip = True Then ' пропускаем комментарий
               If tag = "*)" Then
                  bStrip = False
               End If
               count += 1
               Continue Do
            End If
            ' копирование остального
            tmpLex(tmpLex.Length - 1) = mLex(count)
            ReDim Preserve tmpLex(tmpLex.Length)
            count += 1
         Loop

         mLex = tmpLex
         tmpLex = Nothing

         If bStrip = True Then
            модКокон.Ошибка("Крд: " + Str(mLex(mLex.Length - 1).уКоорд.цСтр + 1) + " -" _
                            + Str(mLex(mLex.Length - 1).уКоорд.цПоз))
            Console.WriteLine(txtLine(mLex(mLex.Length - 1).уКоорд.цСтр + 1))
            Console.WriteLine(Смещ(mLex(mLex.Length - 1).уКоорд.цПоз))
            модКокон.Ошибка("Блок комментария не закрыт")
            sRes = "err"
            Exit Sub
         Else
            sRes = "module"
         End If
      End If
   End Sub
   ''' <summary>
   '''  Правило контролирует начало и коец модуля
   ''' </summary>
   Sub Пр_МОДУЛЬ()
      ' 1.1 МОДУЛЬ должен быть первым
      If sRes = "module" Then
         If mLex(tagc.val).стрСлово = "MODULE" Then
            ' открываем наш модуль
            tagc.Inc()
            sRes = "1.2"
         Else ' если тег не оказался MODULE
            prog.ErrorOpen(txtLine(mLex(0).уКоорд.цСтр), mLex(0))
         End If
      End If
      '1.2 У модуля должно быть имя
      If sRes = "1.2" Then
         ' проверка на допустимое имя. Должно начинаться либо с "_"  либо с буквы
         If модУтиль.ЕслиНачИмени(mLex(tagc.val).стрСлово) Then
            prog.name = mLex(tagc.val).стрСлово
            tagc.Inc()
         Else
            ОшибкаИмени("MODULE", 1)
            Environment.Exit(1)
         End If

         ' имя модуля -- № 1, разделитель -- № 2
         If mLex(tagc.val).стрСлово <> ";" Then ' пропущен разделитель
            sRes = "1.3"
            tagc.Inc()
         Else
            prog.ErrorEndName(txtLine(mLex(tagc.val - 1).уКоорд.цСтр), mLex(tagc.val - 1))

         End If
      End If
      ' 1.3 У Модуля должно быть окончание
      If sRes = "1.3" Then
         Dim bEnd As Boolean = False
         Dim i As Integer = 3 ' начинаем отсчёт сразу за определением модуля
         Do While i < mLex.Length - 2 ' учитываем ссылку вперёд на точку
            ' конец ли это? i+2 -- через имя
            If mLex(i).стрСлово = "END" And модУтиль.ЕслиИмя(mLex(i + 1).стрСлово) = "_name_" And
                mLex(i + 2).стрСлово = "." Then
               bEnd = True
               ' ограничивать будем полем структуры программы
               prog.tag_end = i + 2
               sRes = "1.4"
               Exit Do
            End If
            i += 1
         Loop
         If Not bEnd Then 'а конца то нет!! работаем с последним тегом
            prog.BadEndNModule(txtLine(mLex(mLex.Length - 1).уКоорд.цСтр - 1), mLex(mLex.Length - 1))
         End If
      End If
      '1.4 Модуль должен быть один
      If sRes = "1.4" Then
         ' Организуем цикл в поиске МОДУЛЬ с учётом, что это может быть строка
         ' Интересует только первая тотальная встреча
         Dim i As Integer = 1 ' нельзя брать 0, так как это и есть модуль
         Do While i < prog.tag_end  ' последние тэги мы уже выяснили
            If mLex(i).стрСлово = "MODULE" Then 'надо выясить, может это часть выражения, или строка
               If (mLex(i - 1).стрСлово = ".") Then
                  i += 1
                  Continue Do
               ElseIf mLex(i - 1).стрСлово = """" And mLex(i + 1).стрСлово = """" Then
                  i += 1
                  Continue Do
               ElseIf mLex(i - 1).стрСлово = "'" And mLex(i + 1).стрСлово = "'" Then
                  i += 1
                  Continue Do
               Else ' да. Это не строка, и не часть сущности!!
                  prog.ErrorDoubleNModule(txtLine(mLex(i).уКоорд.цСтр), mLex(i))
               End If
            End If
            i += 1
         Loop
         sRes = "1.5"
      End If
      ' 1.5 Имя модуля и имя конца должны совпадать
      If sRes = "1.5" Then
         If prog.name <> mLex(prog.tag_end - 1).стрСлово Then
            ' это залёт!
            prog.MissMathName(txtLine(mLex(0).уКоорд.цСтр), txtLine(mLex(prog.tag_end - 1).уКоорд.цСтр))
         Else
            sRes = "1.6"
         End If
      End If
      ' дальше должно идти на выбор: IMPORT CONST TYPE VAR PROCEDURE BEGIN
      If sRes = "1.6" Then
         ' первая инструкция после разделителя имени модуля
         If mLex(tagc.val).стрСлово = "IMPORT" Or mLex(tagc.val).стрСлово = "CONST" Or
               mLex(tagc.val).стрСлово = "TYPE" Or mLex(tagc.val).стрСлово = "VAR" Or
               mLex(tagc.val).стрСлово = "PROCEDURE" Or mLex(tagc.val).стрСлово = "BEGIN" Then
            sRes = "import"
         Else
            ' запрещённый символ
            Console.WriteLine(">" + mLex(tagc.val).стрСлово + "<")
            prog.ErrorNextStatement(txtLine(mLex(tagc.val).уКоорд.цСтр), mLex(tagc.val))
         End If
      End If
   End Sub
   ''' <summary>
   '''  Правило контролирует импорт в модуле
   ''' </summary>
   Sub Пр_ИМПОРТ()
      ' прочесали модуль, теперь проверить нет ли импорта
      If sRes = "import" Then ' 2.1 IMPORT может идти тегом № 3 -- проверяем
         If mLex(tagc.val).стрСлово = "IMPORT" Then
            sRes = "2.2"
            tagc.Inc()
         End If
      End If
      ' 2.2 Проверяем весь доступный импорт
      If sRes = "2.2" Then
         ' Может быть прямой импорт, а может быть и с алиасами.
         ' Если импорт прямой, то tagc+2 будет ";", а сли алиас -- то ":="
         ' После ИМПОРТ имя файла или алиас по счёту -- 4 тег в файле
         ' за именем -- либо разделитель, либо присвоение алиаса
         Do While True
            ' Импортов может быть 
            ' прямой, c алиасом, с запятой (продолжение), с ";" -- конец импорта
            tagc.Inc()
            If mLex(tagc.val).стрСлово = "," Or mLex(tagc.val).стрСлово = ";" Then ' Первая ветка -- прямой импорт
               ' проверить имя модуля и алиас на допустимость
               If модУтиль.ЕслиИмя(mLex(tagc.val - 1).стрСлово) <> "_name_" Then
                  '  неправильное имени
                  ОшибкаИмени("IMPORT", tagc.val)
               End If
               ReDim Preserve prog.import(prog.import.Length)
               Dim imp As clsImport = New clsImport(mLex(tagc.val).стрСлово)
               prog.import(prog.import.Length - 1) = imp
               tagc.Inc() ' ищем разделитель импорта
               If mLex(tagc.val).стрСлово = "," Then 'импорт может закончился?
                  tagc.Inc()
                  Continue Do
               ElseIf mLex(tagc.val).стрСлово = ";" Then ' импорт закончить
                  tagc.Inc()
                  sRes = "2.3"
                  Exit Do
               Else ' а это уже ошибка!!
                  prog.import(prog.import.Length - 1).Import_Error(txtLine(mLex(tagc.val + 1).уКоорд.цСтр), mLex(tagc.val + 1))
               End If
            ElseIf mLex(tagc.val).стрСлово = ":=" Then ' вторая ветка -- импорт с алиасом
               ' проверка имени
               tagc.Inc()
               If модУтиль.ЕслиИмя(mLex(tagc.val).стрСлово) <> "_name_" Then
                  '  неправильное имени
                  ОшибкаИмени("IMPORT", tagc.val)
               End If
               ' проверка алиаса
               If модУтиль.ЕслиИмя(mLex(tagc.val - 2).стрСлово) <> "_name_" Then
                  '  неправильное имени
                  ОшибкаИмени("IMPORT", tagc.val - 2)
               End If
               ' значит добавить элемент импрота
               ReDim Preserve prog.import(prog.import.Length + 1)
               Dim imp As clsImport = New clsImport(mLex(tagc.val).стрСлово, mLex(tagc.val - 2).стрСлово)
               prog.import(prog.import.Length - 1) = imp
               ' проверка на продолжение
               tagc.Inc()
               If mLex(tagc.val).стрСлово = "," Then 'импорт может закончился?
                  tagc.Inc()
                  Continue Do
               ElseIf mLex(tagc.val).стрСлово = ";" Then ' импорт закончить
                  tagc.Inc()
                  sRes = "const"
                  Exit Do
               Else ' а это уже ошибка!!
                  prog.import(prog.import.Length - 1).Import_Error(txtLine(mLex(tagc.val).уКоорд.цСтр), mLex(tagc.val + 3))
               End If
            Else
               prog.import(prog.import.Length - 1).Import_Error(txtLine(mLex(tagc.val + 1).уКоорд.цСтр), mLex(tagc.val + 1))
            End If
         Loop
      End If
   End Sub
   ''' <summary>
   '''  Правило контролирует секцию констант.
   '''  TODO: сделать разбор выражений
   ''' </summary>
   Sub Пр_КОНСТ()
      ' Проверяет правильность объявления констант
      If sRes = "const" Then ' Правило объявления инструкции CONST
         If mLex(tagc.val).стрСлово <> "CONST" Then ' возможно просто нет такой секции
            tagc.Inc()
            sRes = "type"
            Exit Sub
            ' такая инструкция есть, но сразу вместо имени может идти ключевое слово.
            ' Остальное запрещено
         Else
            tagc.Inc()
            ' может имя, а может ключевое слово
            If модУтиль.ЕслиИмя(mLex(tagc.val).стрСлово) = "_name_" Then
               Dim cst As clsConst = New clsConst()
               ' если имя оказалось ключевым словом -- идти к следующему блоку
               If cst.NextKeyword(mLex(tagc.val).стрСлово) Then
                  tagc.Inc()
                  sRes = "type"
                  Exit Sub
               End If
               sRes = "3.2"
            Else ' если имя пустое
               prog.const_(0).ErrorMissingName(txtLine(mLex(tagc.val).уКоорд.цСтр), mLex(tagc.val))
            End If
         End If
      End If
      If sRes = "3.2" Then ' начинаем разбор выражения константы
         ' номер тега уже содержит имя константы
         Do
            ' может быть что угодно в выражении, но не ";". Пока не разбираем что именно
            ' Добавляем тег в секцию констант
            ' сначала добавить имя константы, проверим на правильность имени
            If модУтиль.ЕслиИмя(mLex(tagc.val).стрСлово) <> "_name_" Then
               ОшибкаИмени("Константы:", tagc.val)
            End If
            ' проверить на следующее ключевое слово
            ' В это месте ключевое слово разрешено, и если что -- выход
            If prog.const_(0).NextKeyword(mLex(tagc.val).стрСлово) Then
               sRes = "type"
               ' tagc += 1 Указатель итак на ключевое слово
               Exit Sub
            End If
            ' теперь создать лексему-имя в константу
            Dim _lex As туСлово = mLex(tagc.val)
            ' теперь создать саму константу с её выражением для присвоения
            Dim _const As clsConst = New clsConst With {.lex = _lex} ' заполнение константы
            ' в константе может быть выражение для присвоения
            ' перейти к следующему символу для проверки
            tagc.Inc()
            ' если нет "равно" -- сообщить об ошибке, иначе продолжать
            If mLex(tagc.val).стрСлово <> "=" Then
               prog.const_(0).ErrorAsign(txtLine(mLex(tagc.val).уКоорд.цСтр), mLex(tagc.val))
            Else
               tagc.Inc()
            End If
            ' создать массив лексем для выражения
            Dim _exp() As туСлово = Nothing
            ReDim _exp(0)
            tagc.Inc()
            ' перебирать лексемы, пока не закончится выражение, либо не закончится код
            Do While tagc.val < prog.tag_end
               ' если встречен ";"
               If mLex(tagc.val).стрСлово = ";" Then
                  ' здесь надо прервать внутренний цикл с выходом на внешний
                  Exit Do
               End If
               ' TODO: двоеточие в выражениях запрещена (и для SET запятую надо переделать!!!)
               If _const.NextKeyword(mLex(tagc.val).стрСлово) Then
                  ' здесь надо вообще покончить с разбором (пока)
                  prog.const_(0).ErrorKeyword(txtLine(mLex(tagc.val).уКоорд.цСтр), mLex(tagc.val))
               End If
               ' заполняем выражение
               _exp(_exp.Length - 1) = mLex(tagc.val)
               ReDim Preserve _exp(_exp.Length)
               tagc.Inc()
            Loop
            ' если достигнут конец, а ограничителя всё нет -- закончить
            If tagc.val = prog.tag_end Then ' это ошибка!!
               _const.ErrorEndSource(txtLine(prog.tag_end), mLex(tagc.val))
            End If
            ' создать новую константу в массиве констант модуля
            ReDim Preserve prog.const_(prog.const_.Length)
            ' добавить выражение константы
            _const.exp = _exp
            ' сохранить новую константу
            prog.const_(prog.const_.Length - 1) = _const
            ' перейти к следующей литере
            tagc.Inc()
         Loop
      End If
   End Sub
   ''' <summary>
   '''  Правило контролирует секцию TYPE
   ''' </summary>
   Sub Пр_ТИПЫ()
      ' проверка на ключевое слово "TYPE"
      ' такой секции может просто не быть
      If sRes = "type" Then
         If mLex(tagc.val).стрСлово = "TYPE" Then
            Console.WriteLine("Обнаружена секция типы")
            ' продолжаем работу над сбором типов
            tagc.Inc()
            sRes = "4.2"
         End If
      End If
      ' обнаружена и подтверждена секция типов
      If sRes = "4.2" Then
         ' секция типов может быть пустая
         If mLex(tagc.val).стрСлово = "VAR" Or mLex(tagc.val).стрСлово = "PROCEDURE" Or
               mLex(tagc.val).стрСлово = "BEGIN" Or
               (mLex(tagc.val).стрСлово = "END" And mLex(tagc.val + 1).стрСлово = prog.name And
               mLex(tagc.val + 2).стрСлово = ".") Then
            tagc.Inc()
            sRes = "var"
            Exit Sub
         End If
         ' это не следующая секция, это имя типа. Начать перебор
         Do
            If модУтиль.ЕслиИмя(mLex(tagc.val).стрСлово) = "_name_" Then
               ' добавить новый тип
               Dim _type_name As String = mLex(tagc.val).стрСлово
               tagc.Inc() '+1
               ' проверка на разделитель
               If mLex(tagc.val).стрСлово = "=" Then
                  Console.WriteLine("Обнаружен правильный разделитель типа")
                  tagc.Inc() '+2
               Else
                  modType.ErrorTerminal(txtLine(mLex(tagc.val).уКоорд.цСтр), mLex(tagc.val))
               End If
               ' простые типы
               ' Группа ARRAY
               If mLex(tagc.val).стрСлово = "ARRAY" Then
                  tagc.Inc() '+3
                  Dim _type_sort As String = "ARRAY"
                  ' ARRAY <N>
                  If модУтиль.ЕслиЦелое(mLex(tagc.val).стрСлово) Then
                     Dim _type_len As Integer = CInt(mLex(tagc.val).стрСлово)
                     tagc.Inc() '+4
                     ' дальше ДОЛЖНО идти OF
                     If mLex(tagc.val).стрСлово = "OF" Then
                        tagc.Inc() '+5
                        'имя типа
                        Dim t As String = modType.SelectType(mLex(tagc.val).стрСлово)
                        If t <> "_not_" Then
                           Dim _type_of As String = t
                           tagc.Inc() '+6
                           ' проверка на завершение типа-массива
                           If mLex(tagc.val).стрСлово = ";" Then
                              Dim _type_array As clsTypeArray = New clsTypeArray(mLex(tagc.val - 6)) With {
                                 .name = _type_name,
                                 .strOf = _type_of,
                                 .lenArray = _type_len
                              }
                              ' если секция типов отсутствует -- её нужно создать

                              ' добавить тип в модуль
                              prog.types.arrays(prog.types.arrays.Length - 1) = _type_array
                              ReDim Preserve prog.types.arrays(prog.types.arrays.Length)
                              tagc.Inc()
                              Continue Do
                           Else
                              ' нет разделителя определения типа массива
                              modType.ErrorEndArray(txtLine(mLex(tagc.val - 1).уКоорд.цСтр), mLex(tagc.val - 1))
                           End If
                        Else
                           ' ошибка по типу элеметов массива
                           modType.ErrorToElArray(txtLine(mLex(tagc.val).уКоорд.цСтр), mLex(tagc.val))
                        End If
                     Else
                        ' нет элемента OF
                        modType.ErrorOfArray(txtLine(mLex(tagc.val).уКоорд.цСтр), mLex(tagc.val))
                     End If
                  Else
                     ' размерность типа должна быть числом
                     modType.ErrorNumArray(txtLine(mLex(tagc.val).уКоорд.цСтр), mLex(tagc.val))
                  End If
               End If
               ' проверка на ключевое слово RECORD
               If mLex(tagc.val).стрСлово = "RECORD" Then
                  _type_name = "record"
                  tagc.Inc()
                  ' проверяем все члены записи
                  Dim members() As clsTypeMember = Nothing
                  ReDim members(0)
                  Do ' TODO: в типе может быть встречена запись. Надо доделать
                     ' в этой позиции должно идти имя-члена записи 
                     If модУтиль.ЕслиИмя(mLex(tagc.val).стрСлово) = "_name_" Then
                        ' добавить в массив членов новый член
                        Dim mem As clsTypeMember = New clsTypeMember(mLex(tagc.val)) With {
                           .name = mLex(tagc.val).стрСлово,
                           .type_ = _type_name}
                        tagc.Inc()
                        ' в следующей позиции должен идти разделитель
                        If mLex(tagc.val).стрСлово = ":" Then
                           tagc.Inc()
                           ' в следующей позиции должен идти тип члена
                           Dim a As String = modType.SelectType(mLex(tagc.val).стрСлово)
                           If a <> "_not_" Then
                              mem.type_ = a
                              tagc.Inc()
                              ' в следующей позиции должен идти разделитель
                              If mLex(tagc.val).стрСлово = ";" Then
                                 tagc.Inc()
                                 members(members.Length - 1) = mem
                                 ReDim Preserve members(members.Length - 1)
                                 Continue Do
                              Else
                                 ' неверный разделитель в члене записи
                                 modType.ErrorTerminal(txtLine(mLex(tagc.val).уКоорд.цСтр), mLex(tagc.val))
                              End If
                           Else
                              ОшибкаИмени("RECORD: ", tagc.val)
                           End If
                        Else
                           ' нет разделителя между членом записи и его типом
                           modType.ErrorTerminal(txtLine(mLex(tagc.val).уКоорд.цСтр), mLex(tagc.val))
                        End If
                     Else
                        ' сообщить об оишбочном имени члена записи
                        ОшибкаИмени("RECORD:", tagc.val)
                     End If
                  Loop
               End If
               ' Gh
            Else
               ОшибкаИмени("TYPE:", tagc.val)
            End If
         Loop
      End If
   End Sub
   ''' <summary>
   '''  Список всех правил по порядку
   ''' </summary>
   Sub Правила()
      ' проверить правильность полученного исходного текста
      sRes = "comment"
      Console.WriteLine("Пр_КОММЕНТАРИЙ")
      Пр_КОММЕНТАРИЙ()
      Dim i As Integer = 0
      Do While i < mLex.Length - 2
         Console.Write(Str(i) + ")" + mLex(i).стрСлово + vbTab)
         i += 1
         Console.Write(Str(i) + ")" + mLex(i).стрСлово + vbTab)
         i += 1
         Console.WriteLine(Str(i) + ")" + mLex(i).стрСлово)
         i += 1
      Loop
      Console.WriteLine("Пр_МОДУЛЬ")
      Пр_МОДУЛЬ()
      Console.WriteLine("Len(mLex)=" + mLex.Length.ToString)
      Console.WriteLine("Пр_ИМПОРТ")
      Пр_ИМПОРТ()
      Console.WriteLine("Пр_КОНСТ")
      Пр_КОНСТ()
      Console.WriteLine("Пр_ТИПЫ")
      Пр_ТИПЫ()
   End Sub
   Sub Lexer_Run()
      ReDim txtLine(0)
      ReDim mLex(0)
      tagc = New clsCount()
      Console.WriteLine("Копирование структур")
      ' создать объект програмы и упаковать его
      prog = New clsModule()
      Console.WriteLine("Отработать правила")
      Правила()
   End Sub
End Module