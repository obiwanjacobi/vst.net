Imports Jacobi.Vst.Core
Imports Jacobi.Vst.Framework

Friend Class PluginParameterFactory
    Public Categories As VstParameterCategoryCollection = New VstParameterCategoryCollection()
    Public ParameterInfos As VstParameterInfoCollection = New VstParameterInfoCollection()

    Public Sub New()
        Dim paramCat As VstParameterCategory = New VstParameterCategory()
        paramCat.Name = "Delay"

        Categories.Add(paramCat)
    End Sub

    Public Sub CreateParameters(ByVal parameters As VstParameterCollection)
        For Each paramInfo As VstParameterInfo In ParameterInfos
            If Categories.Count > 0 And paramInfo.Category Is Nothing Then paramInfo.Category = Categories(0)

            Dim param As VstParameter = New VstParameter(paramInfo)

            parameters.Add(param)
        Next
    End Sub

End Class
