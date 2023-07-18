<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Label1 = New Label()
        ComboBox1 = New ComboBox()
        Label2 = New Label()
        TextBox1 = New TextBox()
        ComboBox2 = New ComboBox()
        Label3 = New Label()
        DateTimePicker1 = New DateTimePicker()
        Button1 = New Button()
        Label4 = New Label()
        DateTimePicker2 = New DateTimePicker()
        Button2 = New Button()
        Label5 = New Label()
        TextBox2 = New TextBox()
        Button3 = New Button()
        Panel1 = New Panel()
        Button4 = New Button()
        Button5 = New Button()
        Button6 = New Button()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(60, 48)
        Label1.Name = "Label1"
        Label1.Size = New Size(62, 31)
        Label1.TabIndex = 0
        Label1.Text = "名称"
        ' 
        ' ComboBox1
        ' 
        ComboBox1.FlatStyle = FlatStyle.System
        ComboBox1.FormattingEnabled = True
        ComboBox1.Location = New Point(200, 42)
        ComboBox1.MaxDropDownItems = 12
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(340, 39)
        ComboBox1.TabIndex = 1
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(60, 120)
        Label2.Name = "Label2"
        Label2.Size = New Size(86, 31)
        Label2.TabIndex = 2
        Label2.Text = "有效期"
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(200, 114)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(340, 38)
        TextBox1.TabIndex = 3
        ' 
        ' ComboBox2
        ' 
        ComboBox2.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox2.FlatStyle = FlatStyle.System
        ComboBox2.ImeMode = ImeMode.Disable
        ComboBox2.Items.AddRange(New Object() {"分钟", "小时", "天", "周", "月"})
        ComboBox2.Location = New Point(560, 114)
        ComboBox2.Name = "ComboBox2"
        ComboBox2.Size = New Size(150, 39)
        ComboBox2.TabIndex = 4
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(60, 192)
        Label3.Name = "Label3"
        Label3.Size = New Size(110, 31)
        Label3.TabIndex = 5
        Label3.Text = "启用时间"
        ' 
        ' DateTimePicker1
        ' 
        DateTimePicker1.CustomFormat = "yyyy年M月d日 HH:mm"
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.Location = New Point(200, 186)
        DateTimePicker1.Name = "DateTimePicker1"
        DateTimePicker1.Size = New Size(340, 38)
        DateTimePicker1.TabIndex = 6
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(560, 182)
        Button1.Name = "Button1"
        Button1.Size = New Size(150, 46)
        Button1.TabIndex = 7
        Button1.Text = "现在"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(60, 264)
        Label4.Name = "Label4"
        Label4.Size = New Size(110, 31)
        Label4.TabIndex = 8
        Label4.Text = "失效时间"
        ' 
        ' DateTimePicker2
        ' 
        DateTimePicker2.CustomFormat = "yyyy年M月d日 HH:mm"
        DateTimePicker2.Format = DateTimePickerFormat.Custom
        DateTimePicker2.Location = New Point(200, 258)
        DateTimePicker2.Name = "DateTimePicker2"
        DateTimePicker2.Size = New Size(340, 38)
        DateTimePicker2.TabIndex = 9
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(560, 254)
        Button2.Name = "Button2"
        Button2.Size = New Size(150, 46)
        Button2.TabIndex = 10
        Button2.Text = "重置"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(60, 360)
        Label5.Name = "Label5"
        Label5.Size = New Size(62, 31)
        Label5.TabIndex = 11
        Label5.Text = "签名"
        ' 
        ' TextBox2
        ' 
        TextBox2.Location = New Point(200, 354)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(200, 38)
        TextBox2.TabIndex = 12
        ' 
        ' Button3
        ' 
        Button3.Location = New Point(410, 350)
        Button3.Name = "Button3"
        Button3.Size = New Size(100, 46)
        Button3.TabIndex = 13
        Button3.Text = "保存"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' Panel1
        ' 
        Panel1.BorderStyle = BorderStyle.FixedSingle
        Panel1.Location = New Point(20, 326)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(740, 1)
        Panel1.TabIndex = 14
        ' 
        ' Button4
        ' 
        Button4.Location = New Point(560, 38)
        Button4.Name = "Button4"
        Button4.Size = New Size(150, 46)
        Button4.TabIndex = 15
        Button4.Text = "修改配置"
        Button4.UseVisualStyleBackColor = True
        ' 
        ' Button5
        ' 
        Button5.Location = New Point(510, 350)
        Button5.Name = "Button5"
        Button5.Size = New Size(30, 46)
        Button5.TabIndex = 16
        Button5.Text = "…"
        Button5.UseVisualStyleBackColor = True
        ' 
        ' Button6
        ' 
        Button6.Location = New Point(560, 350)
        Button6.Name = "Button6"
        Button6.Size = New Size(150, 46)
        Button6.TabIndex = 17
        Button6.Text = "打印"
        Button6.UseVisualStyleBackColor = True
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(14F, 31F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(774, 429)
        Controls.Add(Button6)
        Controls.Add(Button5)
        Controls.Add(Button4)
        Controls.Add(Panel1)
        Controls.Add(Button3)
        Controls.Add(TextBox2)
        Controls.Add(Label5)
        Controls.Add(Button2)
        Controls.Add(DateTimePicker2)
        Controls.Add(Label4)
        Controls.Add(Button1)
        Controls.Add(DateTimePicker1)
        Controls.Add(Label3)
        Controls.Add(ComboBox2)
        Controls.Add(TextBox1)
        Controls.Add(Label2)
        Controls.Add(ComboBox1)
        Controls.Add(Label1)
        Name = "Form1"
        Text = "ExpTag 2.0"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Button1 As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents DateTimePicker2 As DateTimePicker
    Friend WithEvents Button2 As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Button3 As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Button4 As Button
    Private WithEvents ComboBox2 As ComboBox
    Private WithEvents ComboBox1 As ComboBox
    Friend WithEvents Button5 As Button
    Friend WithEvents Button6 As Button
End Class
