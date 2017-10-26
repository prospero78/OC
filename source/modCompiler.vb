' Главный модуль компилятора
' TODO: Для анализатора -- надо проверять, чтобы имена не начинались  с цифер
Imports System.IO
Imports System.Diagnostics

Namespace пиОк
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
            sw.Close()
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
   Public Class clsLexem
      Inherits клсТег
      Public type_ As String = "" ' тип тега
      Public name_alias As String = "" ' алиас для имени модуля
      Public name_origin As String = "" ' настоящее имя модуля
   End Class
   Public Class clsImport ' содержит имена модулей для имопрта и их алиасы
      Public name As String = ""
      Public alias_ As String = ""
   End Class
   Public Class clsModule ' Описывает модуль целиком
      Inherits clsLexem
      Public tag_end As Integer = 0 'номер последнего значимого тега
      Public level As Integer = 0 ' 0 -- это главный
      Public loaded As Boolean
      Public import() As clsImport  ' Список модулей импорта
      Public proc As Integer
   End Class
   Public Module модКомпиль2
      ' ==================== ПРАВИЛА ============================
      Dim tagc As Integer = 0 ' текущий тег на анализе
      Dim sRes As String = "" ' результат анализа
      Dim lex(0) As clsLexem
      Dim lex_end As Integer = 0 ' конец полезных лексем
      Dim txtLine() As String ' список строк исходника
      Dim prog As clsModule ' объект главного модуля есть программа
      Sub Структуры_Копировать()
         Dim i As Integer = 0
         Dim lex_ As clsLexem
         If Not IsNothing(модТеггер.теги) Then
            Do While (i < модТеггер.теги.Length)
               lex_ = New clsLexem With {
                   .цСтр = модТеггер.теги(i).цСтр,
                   .цПоз = модТеггер.теги(i).цПоз,
                   .стрТег = модТеггер.теги(i).стрТег
               }
               ReDim Preserve lex(i)
               lex(i) = lex_
               i += 1
            Loop
         End If
         ReDim модТеггер.теги(0)

         i = 0
         If Not IsNothing(модТеггер.txtLine) Then
            Do While i <= модТеггер.гцСтр
               If IsNothing(txtLine) Then
                  ReDim txtLine(0)
               Else
                  ReDim Preserve txtLine(i + 1)
               End If

               txtLine(i) = модТеггер.txtLine(i)
               i += 1
            Loop
         End If

         ReDim модТеггер.txtLine(0)

      End Sub

      Function Смещ(ind As Integer) As String
         Dim s As String = "^"
         For i As Integer = 1 To ind - 1
            s = " " + s
         Next
         Return s
      End Function
      Sub Пр_КОММЕНТАРИЙ(sr As String)
         ' правило ищет комметарии и иключает их из кода
         Dim count As Integer = 0
         Dim bStrip As Boolean
         Dim tmpLex() As clsLexem = Nothing
         Dim i As Integer = 0
         Do While count < lex.Length
            If lex(count).стрТег = "(*" Then ' начало коммента
               bStrip = True
            End If
            If bStrip = True Then ' пропускаем комментарий
               If lex(count).стрТег <> "*)" Then
                  count += 1
               Else
                  bStrip = False
                  count += 1
               End If
            End If
            If bStrip = False Then ' копирование остального
               If IsNothing(txtLine) Then
                  ReDim tmpLex(0)
               Else
                  ReDim Preserve tmpLex(i + 1)
               End If
               tmpLex(i) = lex(count)
               i += 1
               count += 1
            End If
         Loop
         lex = tmpLex
         tmpLex = Nothing
         If sr = "comment" Then
            If bStrip = True Then
               модКокон.Ошибка("Крд: " + Str(lex(lex.Length - 1).цСтр) + " -" + Str(lex(lex.Length - 1).цПоз))
               Console.WriteLine(txtLine(lex(lex.Length - 1).цСтр))
               Console.WriteLine(Смещ(lex(lex.Length - 2).цПоз))
               модКокон.Ошибка("Блок комментария не закрыт")
               sRes = "err"
            Else
               sRes = "MODULE"
            End If
         End If
      End Sub
      Sub Пр_МОДУЛЬ()
         ' 1.1 МОДУЛЬ должен быть первым
         If sRes = "MODULE" Then
            If lex(0).стрТег = "MODULE" Then
               lex(0).type_ = "MODULE"
               tagc = 1
               ' открываем наш модуль
               prog.loaded = True
               sRes = "1.2"
            Else
               модКокон.Ошибка("Крд: " + Str(lex(0).цСтр) + " -" + Str(lex(0).цПоз))
               Console.WriteLine(txtLine(lex(0).цСтр))
               Console.WriteLine(Смещ(lex(0).цПоз))
               модКокон.Ошибка("Модуль должен начинаться с ""MODULE""")
               sRes = "err"
            End If
         End If
         '1.2 У модуля должно быть имя
         If sRes = "1.2" Then
            ' имя модуля -- № 1, разделитель -- № 2
            If lex(2).стрТег <> ";" Then ' пропущено имя модуля
               модКокон.Ошибка("Крд: " + Str(lex(1).цСтр) + " -" + Str(lex(1).цПоз))
               Console.WriteLine(txtLine(lex(1).цСтр))
               Console.WriteLine(Смещ(lex(1).цПоз))
               модКокон.Ошибка("Пропущено имя модуля")
               sRes = "err"
               Exit Sub
            End If
            ' проверка на допустимое имя. Должно начинаться либо с "_"  либо с буквы
            If модУтиль.ЕслиНачИмени(Mid(lex(1).стрТег, 1, 1)) Then
               lex(1).type_ = "module_name"
               prog.стрТег = lex(1).стрТег
            Else
               модКокон.Ошибка("Крд: " + Str(lex(1).цСтр) + " -" + Str(lex(1).цПоз))
               Console.WriteLine(txtLine(lex(1).цСтр))
               Console.WriteLine(Смещ(lex(1).цПоз))
               модКокон.Ошибка("Имя модуля начинается только с буквы и ""_""")
               sRes = "err"
               Exit Sub
            End If
            ' проверка на разделитель
            If lex(2).стрТег = ";" Then
               lex(2).type_ = ";"
               sRes = "1.3"
            Else
               '  а нет разделителя после имени модуля!
               модКокон.Ошибка("Крд: " + Str(lex(1).цСтр + 1) + " -" + Str(lex(1).цПоз))
               Console.WriteLine(txtLine(lex(1).цСтр + 1))
               Console.WriteLine(Смещ(lex(1).цПоз))
               модКокон.Ошибка("Нет разделителя для имени модуля")
               sRes = "err"
               Exit Sub
            End If
         End If
         ' 1.3 У Модуля должно быть окончание
         If sRes = "1.3" Then
            Dim bEnd As Boolean = False
            Dim i As Integer = 3 ' начинаем отсчёт сразу за определением модуля
            Do While i < lex.Count
               If lex(i).стрТег = "END" Then ' относится ли это к концу модуля?
                  If lex(i + 2).стрТег = "." Then ' конец ли это? i+2 -- через имя
                     lex(i).type_ = "module_end"
                     lex(i + 1).type_ = "module_name"
                     lex(i + 2).type_ = "module_dot"
                     bEnd = True
                     sRes = "1.4"
                     Exit Do
                  End If
               End If
               i += 1
            Loop
            If bEnd = False Then 'а конца то нет!! работаем с последним тегом
               модКокон.Ошибка("Крд: " + Str(lex(lex.Count - 1).цСтр) + " -" + Str(lex(lex.Count - 1).цПоз))
               Console.WriteLine(txtLine(lex(lex.Count - 1).цСтр))
               Console.WriteLine(Смещ(lex(lex.Count - 1).цПоз))
               модКокон.Ошибка("Модуль должен иметь ""END <NameModule.>""")
               sRes = "err"
            Else
               ' отбрасываем лишние тэги
               ' ограничивать будем счётчиком
               lex_end = i
               sRes = "1.4"
            End If
         End If
         If sRes = "1.4" Then ' 1.4 Модуль должен быть один
            ' Организуем цикл в поиске МОДУЛЬ с учётом, что это может быть строка
            ' Интересует только первая тотальная встреча
            Dim i As Integer = 1 ' нельзя брать 0, так как это и есть модуль
            Dim bKw As Boolean = True
            Do While i < lex_end ' последние тэги мы уже выяснили
               If lex(i).стрТег = "MODULE" Then 'надо выясить, может это часть выражения, или строка
                  If (lex(i - 1).стрТег = ".") Then
                     bKw = False
                  ElseIf lex(i - 1).стрТег = """" And lex(i + 1).стрТег = """" Then
                     bKw = False
                  ElseIf lex(i - 1).стрТег = "'" And lex(i + 1).стрТег = "'" Then
                     bKw = False ' это не ключевое слово
                  Else ' да. Это не строка, и не часть сущности!!
                     bKw = True
                     Exit Do
                  End If
               End If
               i += 1
            Loop
            If i = lex_end Then
               bKw = False
            End If
            If bKw = True Then
               модКокон.Ошибка("Крд: " + Str(lex(i).цСтр + 1) + " -" + Str(lex(i).цПоз))
               Console.WriteLine(txtLine(lex(i).цСтр + 1))
               Console.WriteLine(Смещ(lex(i).цПоз))
               модКокон.Ошибка("MODULE должен быть один")
               sRes = "Err"
            Else
               sRes = "2.1"
            End If
         End If
      End Sub
      Sub Импорт_Ошибка()
         модКокон.Ошибка("Крд: " + Str(lex(tagc).цСтр) + " -" + Str(lex(tagc).цПоз))
         Console.WriteLine(txtLine(lex(tagc).цСтр))
         Console.WriteLine(Смещ(lex(tagc).цПоз))
         модКокон.Ошибка("Нарушение порядка импорта>")
         sRes = "err"
      End Sub
      Sub Пр_ИМПОРТ()
         ' прочесали модуль, теперь проверить нет ли импорта
         If sRes = "2.1" Then ' 2.1 IMPORT может идти тегом № 3 -- проверяем
            If lex(3).стрТег = "IMPORT" Then
               lex(3).type_ = "import"
               tagc = 3
               sRes = "2.2"

            End If
         End If
         If sRes = "2.2" Then ' 2.2 Проверяем весь доступный импорт
            ' Может быть прямой импорт, а может быть и с алиасами.
            ' Если импорт прямой, то tagc+2 будет ";", а сли алиас -- то ":="
            ' После ИМПОРТ имя файла или алиас по счёту -- 4 тег в файле
            tagc = 4 ' за именем -- либо разделитель, либо присвоение алиаса
            Do While True
               ' Импортов может быть 
               ' прямой, c алиасом, с запятой (продолжение), с ";" -- конец импорта
               If lex(tagc + 1).стрТег = "," Or lex(tagc + 1).стрТег = ";" Then ' Первая ветка -- прямой импорт
                  lex(tagc).type_ = "module_alias"

                  ' проверить ия модуля и алиас на допустимость
                  If модУтиль.ЕслиНачИмени(Mid(lex(tagc + 1).стрТег), 1, 1) Then
                     lex(tagc).name_origin = lex(tagc + 1).стрТег
                  Else
                     ' TODO: сделать дома процедуру неправильного имени
                  End If
                  If IsNothing(prog.import) Then
                        ReDim prog.import(0)
                     Else
                        ReDim Preserve prog.import(prog.import.Length + 1)
                     End If
                     prog.import(prog.import.Length - 1).name = lex(tagc + 1).стрТег
                     If lex(tagc + 1).стрТег = "," Then
                        lex(tagc + 1).type_ = ","
                     Else
                        lex(tagc + 1).type_ = ";"
                     End If
                     If lex(tagc + 1).type_ = "," Then 'импорт может закончился?
                        tagc += 2
                        Continue Do
                     ElseIf lex(tagc + 1).type_ = ";" Then ' импорт закончить
                        tagc += 2
                        Exit Do
                     Else ' а это уже ошибка!!
                        Импорт_Ошибка()
                        Exit Do
                     End If
                  ElseIf lex(tagc + 1).стрТег = ":=" Then ' вторая ветка -- импорт с алиасом
                     lex(tagc).type_ = "module_alias" ' имя алиаса
                     lex(tagc).name_origin = lex(tagc + 2).стрТег
                     If lex(tagc + 3).стрТег = "," Then
                        lex(tagc + 3).type_ = ","
                     ElseIf lex(tagc + 3).стрТег = ";" Then
                        lex(tagc + 3).type_ = ";"
                     Else ' а вот это уже ошибка
                        tagc += 2
                        Импорт_Ошибка()
                        Exit Do
                     End If
                     If lex(tagc + 3).type_ = "," Then 'импорт может закончился?
                        tagc += 4
                        Continue Do
                     ElseIf lex(tagc + 3).type_ = ";" Then ' импорт закончить
                        tagc += 4
                        sRes = "3.1"
                        Exit Do
                     Else ' а это уже ошибка!!
                        Импорт_Ошибка()
                        Exit Do
                     End If
                  Else
                     модКокон.Ошибка("Крд: " + Str(lex(tagc).цСтр) + " -" + Str(lex(tagc).цПоз))
                  Console.WriteLine(txtLine(lex(tagc).цСтр))
                  Console.WriteLine(Смещ(lex(tagc).цПоз))
                  модКокон.Ошибка("Нарушение порядка импорта>")
                  sRes = "err"
                  Exit Do
               End If
            Loop
         End If
      End Sub
      Sub Пр_КОНСТ()
         ' Проверяет правильность объявления констант
         If sRes = "3.1" Then ' Правило объявления инструкции CONST
            If lex(tagc).стрТег <> "CONST" Then ' возможно просто нет такой секции
               sRes = "4.1"
               Exit Sub
            Else ' такая инструкция есть
               sRes = "3.2"
               tagc += 1
            End If
         End If
         If sRes = "3.2" Then ' начинаем разбор констант
            Do
               ' секция CONST может быть пустой
               If lex(tagc).стрТег = "TYPE" Or lex(tagc).стрТег = "VAR" Or
                     lex(tagc).стрТег = "PROCEDURE" Or lex(tagc).стрТег = "BEGIN" Or
                     (lex(tagc).стрТег = "END" And lex(tagc + 2).стрТег = ".") Then
                  tagc += 1
                  sRes = "4.1"
                  Exit Do
               End If
               If ЕслиВнутрТег(Mid(lex(tagc).стрТег, 1, 1)) <> модТеггер.multitag Then ' имя не может быть пустым
                  модКокон.Ошибка("Крд: " + Str(lex(tagc).цСтр) + " -" + Str(lex(tagc).цПоз))
                  Console.WriteLine(txtLine(lex(tagc).цСтр))
                  Console.WriteLine(Смещ(lex(tagc).цПоз))
                  модКокон.Ошибка("Пропущено имя константы")
                  sRes = "err"
                  Exit Do
               Else
                  lex(tagc).type_ = "const_name"
                  tagc += 1
                  If lex(tagc).стрТег <> "=" Then
                     модКокон.Ошибка("Крд: " + Str(lex(tagc).цСтр) + " -" + Str(lex(tagc).цПоз))
                     Console.WriteLine(txtLine(lex(tagc).цСтр))
                     Console.WriteLine(Смещ(lex(tagc).цПоз))
                     модКокон.Ошибка("Нарушение присовения константы")
                     sRes = "err"
                     Exit Do
                  Else
                     lex(tagc).type_ = "="
                     tagc += 1
                     If lex(tagc).стрТег = "" Then ' Константа не может быть пустой
                        модКокон.Ошибка("Крд: " + Str(lex(tagc).цСтр) + " -" + Str(lex(tagc).цПоз))
                        Console.WriteLine(txtLine(lex(tagc).цСтр))
                        Console.WriteLine(Смещ(lex(tagc).цПоз))
                        модКокон.Ошибка("Нет значения для присвоения константы")
                        sRes = "err"
                        Exit Do
                     Else
                        lex(tagc).type_ = "const_value"
                        tagc += 1
                        If lex(tagc).стрТег <> ";" Then ' Константа не может быть пустой
                           модКокон.Ошибка("Крд: " + Str(lex(tagc - 1).цСтр) + " -" + Str(lex(tagc - 1).цПоз))
                           Console.WriteLine(txtLine(lex(tagc - 1).цСтр))
                           Console.WriteLine(Смещ(lex(tagc - 1).цПоз))
                           модКокон.Ошибка("Нет ограничения присвоения константы")
                           sRes = "err"
                           Exit Do
                        Else
                           lex(tagc).type_ = ";"
                           tagc += 1
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
         Пр_КОММЕНТАРИЙ(sRes)
         Пр_МОДУЛЬ()
         Пр_ИМПОРТ()
         Пр_КОНСТ()
      End Sub
      Public Sub Компилировать()
         ' нарезать колбасу из исхдника с присовением координат
         Debug.WriteLine("Разметка тегов")
         модТеггер.Тег_Разметить()
         Debug.WriteLine("Копирование структур")
         Структуры_Копировать()
         ' создать объект програмы и упаковать его
         prog = New clsModule()
         Debug.WriteLine("Отработать правила")
         Правила()
         Dim i As Integer = 0
         Do While i < lex.Length - 1
            Console.WriteLine(Str(i) + ": " + lex(i).стрТег)
            i += 1
         Loop
         Console.Write("...end...")
         Console.Read()
      End Sub
   End Module
End Namespace