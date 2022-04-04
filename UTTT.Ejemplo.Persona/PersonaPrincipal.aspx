<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonaPrincipal.aspx.cs" Inherits="UTTT.Ejemplo.Persona.PersonaPrincipal" Debug="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.3.0/font/bootstrap-icons.css"/>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous" />
</head>
<body>
    
    
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
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
        
        <div style="color: #000000; font-size: medium; font-family: Arial; font-weight: bold" class="container-fluid">
            <div class="row">
                <div class="col">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    Persona</div>
            </div>
        </div>
        <div class="container-fluid">
            <div class="row">
                <div class="col-4">
                        <div class="input-group">
                            <label class="input-group-text">Nombre:</label>

                            <asp:TextBox CssClass="form-control" ID="txtNombre" runat="server" Width="174px"
                                OnTextChanged="buscarTextBox" AutoPostBack="true"></asp:TextBox>
                        </div>
                         </div>
                <div class="col-2">
                        <asp:Button ID="btnBuscar" class="btn btn-primary p-1 w-100" runat="server" Text="Buscar"
                            OnClick="btnBuscar_Click" ViewStateMode="Disabled" />
                        &nbsp;&nbsp;&nbsp;
               </div>
        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionInterval="100" EnableCaching="false"
            MinimumPrefixLength="2" ServiceMethod="txtNombre_TextChanged" TargetControlID="txtNombre">
        </ajaxToolkit:AutoCompleteExtender>
                <div class="col-2">
                        <asp:Button ID="btnAgregar" class="btn btn-success p-1 w-100" runat="server" Text="Agregar"
                            OnClick="btnAgregar_Click" ViewStateMode="Disabled" />
                    </div>
               
            </div>
        </div>
        <br />
        <div class="container-fluid">
            <div class="row">
                <div class="col">
                    Sexo:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="ddlSexo" CssClass="dropdown" runat="server" Height="22px" Width="177px">
        </asp:DropDownList>

                </div>
            </div>
        </div>
        <div style="font-weight: bold" class="container-fluid">
            <div class="row">
                <div class="col">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Detalle
                </div>
            </div>
        </div>

        <div>
        </div>
        <div class="container-fluid">
            <div class="row">
                <div class="col">
                    <div class="table-responsive">

            <asp:GridView ID="dgvPersonas" runat="server"
                AllowPaging="True" AutoGenerateColumns="False" DataSourceID="DataSourcePersona"
                Width="1067px" CellPadding="3" GridLines="Horizontal"
                OnRowCommand="dgvPersonas_RowCommand" BackColor="White"
                BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px"
                ViewStateMode="Disabled">
                <AlternatingRowStyle BackColor="#F7F7F7" />
                <Columns>
                    <asp:BoundField DataField="id" HeaderText="Id"
                        ReadOnly="True" SortExpression="id" />
                    <asp:BoundField DataField="strClaveUnica" HeaderText="Clave Unica"
                        ReadOnly="True" SortExpression="strClaveUnica" />
                    <asp:BoundField DataField="strNombre" HeaderText="Nombre" ReadOnly="True"
                        SortExpression="strNombre" />
                    <asp:BoundField DataField="strAPaterno" HeaderText="APaterno" ReadOnly="True"
                        SortExpression="strAPaterno" />
                    <asp:BoundField DataField="strAMaterno" HeaderText="AMaterno" ReadOnly="True"
                        SortExpression="strAMaterno" />
                    <asp:BoundField DataField="CatSexo" HeaderText="Sexo"
                        SortExpression="CatSexo" />
                    <asp:BoundField DataField="dteFechaNacimiento" HeaderText="Fecha de Nacimiento"
                        SortExpression="dteFechaNacimiento" />
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

                    <%--<asp:TemplateField HeaderText="Direccion">
                        <ItemTemplate>
                            <asp:ImageButton runat="server" ID="imgDireccion" CommandName="Direccion" CommandArgument='<%#Bind("id") %>' ImageUrl="~/Images/editrecord_16x16.png" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="50px" />

                    </asp:TemplateField>--%>
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
        </div>
        
        
        <asp:LinqDataSource ID="DataSourcePersona" runat="server"
            ContextTypeName="UTTT.Ejemplo.Linq.Data.Entity.DcGeneralDataContext"
            OnSelecting="DataSourcePersona_Selecting"
            Select="new (id,strNombre, strAPaterno, strAMaterno, CatSexo, strClaveUnica,id,dteFechaNacimiento)"
            TableName="Persona" EntityTypeName="">
        </asp:LinqDataSource>
    </form>
    

        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-MrcW6ZMFYlzcLA8Nl+NtUVF0sA7MsXsP1UyJoMp4YLEuNSfAP+JcXn/tWtIaxVXM" crossorigin="anonymous"></script>

 
</body>
</html>
