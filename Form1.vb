Imports System.Drawing.Imaging
Imports System.Drawing.Printing
Imports System.Net.Security
Imports Microsoft.VisualBasic.FileIO

Public Class Form1
    Private ReadOnly csvData As New Dictionary(Of String, Tuple(Of Integer, String))
    Private trigger As Integer = 0

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 指定CSV文件的路径
        Dim filePath As String = "data.csv"

        ' 创建TextFieldParser对象，并设置适当的选项
        Using parser As New TextFieldParser(filePath)
            parser.TextFieldType = FieldType.Delimited
            parser.SetDelimiters(",")

            ' 逐行读取CSV文件
            While Not parser.EndOfData
                ' 读取当前行的字段
                Dim fields As String() = parser.ReadFields()

                ' 将字段的第一个项作为键，将第二个项拆分为数字部分和单位部分存储到元组中
                If fields.Length >= 2 Then
                    Dim key As String = fields(0)
                    Dim value2 As String = fields(1)

                    ' 拆分数字部分和单位部分
                    Dim numericValue As Integer = 0
                    Dim unitValue As String = ""
                    If SplitNumericValue(value2, numericValue, unitValue) Then
                        Dim data As Tuple(Of Integer, String) = Tuple.Create(numericValue, unitValue)
                        csvData.Add(key, data)
                        ComboBox1.Items.Add(key)
                    End If
                End If
            End While
        End Using

        ComboBox2.SelectedIndex = 0

        ' 设置ComboBox1的默认选中项
        If ComboBox1.Items.Count > 0 Then
            ComboBox1.SelectedIndex = 0
        End If

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If trigger = 0 Then
            trigger = 1

            ' 获取选中项的值
            Dim selectedValue As String = ComboBox1.SelectedItem.ToString()

            ' 根据选中项的值查找对应行的其他字段数据
            Dim data As Tuple(Of Integer, String) = Nothing
            If csvData.TryGetValue(selectedValue, data) Then
                ' 在这里可以使用其他字段数据做进一步的处理或显示
                ' 例如：将第二个字段的数字部分显示在文本框中，将单位部分显示在ComboBox2中
                TextBox1.Text = data.Item1.ToString()
                ComboBox2.SelectedItem = data.Item2
            End If
            trigger = 0
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim csvPath As String = "data.csv"

        Try
            Shell("explorer.exe " & csvPath, AppWinStyle.NormalFocus)
        Catch ex As Exception
            MsgBox("无法打开文件：" & ex.Message)
        End Try
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If trigger = 0 Then
            trigger = 2

            UpdateTime()
            trigger = 0

        ElseIf trigger = 1 Or trigger = 7 Then
            UpdateTime()
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        If trigger = 0 Then
            trigger = 3

            UpdateTime()
            trigger = 0

        ElseIf trigger = 1 Or trigger = 7 Then
            UpdateTime()
        End If
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        If trigger = 0 Then
            trigger = 4

            UpdateTime()
            trigger = 0

        ElseIf trigger = 5 Then
            UpdateTime()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If trigger = 0 Then
            trigger = 5

            DateTimePicker1.Value = DateTime.Now
            trigger = 0
        End If
    End Sub

    Private Sub DateTimePicker2_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker2.ValueChanged
        If trigger = 0 Then
            trigger = 6

            ' 获取 DateTimePicker2 和 DateTimePicker1 的值
            Dim dateTime2 As DateTime = DateTimePicker2.Value
            Dim dateTime1 As DateTime = DateTimePicker1.Value

            ' 获取时间差（以分钟为单位）
            Dim timeSpan As TimeSpan = TimeSpan.FromMinutes(Math.Round((dateTime2 - dateTime1).TotalMinutes))

            ' 根据时间差选择适当的单位
            Dim unit As String
            Dim value As Double
            If timeSpan.TotalDays >= 30 AndAlso timeSpan.TotalDays Mod 30 = 0 Then
                unit = "月"
                value = timeSpan.TotalDays / 30
            ElseIf timeSpan.TotalDays >= 7 AndAlso timeSpan.TotalDays Mod 7 = 0 Then
                unit = "周"
                value = timeSpan.TotalDays / 7
            ElseIf timeSpan.TotalHours >= 24 AndAlso timeSpan.TotalHours Mod 24 = 0 Then
                unit = "天"
                value = timeSpan.TotalDays
            ElseIf timeSpan.TotalMinutes >= 60 AndAlso timeSpan.TotalMinutes Mod 60 = 0 Then
                unit = "小时"
                value = timeSpan.TotalHours
            Else
                unit = "分钟"
                value = timeSpan.TotalMinutes
            End If

            ' 更新 TextBox1 和 ComboBox2
            TextBox1.Text = value.ToString() ' 获取时间差的值，并更新到 TextBox1
            ComboBox2.SelectedItem = unit ' 更新 ComboBox2 的单位部分
            trigger = 0
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If trigger = 0 Then
            trigger = 7

            ' 获取选中项的值
            Dim selectedValue As String = ComboBox1.SelectedItem.ToString()

            ' 根据选中项的值查找对应行的其他字段数据
            Dim data As Tuple(Of Integer, String) = Nothing
            If csvData.TryGetValue(selectedValue, data) Then
                ' 在这里可以使用其他字段数据做进一步的处理或显示
                ' 例如：将第二个字段的数字部分显示在文本框中，将单位部分显示在ComboBox2中
                TextBox1.Text = data.Item1.ToString()
                ComboBox2.SelectedItem = data.Item2
            End If
            trigger = 0
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ' 创建一个位图对象，尺寸可以根据需要进行调整
        Dim bitmap As New Bitmap(384, 256)
        Using graphics As Graphics = Graphics.FromImage(bitmap)
            ' 设置背景颜色为白色
            graphics.Clear(Color.White)
            ' 设置字体和字号
            Dim font As New Font("微软雅黑", 11)
            ' 设置文字颜色为黑色
            Dim brush As New SolidBrush(Color.Black)
            ' 设置抗锯齿模式
            graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            ' 在位图上绘制文字
            graphics.DrawString(ComboBox1.Text, font, brush, New PointF(14, 30))
            graphics.DrawString("启用：" & DateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm"), font, brush, New PointF(14, 80))
            graphics.DrawString("失效：" & DateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm"), font, brush, New PointF(14, 130))
            graphics.DrawString("签名：" & TextBox2.Text, font, brush, New PointF(14, 180))
            ' 设置文字对齐方式
            Dim format As New StringFormat With {
                .Alignment = StringAlignment.Far
            }
            ' 绘制矩形框
            Dim rect As New RectangleF(14, 30, 342, 100)
            graphics.DrawString("有效期：" & TextBox1.Text & ComboBox2.Text, font, brush, rect, format)
        End Using

        ' 将位图保存为文件
        Dim outputPath As String = "ExpTag" & DateTime.Now.ToString("yyyyMMddHHmmss") & ".png"
        bitmap.Save(outputPath, ImageFormat.Png)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ' 创建SaveFileDialog对象并设置相关属性
        Dim saveFileDialog As New SaveFileDialog With {
            .Filter = "PNG图像文件 (*.png)|*.png",
            .FilterIndex = 1,
            .RestoreDirectory = True
        }

        ' 设置默认的文件名为当前日期和时间
        Dim fileName As String = "ExpTag" & DateTime.Now.ToString("yyyyMMddHHmmss") ' 格式化为"yyyyMMdd_HHmmss"

        ' 设置SaveFileDialog的默认文件名
        saveFileDialog.FileName = fileName

        ' 如果用户点击了保存按钮
        If saveFileDialog.ShowDialog() = DialogResult.OK Then
            ' 获取用户选择的文件路径
            Dim filePath As String = saveFileDialog.FileName

            ' 进行文件保存操作
            ' 创建一个位图对象，尺寸可以根据需要进行调整
            Dim bitmap As New Bitmap(384, 256)
            Using graphics As Graphics = Graphics.FromImage(bitmap)
                ' 设置背景颜色为白色
                graphics.Clear(Color.White)
                ' 设置字体和字号
                Dim font As New Font("微软雅黑", 11)
                ' 设置文字颜色为黑色
                Dim brush As New SolidBrush(Color.Black)
                ' 设置抗锯齿模式
                graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
                ' 在位图上绘制文字
                graphics.DrawString(ComboBox1.Text, font, brush, New PointF(14, 30))
                graphics.DrawString("启用：" & DateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm"), font, brush, New PointF(14, 80))
                graphics.DrawString("失效：" & DateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm"), font, brush, New PointF(14, 130))
                graphics.DrawString("签名：" & TextBox2.Text, font, brush, New PointF(14, 180))
                ' 设置文字对齐方式
                Dim format As New StringFormat With {
                .Alignment = StringAlignment.Far
            }
                ' 绘制矩形框
                Dim rect As New RectangleF(14, 30, 342, 100)
                graphics.DrawString("有效期：" & TextBox1.Text & ComboBox2.Text, font, brush, rect, format)
            End Using
            bitmap.Save(filePath, ImageFormat.Png)
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        ' 创建一个位图对象，尺寸可以根据需要进行调整
        Dim bitmap As New Bitmap(384, 256)
        Using graphics As Graphics = Graphics.FromImage(bitmap)
            ' 设置背景颜色为白色
            graphics.Clear(Color.White)
            ' 设置字体和字号
            Dim font As New Font("微软雅黑", 11)
            ' 设置文字颜色为黑色
            Dim brush As New SolidBrush(Color.Black)
            ' 设置抗锯齿模式
            graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            ' 在位图上绘制文字
            graphics.DrawString(ComboBox1.Text, font, brush, New PointF(14, 30))
            graphics.DrawString("启用：" & DateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm"), font, brush, New PointF(14, 80))
            graphics.DrawString("失效：" & DateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm"), font, brush, New PointF(14, 130))
            graphics.DrawString("签名：" & TextBox2.Text, font, brush, New PointF(14, 180))
            ' 设置文字对齐方式
            Dim format As New StringFormat With {
                .Alignment = StringAlignment.Far
            }
            ' 绘制矩形框
            Dim rect As New RectangleF(14, 30, 342, 100)
            graphics.DrawString("有效期：" & TextBox1.Text & ComboBox2.Text, font, brush, rect, format)
        End Using

        ' 创建PrintDocument对象
        Dim printDocument As New PrintDocument()

        ' 设置PrintDocument的打印页面内容
        AddHandler printDocument.PrintPage, Sub(senderObj As Object, args As PrintPageEventArgs)
                                                args.Graphics.DrawImage(bitmap, 0, 0) ' 在打印页面上绘制图像
                                                args.HasMorePages = False ' 没有更多的页面需要打印
                                            End Sub

        ' 创建PrintDialog对象并设置PrintDocument
        Dim printDialog As New PrintDialog With {
            .Document = printDocument
        }

        ' 如果用户点击了打印按钮
        If printDialog.ShowDialog() = DialogResult.OK Then
            ' 调用PrintDocument的Print方法开始打印
            printDocument.Print()
        End If
    End Sub

    Private Sub UpdateTime()
        ' 获取当前的日期和时间
        Dim currentDateTime As DateTime = DateTimePicker1.Value

        ' 获取时间数量和单位
        Dim timeNum As Integer = Val(TextBox1.Text)
        Dim unit As String = ComboBox2.SelectedItem.ToString()

        ' 根据单位选择合适的 Add 方法进行时间计算
        Select Case unit
            Case "分钟"
                currentDateTime = currentDateTime.AddMinutes(timeNum)
            Case "小时"
                currentDateTime = currentDateTime.AddHours(timeNum)
            Case "天"
                currentDateTime = currentDateTime.AddDays(timeNum)
            Case "周"
                currentDateTime = currentDateTime.AddDays(timeNum * 7)
            Case "月"
                currentDateTime = currentDateTime.AddMonths(timeNum)
        End Select

        ' 更新 DateTimePicker2 的值
        DateTimePicker2.Value = currentDateTime
    End Sub

    Private Shared Function SplitNumericValue(value As String, ByRef numericValue As Integer, ByRef unitValue As String) As Boolean
        ' 假设数字部分和单位部分之间没有空格，例如："48小时"
        Dim index As Integer = 0
        While index < value.Length AndAlso Char.IsDigit(value(index))
            index += 1
        End While

        If index > 0 AndAlso index < value.Length Then
            numericValue = Integer.Parse(value.Substring(0, index))
            unitValue = value.Substring(index)
            Return True
        End If

        Return False
    End Function
End Class
