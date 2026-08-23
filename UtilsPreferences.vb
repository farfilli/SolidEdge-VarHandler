Option Strict On

Public Class UtilsPreferences

    Public Function GetProgramSettingsFilename(CheckExisting As Boolean) As String
        Dim Filename = "program_settings.json"
        Filename = $"{GetPreferencesDirectory()}\{Filename}"

        If CheckExisting Then
            If FileIO.FileSystem.FileExists(Filename) Then
                Return Filename
            Else
                Return ""
            End If
        Else
            Return Filename
        End If

    End Function

    Public Sub SaveProgramSettings(FVarHandler As Form_VarHandler)

        Dim tmpJSONDict As New Dictionary(Of String, String)
        Dim JSONString As String

        Dim Outfile = GetProgramSettingsFilename(CheckExisting:=False)

        Dim FormType As Type = FVarHandler.GetType()
        Dim PropInfos = New List(Of System.Reflection.PropertyInfo)(FormType.GetProperties())
        Dim Value As String
        Dim PropType As String

        Dim SkipProps As New List(Of String)
        'SkipProps.AddRange({"version", "previewversion", "stopprocess", "tasklist", "linkmanagementorder"})
        'SkipProps.AddRange({"listviewfilesoutofdate"})
        'SkipProps.AddRange({"propertiesdata", "listofcolumns", "presets", "propertyfilters"})
        'SkipProps.AddRange({"cliactive", "clipresetname", "clifilelistname"})
        'SkipProps.AddRange({"hcdebuglogger", "savedexpressions", "usea"})

        Dim AllProps As New List(Of String)

        For Each PropInfo As System.Reflection.PropertyInfo In PropInfos

            AllProps.Add(PropInfo.Name.ToLower)

            PropType = PropInfo.PropertyType.Name.ToLower

            Dim PropModule As String = PropInfo.Module.ToString.ToLower

            Dim tf As Boolean
            tf = PropInfo.Module.ToString.ToLower.Contains("solidedge-varhandler")
            tf = tf Or {"Left", "Top", "Width", "Height"}.ToList.Contains(PropInfo.Name)
            If Not tf Then Continue For

            If SkipProps.Contains(PropInfo.Name.ToLower) Then Continue For

            Value = Nothing

            Select Case PropType
                Case "string", "double", "int32", "boolean"
                    Value = CStr(PropInfo.GetValue(FVarHandler, Nothing))
                Case "list`1"
                    Value = Newtonsoft.Json.JsonConvert.SerializeObject(PropInfo.GetValue(FVarHandler, Nothing))
                Case Else
                    MsgBox($"In UP.SaveFormMainSettings: PropInfo.Name '{PropInfo.Name.ToLower}' not recognized")
                    'If PropInfo.Module.ToString.ToLower.Contains("housekeeper") Then tmpUnhandledPropTypes.Add(PropType)
            End Select


            If Value Is Nothing Then
                Select Case PropType
                    Case "string"
                        Value = ""
                    Case "double", "int32"
                        Value = "0"
                    Case "boolean"
                        Value = "False"
                    Case "list`1"
                        Value = Newtonsoft.Json.JsonConvert.SerializeObject(New List(Of String))
                    Case Else
                        MsgBox($"In UtilsPreferences.SaveFormMainSettings: PropInfo.PropertyType.Name '{PropInfo.PropertyType.Name}' not recognized")
                        'If PropInfo.Module.ToString.ToLower.Contains("housekeeper") Then tmpUnhandledPropTypes.Add(PropType)
                End Select
            End If

            tmpJSONDict(PropInfo.Name) = Value

        Next

        JSONString = Newtonsoft.Json.JsonConvert.SerializeObject(tmpJSONDict)

        IO.File.WriteAllText(Outfile, JSONString)

        'Dim MissingProps As String = ""
        'For Each s As String In SkipProps
        '    If Not AllProps.Contains(s) Then
        '        MissingProps = $"{MissingProps}    {s}{vbCrLf}"
        '    End If
        'Next
        'If Not MissingProps = "" Then
        '    MissingProps = $"SkipProps not found in AllProps {vbCrLf}{MissingProps}"
        '    MsgBox(MissingProps)
        'End If

    End Sub

    Public Sub GetProgramSettings(FVarHandler As Form_VarHandler)

        Dim tmpJSONDict As New Dictionary(Of String, String)
        Dim JSONString As String

        Dim Infile = GetProgramSettingsFilename(CheckExisting:=True)

        Dim FormType As Type = FVarHandler.GetType()
        Dim PropInfos = New List(Of System.Reflection.PropertyInfo)(FormType.GetProperties())

        If Not Infile = "" Then
            JSONString = IO.File.ReadAllText(Infile)

            tmpJSONDict = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(JSONString)

            For Each PropInfo As System.Reflection.PropertyInfo In PropInfos

                If tmpJSONDict.Keys.Contains(PropInfo.Name) Then
                    Dim PropTypestring = PropInfo.PropertyType.Name

                    Select Case PropInfo.PropertyType.Name.ToLower
                        Case "string"
                            PropInfo.SetValue(FVarHandler, CStr(tmpJSONDict(PropInfo.Name)))
                        Case "double"
                            PropInfo.SetValue(FVarHandler, CDbl(tmpJSONDict(PropInfo.Name)))
                        Case "int32"
                            PropInfo.SetValue(FVarHandler, CInt(tmpJSONDict(PropInfo.Name)))
                        Case "boolean"
                            PropInfo.SetValue(FVarHandler, CBool(tmpJSONDict(PropInfo.Name)))
                        Case "list`1"
                            Dim L = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of String))(tmpJSONDict(PropInfo.Name))
                            PropInfo.SetValue(FVarHandler, L)
                    End Select

                End If
            Next
        End If

    End Sub


    '###### FOLDERS ######
    Public Function GetStartupDirectory() As String

        ' Returns the location of SolidEdge-VarHandler.exe

        Dim StartupDirectory As String = System.Windows.Forms.Application.StartupPath()
        Return StartupDirectory
    End Function

    Public Function GetPreferencesDirectory() As String
        Dim StartupPath As String = GetStartupDirectory()
        Dim PreferencesDirectory = "Preferences"
        Return $"{StartupPath}\{PreferencesDirectory}"
    End Function

    Public Sub CreatePreferencesDirectory()
        Dim PreferencesDirectory = GetPreferencesDirectory()
        If Not FileIO.FileSystem.DirectoryExists(PreferencesDirectory) Then
            Try
                FileIO.FileSystem.CreateDirectory(PreferencesDirectory)
            Catch ex As Exception
                Dim s As String = $"Unable to create Preferences directory '{PreferencesDirectory}'.  "
                s = $"{s}You may not have the correct permissions.  Exception: {ex.Message}"
                MsgBox(s, vbOKOnly)
            End Try
        End If
    End Sub


End Class
