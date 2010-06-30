Public Class frmMain


    Private Sub btnExtractUpdateFields_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExtractUpdateFields.Click
        ExtractUpdateFields()
    End Sub

    Private Sub btnExtractOpcodes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExtractOpcodes.Click
        ExtractOpcodes()
    End Sub

    Private Sub btnExtractSpellFailedReasons_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExtractSpellFailedReasons.Click
        ExtractSpellFailedReason()
    End Sub

    Private Sub btnExtractChatTypes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExtractChatTypes.Click
        ExtractChatTypes()
    End Sub
End Class
