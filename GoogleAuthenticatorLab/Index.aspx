<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="GoogleAuthenticatorLab.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Image ID="imgBarcode" runat="server" Width="300px" />

            <br />
            <asp:Label ID="lblSecretKey" runat="server" Text=""></asp:Label>

            <br />

        </div>

        <asp:TextBox ID="txtCode" runat="server" Width="208px" ></asp:TextBox>

        <asp:Button ID="btnVerify" runat="server" Text="驗證代碼" OnClick="btnVerify_Click" />

        <br />
            <asp:Label ID="lblVerifyResult" runat="server" Text=""></asp:Label>

    </form>
</body>
</html>
