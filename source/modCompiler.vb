' Главный модуль компилятора
' TODO: Для анализатора -- надо проверять, чтобы имена не начинались  с цифер

Imports System.IO
Imports System.Diagnostics

Namespace пиОк
   ''' <summary>
   ''' Содержит имя объекта и методы обработки имени
   ''' </summary>
   Public Class clsName
      Dim _name As String = ""
      ''' <summary>
      ''' Строкове обозначение имени объекта.
      ''' </summary>
      ''' <param name="_str"></param>
      ''' <returns></returns>
      Public Property strVal() As String
         Get
            Return Me._name
         End Get
         Set(value As String)
            Dim res As String
            res = модУтиль.ЕслиИмя(value)
            If res = "" Then
               Me._name = value
            Else
               Throw New Exception(Me._name + ":" + res + " val=" + value)
            End If
         End Set
      End Property
      Public Sub New(_Name As String)
         Me._name = _Name
      End Sub
   End Class
   ''' <summary>
   ''' Содержит распознанный тэг -- лексему.
   '''наследуется от clsTag 
   ''' </summary>
   Public Class clsLex
      Inherits clsTag
      Public type_ As String = "" ' тип тега
      ''' <summary>
      ''' Строковое предствление тега для лексемы.
      ''' </summary>
      Public litName As clsName ' настоящее имя модуля
      Public Sub New(_strTag As String, iStr As Integer, iPoz As Integer)
         MyBase.New(_strTag, iStr, iPoz)
         Me.litName = New clsName(_strTag)
      End Sub
   End Class
   ''' <summary>
   ''' Содержит список модулей для импорта
   ''' </summary>
   Public Class clsImport ' содержит имена модулей для имопрта и их алиасы
      Public name As clsName
      Public alias_ As clsName
      Public Sub New(Optional _name As String = "", Optional _alias As String = "")
         Me.name = New clsName(_name)
         Me.alias_ = New clsName(_alias)
      End Sub
   End Class
   Public Class clsConst ' класс содержащий константу
      ' Стили именования
      Public name As String
      Public val As String
      Public type_ As String
   End Class
   Public Class clsModule ' Описывает модуль целиком
      Public tag_end As Integer = 0 'номер последнего значимого тега
      Public level As Integer = 0 ' 0 -- это главный
      Public loaded As Boolean
      Public import() As clsImport  ' Список модулей импорта
      Public const_() As clsConst 'список констант
      Public proc As Integer
      Public name As String = ""
      Public Sub ErrorOpen(txtLine As String, _mLex As clsLex)
         модКокон.Ошибка("Крд: " + Str(_mLex.coord.iStr) + " -" + Str(_mLex.coord.iPos))
         Console.WriteLine(txtLine)
         Console.WriteLine(Смещ(_mLex.coord.iPos))
         модКокон.Ошибка("Модуль должен начинаться с ""MODULE""")
         Environment.Exit(1)
      End Sub
   End Class
   Public Module модКомпиль
      Dim литАнализ As String = "" ' Look литера для анализа
      Dim txtBeg As String = "" ' Начало текст Visual Basic
      Dim txtOut As String = "" ' Конец текст Visual Basic
      Dim перем(1000) As String ' Массив добавляемых переменных
      Dim цПерем As Integer = 0 'Свободный элемент массива
      Sub Вых_Записать()
         Dim sw As StreamWriter
         sw = File.CreateText("out.vb")
         Using sw
            sw.Write(txtOut)
         End Using
      End Sub

      Sub Транслировать()
         Console.WriteLine("Начало трансляции")
         Dim pr As New Process()
         pr.StartInfo.FileName = "vbc"
         pr.StartInfo.Arguments = "/debug- /t:exe /platform:x86 /nologo /utf8output /optionexplicit+ /optioninfer+ /rootnamespace:Oberon07 /out:out.exe out.vb"
         pr.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
         pr.Start()
         pr.WaitForExit()

         'Process.Start("vbc /debug- /t:exe /platform:x86 /nologo /utf8output /optionexplicit+ /optioninfer+ /rootnamespace:Oberon07 /out:out.exe out.vb")
      End Sub

      Sub Заголовок()
         txtBeg = "' Автогенерация текста Visual Basic" + vbCrLf
         txtBeg += "' Данные для компиляции" + vbCrLf
         txtBeg += "Namespace Oberon07" + vbCrLf
         txtBeg += "Public Module modOut" + vbCrLf + vbCrLf
         txtBeg += "Dim рег0 As Integer = 0" + vbCrLf
         txtBeg += "Dim рег1 As Integer = 0" + vbCrLf
         txtBeg += "Dim head As Integer = 0" + vbCrLf
         txtBeg += "Dim стек(1000) As Integer' программный стек" + vbCrLf
         txtBeg += "Dim sp As Integer = 0'указатель стека" + vbCrLf + vbCrLf
         txtBeg += "Sub push(arg As Integer)" + vbCrLf
         txtBeg += "   If (sp+1)<1000 Then" + vbCrLf
         txtBeg += "      sp +=1" + vbCrLf
         txtBeg += "   Else" + vbCrLf
         txtBeg += "      Console.WriteLine(""ВНИМАНИЕ! Стек переполнен!!!"")" + vbCrLf
         txtBeg += "   End If" + vbCrLf
         txtBeg += "   стек(sp) = arg" + vbCrLf
         txtBeg += "End Sub" + vbCrLf + vbCrLf

         txtBeg += "Sub pop(ByRef arg As Integer)" + vbCrLf
         txtBeg += "   arg = стек(sp)" + vbCrLf
         txtBeg += "   If (sp-1)>=0 Then" + vbCrLf
         txtBeg += "      sp -=1" + vbCrLf
         txtBeg += "   Else" + vbCrLf
         txtBeg += "      Console.WriteLine(""ВНИМАНИЕ! Стек пустой!!!"")" + vbCrLf
         txtBeg += "   End If" + vbCrLf
         txtBeg += "End Sub" + vbCrLf + vbCrLf

         txtOut += "Sub Main()" + vbCrLf
      End Sub

      Sub Подвал()
         For i As Integer = 0 To цПерем - 1
            txtBeg += "Dim " + перем(i) + " As Integer = 0" + vbCrLf
         Next
         txtOut += "Console.WriteLine(""Result: "" + Str(рег0))" + vbCrLf
         txtOut += "End Sub" + vbCrLf
         txtOut += "End Module" + vbCrLf
         txtOut += "End Namespace"
         txtOut = txtBeg + txtOut
         Вых_Записать()
         Транслировать()
      End Sub

      Public Sub Компилировать()
         Заголовок()
         Подвал()
      End Sub
   End Module
   Public Module модКомпиль2
      ' ==================== ПРАВИЛА ============================
      Dim tagc As Integer = 0 ' текущий тег на анализе
      Dim sRes As String = "" ' результат анализа
      Dim mLex() As clsLex
      Dim txtLine() As String ' список строк исходника
      Dim prog As clsModule ' объект главного модуля есть программ
      Sub Структуры_Копировать()
         Dim i As Integer = 0
         Dim lex_ As clsLex
         If Not IsNothing(modTagger.tags) Then
            Do While (i < modTagger.tags.Length)
               lex_ = New clsLex(modTagger.tags(i).strTag,
                                      modTagger.tags(i).coord.iStr,
                                      modTagger.tags(i).coord.iPos)
               If IsNothing(mLex) Then
                  ReDim mLex(0)
               Else
                  ReDim Preserve mLex(mLex.Length)
               End If

               mLex(mLex.Length - 1) = lex_
               i += 1
            Loop
         End If
         modTagger.tags = Nothing

         i = 0
         If Not IsNothing(modTagger.txtLine) Then
            Do While i < modTagger.gCoord.iStr
               If IsNothing(txtLine) Then
                  ReDim txtLine(0)
               Else
                  ReDim Preserve txtLine(txtLine.Length)
               End If
               txtLine(txtLine.Length - 1) = modTagger.txtLine(i)
               i += 1
            Loop
         End If
         modTagger.txtLine = Nothing
      End Sub
      Function Смещ(ind As Integer) As String
         Dim s As String = "^"
         For i As Integer = 1 To ind - 1
            s = " " + s
         Next
         Return s
      End Function
      Sub ОшибкаИмени(msg As String, t As Integer)
         модКокон.Ошибка(msg + ":" + Str(t) + " >" + Str(mLex(t).strTag) + "<")
         модКокон.Ошибка("Крд: " + Str(mLex(t).coord.iStr) + " -" + Str(mLex(t).coord.iPos))
         Console.WriteLine(txtLine(mLex(t).coord.iStr))
         Console.WriteLine(Смещ(mLex(t).coord.iPos))
         модКокон.Ошибка("Имя должно начинается с буквы или ""_""")
      End Sub
      Sub Пр_КОММЕНТАРИЙ()
         ' правило ищет комметарии и иключает их из кода
         Dim count As Integer = 0
         Dim bStrip As Boolean
         Dim tmpLex() As clsLex = Nothing
         Dim tag As String
         If sRes = "comment" Then
            Do While count < mLex.Length
               tag = mLex(count).strTag
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
               If IsNothing(tmpLex) Then
                  ReDim tmpLex(0)
               Else
                  ReDim Preserve tmpLex(tmpLex.Length)
               End If

               tmpLex(tmpLex.Length - 1) = mLex(count)
               count += 1
            Loop

            mLex = tmpLex
            tmpLex = Nothing

            If bStrip = True Then
               модКокон.Ошибка("Крд: " + Str(mLex(mLex.Length - 1).coord.iStr + 1) + " -" _
                               + Str(mLex(mLex.Length - 1).coord.iPos))
               Console.WriteLine(txtLine(mLex(mLex.Length - 1).coord.iStr + 1))
               Console.WriteLine(Смещ(mLex(mLex.Length - 1).coord.iPos))
               модКокон.Ошибка("Блок комментария не закрыт")
               sRes = "err"
               Exit Sub
            Else
               sRes = "module"
            End If
         End If
      End Sub
      Sub Пр_МОДУЛЬ()
         ' 1.1 МОДУЛЬ должен быть первым
         If sRes = "module" Then
            If mLex(0).strTag = "MODULE" Then
               ' открываем наш модуль
               sRes = "1.2"
            Else
               prog.ErrorOpen(txtLine(mLex(0).coord.iStr), mLex(0))
               Exit Sub
            End If
         End If
         '1.2 У модуля должно быть имя
         If sRes = "1.2" Then
            ' имя модуля -- № 1, разделитель -- № 2
            If mLex(2).strTag <> ";" Then ' пропущено имя модуля
               модКокон.Ошибка("Крд: " + Str(mLex(1).coord.iStr) + " -" + Str(mLex(1).coord.iPos))
               Console.WriteLine(txtLine(mLex(1).coord.iStr))
               Console.WriteLine(Смещ(mLex(1).coord.iPos))
               модКокон.Ошибка("Пропущено имя модуля или разделитель")
               sRes = "err"
               Exit Sub
            End If
            ' проверка на допустимое имя. Должно начинаться либо с "_"  либо с буквы
            If модУтиль.ЕслиНачИмени(Mid(mLex(1).strTag, 1, 1)) Then
               prog.name = mLex(1).strTag
               sRes = "1.3"
            Else
               ОшибкаИмени("MODULE", 1)
               sRes = "err"
               Exit Sub
            End If
         End If
         ' 1.3 У Модуля должно быть окончание
         If sRes = "1.3" Then
            Dim bEnd As Boolean = False
            Dim i As Integer = 3 ' начинаем отсчёт сразу за определением модуля
            Do While i < mLex.Length
               If mLex(i).strTag = "END" Then ' относится ли это к концу модуля?
                  If mLex(i + 2).strTag = "." Then ' конец ли это? i+2 -- через имя
                     bEnd = True
                     ' отбрасываем лишние тэги
                     ' ограничивать будем полем структуры программы
                     prog.tag_end = i + 2
                     sRes = "1.4"
                     Exit Do
                  End If
               End If
               i += 1
            Loop
            If Not bEnd Then 'а конца то нет!! работаем с последним тегом
               модКокон.Ошибка("Крд: " + Str(mLex(mLex.Length - 1).coord.iStr) + " -" +
                               Str(mLex(mLex.Length - 1).coord.iPos))
               Console.WriteLine(txtLine(mLex(mLex.Length - 1).coord.iStr))
               Console.WriteLine(Смещ(mLex(mLex.Length - 1).coord.iPos))
               модКокон.Ошибка("Модуль должен иметь ""END <NameModule.>""")
               sRes = "err"
            End If
         End If
         '1.4 Модуль должен быть один
         If sRes = "1.4" Then
            ' Организуем цикл в поиске МОДУЛЬ с учётом, что это может быть строка
            ' Интересует только первая тотальная встреча
            Dim i As Integer = 1 ' нельзя брать 0, так как это и есть модуль
            Dim bKw As Boolean = True
            Do While i < prog.tag_end  ' последние тэги мы уже выяснили
               If mLex(i).strTag = "MODULE" Then 'надо выясить, может это часть выражения, или строка
                  If (mLex(i - 1).strTag = ".") Then
                     bKw = False ' часть сущности
                  ElseIf mLex(i - 1).strTag = """" And mLex(i + 1).strTag = """" Then
                     bKw = False ' строка
                  ElseIf mLex(i - 1).strTag = "'" And mLex(i + 1).strTag = "'" Then
                     bKw = False ' строка
                  Else ' да. Это не строка, и не часть сущности!!
                     bKw = True
                     модКокон.Ошибка("Крд: " + Str(mLex(i).coord.iStr + 1) + " -" + Str(mLex(i).coord.iPos))
                     Console.WriteLine(txtLine(mLex(i).coord.iStr + 1))
                     Console.WriteLine(Смещ(mLex(i).coord.iPos))
                     модКокон.Ошибка("MODULE должен быть один")
                     sRes = "err"
                     Exit Sub
                  End If
               End If
               i += 1
            Loop
            If i = prog.tag_end Then
               bKw = False
               sRes = "1.5"
            End If
         End If
         ' 1.5 Имя модуля и имя конца должны совпадать
         If sRes = "1.5" Then
            If prog.name <> mLex(prog.tag_end - 1).strTag Then
               ' это залёт!
               Console.WriteLine(txtLine(mLex(0).coord.iStr))
               Console.WriteLine(txtLine(mLex(prog.tag_end - 1).coord.iStr))
               модКокон.Ошибка("Имя модуля несогласовано")
               sRes = "err"
               Exit Sub
            Else
               sRes = "import"
            End If
         End If
      End Sub
      Sub Импорт_Ошибка()
         модКокон.Ошибка("Крд: " + Str(mLex(tagc).coord.iStr) + " -" + Str(mLex(tagc).coord.iPos))
         Console.WriteLine(txtLine(mLex(tagc).coord.iStr))
         Console.WriteLine(Смещ(mLex(tagc).coord.iPos))
         модКокон.Ошибка("Нарушение порядка импорта>")
         sRes = "err"
      End Sub
      Sub Пр_ИМПОРТ()
         ' прочесали модуль, теперь проверить нет ли импорта
         If sRes = "import" Then ' 2.1 IMPORT может идти тегом № 3 -- проверяем
            If mLex(3).strTag = "IMPORT" Then
               sRes = "2.2"
            End If
         End If
         ' 2.2 Проверяем весь доступный импорт
         If sRes = "2.2" Then
            ' Может быть прямой импорт, а может быть и с алиасами.
            ' Если импорт прямой, то tagc+2 будет ";", а сли алиас -- то ":="
            ' После ИМПОРТ имя файла или алиас по счёту -- 4 тег в файле
            tagc = 4 ' за именем -- либо разделитель, либо присвоение алиаса
            Do While True
               ' Импортов может быть 
               ' прямой, c алиасом, с запятой (продолжение), с ";" -- конец импорта
               If mLex(tagc + 1).strTag = "," Or mLex(tagc + 1).strTag = ";" Then ' Первая ветка -- прямой импорт
                  ' проверить имя модуля и алиас на допустимость
                  If Not модУтиль.ЕслиНачИмени(Mid(mLex(tagc).strTag, 1, 1)) Then
                     '  неправильное имени
                     ОшибкаИмени("IMPORT", tagc + 1)
                     sRes = "err"
                     Exit Sub
                  End If
                  If IsNothing(prog.import) Then
                     ReDim prog.import(0)
                  Else
                     ReDim Preserve prog.import(prog.import.Length + 1)
                  End If
                  Dim imp As clsImport = New clsImport(mLex(tagc).strTag)
                  prog.import(prog.import.Length - 1) = imp
                  If mLex(tagc + 1).strTag = "," Then 'импорт может закончился?
                     tagc += 2
                     Continue Do
                  ElseIf mLex(tagc + 1).strTag = ";" Then ' импорт закончить
                     tagc += 2
                     sRes = "2.3"
                     Exit Do
                  Else ' а это уже ошибка!!
                     Импорт_Ошибка()
                     Exit Sub
                  End If
               ElseIf mLex(tagc + 1).strTag = ":=" Then ' вторая ветка -- импорт с алиасом
                  ' проверка имени
                  If Not модУтиль.ЕслиНачИмени(Mid(mLex(tagc + 2).strTag, 1, 1)) Then
                     '  неправильное имени
                     ОшибкаИмени("IMPORT", tagc + 2)
                     sRes = "err"
                     Exit Sub
                  End If
                  ' проверка алиаса
                  If Not модУтиль.ЕслиНачИмени(Mid(mLex(tagc).strTag, 1, 1)) Then
                     '  неправильное имени
                     ОшибкаИмени("IMPORT", tagc + 1)
                     sRes = "err"
                     Exit Sub
                  End If
                  ' значит добавить элемент импрота
                  If IsNothing(prog.import) Then
                     ReDim prog.import(0)
                  Else
                     ReDim Preserve prog.import(prog.import.Length + 1)
                  End If
                  Dim imp As clsImport = New clsImport(mLex(tagc + 2).strTag, mLex(tagc).strTag)
                  prog.import(prog.import.Length - 1) = imp
                  ' проверка на продолжение
                  If mLex(tagc + 3).strTag = "," Then 'импорт может закончился?
                     tagc += 4
                     Continue Do
                  ElseIf mLex(tagc + 3).strTag = ";" Then ' импорт закончить
                     tagc += 4
                     sRes = "2.3"
                     Exit Do
                  Else ' а это уже ошибка!!
                     Импорт_Ошибка()
                     sRes = "err"
                     Exit Sub
                  End If
               Else
                  Импорт_Ошибка()
                  sRes = "err"
                  Exit Sub
               End If
            Loop
         End If
         ' 2.3 Проверка на ошибку досрочного окончания импорта
         If sRes = "2.3" Then
            If (mLex(tagc).strTag = "CONST" Or mLex(tagc).strTag = "TYPE" Or
                  mLex(tagc).strTag = "VAR" Or mLex(tagc).strTag = "PROCEDURE" Or
                  mLex(tagc).strTag = "BEGIN") Or
                  (mLex(prog.tag_end - 2).strTag = mLex(tagc).strTag And
                  mLex(prog.tag_end).strTag = ".") Then
               ' здесь вообще варианты. Но все дальше должны проверить
               sRes = "const"
            Else
               tagc -= 1
               Импорт_Ошибка()
               модКокон.Ошибка("Досрочное прекращение импорта")
               sRes = "err"
               Exit Sub
            End If
         End If
      End Sub
      Sub Пр_КОНСТ()
         ' Проверяет правильность объявления констант
         If sRes = "const" Then ' Правило объявления инструкции CONST
            If mLex(tagc).strTag <> "CONST" Then ' возможно просто нет такой секции
               tagc += 1
               sRes = "4.1"
               Exit Sub
            Else ' такая инструкция есть";"-- запрещено
               tagc += 1
               If mLex(tagc).strTag = ";" Then
                  модКокон.Ошибка("Крд: " + Str(mLex(tagc).coord.iStr) + " -" + Str(mLex(tagc).coord.iPos))
                  Console.WriteLine(txtLine(mLex(tagc).coord.iStr))
                  Console.WriteLine(Смещ(mLex(tagc).coord.iPos))
                  модКокон.Ошибка("Пропущено имя константы")
                  sRes = "err"
                  Exit Sub
               Else
                  sRes = "3.2"
               End If
            End If
         End If
         If sRes = "3.2" Then ' начинаем разбор констант
            Do
               ' секция CONST может быть пустой
               If mLex(tagc).strTag = "TYPE" Or mLex(tagc).strTag = "VAR" Or
                     mLex(tagc).strTag = "PROCEDURE" Or mLex(tagc).strTag = "BEGIN" Or
                     (mLex(tagc).strTag = "END" And mLex(tagc + 2).strTag = ".") Then
                  tagc += 1
                  sRes = "4.1"
                  Exit Do
               End If
               If ClassTag(Mid(mLex(tagc).strTag, 1, 1)) <> modConst.multitag Then ' имя не может быть пустым
                  модКокон.Ошибка("Крд: " + Str(mLex(tagc).coord.iStr) + " -" + Str(mLex(tagc).coord.iPos))
                  Console.WriteLine(txtLine(mLex(tagc).coord.iStr))
                  Console.WriteLine(Смещ(mLex(tagc).coord.iPos))
                  модКокон.Ошибка("Пропущено имя константы")
                  sRes = "err"
                  Exit Sub
               Else
                  ' Добавляем константу в секцию констант
                  If (Not ЕслиНачИмени(Mid(mLex(tagc).strTag, 1, 1))) Then
                     ОшибкаИмени("Константы:", tagc)
                     sRes = "err"
                     Exit Sub
                  Else
                     If IsNothing(prog.const_) Then
                        ReDim prog.const_(0)
                     Else
                        ReDim Preserve prog.const_(prog.const_.Length + 1)
                     End If
                     Dim const_ As clsConst = New clsConst With {
                        .name = mLex(tagc).strTag}
                     prog.const_(prog.const_.Length - 1) = const_
                     tagc += 1
                     If mLex(tagc).strTag <> "=" Then
                        модКокон.Ошибка("Крд: " + Str(mLex(tagc).coord.iStr) + " -" + Str(mLex(tagc).coord.iPos))
                        Console.WriteLine(txtLine(mLex(tagc).coord.iStr))
                        Console.WriteLine(Смещ(mLex(tagc).coord.iPos))
                        модКокон.Ошибка("Нарушение присвоения константы")
                        sRes = "err"
                        Exit Sub
                     Else
                        tagc += 1
                        'TODO: тут надо выяснять что за тип данных
                        ' тут могут быть BOOLEAN, CHAR, STRING, INTEGER, REAL
                        If mLex(tagc).strTag = """" And mLex(tagc + 2).strTag = """" Then 'Это строка
                           prog.const_(tagc + 1).val = mLex(tagc).strTag
                           prog.const_(tagc + 1).type_ = "string"
                           tagc += 3
                        ElseIf mLex(tagc).strTag = "TRUE" Or mLex(tagc).strTag = "FALSE" Then
                           prog.const_(tagc).val = mLex(tagc).strTag
                           prog.const_(tagc).type_ = "boolean"
                           tagc += 1
                           ' проверка на real
                        ElseIf модУтиль.ЕслиЦелое(mLex(tagc).strTag) And InStr(".", mLex(tagc + 1).strTag) > 0 And
                               модУтиль.ЕслиЦелое(mLex(tagc + 2).strTag) Then
                           prog.const_(tagc).val = mLex(tagc).strTag + mLex(tagc + 1).strTag + mLex(tagc + 2).strTag
                           prog.const_(tagc).type_ = "real"
                           tagc += 3
                        ElseIf модУтиль.ЕслиЦелое(mLex(tagc).strTag) And InStr(";", mLex(tagc + 1).strTag) > 0 Then
                           prog.const_(tagc).val = mLex(tagc).strTag
                           prog.const_(tagc).type_ = "integer"
                           tagc += 2
                        Else
                           модКокон.Ошибка("Крд: " + Str(mLex(tagc).coord.iStr) + " -" + Str(mLex(tagc).coord.iPos))
                           Console.WriteLine(txtLine(mLex(tagc).coord.iStr))
                           Console.WriteLine(Смещ(mLex(tagc).coord.iPos))
                           модКокон.Ошибка("Нарушение присвоения константы")
                           sRes = "err"
                           Exit Sub

                        End If
                     End If
                  End If
               End If
            Loop
         End If
      End Sub
      Sub Правила()
         ' проверить правильность полученного исходного текста
         sRes = "comment"
         Пр_КОММЕНТАРИЙ()
         Dim i As Integer = 0
         Do While i < mLex.Length - 1
            Console.Write(Str(i) + ")" + mLex(i).strTag + vbTab)
            i += 1
            Console.Write(Str(i) + ")" + mLex(i).strTag + vbTab)
            i += 1
            Console.WriteLine(Str(i) + ")" + mLex(i).strTag)
            i += 1
         Loop
         Пр_МОДУЛЬ()
         Пр_ИМПОРТ()
         Пр_КОНСТ()
      End Sub
      Public Sub Компилировать()
         ' нарезать колбасу из исхдника с присовением координат
         Console.WriteLine("Разметка тегов")
         modTagger.Тег_Разметить()
         Console.WriteLine("All tags:" + Str(modTagger.tags.Length))
         Dim i As Integer = 0
         Do While i < modTagger.tags.Length
            Console.Write(Str(i) + ")" + modTagger.tags(i).strTag + vbTab)
            i += 1
            Console.Write(Str(i) + ")" + modTagger.tags(i).strTag + vbTab)
            i += 1
            Console.WriteLine(Str(i) + ")" + modTagger.tags(i).strTag)
            i += 1
         Loop
         Console.WriteLine("Копирование структур")
         Структуры_Копировать()
         ' создать объект програмы и упаковать его
         prog = New clsModule()
         Console.WriteLine("Отработать правила")
         Правила()
         i = 0
         Do While i < mLex.Length - 1 And i < 20
            Console.WriteLine(Str(i) + ": " + mLex(i).strTag)
            i += 1
         Loop
         Console.WriteLine("_end_")

      End Sub
   End Module
End Namespace