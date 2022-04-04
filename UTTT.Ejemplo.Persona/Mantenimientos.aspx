<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mantenimientos.aspx.cs" Inherits="UTTT.Ejemplo.Persona.Mantenimientos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <nav class="navbar fixed-top navbar-dark bg-dark">
                <div class="container-fluid d-flex">
                    <asp:Button CssClass="btn btn-outline-info me-2" type="button" runat="server" id="btnCatdepartamento" onclick="btnCatDepartamento_Click" Text="catDepartamento"></asp:Button>
                    <asp:Button CssClass="btn btn-outline-info me-2" type="button" runat="server" id="btnEmpleado" onclick="btnEmpleado_Click" Text="Empleado"></asp:Button>
                    <asp:Button CssClass="btn btn-outline-info me-2" type="button" runat="server" id="btnEquipo" onclick="btnEquipo_Click" Text="Equipo"></asp:Button>
                    <asp:Button CssClass="btn btn-outline-info me-2" type="button" runat="server" id="btnSalir" onclick="btnSalir_Click" Text="Salir"></asp:Button>

                </div>
            </nav>
           <br />
        <br />
        <br />
        <br />
        <br />
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
        <div class="container-fluid" style="font-family: Arial; font-size: medium; font-weight: bold">
            <div class="row">
                <div class="col-md-7 ms-12 m-auto text-center">
                    Mantenimientos
                </div>
                <div class="col-md-7 ms-12 m-auto text-center">
                    <asp:Label ID="lblAccion" runat="server" Text="Accion" Font-Bold="True"></asp:Label>

                </div>
                
                
            </div>
            <div class="col d-flex">
            <div class="input-group">
                    <asp:Label ID="lblMantenimientos" CssClass="input-group-text" runat="server" Text="Mantenimientos:"></asp:Label>

                <asp:Label ID="txtMantenimientos" CssClass="input-group-text" runat="server" Text="Mantenimientos"></asp:Label>
                </div>
            
            <div>
                <asp:Button ID="btnAgregar" class="btn btn-success" runat="server" Text="Agregar" OnClick="btnAgregar_Click" ViewStateMode="Disabled" />

            <asp:Button ID="btnRegresar" CssClass="btn btn-danger" runat="server" OnClick="btnRegresar_Click" Text="Regresar" />
            </div>
        </div>
        
            
           
            <div></div>
        </div>
        <div>

            <asp:GridView ID="dgvMantenimientos" runat="server" AutoGenerateColumns="False"
                DataSourceID="LinqDataSourceDireccion" Width="1062px" BackColor="White"
                BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                GridLines="Horizontal" OnRowCommand="dgvMantenimientos_RowCommand">
                <AlternatingRowStyle BackColor="#F7F7F7" />
                <Columns>
                    <asp:BoundField DataField="id" HeaderText="id" ReadOnly="True"
                        SortExpression="id" Visible="False" />
                    <asp:BoundField DataField="dteFechaMantenimiento" HeaderText="Fecha Mantenimiento" ReadOnly="True"
                        SortExpression="dteFechaMantenimiento" />
                    <asp:BoundField DataField="strObservaciones" HeaderText="Observaciones" ReadOnly="True"
                        SortExpression="strObservaciones" />
                    <asp:TemplateField HeaderText="Editar">
                        <ItemTemplate>
                            <asp:ImageButton runat="server" ID="imgEditar" CommandName="Editar" CommandArgument='<%#Bind("id") %>' ImageUrl="~/Images/editrecord_16x16.png" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="50px" />

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Eliminar" Visible="True">
                        <ItemTemplate>
                            <asp:ImageButton runat="server" ID="imgEliminar" CommandName="Eliminar" CommandArgument='<%#Bind("id") %>' ImageUrl="~/Images/delrecord_16x16.png" OnClientClick="javascript:return confirm('¿Está seguro de querer eliminar el registro seleccionado?', 'Mensaje de sistema')" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                <SortedAscendingCellStyle BackColor="#F4F4FD" />
                <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                <SortedDescendingCellStyle BackColor="#D8D8F0" />
                <SortedDescendingHeaderStyle BackColor="#3E3277" />
            </asp:GridView>

        </div>
        <div>

            <asp:LinqDataSource ID="LinqDataSourceDireccion" runat="server"
                ContextTypeName="UTTT.Ejemplo.Linq.Data.Entity.DcGeneralDataContext"
                EntityTypeName="" Select="new (id, dteFechaMantenimiento, strObservaciones)"
                TableName="Mantenimientos" OnSelecting="LinqDataSourceMantenimientos_Selecting">
            </asp:LinqDataSource>

        </div>
        </div>
        
    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-MrcW6ZMFYlzcLA8Nl+NtUVF0sA7MsXsP1UyJoMp4YLEuNSfAP+JcXn/tWtIaxVXM" crossorigin="anonymous"></script>
</body>
</html>
