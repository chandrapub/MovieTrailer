<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="MovieTralier.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Movie Tralier</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="  
  display:flex; align-items:center; width: 100%; height: 100%; background-color: #3498db;"><h1>
            <asp:Label ID="LabelFiletype" runat="server" Text="Type the movie name"></asp:Label>
            </h1>
            <br />
            <br />
           
            <asp:TextBox ID="TextBoxInput" runat="server" Width="298px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Buttonfind" runat="server" OnClick="Buttonfind_Click" Text="Find Movie" />
            <br />
            <br />
            <asp:Label ID="LabelMessages" runat="server" Text="Message"></asp:Label>
            <br />
            <br />
            <asp:Label ID="LabelResult" runat="server" Text="Result"></asp:Label>
            <br />
            <br />
            <asp:Image ID="ImagePoster" runat="server" ImageUrl="~/MyFiles/MovieEmoji.jpg" />
            <br />
            <br />
            <asp:Button ID="ButtonPlayTralier" runat="server" OnClick="ButtonPlayTralier_Click" Text="Play trailer" />
            <br />
            <iframe id="youTubeTrailer" runat="server" width="560" height="315" frameborder="2" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen= "allowfullscreen"></iframe>
            <br />
            <asp:Label ID="LabelTralier" runat="server" Text="Tralier's status"></asp:Label>
            <br />
            <br />
        </div>
    </form>
</body>
</html>
