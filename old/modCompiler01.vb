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
         Прервать(msg + " ожидалось")
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
         Console.Write(vbTab, txt)
      End Sub
      
      Sub ВыводНов(ByVal txt As String) ' EmitLn
         Вывод(txt)
         Console.Write(vbCrLf)
      End Sub
      
      Sub Заголовок()
         txtOut = "' Автогенерация текста Visual Basic" + vbCrLf
         txtOut += "' Данные для компиляции" + vbCrLf
         txtOut +="Namespace Oberon07"+vbCrLf
         txtOut +="Public Module modOut"+vbCrLf
         txtOut +="Dim рег0 As Integer = 0"+vbCrLf
         txtOut +="Sub Main()"+vbCrLf
      End Sub
      
      Sub Подвал()
         txtOut += "Console.WriteLine(""Result: "" + Str(рег0))" + vbCrLf
         txtOut += "End Sub" + vbCrLf
         txtout += "End Module" + vbCrLf
         txtOut += "End Namespace"
      End Sub
      
      Sub Настр() ' Init
         Лит_Получ()
      End Sub
      
      Sub Выражение() ' Expression
         Dim lit As String = Цифра_Получ()
         ВыводНов("рег0 = " + lit)
         txtOut += "рег0 = " + lit + vbCrLf
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
      
      Sub Запустить()
      End Sub
      
      Public Sub Компилировать()
         Заголовок()
         Настр()
         Выражение()
         Подвал()
         Вых_Записать()
         Транслировать()
         Запустить()
      End Sub
   End Module
End Namespace