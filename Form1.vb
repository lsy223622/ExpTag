Imports Microsoft.VisualBasic.FileIO

Public Class Form1
    Private ReadOnly csvData As New Dictionary(Of String, Tuple(Of Integer, String))

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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        DateTimePicker1.Value = DateTime.Now
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
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
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        UpdateTime()
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

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        UpdateTime()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        UpdateTime()
    End Sub
End Class
