Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ListBox1.Items.Add(My.Computer.Clipboard.GetText)
        NoHistoryInClipboardToolStripMenuItem.Text = My.Computer.Clipboard.GetText
        TextBox1.Text = My.Computer.Clipboard.GetText
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        TextBox1.Text = My.Computer.Clipboard.GetText
        If Me.Visible = True Then
            ShowProgramToolStripMenuItem.Enabled = False
            HideProgramToolStripMenuItem.Enabled = True
        End If
        If Me.Visible = False Then
            ShowProgramToolStripMenuItem.Enabled = True
            HideProgramToolStripMenuItem.Enabled = False
        End If
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        If TextBox1.Text = My.Computer.Clipboard.GetText Then

        Else
            ListBox1.Items.Add(My.Computer.Clipboard.GetText)
            NoHistoryInClipboardToolStripMenuItem.Text = My.Computer.Clipboard.GetText
            TextBox1.Text = My.Computer.Clipboard.GetText
        End If
    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripMenuItem.Click
        Dim copy_buffer As New System.Text.StringBuilder
        For Each item As Object In ListBox1.SelectedItems
            copy_buffer.AppendLine(item.ToString)
        Next
        If copy_buffer.Length > 0 Then
            Clipboard.SetText(copy_buffer.ToString)
        End If
    End Sub

    Private Sub OpenInBrowserToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenInBrowserToolStripMenuItem.Click
        Dim copy_buffer As New System.Text.StringBuilder
        For Each item As Object In ListBox1.SelectedItems
            copy_buffer.AppendLine(item.ToString)
        Next
        If copy_buffer.Length > 0 Then
            Process.Start("https://www.google.com/search?q=" & copy_buffer.ToString)
        End If
    End Sub

    Private Sub RemoveFromListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveFromListToolStripMenuItem.Click
        ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
    End Sub

    Private Sub ListBox1_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles ListBox1.DrawItem
        e.DrawBackground()
        If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
            e.Graphics.FillRectangle(Brushes.Blue, e.Bounds)
        End If
        Using b As New SolidBrush(e.ForeColor)
            e.Graphics.DrawString(ListBox1.GetItemText(ListBox1.Items(e.Index)), e.Font, b, e.Bounds)
        End Using
        e.DrawFocusRectangle()
    End Sub

    Private Sub ChangeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            Timer1.Start()
            Timer2.Start()
            CheckBox1.Text = "Enabled"
        End If
        If CheckBox1.Checked = False Then
            Timer1.Stop()
            Timer2.Stop()
            MsgBox("Disabled the program. Please click again to enable!", MsgBoxStyle.Exclamation, "Exclamation")
            CheckBox1.Text = "Disabled"
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            NotifyIcon1.Visible = True
        End If
        If CheckBox2.Checked = False Then
            NotifyIcon1.Visible = False
        End If
    End Sub

    Private Sub ShowProgramToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowProgramToolStripMenuItem.Click
        Me.Show()
        Me.WindowState = System.Windows.Forms.FormWindowState.Normal
    End Sub

    Private Sub HideProgramToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HideProgramToolStripMenuItem.Click
        Me.Hide()
        MsgBox("The program is hide, don't closed!", MsgBoxStyle.Information, "Information")
    End Sub
    Private Sub Form1_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If (MessageBox.Show("You are close the application?", "Close?", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No) Then
            e.Cancel = True
        End If
    End Sub

    Private Sub CheckBox5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox5.CheckedChanged
        Dim applicationName As String = Application.ProductName
        Dim applicationPath As String = Application.ExecutablePath

        If CheckBox5.Checked = True Then
            Dim regKey As Microsoft.Win32.RegistryKey
            regKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True)
            regKey.SetValue(applicationName, """" & applicationPath & """")
            regKey.Close()
        End If
        If CheckBox5.Checked = False Then
            Dim regKey As Microsoft.Win32.RegistryKey
            regKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True)
            regKey.DeleteValue(applicationName, False)
            regKey.Close()
        End If
    End Sub

    Private Sub CheckBox6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox6.CheckedChanged
        If CheckBox6.Checked = True Then
            CheckBox6.Text = "Use the program..."
        End If
        If CheckBox6.Checked = False Then
            If CheckBox2.Checked = True Then
                Me.Hide()
                CheckBox6.Text = "Use the GUI..."
                EnableProgramToolStripMenuItem.Visible = True
                ToolStripSeparator5.Visible = True
                MsgBox("The program hide, you use small GUI. If you use a program, click 'Enable Program' on small GUI!", MsgBoxStyle.Information, "Yes?")
            Else
                MsgBox("You don't enable 'small GUI'!", MsgBoxStyle.Exclamation, "Ohh Error")
                CheckBox6.Checked = True
            End If
        End If
    End Sub

    Private Sub IfYouNeedMoreToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IfYouNeedMoreToolStripMenuItem.Click
        Me.Show()
        Me.WindowState = System.Windows.Forms.FormWindowState.Normal
    End Sub

    Private Sub EnableProgramToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EnableProgramToolStripMenuItem.Click
        Me.Show()
        EnableProgramToolStripMenuItem.Visible = False
        ToolStripSeparator5.Visible = False
        CheckBox6.Checked = True
        MsgBox("You are activated a program!", MsgBoxStyle.Information, "Ohh")
    End Sub
    Private Sub NoHistoryInClipboardToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NoHistoryInClipboardToolStripMenuItem.Click
        Clipboard.SetText(NoHistoryInClipboardToolStripMenuItem.Text)
    End Sub
End Class
