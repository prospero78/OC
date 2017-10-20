'
' Сделано в SharpDevelop.
' Пользователь: User
' Дата: 20.10.2017
' Время: 17:50
' 
' Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
'
Partial Class окнКонв
	Inherits System.Windows.Forms.Form
	
	''' <summary>
	''' Designer variable used to keep track of non-visual components.
	''' </summary>
	Private components As System.ComponentModel.IContainer
	
	''' <summary>
	''' Disposes resources used by the form.
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
		Me.components = New System.ComponentModel.Container()
		Me.txtOut = New System.Windows.Forms.TextBox()
		Me.timer1 = New System.Windows.Forms.Timer(Me.components)
		Me.SuspendLayout
		'
		'txtOut
		'
		Me.txtOut.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
						Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.txtOut.Font = New System.Drawing.Font("Consolas", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204,Byte))
		Me.txtOut.Location = New System.Drawing.Point(12, 12)
		Me.txtOut.Multiline = true
		Me.txtOut.Name = "txtOut"
		Me.txtOut.Size = New System.Drawing.Size(615, 332)
		Me.txtOut.TabIndex = 2
		'
		'timer1
		'
		Me.timer1.Enabled = true
		AddHandler Me.timer1.Tick, AddressOf Me.Timer1Tick
		'
		'окнКонв
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(639, 356)
		Me.Controls.Add(Me.txtOut)
		Me.Name = "окнКонв"
		Me.Text = "окнКонв"
		Me.ResumeLayout(false)
		Me.PerformLayout
	End Sub
	Private timer1 As System.Windows.Forms.Timer
	Private txtOut As System.Windows.Forms.TextBox
End Class
