' Главный модуль компилятора
Imports System.IO
Imports System.Diagnostics

Namespace пиОк
   Public Module модКомпиль
      Dim литАнализ As String = "" ' Look литера для анализа
      Dim txtBeg As String = "" ' Начало текст Visual Basic
      Dim txtOut As String = "" ' Конец текст Visual Basic
      Dim перем(1000) As String ' Массив добавляемых переменных
      Dim цПерем As Integer=0 'Свободный элемент массива
      Sub Вых_Записать()
         Using sw As StreamWriter = File.CreateText("out.vb")
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
         txtBeg +="Namespace Oberon07"+vbCrLf
         txtBeg +="Public Module modOut"+vbCrLf+vbCrLf
         txtBeg +="Dim рег0 As Integer = 0"+vbCrLf
         txtBeg +="Dim рег1 As Integer = 0"+vbCrLf
         txtBeg +="Dim head As Integer = 0"+vbCrLf
         txtBeg +="Dim стек(1000) As Integer' программный стек"+vbCrlf
         txtBeg +="Dim sp As Integer = 0'указатель стека"+vbCrLf+vbCrLf
         txtBeg +="Sub push(arg As Integer)"+vbCrlf
         txtBeg +="   If (sp+1)<1000 Then"+vbCrLf
         txtBeg +="      sp +=1"+vbCrLf
         txtBeg +="   Else"+vbCrLf
         txtBeg +="      Console.WriteLine(""ВНИМАНИЕ! Стек переполнен!!!"")"+vbCrLf
         txtBeg +="   End If"+vbCrLf
         txtBeg +="   стек(sp) = arg"+vbCrLf
         txtBeg +="End Sub" + vbCrLf+vbCrLf
         
         txtBeg +="Sub pop(ByRef arg As Integer)"+vbCrlf
         txtBeg +="   arg = стек(sp)"+vbCrLf
         txtBeg +="   If (sp-1)>=0 Then"+vbCrLf
         txtBeg +="      sp -=1"+vbCrLf
         txtBeg +="   Else"+vbCrLf
         txtBeg +="      Console.WriteLine(""ВНИМАНИЕ! Стек пустой!!!"")"+vbCrLf
         txtBeg +="   End If"+vbCrLf
         txtBeg +="End Sub" + vbCrLf+vbCrLf
         
         txtOut +="Sub Main()"+vbCrLf
         End Sub
      
      Sub Подвал()
         For i As Integer=0 To цПерем - 1
            txtBeg += "Dim " + перем(i) + " As Integer = 0"+ vbCrLf
         Next
         txtOut += "Console.WriteLine(""Result: "" + Str(рег0))" + vbCrLf
         txtOut += "End Sub" + vbCrLf
         txtout += "End Module" + vbCrLf
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
   Public Module модКомпиль2
        ' ==================== ПРАВИЛА ============================
        Dim tagc As Integer = 0 ' текущий тег на анализе
        Dim sRes As String = "" ' результат анализа
        Dim lex(0) As clsLexem
        Dim lex_end As Integer = 0 ' конец полезных лексем
        Dim txtLine() As String ' список строк исходника
        Sub Структуры_Копировать()
            Dim i As Integer = 0
            Dim lex_ As clsLexem
            Do While Not IsNothing(модЛексер.теги(i)) And i < модЛексер.теги.Length - 1
                lex_ = New clsLexem With {
                    .цСтр = модЛексер.теги(i).цСтр,
                    .цПоз = модЛексер.теги(i).цПоз,
                    .стрТег = модЛексер.теги(i).стрТег
                }
                ReDim Preserve lex(i)
                lex(i) = lex_
                i += 1
            Loop
            ReDim модЛексер.теги(0)

            Dim sLine As String
            Do While Not IsNothing(модЛексер.txtLine)
                sLine = модЛексер.txtLine(i)
                ReDim txtLine(i + 1)
                txtLine(i) = sLine
                i += 1
            Loop
            ReDim модЛексер.txtLine(0)

        End Sub

        Function Смещ(ind As Integer) As String
         Dim s As String ="^"
         For i As Integer = 1 To ind
            s = " " + s
         Next
         Return s
         End Function
      Sub Пр_МОДУЛЬ()
         Console.Write("1")
         If sRes="1.1" Then ' 1.1 МОДУЛЬ должен быть первым
         Console.WriteLine("<"+lex(0).стрТег + ">")
         Console.WriteLine(lex(0).стрТег="MODULE")
            If lex(0).стрТег="MODULE" Then
               lex(0).type_="MODULE"
               tagc =1
               sRes = "1.2"
            Else
               'Console.Write("3")
               'модКокон.Ошибка("Ошибка: стр " + Str(теги(0).цСтр) + " поз " + Str(теги(0).цПоз))
               'Console.Write("4")
               'Console.WriteLine(txtLine(теги(1).цСтр-1))
               'Console.Write("5")
               'Console.WriteLine(Смещ(теги(0).цПоз))
               'Console.Write("6")
               'модКокон.Ошибка("Модуль должен начинаться с ""MODULE""")
               sRes="err"
            End If
            End If
         If sRes="1.2" Then ' 1.2 У модуля должно быть имя
            If теги(tagc).стрТег=";" Then ' пропущено имя модуля
               модКокон.Ошибка("Ошибка: стр " + Str(lex(tagc).цСтр) + " поз " + Str(lex(tagc).цПоз))
               Console.WriteLine(модЛексер.txtLine(lex(tagc).цСтр-1))
               Console.WriteLine(Смещ(lex(tagc).цПоз))
               модКокон.Ошибка("Модуль должен иметь имя")
               sRes="err"
            Else
               lex(tagc).type_="module_name"
               lex(tagc+1).type_=";"
               tagc += 2 'Пропускаем ";"
               sRes = "1.3"
            End If
            End If
         If sRes="1.3" Then ' 1.3 У Модуля должно быть окончание
            Dim bEnd As Boolean=False
            Dim i As Integer
            Do While i < lex.Count
               If lex(i).стрТег="END" Then ' относится ли это к концу модуля?
                  If lex(i+2).стрТег="." Then ' конец ли это? i+2 -- через имя
                     lex(i).type_="module_end"
                     lex(i+1).type_="module_name"
                     lex(i+2).type_="module_dot"
                     bEnd=True
                     tagc=i
                     Exit Do
                  End If
               End If
               i += 1
            Loop
            If bEnd = False Then'а конца то нет!! работаем с последним тегом
               модКокон.Ошибка("Ошибка: стр " + Str(lex(lex.Count-1).цСтр) + " поз " + Str(lex(lex.Count-1).цПоз))
               Console.WriteLine(txtLine(lex(lex.Count-1).цСтр-1))
               Console.WriteLine(Смещ(lex(lex.Count-1).цПоз))
               модКокон.Ошибка("Модуль должен иметь ""END <NameModule.>""")
               sRes="err"
            Else
               ' отбрасываем лишние тэги
               ' ограничивать будем счётчиком
               lex_end = i
               tagc = 4' 1-модуль; 2-имя модуля; 3-";"
               sRes = "1.4"
            End If
            End If
         If sRes="1.4" Then ' 1.4 Модуль должен быть один
            ' Организуем цикл в поиске МОДУЛЬ с учётом, что это может быть строка
            ' Интересует только первая тотальная встреча
            Dim i As Integer = 0 
            Dim bKw As Boolean = True
            Do While i < lex_end ' последние тэги мы уже выяснили
               If lex(i).стрТег="MODULE" Then 'надо выясить, может это часть выражения, или строка
                  If (lex(i-1).стрТег=".") Then
                     bKw=False
                  Else If lex(i-1).стрТег="""" And lex(i+1).стрТег="""" Then
                     bKw=False
                  Else If lex(i-1).стрТег="'" And lex(i+1).стрТег="'" Then
                     bKw = False ' это не ключевое слово
                  Else ' да. Это не строка ,и не часть сущности!!
                     Exit Do
                  End IF
               End If
               i += 1
            Loop
            If i = lex_end Then
               bKw = False
            End If
            If bKw = True Then
               модКокон.Ошибка("Ошибка: стр " + Str(lex(i).цСтр) + " поз " + Str(lex(i).цПоз))
               Console.WriteLine(txtLine(lex(i).цСтр-1))
               Console.WriteLine(Смещ(lex(i).цПоз))
               модКокон.Ошибка("MODULE должен быть один")
               sRes = "Err"
            ELSE
               sRes = "2.1"
            End If
            End If
         End Sub
      Function Импорт_Конец() As Boolean
         ' Проверяет правильн оли закончился импорт
         Dim bRes As Boolean
         bRes = lex(tagc).стрТег="TYPE" Or lex(tagc).стрТег="CONST" Or lex(tagc).стрТег="VAR"
         bRes = bRes Or lex(tagc).стрТег="PROCEDURE" Or lex(tagc).стрТег="BEGIN"
         bRes = bRes Or lex(tagc).type_="module_end"
         Return bRes
         End Function
      Sub Пр_ИМПОРТ()
         ' прочесали модуль, теперь проверить нет ли импорта
         If sRes="2.1" Then ' 2.1 IMPORT может идти тегом № 3 -- проверяем
            If lex(3).стрТег="IMPORT" Then
               lex(3).type_="import"
               tagc = 3
               sRes = "2.2"
            End If
            End If
         If sRes="2.2" Then ' 2.2 Проверяем весь доступный импорт
            ' Может быть прямой импорт, а может быть и с алиасами.
            ' Если импорт прямой, то tagc+2 будет ";", а сли алиас -- то ":="
            tagc = 4 ' После ИМПОРТ имя файла или алиас по счёту -- 4 тег в файле
            Do While True
               Console.WriteLine("+0 "+lex(tagc).стрТег)
               Console.WriteLine("+1 "+lex(tagc+1).стрТег)
               Console.WriteLine("+2 "+lex(tagc+2).стрТег)
               ' Импортов может быть 
               ' прямой, c алиасом, с запятой (продолжение), с ";" -- конец импорта
               If lex(tagc+1).стрТег="," Or lex(tagc+1).стрТег=";" Then' Первая ветка -- прямой импорт
                  lex(tagc).type_="module"
                  lex(tagc).name_origin = lex(tagc).стрТег
                  tagc += 2 ' утановить счётчик на следующий тег (возможно импорта)
                  If lex(tagc+1).стрТег=";" Then ' импорт закончить
                     Exit Do
                  End If
               Else If lex(tagc+1).стрТег=":=" Then ' вторая ветка -- импорт с алиасом
                  lex(tagc).type_="module_alias"
                  lex(tagc).name_origin = lex(tagc+2).стрТег
                  lex(tagc+2).type_ = "module"
                  lex(tagc+2).name_origin = lex(tagc+2).стрТег
                  tagc += 3 ' утановить счётчик на следующий тег (возможно импорта)
                  If lex(tagc+3).стрТег=";" Then ' импорт закончить
                     Exit Do
                  End If
               End If
            Loop
            If  Импорт_Конец() Then ' Если не "," и не ";" и не ":=" -- возможно нарушение инструкции импорта
               sRes = "2.3"
            Else ' Ошибка импорта
               модКокон.Ошибка("Ошибка: стр " + Str(lex(tagc+2).цСтр) + " поз " + Str(lex(tagc+2).цПоз))
               Console.WriteLine(txtLine(lex(tagc).цСтр-1))
               Console.WriteLine(Смещ(lex(tagc+2).цПоз))
               модКокон.Ошибка("Дожно быть имя модуля для импорта в виде <mName := modFullName;>")
               sRes="err"
            End If
         End If
         End Sub
      Sub Правила()
         sRes="1.1"
         Пр_МОДУЛЬ()
         Пр_ИМПОРТ()
         End Sub
      Public Sub Компилировать()
            ' нарезать колбасу из исхдника с присовением координат
            модЛексер.Тег_Разметить()
            Структуры_Копировать()
            Console.WriteLine("Len(lex) " + Str(lex.Length))
            For i As Integer = 0 To 10
                Console.WriteLine(Str(i) + ": " + lex(i).стрТег)
            Next
            ' проверить правильность полученного исходного текста
            Правила()
         End Sub
      End Module
End Namespace