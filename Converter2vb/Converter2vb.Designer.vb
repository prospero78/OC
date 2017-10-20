'
' Сделано в SharpDevelop.
' Пользователь: User
' Дата: 20.10.2017
' Время: 15:08
' ${res:XML.StandardHeader.License} ${LICENSE}
' 
' Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
'
Partial Class Converter2vb
    ''' <summary>
    ''' Designer variable used to keep track of non-visual components.
    ''' </summary>
    Private components As System.ComponentModel.IContainer
    
    ''' <summary>
    ''' Disposes resources used by the control.
    ''' </summary>
    ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If components IsNot Nothing Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub
    
    ''' <summary>
    ''' This method is required for Windows Forms designer support.
    ''' Do not change the method contents inside the source code editor. The Forms designer might
    ''' not be able to load this method if it was changed manually.
    ''' </summary>
    Private Sub InitializeComponent()
        '
        'UserControl1
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Name = "UserControl1"
    End Sub
End Class
