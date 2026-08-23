Imports System.ComponentModel
Imports System.IO

Public Class Form_WorkFlow

    Public Variables As List(Of Object)
    Public UpdateDoc As Boolean = False
    Public SaveImages As Boolean = False
    Public CheckInterference As Boolean = False
    Public LengthUnits As SolidEdgeConstants.UnitOfMeasureLengthReadoutConstants
    Public Export As Boolean = False

    Public StartEvent As Integer = 1
    Public CurrentEvent As Integer = 1
    Public IsSingleStep As Boolean = False
    'Public ReverseStep As Boolean = False

    Public RequestPause As Boolean
    'Private _RequestPause As Boolean
    'Public Property RequestPause As Boolean
    '    Get
    '        Return _RequestPause
    '    End Get
    '    Set(value As Boolean)
    '        _RequestPause = value
    '        If Me.IsHandleCreated Then
    '            If _RequestPause Then
    '                BT_Play.Image = My.Resources.video_pause_pending
    '            Else
    '                BT_Play.Image = My.Resources.video_play
    '            End If
    '            Windows.Forms.Application.DoEvents()
    '        End If
    '    End Set
    'End Property

    Private _IsPaused As Boolean
    Public Property IsPaused As Boolean
        Get
            Return _IsPaused
        End Get
        Set(value As Boolean)
            _IsPaused = value
            If Me.IsHandleCreated Then
                If _IsPaused Then
                    BT_Play.Image = My.Resources.video_play
                Else
                    BT_Play.Image = My.Resources.video_pause
                End If
            End If
        End Set
    End Property


    Public NewWay As Boolean = True


    Private Sub Add_Event_Click(sender As Object, e As EventArgs) Handles Add_Event.Click

        Dim tmpStep = NewEvent()

        FLP_Events.Controls.Add(tmpStep)

        tmpStep.DG_Variables.ClearSelection()

        SetupAnchors()

        SetPrevious(tmpStep)

    End Sub

    Private Function BuildVariableList(UU As UtilsUnits) As List(Of EventVariable)

        ' Queries the current value of every eligible variable from Solid Edge, once.
        ' Callers that need this repeatedly (eg. once per workflow event on file load)
        ' should call this a single time and clone the result instead of calling it
        ' again, since each call is a round-trip to Solid Edge per variable.

        Dim list = New List(Of EventVariable)
        For Each tmpVar In Variables

            Dim tmpVariable As EventVariable = Nothing
            If Not (tmpVar.IsReadOnly Or tmpVar.Formula <> "") Then
                'tmpVariable.Value = Math.Round(UC_Slider.CadToValue(tmpVar.value, tmpVar.UnitsType, LengthUnits), 2)
                If NewWay Then
                    Dim tmpValue As Double = UU.GetVarValue(tmpVar)

                    tmpVariable = New EventVariable With {
                    .Check = True,
                    .Name = tmpVar.DisplayName,
                    .Value = Math.Round(tmpValue, 3),
                    .ObjVar = tmpVar
                }
                Else
                    tmpVariable = New EventVariable With {
                    .Check = True,
                    .Name = tmpVar.DisplayName,
                    .Value = Math.Round(UC_Slider.CadToValue(tmpVar.value, tmpVar.UnitsType, LengthUnits), 3),
                    .ObjVar = tmpVar
                }
                End If

                list.Add(tmpVariable)
            End If

        Next

        Return list

    End Function

    Private Function CloneVariableList(BaseList As List(Of EventVariable)) As List(Of EventVariable)

        Dim list = New List(Of EventVariable)
        For Each v In BaseList
            list.Add(New EventVariable With {
                .Check = v.Check,
                .Name = v.Name,
                .Value = v.Value,
                .ObjVar = v.ObjVar
            })
        Next

        Return list

    End Function

    Private Function NewEvent(Optional BaseList As List(Of EventVariable) = Nothing) As UC_WorkFlowEvent

        Dim list As List(Of EventVariable)

        If BaseList Is Nothing Then
            Dim UU As New UtilsUnits(Form_VarHandler.ObjDoc)
            list = BuildVariableList(UU)
        Else
            list = CloneVariableList(BaseList)
        End If

        Dim bindingList = New BindingList(Of EventVariable)(list)
        Dim source = New BindingSource(bindingList, Nothing)

        Dim tmpStep As New UC_WorkFlowEvent

        tmpStep.DG_Variables.Columns.Clear()
        tmpStep.DG_Variables.AutoGenerateColumns = True
        tmpStep.DG_Variables.DataSource = source
        tmpStep.LB_SEQ.Text = FLP_Events.Controls.Count + 1

        tmpStep.Height = 22 + tmpStep.DG_Variables.RowTemplate.Height * tmpStep.DG_Variables.Rows.Count

        tmpStep.DG_Variables.Columns.Item("check").Visible = False
        tmpStep.DG_Variables.Columns.Item("objVar").Visible = False
        tmpStep.DG_Variables.Columns.Item("Value").Width = 80
        tmpStep.DG_Variables.Columns.Item("Value").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        tmpStep.DG_Variables.Columns.Item("Value").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        tmpStep.DG_Variables.Columns.Item("Name").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        tmpStep.DG_Variables.Columns.Item("Name").ReadOnly = True

        Return tmpStep

    End Function

    Private Sub SetPrevious(StepEvent As UC_WorkFlowEvent)

        If FLP_Events.Controls.Count > 1 Then

            Dim PreviousStep As UC_WorkFlowEvent = FLP_Events.Controls.Item(FLP_Events.Controls.Count - 2)
            For Each tmpRow As DataGridViewRow In PreviousStep.DG_Variables.Rows
                StepEvent.DG_Variables.Rows.Item(tmpRow.Index).Cells("Value").Value = tmpRow.Cells("Value").Value
            Next

        End If

    End Sub

    Private Sub SetupAnchors()

        FLP_Events.SuspendLayout()

        If FLP_Events.Controls.Count > 0 Then
            For i = 0 To FLP_Events.Controls.Count - 1
                Dim c As Control = FLP_Events.Controls(i)
                If i = 0 Then
                    ' Its the first control, all subsequent controls follow
                    ' the anchor behavior of this control.
                    c.Width = FLP_Events.Width - 6
                    c.Anchor = AnchorStyles.Left + AnchorStyles.Top

                    If FLP_Events.VerticalScroll.Visible Then c.Width += -SystemInformation.VerticalScrollBarWidth + 0

                Else

                    ' It is not the first control. Set its anchor to
                    ' copy the width of the first control in the list.
                    c.Anchor = AnchorStyles.Left + AnchorStyles.Right

                End If
            Next
        End If

        FLP_Events.ResumeLayout()

    End Sub

    Private Sub Form_WorkFlow_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        SetupAnchors()
    End Sub

    Private Sub DoUpdateExports(
        ExportList As List(Of String),
        StepEvent As UC_WorkFlowEvent,
        UU As UtilsUnits)

        ' ExportList format
        ' Event, Var1, Var2, ...
        ' 1, 1.75, 30, ...
        ' 1, 2.00, 29, ...

        Dim RowString As String = ""
        Dim EventNumber As String = StepEvent.LB_SEQ.Text

        If ExportList.Count = 0 Then  ' Needs a header row
            RowString = "Event"
            For Each tmpRow As DataGridViewRow In StepEvent.DG_Variables.Rows
                Dim tmpVariable As Object = tmpRow.Cells("objVar").Value
                RowString = String.Format("{0},{1}", RowString, tmpVariable.Name)
                ExportList.Add(RowString)
            Next
        End If

        RowString = EventNumber
        For Each tmpRow As DataGridViewRow In StepEvent.DG_Variables.Rows
            Dim tmpVariable As Object = tmpRow.Cells("objVar").Value

            If NewWay Then
                Dim tmpValue As Double = UU.GetVarValue(tmpVariable)
                RowString = String.Format("{0},{1}", RowString, CStr(tmpValue))
            Else
                RowString = String.Format("{0},{1}", RowString, CStr(UC_Slider.CadToValue(tmpVariable.Value, tmpVariable.UnitsType, LengthUnits)))
            End If
            ExportList.Add(RowString)
        Next

    End Sub

    Private Function CheckVarLimits() As Boolean
        Dim Success As Boolean = True

        Dim UU As New UtilsUnits(Form_VarHandler.ObjDoc)
        Dim ErrorList As New List(Of String)
        Dim StepCount As Integer = 1
        Dim s As String

        ' A variable's limit metadata (whether it has one, and its min/max) doesn't
        ' change from event to event, so query it from Solid Edge once per variable
        ' name and cache it here instead of re-querying it for every event - same
        ' O(events x variables) COM round-trip problem fixed in the CSV load path.
        Dim LimitCache As New Dictionary(Of String, VarLimit)

        If FLP_Events.Controls.Count > 0 Then
            For Each StepEvent As UC_WorkFlowEvent In FLP_Events.Controls

                For Each tmpRow As DataGridViewRow In StepEvent.DG_Variables.Rows

                    Dim tmpVariable As Object = tmpRow.Cells("objVar").Value
                    Dim tmpName As String = tmpRow.Cells("Name").Value

                    Dim Limit As VarLimit = Nothing
                    If Not LimitCache.TryGetValue(tmpName, Limit) Then
                        Limit = New VarLimit With {.HasLimit = UU.HasVariableLimit(tmpVariable)}
                        If Limit.HasLimit Then
                            Limit.Min = UU.GetValueRangeLowValue(tmpVariable)
                            Limit.Max = UU.GetValueRangeHighValue(tmpVariable)
                        End If
                        LimitCache(tmpName) = Limit
                    End If

                    If Limit.HasLimit Then

                        LabelStatus.Text = String.Format("Checking variable '{0}'", tmpName)

                        Dim tmpValue As Double = tmpRow.Cells("Value").Value
                        If tmpValue < Limit.Min Then
                            Success = False
                            s = String.Format("Event {0}: {1}: Value {2} < minimum limit {3}", StepCount, tmpName, tmpValue, Limit.Min)
                            ErrorList.Add(s)
                        ElseIf tmpValue > Limit.Max Then
                            Success = False
                            s = String.Format("Event {0}: {1} Value {2} > maximum limit {3}", StepCount, tmpName, tmpValue, Limit.Max)
                            ErrorList.Add(s)
                        End If

                    End If
                Next

                StepCount += 1
            Next
        End If

        If ErrorList.Count > 0 Then
            s = String.Format("Variables outside of limits{0}", vbCrLf)
            For Each line As String In ErrorList
                s = String.Format("{0}{1}{2}", s, line, vbCrLf)
            Next
            MsgBox(s, vbOKOnly)
        End If

        LabelStatus.Text = ""

        Return Success
    End Function

    Private Sub BT_Play_Click(sender As Object, e As EventArgs) Handles BT_Play.Click

        If Not IsPaused And RequestPause Then Exit Sub ' Ignore repeated clicks

        If IsPaused Then
            IsPaused = False
        Else
            RequestPause = True
            Exit Sub
        End If

        If Not CheckVarLimits() Then Exit Sub

        'BT_Skip.Text = "Stop"
        'BT_Skip.Image = My.Resources._Stop

        Dim InterferenceMessage As String = ""
        Dim ExportList As List(Of String) = Nothing
        If Export Then ExportList = New List(Of String)

        Dim UU As New UtilsUnits(Form_VarHandler.ObjDoc)

        Dim EventsCount As Integer = FLP_Events.Controls.Count

        Dim StartTime As DateTime = Now
        Dim ElapsedTime As Double

        If FLP_Events.Controls.Count > 0 Then

            If CurrentEvent = FLP_Events.Controls.Count Then CurrentEvent = 1

            Dim StartIdx As Integer = CurrentEvent - 1

            Dim EndIdx As Integer
            If Not IsSingleStep Then
                EndIdx = FLP_Events.Controls.Count - 1
            Else
                StartIdx = CurrentEvent
                EndIdx = StartIdx
            End If

            For idx = StartIdx To EndIdx
                CurrentEvent = idx + 1

                Dim tmpEvent As UC_WorkFlowEvent = FLP_Events.Controls(idx)

                FLP_Events.ScrollControlIntoView(tmpEvent)

                tmpEvent.LB_SEQ.ForeColor = Color.DarkGreen

                SetSteps(tmpEvent, UU)

                ProcessEventSteps(tmpEvent, StartTime, ElapsedTime, EventsCount, InterferenceMessage, ExportList, UU)

                tmpEvent.LB_SEQ.ForeColor = Color.DarkGray

                System.Windows.Forms.Application.DoEvents()
                If RequestPause Then
                    'BT_Skip.Text = "Skip"
                    'BT_Skip.Image = My.Resources.skip
                    RequestPause = False
                    IsPaused = True
                    LabelStatus.Text = "Paused"
                    Exit Sub
                End If

            Next

            If Export Then
                Dim Dirname = System.IO.Path.GetDirectoryName(Form_VarHandler.ObjDoc.FullName)
                Dim Filename As String = String.Format("{0}\results.csv", Dirname)

                Try
                    IO.File.WriteAllLines(Filename, ExportList)
                Catch ex As Exception
                    MsgBox(String.Format("Could not save results file '{0}'", Filename))
                End Try

            End If
            LabelStatus.Text = String.Format("Processing complete in {0} min", ElapsedTime.ToString("0.0"))

            If Not InterferenceMessage = "" Then
                MsgBox(InterferenceMessage, vbOKOnly)
            End If

        End If

        'BT_Skip.Text = "Skip"
        'BT_Skip.Image = My.Resources.skip

        IsPaused = True

    End Sub

    Private Sub ProcessEventSteps(
        tmpEvent As UC_WorkFlowEvent,
        StartTime As DateTime,
        ByRef ElapsedTime As Double,
        EventsCount As Integer,
        ByRef InterferenceMessage As String,
        ByRef ExportList As List(Of String),
        UU As UtilsUnits)

        For StepNumber = 1 To tmpEvent.steps

            Dim StepsCount As Integer = tmpEvent.steps

            ElapsedTime = Now.Subtract(StartTime).TotalMinutes

            LabelStatus.Text = $"Event {tmpEvent.LB_SEQ.Text}/{EventsCount}, Step {StepNumber}/{StepsCount}, Elapsed {ElapsedTime.ToString("0.0")} min"

            ' Process first step before incrementing variables
            Dim IsFirstStep As Boolean = (tmpEvent.LB_SEQ.Text = "1") And (StepNumber = 1)
            If IsFirstStep Then ProcessEventStepOptionalUpdates(tmpEvent, InterferenceMessage, ExportList, UU, StepNumber)

            Form_VarHandler.ObjDoc.Parent.DelayCompute = True

            For Each tmpRow As DataGridViewRow In tmpEvent.DG_Variables.Rows

                Dim tmpVariable As Object = tmpRow.Cells("objVar").Value

                ' ###### TODO The variable is sometimes getting out of range in SE. ######
                ' Need a min/max check somewhere.  Not sure this is the place to do it.

                If NewWay Then
                    Dim tmpValue = UU.GetVarValue(tmpVariable) + tmpRow.Tag
                    UU.SetVarValue(tmpVariable, tmpValue)
                Else
                    tmpVariable.Value += UC_Slider.ValueToCad(tmpRow.Tag, tmpVariable.UnitsType, LengthUnits)
                End If

            Next

            Form_VarHandler.ObjDoc.Parent.DelayCompute = False

            ProcessEventStepOptionalUpdates(tmpEvent, InterferenceMessage, ExportList, UU, StepNumber)
        Next

    End Sub

    Private Sub ProcessEventStepOptionalUpdates(
        tmpEvent As UC_WorkFlowEvent,
        ByRef InterferenceMessage As String,
        ByRef ExportList As List(Of String),
        UU As UtilsUnits,
        StepNumber As Integer)

        If UpdateDoc Then UC_Slider.DoUpdateDoc(Form_VarHandler.ObjDoc)

        Form_VarHandler.ObjDoc.Parent.DoIdle()

        If SaveImages Then UC_Slider.DoSaveImage(Form_VarHandler.ObjDoc)

        If CheckInterference Then
            If Not UC_Slider.DoCheckInterference(Form_VarHandler.ObjDoc) Then
                If InterferenceMessage = "" Then
                    InterferenceMessage = String.Format("Interference first detected in Event {0}, Step {1}", tmpEvent.LB_SEQ.Text, StepNumber)
                End If
            End If
        End If

        If Export Then DoUpdateExports(ExportList, tmpEvent, UU)

    End Sub

    Private Sub SetSteps(stepEvent As UC_WorkFlowEvent, UU As UtilsUnits)

        Dim tmpSteps = stepEvent.steps

        For Each tmpRow As DataGridViewRow In stepEvent.DG_Variables.Rows

            Dim tmpVariable As Object = tmpRow.Cells("objVar").Value
            Dim stepValue As Double
            Dim tmpValue As Double

            If NewWay Then
                tmpValue = UU.GetVarValue(tmpVariable)
                stepValue = (CDbl(tmpRow.Cells("Value").Value) - tmpValue) / tmpSteps
            Else
                tmpValue = UC_Slider.CadToValue(tmpVariable.Value, tmpVariable.UnitsType, LengthUnits)
                stepValue = (CDbl(tmpRow.Cells("Value").Value) - tmpValue) / tmpSteps
            End If

            tmpRow.Tag = stepValue

        Next

    End Sub

    Private Sub BT_Save_Click(sender As Object, e As EventArgs) Handles BT_Save.Click

        If FLP_Events.Controls.Count > 0 Then

            Dim tmpRighe As String = ""

            For Each StepEvent As UC_WorkFlowEvent In FLP_Events.Controls

                Dim tmpRiga As String = ""

                For Each tmpRow As DataGridViewRow In StepEvent.DG_Variables.Rows

                    Dim tmpNome As String = tmpRow.Cells("Name").Value
                    Dim tmpValue As String = tmpRow.Cells("Value").Value

                    tmpRiga += (tmpNome & ":" & tmpValue & "|")

                Next

                'tmpRiga = tmpRiga.Substring(0, tmpRiga.Length - 1)
                tmpRiga += String.Format("Steps:{0}", StepEvent.steps)

                tmpRighe += tmpRiga & vbCrLf

            Next

            tmpRighe = tmpRighe.TrimEnd

            Dim tmpFileDialog As New SaveFileDialog With {
                .Filter = "Text File|*.txt",
                .Title = "Save a Text File"
            }
            tmpFileDialog.ShowDialog()

            If tmpFileDialog.FileName <> "" Then

                Dim sw As StreamWriter
                sw = My.Computer.FileSystem.OpenTextFileWriter(tmpFileDialog.FileName, False)
                sw.Write(tmpRighe)
                sw.Close()

            End If

        End If

    End Sub

    Private Sub BT_Open_Click(sender As Object, e As EventArgs) Handles BT_Open.Click
        'alpha:90|theta:0.001|Steps:20
        'alpha:130|theta:0.001|Steps:5

        'tmpFileDialog.Filter = "Text File|*.txt"
        Dim tmpFileDialog As New OpenFileDialog With {
            .Filter = "CSV files|*.csv|Text files|*.txt",
            .Title = "Open a Text File"
        }
        tmpFileDialog.ShowDialog()

        Me.Cursor = Cursors.WaitCursor

        Dim Filename = tmpFileDialog.FileName

        'If tmpFileDialog.FileName <> "" Then
        If Filename <> "" Then

            Dim ext As String = IO.Path.GetExtension(Filename)
            If ext = ".csv" Then
                Filename = ParseCSV(Filename)
                If Filename Is Nothing Then Exit Sub
            End If

            'Dim prg = My.Computer.FileSystem.ReadAllText(tmpFileDialog.FileName)
            Dim prg = My.Computer.FileSystem.ReadAllText(Filename)
            Dim righe = prg.Replace(vbCrLf, vbCr).Split(vbCrLf)

            ' Query each variable's current value from Solid Edge once, up front,
            ' rather than once per event. The document isn't modified during load,
            ' so the value would be identical every time - re-querying it per event
            ' was the main cause of slow loads on large files.
            Dim OpenUU As New UtilsUnits(Form_VarHandler.ObjDoc)
            Dim BaseVariableList = BuildVariableList(OpenUU)

            FLP_Events.SuspendLayout()

            For i = 0 To righe.Count - 1

                LabelStatus.Text = String.Format("Loading event {0}", i + 1)
                System.Windows.Forms.Application.DoEvents()

                If righe(i).Trim = "" Then Continue For

                Dim tmpStep = NewEvent(BaseVariableList)

                FLP_Events.Controls.Add(tmpStep)

                tmpStep.DG_Variables.ClearSelection()

                Dim StepIdx As Integer = righe(i).IndexOf("|Steps:")
                Dim StepString = righe(i).Substring(StepIdx)
                tmpStep.steps = CInt(StepString.Split(CChar(":"))(1))
                Dim ValString = righe(i).Substring(0, StepIdx)

                'SetValues(tmpStep, righe(i))
                SetValues(tmpStep, ValString)

            Next

            FLP_Events.ResumeLayout()

            LabelStatus.Text = ""
            System.Windows.Forms.Application.DoEvents()

            SetupAnchors()

        End If

        Me.Cursor = Cursors.Default

    End Sub

    Private Function ParseCSV(CSVFilename As String) As String

        ' ###### CSV format example ######
        '[REF]Notes,Whatever,Whatever,Whatever
        '[REF]Event,1,2,3
        'Steps,20,5,20
        'robot_x,33,,
        'robot_y,11.75,20,

        Dim NewFilename As String = IO.Path.ChangeExtension(CSVFilename, ".txt")

        Dim InText As List(Of String) = IO.File.ReadAllLines(CSVFilename).ToList

        Dim VarsAndVals As New Dictionary(Of String, List(Of String))

        ' ###### Dictionary format example ######
        ' "Steps": ["20", "5", "20"]
        ' "robot_x": ["33", "", ""]
        ' "robot_y": ["11.75", "20", ""]

        Dim EventsCount As Integer
        Dim OutList As New List(Of String)
        Dim OutString As String
        Dim VarName As String
        Dim VarValue As String

        For Each Line As String In InText

            ' ###### Check for '[REF]' ######
            If Line.ToLower.Trim.StartsWith("[ref]") Then Continue For

            Dim InList As List(Of String) = Line.Split(CChar(",")).ToList

            Dim Var = InList(0).Trim
            InList.RemoveAt(0)

            LabelStatus.Text = $"Reading variable {Var}"
            System.Windows.Forms.Application.DoEvents()

            ' ###### Check for unnamed variables ######
            If Var = "" Then
                MsgBox(String.Format("Blank variable name in '{0}'", CSVFilename), vbOKOnly)
                Return Nothing
            End If

            ' ###### Check first value is not blank ######
            If InList(0).Trim = "" Then
                MsgBox(String.Format("First value in '{0}' cannot be blank", Var), vbOKOnly)
                Return Nothing
            End If

            ' ###### Check duplicate variable names ######
            If VarsAndVals.Keys.Contains(Var) Then
                MsgBox(String.Format("Duplicate variable '{0}' in '{1}'", Var, CSVFilename), vbOKOnly)
                Return Nothing
            Else
                VarsAndVals(Var) = InList
            End If
        Next

        ' #### Check for a 'Steps' variable ####
        If Not VarsAndVals.Keys.Contains("Steps") Then
            MsgBox(String.Format("Missing variable 'Steps' in '{0}'", CSVFilename), vbOKOnly)
            Return Nothing
        End If

        EventsCount = VarsAndVals(VarsAndVals.Keys(0)).Count

        ' ###### Check all variable have the same number of values ######
        For Each VarName In VarsAndVals.Keys
            If Not VarsAndVals(VarName).Count = EventsCount Then
                MsgBox(String.Format("Variable '{0}' wrong number of values", VarName), vbOKOnly)
                Return Nothing
            End If
        Next

        ' ###### Fill in blank values ######
        For Each VarName In VarsAndVals.Keys
            For i = 0 To EventsCount - 1
                VarValue = VarsAndVals(VarName)(i).Trim
                If VarValue = "" Then
                    VarsAndVals(VarName)(i) = VarsAndVals(VarName)(i - 1)
                End If
            Next
        Next

        ' ###### Workflow format example ######
        ' robot_x:33|robot_y:11.75|Steps:20
        ' robot_x:33|robot_y:20|Steps:5
        ' robot_x:33|robot_y:20|Steps:20

        For i = 0 To EventsCount - 1

            OutString = ""

            For Each VarName In VarsAndVals.Keys

                LabelStatus.Text = $"Writing variable {VarName}"
                System.Windows.Forms.Application.DoEvents()

                If VarName = "Steps" Then Continue For

                VarValue = VarsAndVals(VarName)(i).Trim
                If VarValue = "" Then VarValue = VarsAndVals(VarName)(i - 1)

                OutString = String.Format("{0}{1}:{2}|", OutString, VarName, VarValue)
            Next

            ' ###### Add the 'Steps' variable at the end ######
            VarValue = VarsAndVals("Steps")(i).Trim
            If VarValue = "" Then VarValue = VarsAndVals("Steps")(i - 1)

            OutString = String.Format("{0}Steps:{1}", OutString, VarValue)

            OutList.Add(OutString)

        Next

        IO.File.WriteAllLines(NewFilename, OutList)

        Return NewFilename
    End Function

    Private Sub SetValues(tmpStep As UC_WorkFlowEvent, riga As String)

        Dim variabili = Split(riga, "|")

        For Each item In variabili

            Dim valori = item.Split(":")

            For Each tmpRow As DataGridViewRow In tmpStep.DG_Variables.Rows

                If tmpRow.Cells.Item("Name").Value = valori(0) Then tmpRow.Cells.Item("Value").Value = valori(1)

            Next

        Next

    End Sub

    Private Sub BT_Close_Click(sender As Object, e As EventArgs) Handles BT_Close.Click

        FLP_Events.Controls.Clear()

    End Sub

    Private Sub Form_WorkFlow_Load(sender As Object, e As EventArgs) Handles Me.Load
        '################# Questo risolver il problema del bordo sgrazinato della ToolStrip
        ToolStrip1.Renderer = New MySR()
        '################# rif: https://stackoverflow.com/questions/1918247/how-to-disable-the-line-under-tool-strip-in-winform-c

        IsPaused = True
        RequestPause = False
    End Sub

    Friend Sub ReNumber()

        FLP_Events.SuspendLayout()

        Dim i = 1

        For Each item As UC_WorkFlowEvent In FLP_Events.Controls

            item.LB_SEQ.Text = i.ToString
            i += 1

        Next

        SetupAnchors()

        FLP_Events.ResumeLayout()

    End Sub

    Private Sub BT_Skip_Click(sender As Object, e As EventArgs) Handles BT_Skip.Click

        'If Not RequestPause Then  ' Ignore multiple clicks
        '    If BT_Skip.Text = "Skip" Then
        '        Dim Result = InputBox("Enter start event number",, StartEvent)
        '        If Not Result = "" Then StartEvent = CInt(Result)
        '        CurrentEvent = StartEvent
        '    Else
        '        BT_Skip.Text = "Skip"
        '        BT_Skip.Image = My.Resources.skip
        '        RequestPause = True
        '    End If
        'End If

        If Not IsPaused Then Exit Sub

        Dim Result = InputBox("Enter start event number",, StartEvent)
        If Not Result = "" Then StartEvent = CInt(Result)
        CurrentEvent = StartEvent

    End Sub

    Private Sub BT_Step_Click(sender As Object, e As EventArgs) Handles BT_Step.Click

        If Not IsPaused Then Exit Sub

        IsSingleStep = True
        BT_Play.PerformClick()
        IsSingleStep = False
        IsPaused = True

    End Sub

    Private Sub BT_Rewind_Click(sender As Object, e As EventArgs) Handles BT_Rewind.Click

        If Not IsPaused Then Exit Sub

        CurrentEvent = 1
    End Sub

    Private Sub BT_End_Click(sender As Object, e As EventArgs) Handles BT_End.Click

        If Not IsPaused Then Exit Sub

        CurrentEvent = FLP_Events.Controls.Count

    End Sub
End Class

Public Class EventVariable

    Public Property Check As Boolean = True
    Public Property Name As String = ""
    Public Property Value As Double = 0
    Public Property ObjVar As Object

End Class

Public Class VarLimit

    Public Property HasLimit As Boolean = False
    Public Property Min As Double = 0
    Public Property Max As Double = 0

End Class