'
' Сделано в SharpDevelop.
' Пользователь: User
' Дата: 20.10.2017
' Время: 14:28
' ${res:XML.StandardHeader.License} ${LICENSE}
' 
' Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
'
Imports Microsoft.VisualBasic.ApplicationServices

Namespace My
    ' This file controls the behaviour of the application.
    Class MyApplication
        Public Sub New()
            MyBase.New(AuthenticationMode.Windows)
            Me.IsSingleInstance = False
            Me.EnableVisualStyles = True
            Me.SaveMySettingsOnExit = True
            Me.ShutDownStyle = ShutdownMode.AfterMainFormCloses
        End Sub
        
        Protected Overrides Sub OnCreateMainForm()
            Me.MainForm = oc.окнГлавное
        End Sub
    End Class
End Namespace
