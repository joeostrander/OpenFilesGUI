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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.ButtonScan = New System.Windows.Forms.Button()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripProgressBar1 = New System.Windows.Forms.ToolStripProgressBar()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.ColumnHeaderPath = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeaderUsername = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeaderNumberOfLocks = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeaderOpenMode = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeaderID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ContextMenuStripListView = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CloseFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowInExplorerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ClearResultsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TextBoxFilter = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ComboBoxServer = New System.Windows.Forms.ComboBox()
        Me.ContextMenuStripServer = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.RemoveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStripForm = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusStrip1.SuspendLayout()
        Me.ContextMenuStripListView.SuspendLayout()
        Me.ContextMenuStripServer.SuspendLayout()
        Me.ContextMenuStripForm.SuspendLayout()
        Me.SuspendLayout()
        '
        'ButtonScan
        '
        Me.ButtonScan.Location = New System.Drawing.Point(248, 24)
        Me.ButtonScan.Name = "ButtonScan"
        Me.ButtonScan.Size = New System.Drawing.Size(75, 23)
        Me.ButtonScan.TabIndex = 4
        Me.ButtonScan.Text = "&Start"
        Me.ButtonScan.UseVisualStyleBackColor = True
        '
        'BackgroundWorker1
        '
        Me.BackgroundWorker1.WorkerReportsProgress = True
        Me.BackgroundWorker1.WorkerSupportsCancellation = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripProgressBar1, Me.ToolStripStatusLabel1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 365)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(563, 22)
        Me.StatusStrip1.TabIndex = 6
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripProgressBar1
        '
        Me.ToolStripProgressBar1.Name = "ToolStripProgressBar1"
        Me.ToolStripProgressBar1.Size = New System.Drawing.Size(100, 16)
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(120, 17)
        Me.ToolStripStatusLabel1.Text = "ToolStripStatusLabel1"
        '
        'ListView1
        '
        Me.ListView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeaderPath, Me.ColumnHeaderUsername, Me.ColumnHeaderNumberOfLocks, Me.ColumnHeaderOpenMode, Me.ColumnHeaderID})
        Me.ListView1.ContextMenuStrip = Me.ContextMenuStripListView
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        Me.ListView1.Location = New System.Drawing.Point(12, 69)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(539, 293)
        Me.ListView1.TabIndex = 5
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'ColumnHeaderPath
        '
        Me.ColumnHeaderPath.Tag = "String"
        Me.ColumnHeaderPath.Text = "Path"
        Me.ColumnHeaderPath.Width = 300
        '
        'ColumnHeaderUsername
        '
        Me.ColumnHeaderUsername.Tag = "String"
        Me.ColumnHeaderUsername.Text = "User ID"
        '
        'ColumnHeaderNumberOfLocks
        '
        Me.ColumnHeaderNumberOfLocks.Tag = "Numeric"
        Me.ColumnHeaderNumberOfLocks.Text = "# Locks"
        '
        'ColumnHeaderOpenMode
        '
        Me.ColumnHeaderOpenMode.Tag = "String"
        Me.ColumnHeaderOpenMode.Text = "Open Mode"
        Me.ColumnHeaderOpenMode.Width = 80
        '
        'ColumnHeaderID
        '
        Me.ColumnHeaderID.Tag = "Numeric"
        Me.ColumnHeaderID.Text = "ID"
        Me.ColumnHeaderID.Width = 0
        '
        'ContextMenuStripListView
        '
        Me.ContextMenuStripListView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CloseFileToolStripMenuItem, Me.ShowInExplorerToolStripMenuItem, Me.ExportListToolStripMenuItem, Me.ClearResultsToolStripMenuItem})
        Me.ContextMenuStripListView.Name = "ContextMenuStrip1"
        Me.ContextMenuStripListView.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ContextMenuStripListView.ShowImageMargin = False
        Me.ContextMenuStripListView.Size = New System.Drawing.Size(152, 92)
        '
        'CloseFileToolStripMenuItem
        '
        Me.CloseFileToolStripMenuItem.Name = "CloseFileToolStripMenuItem"
        Me.CloseFileToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
        Me.CloseFileToolStripMenuItem.Text = "&Close Selected Files"
        '
        'ShowInExplorerToolStripMenuItem
        '
        Me.ShowInExplorerToolStripMenuItem.Name = "ShowInExplorerToolStripMenuItem"
        Me.ShowInExplorerToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
        Me.ShowInExplorerToolStripMenuItem.Text = "&Show in Explorer"
        '
        'ExportListToolStripMenuItem
        '
        Me.ExportListToolStripMenuItem.Name = "ExportListToolStripMenuItem"
        Me.ExportListToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
        Me.ExportListToolStripMenuItem.Text = "&Export List"
        '
        'ClearResultsToolStripMenuItem
        '
        Me.ClearResultsToolStripMenuItem.Name = "ClearResultsToolStripMenuItem"
        Me.ClearResultsToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
        Me.ClearResultsToolStripMenuItem.Text = "Clear &Results"
        '
        'TextBoxFilter
        '
        Me.TextBoxFilter.Location = New System.Drawing.Point(142, 26)
        Me.TextBoxFilter.Name = "TextBoxFilter"
        Me.TextBoxFilter.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxFilter.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Ser&ver:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(139, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "&Filter (optional)"
        '
        'ComboBoxServer
        '
        Me.ComboBoxServer.ContextMenuStrip = Me.ContextMenuStripServer
        Me.ComboBoxServer.FormattingEnabled = True
        Me.ComboBoxServer.Location = New System.Drawing.Point(15, 26)
        Me.ComboBoxServer.Name = "ComboBoxServer"
        Me.ComboBoxServer.Size = New System.Drawing.Size(121, 21)
        Me.ComboBoxServer.TabIndex = 1
        '
        'ContextMenuStripServer
        '
        Me.ContextMenuStripServer.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RemoveToolStripMenuItem})
        Me.ContextMenuStripServer.Name = "ContextMenuStripServer"
        Me.ContextMenuStripServer.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ContextMenuStripServer.ShowImageMargin = False
        Me.ContextMenuStripServer.Size = New System.Drawing.Size(93, 26)
        '
        'RemoveToolStripMenuItem
        '
        Me.RemoveToolStripMenuItem.Name = "RemoveToolStripMenuItem"
        Me.RemoveToolStripMenuItem.Size = New System.Drawing.Size(92, 22)
        Me.RemoveToolStripMenuItem.Text = "&Remove"
        '
        'ContextMenuStripForm
        '
        Me.ContextMenuStripForm.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutToolStripMenuItem})
        Me.ContextMenuStripForm.Name = "ContextMenuStripForm"
        Me.ContextMenuStripForm.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ContextMenuStripForm.ShowImageMargin = False
        Me.ContextMenuStripForm.Size = New System.Drawing.Size(83, 26)
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(82, 22)
        Me.AboutToolStripMenuItem.Text = "&About"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(563, 387)
        Me.ContextMenuStrip = Me.ContextMenuStripForm
        Me.Controls.Add(Me.ComboBoxServer)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxFilter)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.ButtonScan)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(579, 426)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ContextMenuStripListView.ResumeLayout(False)
        Me.ContextMenuStripServer.ResumeLayout(False)
        Me.ContextMenuStripForm.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ButtonScan As System.Windows.Forms.Button
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents ToolStripProgressBar1 As ToolStripProgressBar
    Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
    Friend WithEvents ListView1 As ListView
    Friend WithEvents ColumnHeaderID As ColumnHeader
    Friend WithEvents ColumnHeaderNumberOfLocks As ColumnHeader
    Friend WithEvents ColumnHeaderUsername As ColumnHeader
    Friend WithEvents ColumnHeaderOpenMode As ColumnHeader
    Friend WithEvents ColumnHeaderPath As ColumnHeader
    Friend WithEvents TextBoxFilter As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents ContextMenuStripListView As ContextMenuStrip
    Friend WithEvents CloseFileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExportListToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ComboBoxServer As ComboBox
    Friend WithEvents ContextMenuStripServer As ContextMenuStrip
    Friend WithEvents RemoveToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ContextMenuStripForm As ContextMenuStrip
    Friend WithEvents AboutToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ShowInExplorerToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ClearResultsToolStripMenuItem As ToolStripMenuItem
End Class
