<%@ Page Language="C#" AutoEventWireup="true"
 CodeBehind="AppointmentRecords.aspx.cs"
 Inherits="MedicalAppointmentAssistant.AppointmentRecords" %>
<!DOCTYPE html>
<html>
<head runat="server">
 <title>Appointment Records</title>
</head>
<body>
<form id="form1" runat="server">
 <h2>Appointment Records</h2>
 <asp:GridView ID="gvAppointments" runat="server"
 AutoGenerateColumns="false" BorderWidth="1" CellPadding="6"
 EmptyDataText="No appointments found.">
 <Columns>
 <asp:BoundField DataField="AppointmentId" HeaderText="ID" />
 <asp:BoundField DataField="PatientName" HeaderText="Patient Name" />
 <asp:BoundField DataField="Age" HeaderText="Age" />
 <asp:BoundField DataField="Gender" HeaderText="Gender" />
 <asp:BoundField DataField="DoctorName" HeaderText="Doctor" />
 <asp:BoundField DataField="AppointmentDate" HeaderText="Date"
 DataFormatString="{0:dd-MM-yyyy}" />
 <asp:BoundField DataField="AppointmentType" HeaderText="Type" />
 <asp:BoundField DataField="Description" HeaderText="Description" />

 </Columns>
 </asp:GridView>
 <br />
 <asp:HyperLink ID="lnkBook" runat="server" NavigateUrl="~/Appointment.aspx"
 Text="Book a new appointment"></asp:HyperLink>
</form>
</body>
</html>