<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EquipoPrincipal.aspx.cs" Inherits="UTTT.Ejemplo.Persona.EquipoPrincipal" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <nav class="navbar fixed-top navbar-dark bg-dark">
                <div class="container-fluid">
                    <asp:Button CssClass="btn btn-outline-info me-2" type="button" runat="server" id="btnCatdepartamento" onclick="btnCatDepartamento_Click" Text="catDepartamento"></asp:Button>
                    <asp:Button CssClass="btn btn-outline-info me-2" type="button" runat="server" id="btnEmpleado" onclick="btnEmpleado_Click" Text="Empleado"></asp:Button>
                    <asp:Button CssClass="btn btn-outline-info me-2" type="button" runat="server" id="btnEquipo" onclick="btnEquipo_Click" Text="Equipo"></asp:Button>
                    <form class="d-flex">
                        <asp:HyperLink runat="server" Cssclass="navbar-brand justify-content-end" id="btnSalir" href="#" onclick="btnSalir_Click">
                            <img src="Images/logout-blue.png" alt="" width="30" height="24" />
                            Salir
                        </asp:HyperLink>
                    </form>

                </div>
            </nav>
           
        </div>
         <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
         <div style="color: #000000; font-size: medium; font-family: Arial; font-weight: bold" class="container-fluid">
            <div class="row">
                <div class="col-md-7 ms-12 m-auto text-center">Equipo</div>
            </div>
        </div>
        <div class="container-fluid">
            <div class="row">
                <div class="col-4">
                        <div class="input-group">
                            <label class="input-group-text">Equipo:</label>

                            <asp:TextBox CssClass="form-control" ID="txtEquipo" runat="server" Width="174px"
                                OnTextChanged="buscarTextBox" AutoPostBack="true"></asp:TextBox>
                        </div>
                         </div>
                <div class="col-2">
                        <asp:Button ID="btnBuscar" class="btn btn-primary p-1 w-100" runat="server" Text="Buscar"
                            OnClick="btnBuscar_Click" ViewStateMode="Disabled" />
                        &nbsp;&nbsp;&nbsp;
               </div>
        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionInterval="100" EnableCaching="false"
            MinimumPrefixLength="2" ServiceMethod="txtNombre_TextChanged" TargetControlID="txtEquipo">
        </ajaxToolkit:AutoCompleteExtender>
                <div class="col-2">
                        <asp:Button ID="btnAgregar" class="btn btn-success p-1 w-100" runat="server" Text="Agregar"
                            OnClick="btnAgregar_Click" ViewStateMode="Disabled" />
                    </div>
               
            </div>
        </div>
        <div>
        </div>
        <div class="container-fluid">
            <div class="row">
                <div class="col">
                    <div class="table-responsive">

            <asp:GridView ID="dgvEquipo" runat="server"
                AllowPaging="True" AutoGenerateColumns="False" DataSourceID="DataSourcePersona"
                Width="1067px" CellPadding="3" GridLines="Horizontal"
                OnRowCommand="dgvEquipo_RowCommand" BackColor="White"
                BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px"
                ViewStateMode="Disabled">
                <AlternatingRowStyle BackColor="#F7F7F7" />
                <Columns>
                    <asp:BoundField DataField="id" HeaderText="ID"
                        ReadOnly="True" SortExpression="id" />
                    <asp:BoundField DataField="strNombre" HeaderText="Nombre" ReadOnly="True"
                        SortExpression="strNombre" />
                    <asp:BoundField DataField="strDescripcion" HeaderText="Descripcion" ReadOnly="True"
                        SortExpression="strDescripcion" />
                    <asp:BoundField DataField="dteFechaCompra" HeaderText="Fecha de Compra" ReadOnly="True"
                        SortExpression="dteFechaCompra" />
                    <asp:BoundField DataField="catDepartamento" HeaderText="Departamento"
                        SortExpression="catDepartamento" />
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

                    <asp:TemplateField HeaderText="Mantenimientos">
                        <ItemTemplate>
                            <asp:ImageButton runat="server" ID="imgMantenimientos" CommandName="Mantenimientos" CommandArgument='<%#Bind("id") %>' ImageUrl="~/Images/editrecord_16x16.png" />
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
                </div>
            </div>
        </div>
        <asp:LinqDataSource ID="DataSourcePersona" runat="server"
            ContextTypeName="UTTT.Ejemplo.Linq.Data.Entity.DcGeneralDataContext"
            OnSelecting="DataSourcePersona_Selecting"
            Select="new (id,strNombre, strDescripcion, dteFechaCompra, catDepartamento)"
            TableName="Persona" EntityTypeName="">
        </asp:LinqDataSource>
    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-MrcW6ZMFYlzcLA8Nl+NtUVF0sA7MsXsP1UyJoMp4YLEuNSfAP+JcXn/tWtIaxVXM" crossorigin="anonymous"></script>
</body>
</html>
