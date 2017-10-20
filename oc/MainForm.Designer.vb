'
' Сделано в SharpDevelop.
' Пользователь: User
' Дата: 20.10.2017
' Время: 14:28
' ${res:XML.StandardHeader.License} ${LICENSE}
' 
' Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
'
Partial Class окнГлавное
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
        Me.statusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.toolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.лблДата = New System.Windows.Forms.ToolStripStatusLabel()
        Me.timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.statusStrip1.SuspendLayout
        Me.SuspendLayout
        '
        'statusStrip1
        '
        Me.statusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.toolStripStatusLabel1, Me.лблДата})
        Me.statusStrip1.Location = New System.Drawing.Point(0, 395)
        Me.statusStrip1.Name = "statusStrip1"
        Me.statusStrip1.Size = New System.Drawing.Size(527, 24)
        Me.statusStrip1.TabIndex = 0
        Me.statusStrip1.Text = "statusStrip1"
        '
        'toolStripStatusLabel1
        '
        Me.toolStripStatusLabel1.Name = "toolStripStatusLabel1"
        Me.toolStripStatusLabel1.Size = New System.Drawing.Size(32, 19)
        Me.toolStripStatusLabel1.Text = "Дата"
        '
        'лблДата
        '
        Me.лблДата.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)  _
                        Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)  _
                        Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom),System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.лблДата.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner
        Me.лблДата.Name = "лблДата"
        Me.лблДата.Size = New System.Drawing.Size(114, 19)
        Me.лблДата.Text = "0000-00-00 00:00:00"
        '
        'timer1
        '
        Me.timer1.Enabled = true
        Me.timer1.Interval = 1000
        AddHandler Me.timer1.Tick, AddressOf Me.Timer1Tick
        '
        'окнГлавное
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(527, 419)
        Me.Controls.Add(Me.statusStrip1)
        Me.Name = "окнГлавное"
        Me.Text = "Компилятор Oberon-07"
        Me.statusStrip1.ResumeLayout(false)
        Me.statusStrip1.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout
    End Sub
    Private timer1 As System.Windows.Forms.Timer
    Private лблДата As System.Windows.Forms.ToolStripStatusLabel
    Private toolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Private statusStrip1 As System.Windows.Forms.StatusStrip
End Class
