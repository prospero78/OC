'
' Сделано в SharpDevelop.
' Пользователь: prospero78su
' Дата: 20.10.2017
' Время: 14:28
' LICENSE: BSD-2
' 
' Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
'
Imports mConv=Converter2vb

Public Class окнГлавное
    Dim conv As mConv.clsConverter2vb 
    Public Sub New()
        ' The Me.InitializeComponent call is required for Windows Forms designer support.
        Me.InitializeComponent()
        
        '
        ' TODO : Add constructor code after InitializeComponents
        '
        conv = New mConv.clsConverter2vb()
    End Sub
    
    Sub Timer1Tick(sender As Object, e As EventArgs)
        лблДата.Text=Format(Now(), "yyyy-MM-dd hh:mm:ss")
    End Sub
    
    Sub ВыходToolStripMenuItemClick(sender As Object, e As EventArgs)
        Application.Exit()
    End Sub
    
    Sub ToolStripButton1Click(sender As Object, e As EventArgs)
        conv.txtOb=Me.txtOb.Text
    End Sub
End Class
