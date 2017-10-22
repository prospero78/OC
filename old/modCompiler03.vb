' Главный модуль компилятора
Imports System.IO
Imports System.Diagnostics

Namespace пиОк
   Public Module модКомпиль
      Dim литАнализ As String = "" ' Look литера для анализа
      Dim txtOut As String = "" ' Выходной текст Visual Basic
      
      Sub Лит_Получ() 'GetChar получение символа из входного потока
         литАнализ = Chr(Console.Read())
      End Sub
      
      Sub Ошибка(ByRef msg As String) ' Error вывод сообщения об ошибке
         Console.WriteLine()
         модКокон.Ошибка("   Ошибка: " + msg)
      End Sub
      
      Sub Прервать(ByRef msg As String) '  Abort Прерывание компиляции
         Ошибка(msg)
         Exit Sub 
      End Sub
      
      Sub Ожидалось(ByRef msg As String) ' Expected
         Прервать("Ожидалось " + msg)
      End Sub
      
      Sub Совпадение(lit As String) '  Math
         If литАнализ = lit Then
            Лит_Получ()
         Else
            Ожидалось("'" + lit + "'")
         End If
      End Sub
      
      Function ЕслиБуква(lit As String) As Boolean '  IsAlpha
         Dim res As Boolean = False
         lit = UCase(lit)
         If InStr("ABCDEFGHIJKLMNOPQRSTUVWXYZ", lit) <> 0 Then
            res = True
         End If
         If InStr("АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ", lit)<>0 Then
            res = True
         End If
         Return res
      End Function
      
      Function ЕслиЦифра(lit As String) As Boolean '  IsDigit
         Dim res As Boolean = False
         If InStr("0123456789", lit)<>0 Then
            res = True
         End If
         Return res
      End Function
      
      Function Имя_Получ() As String ' GetName
         If Not ЕслиБуква(литАнализ) Then
            Ожидалось("Имя")
         End If
         Dim lit As String = UCase(литАнализ)
         Лит_Получ()
         Return lit
      End Function
      
      Function Цифра_Получ() As String ' GetNum
         If Not ЕслиЦифра(литАнализ) Then
            Ожидалось("Целое")
         End If
         Dim lit As String = литАнализ
         Лит_Получ()
         Return lit
      End Function
      
      Sub Вывод(ByVal txt As String) ' Emit
         Console.Write(vbTab + txt)
      End Sub
      
      Sub ВыводНов(ByVal txt As String) ' EmitLn
         Вывод(txt)
         Console.Write(vbCrLf)
      End Sub
      
      Sub Множитель() ' Factor
         If литАнализ = "(" Then ' анализ скобок
            Совпадение("(")
            Выражение()
            Совпадение(")")
         Else If ЕслиБуква(литАнализ) Then ' анализ имён
            Dim lit As String = Имя_Получ()
            ВыводНов("рег0 = " + lit)
            txtOut += "рег0 = Asc(" + lit+")"+vbCrLf
         Else                               ' анализ констант
            Dim lit As String = Цифра_Получ()
            txtOut += "рег0 = " + lit+vbCrLf
         End If
      End Sub
      
      Sub Умножить() ' Multiply
         Совпадение("*")
         Множитель()
         ВыводНов("pop(head)"+vbCrLf + _
                  vbTab + "рег0 *= head")
         txtOut += "pop(head)"+vbCrLf + _
                  "рег0 *= head"+vbCrLf
      End Sub
      
      Sub Разделить() ' Divide
         Совпадение("/")
         Множитель()
         ВыводНов("pop(head)"+vbCrLf + _
                  vbTab + "рег0 = head / рег0")
         txtOut += "pop(head)"+vbCrLf + _
                  "рег0 = head / рег0"+vbCrLf
      End Sub
      
      Sub Терминал() ' Term
         Множитель()
         Do While InStr("*/", литАнализ)>0
            ВыводНов("push(рег0)")
            txtOut += "push(рег0)" + vbCrLf
            Select Case литАнализ
               Case "*"
                  Умножить()
               Case "/"
                  Разделить()
               Case Else
                  Ожидалось("операция * или /")
               End Select
         Loop
      End Sub
      
      Sub Сложить() ' Add
         Совпадение("+")
         Терминал()
         ВыводНов("pop(head)" + vbCrLf + _
                  vbTab + "рег0 += head")
         txtOut += "pop(head)" + vbCrLf + _
                  "рег0 += head" + vbCrLf
      End Sub
      
      Sub Вычесть()' Substract
         Совпадение("-")
         Терминал()
         ВыводНов("pop(head)" + vbCrLf + _
                  vbTab +"рег0 = head - рег0")
         txtOut += "pop(head)" + vbCrLf + _
                  "рег0 = head - рег0" + vbCrLf
      End Sub
      
      Function ЕслиПлюсМинус(lit As String) As Boolean ' IsAddop
         Dim res As Boolean = False
         If InStr("+-", lit)>0 Then
            res = True
         End If
         Return res
      End Function
      
      Sub Выражение() ' Expression
         If ЕслиПлюсМинус(литАнализ) Then
            ВыводНов("рег0 *= 0")
            txtOut += "рег0 *= 0" + vbCrLf
         Else
            Терминал()
         End If
         Do While ЕслиПлюсМинус(литАнализ)
            ВыводНов("push(рег0)")
            ' для выходного файла
            txtOut += "push(рег0)" + vbCrLf
            Select Case литАнализ
               Case "+" 
                  Сложить()
               Case "-" 
                  Вычесть()
               Case Else
                  Ожидалось("операция +/-")
            End Select
         Loop
      End Sub
      
      Sub Настр() ' Init
         Лит_Получ()
      End Sub
      
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
         Настр()
         txtOut = "' Автогенерация текста Visual Basic" + vbCrLf
         txtOut += "' Данные для компиляции" + vbCrLf
         txtOut +="Namespace Oberon07"+vbCrLf
         txtOut +="Public Module modOut"+vbCrLf
         txtOut +="Dim рег0 As Integer = 0"+vbCrLf
         txtOut +="Dim рег1 As Integer = 0"+vbCrLf
         txtOut +="Dim head As Integer = 0"+vbCrLf
         txtOut +="Dim стек(1000) As Integer' программный стек"+vbCrlf
         txtOut +="Dim sp As Integer = 0'указатель стека"+vbCrLf
         txtOut +="Sub push(arg As Integer)"+vbCrlf
         txtOut +="   If (sp+1)<1000 Then"+vbCrLf
         txtOut +="      sp +=1"+vbCrLf
         txtOut +="   Else"+vbCrLf
         txtOut +="      Console.WriteLine(""ВНИМАНИЕ! Стек переполнен!!!"")"+vbCrLf
         txtOut +="   End If"+vbCrLf
         txtOut +="   стек(sp) = arg"+vbCrLf
         txtOut +="End Sub" + vbCrLf
         
         txtOut +="Sub pop(ByRef arg As Integer)"+vbCrlf
         txtOut +="   arg = стек(sp)"+vbCrLf
         txtOut +="   If (sp-1)>=0 Then"+vbCrLf
         txtOut +="      sp -=1"+vbCrLf
         txtOut +="   Else"+vbCrLf
         txtOut +="      Console.WriteLine(""ВНИМАНИЕ! Стек пустой!!!"")"+vbCrLf
         txtOut +="   End If"+vbCrLf
         txtOut +="End Sub" + vbCrLf
         
         txtOut +="Sub Main()"+vbCrLf
      End Sub
      
      Sub Подвал()
         txtOut += "Console.WriteLine(""Result: "" + Str(рег0))" + vbCrLf
         txtOut += "End Sub" + vbCrLf
         txtout += "End Module" + vbCrLf
         txtOut += "End Namespace"
         Вых_Записать()
         Транслировать()
      End Sub
      
      Public Sub Компилировать()
         Заголовок()
         Выражение()
         Подвал()
      End Sub
   End Module
End Namespace