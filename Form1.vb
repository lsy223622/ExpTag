Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing.Printing
Imports System.IO
Imports Microsoft.VisualBasic.FileIO

Public Class Form1
    Private ReadOnly csvData As New Dictionary(Of String, Tuple(Of Long, String))
    Private trigger As Integer = 0
    Private ReadOnly bitmap As New Bitmap(384, 256)

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        trigger = 0

        ' 设置图像的 DPI
        bitmap.SetResolution(96, 96)

        ' 指定CSV文件的路径
        Dim filePath As String = "data.csv"

        ' 检查文件是否存在
        If Not File.Exists(filePath) Then
            ' 文件不存在，弹出提示窗口并退出程序
            Dim message As String = "找不到文件 " & filePath & "。请确保文件存在软件目录下。"
            MessageBox.Show(message, "文件不存在", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Environment.Exit(1) ' 退出程序，并返回错误代码 1
        End If

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
                    Dim numericValue As Long = 0
                    Dim unitValue As String = ""
                    If SplitNumericValue(value2, numericValue, unitValue) Then
                        Dim data As Tuple(Of Long, String) = Tuple.Create(numericValue, unitValue)
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

        DateTimePicker2.MinDate = DateTimePicker1.Value

    End Sub

    Private Sub ComboBox1_TextChanged(sender As Object, e As EventArgs) Handles ComboBox1.TextChanged
        Button2.Enabled = (ComboBox1.SelectedIndex <> -1)
        If trigger = 0 And ComboBox1.SelectedIndex <> -1 Then
            trigger = 1

            ' 获取选中项的值
            Dim selectedValue As String = ComboBox1.SelectedItem.ToString()

            ' 根据选中项的值查找对应行的其他字段数据
            Dim data As Tuple(Of Long, String) = Nothing
            If csvData.TryGetValue(selectedValue, data) Then
                ' 将第二个字段的数字部分显示在文本框中，将单位部分显示在ComboBox2中
                NumericUpDown1.Value = Val(data.Item1)
                ComboBox2.SelectedItem = data.Item2
            End If

            trigger = 0
        End If
        GenerateBitmap()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim csvPath As String = "data.csv"

        Try
            Shell("explorer.exe " & csvPath, AppWinStyle.NormalFocus)
        Catch ex As Exception
            MsgBox("无法打开文件：" & ex.Message)
        End Try
    End Sub

    Private Sub NumericUpDown1_TextChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.TextChanged
        If trigger = 0 Then
            trigger = 2

            UpdateTime()
            GenerateBitmap()
            trigger = 0

        ElseIf trigger = 1 Or trigger = 7 Then
            UpdateTime()
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        If trigger = 0 Then
            trigger = 3

            UpdateTime()
            GenerateBitmap()
            trigger = 0

        ElseIf trigger = 1 Or trigger = 7 Then
            UpdateTime()
        End If
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        If DateTimePicker2.Value < DateTimePicker1.Value Then
            DateTimePicker2.Value = DateTimePicker1.Value
        End If
        DateTimePicker2.MinDate = DateTimePicker1.Value

        If trigger = 0 Then
            trigger = 4

            UpdateTime()
            GenerateBitmap()
            trigger = 0

        ElseIf trigger = 5 Then
            UpdateTime()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If trigger = 0 Then
            trigger = 5

            DateTimePicker1.Value = Date.Now
            GenerateBitmap()
            trigger = 0
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If trigger = 0 And ComboBox1.SelectedIndex <> -1 Then
            trigger = 7
            Dim comboValue As String
            ' 获取选中项的值
            comboValue = ComboBox1.SelectedItem.ToString()
            ' 根据选中项的值查找对应行的其他字段数据
            Dim data As Tuple(Of Long, String) = Nothing
            If csvData.TryGetValue(comboValue, data) Then
                ' 将第二个字段的数字部分显示在 NumericUpdown1 中，将单位部分显示在 ComboBox2 中
                NumericUpDown1.Value = Val(data.Item1)
                ComboBox2.SelectedItem = data.Item2
            End If

            GenerateBitmap()

            trigger = 0
        End If
    End Sub

    Private Sub DateTimePicker2_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker2.ValueChanged
        If DateTimePicker2.Value < DateTimePicker1.Value Then
            DateTimePicker1.Value = DateTimePicker2.Value
        End If

        If trigger = 0 Then
            trigger = 6

            ' 获取 DateTimePicker2 和 DateTimePicker1 的值
            Dim dateTime2 As Date = DateTimePicker2.Value
            Dim dateTime1 As Date = DateTimePicker1.Value

            ' 获取时间差（以分钟为单位）
            Dim timeSpan As TimeSpan = TimeSpan.FromMinutes(Math.Round((dateTime2 - dateTime1).TotalMinutes))

            ' 根据时间差选择适当的单位
            Dim unit As String
            Dim value As Long
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

            ' 更新 NumericUpdown1 和 ComboBox2
            NumericUpDown1.Value = value ' 获取时间差的值，并更新到 NumericUpdown1
            ComboBox2.SelectedItem = unit ' 更新 ComboBox2 的单位部分

            GenerateBitmap()
            trigger = 0
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        If PictureBox1.Enabled Then
            PictureBox1.Image = Nothing
            Panel1.Width = CInt(368 * GetDPIScaleFactor())
            Width = CInt(407 * GetDPIScaleFactor())
            Button7.Text = "预览 >>"
            ToolTip1.SetToolTip(Button7, "展开预览图。")
        Else
            Width = CInt(632 * GetDPIScaleFactor())
            Panel1.Width = CInt(594 * GetDPIScaleFactor())
            PictureBox1.Image = GetRoundedImage(bitmap, 20)
            Button7.Text = "<< 收起"
            ToolTip1.SetToolTip(Button7, "收起预览图。")
        End If
        PictureBox1.Enabled = Not PictureBox1.Enabled
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        GenerateBitmap()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ' 将位图保存为文件
        Dim outputPath As String = "ExpTag" & Date.Now.ToString("yyyyMMddHHmmss") & ".png"
        bitmap.Save(outputPath, ImageFormat.Png)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ' 创建 SaveFileDialog 对象并设置相关属性
        Dim saveFileDialog As New SaveFileDialog With {
            .Filter = "PNG图像文件 (*.png)|*.png",
            .FilterIndex = 1,
            .RestoreDirectory = True
        }

        ' 设置默认的文件名为当前日期和时间
        Dim fileName As String = "ExpTag" & DateTime.Now.ToString("yyyyMMddHHmmss") ' 格式化为"yyyyMMdd_HHmmss"

        ' 设置 SaveFileDialog 的默认文件名
        saveFileDialog.FileName = fileName

        ' 如果用户点击了保存按钮
        If saveFileDialog.ShowDialog() = DialogResult.OK Then
            ' 获取用户选择的文件路径
            Dim filePath As String = saveFileDialog.FileName

            GenerateBitmap()
            bitmap.Save(filePath, ImageFormat.Png)
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        ' 创建 PrintDocument 对象
        Dim printDocument As New PrintDocument()

        ' 设置 PrintDocument 的打印页面内容
        AddHandler printDocument.PrintPage, Sub(senderObj As Object, args As PrintPageEventArgs)
                                                args.Graphics.DrawImage(bitmap, 0, 0) ' 在打印页面上绘制图像
                                                args.HasMorePages = False ' 没有更多的页面需要打印
                                            End Sub

        ' 创建 PrintDialog 对象并设置 PrintDocument
        Dim printDialog As New PrintDialog With {
            .Document = printDocument
        }

        ' 如果用户点击了打印按钮
        If printDialog.ShowDialog() = DialogResult.OK Then
            ' 调用 PrintDocument 的 Print 方法开始打印
            printDocument.Print()
        End If
    End Sub

    Private Sub UpdateTime()
        ' 获取当前的日期和时间
        Dim currentDateTime As Date = DateTimePicker1.Value

        ' 获取时间数量和单位
        Dim timeNum As Long = NumericUpDown1.Value
        Dim unit As String = ComboBox2.SelectedItem.ToString()

        ' 计算当前单位在 DateTimePicker2 上可以增加的最大数值
        Dim maxNum As Long = CalculateMaxNum(currentDateTime, unit)

        ' 判断是否会溢出上限
        If timeNum > maxNum Then
            MessageBox.Show("计算结果超出日期范围！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning)

            Return
        Else
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
        End If
    End Sub

    Private Shared Function CalculateMaxNum(currentDateTime As Date, unit As String) As Long
        ' 根据单位计算当前单位在 DateTimePicker2 上可以增加的最大数值
        Select Case unit
            Case "分钟"
                Return (New Date(9998, 12, 31) - currentDateTime).TotalMinutes
            Case "小时"
                Return (New Date(9998, 12, 31) - currentDateTime).TotalHours
            Case "天"
                Return (New Date(9998, 12, 31) - currentDateTime).TotalDays
            Case "周"
                Return (New Date(9998, 12, 31) - currentDateTime).TotalDays / 7
            Case "月"
                Return (New Date(9998, 12, 31).Year - currentDateTime.Year) * 12 + (New Date(9998, 12, 31).Month - currentDateTime.Month)
            Case Else
                Return 0
        End Select
    End Function

    Private Shared Function SplitNumericValue(value As String, ByRef numericValue As Long, ByRef unitValue As String) As Boolean
        ' 假设数字部分和单位部分之间没有空格，例如："48小时"
        Dim index As Integer = 0
        While index < value.Length AndAlso Char.IsDigit(value(index))
            index += 1
        End While

        If index > 0 AndAlso index < value.Length Then
            numericValue = Long.Parse(value.Substring(0, index))
            unitValue = value.Substring(index)
            Return True
        End If

        Return False
    End Function

    Private Sub GenerateBitmap()
        Using graphics As Graphics = Graphics.FromImage(bitmap)
            ' 设置背景颜色为白色
            graphics.Clear(Color.White)
            ' 设置字体和字号
            Dim font As New Font("微软雅黑", 22)
            ' 设置文字颜色为黑色
            Dim brush As New SolidBrush(Color.Black)
            ' 设置抗锯齿模式
            graphics.SmoothingMode = SmoothingMode.AntiAlias
            ' 在位图上绘制文字
            graphics.DrawString(ComboBox1.Text, font, brush, New PointF(20, 30))
            graphics.DrawString("启用：" & DateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm"), font, brush, New PointF(20, 80))
            graphics.DrawString("失效：" & DateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm"), font, brush, New PointF(20, 130))
            graphics.DrawString("签名：" & TextBox2.Text, font, brush, New PointF(20, 180))
            ' 设置文字对齐方式
            Dim format As New StringFormat With {
                .Alignment = StringAlignment.Far
            }
            ' 绘制矩形框
            Dim rect As New RectangleF(20, 30, 344, 100)
            graphics.DrawString("有效期: " & NumericUpDown1.Value & ComboBox2.Text, font, brush, rect, format)
        End Using

        If PictureBox1.Enabled Then
            PictureBox1.Image = GetRoundedImage(bitmap, 20)
        End If
    End Sub

    Private Shared Function GetRoundedImage(originalImage As Image, radius As Integer) As Image
        Dim roundedImage As New Bitmap(originalImage.Width, originalImage.Height)
        roundedImage.SetResolution(96, 96) ' 设置图像的 DPI
        Using g As Graphics = Graphics.FromImage(roundedImage)
            g.SmoothingMode = SmoothingMode.AntiAlias
            Using path As New GraphicsPath()
                path.AddArc(0, 0, radius * 2, radius * 2, 180, 90)
                path.AddArc(originalImage.Width - radius * 2, 0, radius * 2, radius * 2, 270, 90)
                path.AddArc(originalImage.Width - radius * 2, originalImage.Height - radius * 2, radius * 2, radius * 2, 0, 90)
                path.AddArc(0, originalImage.Height - radius * 2, radius * 2, radius * 2, 90, 90)
                g.SetClip(path)
                g.DrawImage(originalImage, 0, 0)
            End Using
        End Using
        Return roundedImage
    End Function

    Private Function GetDPIScaleFactor() As Single
        ' 获取当前显示设备的 DPI 设置
        Using g As Graphics = CreateGraphics()
            Dim dpiX As Single = g.DpiX
            Dim dpiY As Single = g.DpiY
            Return dpiX / 96.0F ' 返回 DPI 缩放比例
        End Using
    End Function
End Class
