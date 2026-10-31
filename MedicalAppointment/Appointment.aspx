<%@ Page Language="C#" Async="true" AutoEventWireup="true"
 CodeBehind="Appointment.aspx.cs"
 Inherits="MedicalAppointmentAssistant.Appointment" %>
<!DOCTYPE html>
<html>
<head runat="server">
 <title>Medical Appointment Assistant</title>
 <style>
 body { font-family: Arial; background-color: #f2f6ff; }
 .container {
 width: 600px;
 margin: 40px auto;
 background: white;
 padding: 30px;
 border-radius: 10px;
 }
 h2 { text-align: center; }
 .textbox { width: 95%; padding: 8px; }
 .button { padding: 10px 25px; }
 .message { font-weight: bold; }
 </style>
</head>
<body>
<form id="form1" runat="server">
<div class="container">
 <h2>AI-Based Medical Appointment Assistant</h2>
 <asp:Label ID="lblName" runat="server" Text="Patient Name"></asp:Label>
 <br />
 <asp:TextBox ID="txtPatientName" runat="server" CssClass="textbox"
 MaxLength="100"></asp:TextBox>
 <br /><br />
 <asp:Label ID="lblAge" runat="server" Text="Age"></asp:Label>
 <br />
 <asp:TextBox ID="txtAge" runat="server" CssClass="textbox"></asp:TextBox>
 <br /><br />
 <asp:Label ID="lblGender" runat="server" Text="Gender"></asp:Label>
 <br />
 <asp:DropDownList ID="ddlGender" runat="server" CssClass="textbox">
 <asp:ListItem Text="Select Gender" Value=""></asp:ListItem>
 <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
 <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
 <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
 </asp:DropDownList>
 <br /><br />
 <asp:Label ID="lblDoctor" runat="server" Text="Doctor"></asp:Label>
<br />
 <asp:DropDownList ID="ddlDoctor" runat="server" CssClass="textbox">
 <asp:ListItem Text="Select Doctor" Value=""></asp:ListItem>
 <asp:ListItem Text="Dr. Sharma" Value="Dr. Sharma"></asp:ListItem>
 <asp:ListItem Text="Dr. Patil" Value="Dr. Patil"></asp:ListItem>
 <asp:ListItem Text="Dr. Joshi" Value="Dr. Joshi"></asp:ListItem>
 </asp:DropDownList>
 <br /><br />
 <asp:Label ID="lblDate" runat="server" Text="Appointment Date"></asp:Label>
 <br />
 <asp:TextBox ID="txtDate" runat="server" TextMode="Date"
 CssClass="textbox"></asp:TextBox>
 <br /><br />
 <asp:Label ID="lblType" runat="server" Text="Appointment Type"></asp:Label>
 <br />
 <asp:DropDownList ID="ddlType" runat="server" CssClass="textbox">
 <asp:ListItem Text="Select Type" Value=""></asp:ListItem>
 <asp:ListItem Text="New Consultation" Value="New Consultation"></asp:ListItem>
 <asp:ListItem Text="Follow-up" Value="Follow-up"></asp:ListItem>
 <asp:ListItem Text="Emergency" Value="Emergency"></asp:ListItem>
 </asp:DropDownList>
 <br /><br />
 <asp:Label ID="lblDescription" runat="server"
 Text="Problem / Description (max 500 characters)"></asp:Label>
 <br />
 <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine"
 Rows="5" MaxLength="500" CssClass="textbox"></asp:TextBox>
 <br /><br />
 <asp:Button ID="btnBook" runat="server" Text="Book Appointment"
 CssClass="button" OnClick="btnBook_Click" />
 <br /><br />
 <asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>
 <br /><br />
 <asp:HyperLink ID="lnkRecords" runat="server"
 NavigateUrl="~/AppointmentRecords.aspx"
 Text="View Appointment Records"></asp:HyperLink>
</div>
</form>
</body>
</html>