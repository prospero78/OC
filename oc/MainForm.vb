'
' Сделано в SharpDevelop.
' Пользователь: User
' Дата: 20.10.2017
' Время: 14:28
' ${res:XML.StandardHeader.License} ${LICENSE}
' 
' Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
'
Public Partial Class окнГлавное
    Public Sub New()
        ' The Me.InitializeComponent call is required for Windows Forms designer support.
        Me.InitializeComponent()
        
        '
        ' TODO : Add constructor code after InitializeComponents
        '
    End Sub
    
    Sub Timer1Tick(sender As Object, e As EventArgs)
        лблДата.Text=Format(Now(), "yyyy-MM-dd hh:mm:ss")
    End Sub
End Class
