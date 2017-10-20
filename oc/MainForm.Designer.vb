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
        Me.menuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.файлToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.выходToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tabControl1 = New System.Windows.Forms.TabControl()
        Me.tabPage1 = New System.Windows.Forms.TabPage()
        Me.tabPage2 = New System.Windows.Forms.TabPage()
        Me.textBox1 = New System.Windows.Forms.TextBox()
        Me.statusStrip1.SuspendLayout
        Me.menuStrip1.SuspendLayout
        Me.tabControl1.SuspendLayout
        Me.tabPage1.SuspendLayout
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
        'menuStrip1
        '
        Me.menuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.файлToolStripMenuItem})
        Me.menuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.menuStrip1.Name = "menuStrip1"
        Me.menuStrip1.Size = New System.Drawing.Size(527, 24)
        Me.menuStrip1.TabIndex = 1
        '
        'файлToolStripMenuItem
        '
        Me.файлToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.toolStripMenuItem1, Me.выходToolStripMenuItem})
        Me.файлToolStripMenuItem.Name = "файлToolStripMenuItem"
        Me.файлToolStripMenuItem.Size = New System.Drawing.Size(48, 20)
        Me.файлToolStripMenuItem.Text = "&Файл"
        '
        'toolStripMenuItem1
        '
        Me.toolStripMenuItem1.Name = "toolStripMenuItem1"
        Me.toolStripMenuItem1.Size = New System.Drawing.Size(105, 6)
        '
        'выходToolStripMenuItem
        '
        Me.выходToolStripMenuItem.Name = "выходToolStripMenuItem"
        Me.выходToolStripMenuItem.Size = New System.Drawing.Size(108, 22)
        Me.выходToolStripMenuItem.Text = "&Выход"
        AddHandler Me.выходToolStripMenuItem.Click, AddressOf Me.ВыходToolStripMenuItemClick
        '
        'tabControl1
        '
        Me.tabControl1.Controls.Add(Me.tabPage1)
        Me.tabControl1.Controls.Add(Me.tabPage2)
        Me.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabControl1.Location = New System.Drawing.Point(0, 24)
        Me.tabControl1.Name = "tabControl1"
        Me.tabControl1.SelectedIndex = 0
        Me.tabControl1.Size = New System.Drawing.Size(527, 371)
        Me.tabControl1.TabIndex = 2
        '
        'tabPage1
        '
        Me.tabPage1.Controls.Add(Me.textBox1)
        Me.tabPage1.Location = New System.Drawing.Point(4, 22)
        Me.tabPage1.Name = "tabPage1"
        Me.tabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPage1.Size = New System.Drawing.Size(519, 345)
        Me.tabPage1.TabIndex = 0
        Me.tabPage1.Text = "tabPage1"
        Me.tabPage1.UseVisualStyleBackColor = true
        '
        'tabPage2
        '
        Me.tabPage2.Location = New System.Drawing.Point(4, 22)
        Me.tabPage2.Name = "tabPage2"
        Me.tabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPage2.Size = New System.Drawing.Size(216, 142)
        Me.tabPage2.TabIndex = 1
        Me.tabPage2.Text = "tabPage2"
        Me.tabPage2.UseVisualStyleBackColor = true
        '
        'textBox1
        '
        Me.textBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.textBox1.Font = New System.Drawing.Font("Consolas", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204,Byte))
        Me.textBox1.Location = New System.Drawing.Point(3, 3)
        Me.textBox1.Multiline = true
        Me.textBox1.Name = "textBox1"
        Me.textBox1.Size = New System.Drawing.Size(513, 339)
        Me.textBox1.TabIndex = 0
        Me.textBox1.Text = "MODULE Test:"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"END Test."
        '
        'окнГлавное
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(527, 419)
        Me.Controls.Add(Me.tabControl1)
        Me.Controls.Add(Me.statusStrip1)
        Me.Controls.Add(Me.menuStrip1)
        Me.MainMenuStrip = Me.menuStrip1
        Me.Name = "окнГлавное"
        Me.Text = "Компилятор Oberon-07"
        Me.statusStrip1.ResumeLayout(false)
        Me.statusStrip1.PerformLayout
        Me.menuStrip1.ResumeLayout(false)
        Me.menuStrip1.PerformLayout
        Me.tabControl1.ResumeLayout(false)
        Me.tabPage1.ResumeLayout(false)
        Me.tabPage1.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout
    End Sub
    Private tabPage2 As System.Windows.Forms.TabPage
    Private textBox1 As System.Windows.Forms.TextBox
    Private tabPage1 As System.Windows.Forms.TabPage
    Private tabControl1 As System.Windows.Forms.TabControl
    Private выходToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private toolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Private файлToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private menuStrip1 As System.Windows.Forms.MenuStrip
    Private timer1 As System.Windows.Forms.Timer
    Private лблДата As System.Windows.Forms.ToolStripStatusLabel
    Private toolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Private statusStrip1 As System.Windows.Forms.StatusStrip
End Class
