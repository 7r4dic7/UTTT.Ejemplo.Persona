<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartamentosManager.aspx.cs" Inherits="UTTT.Ejemplo.Persona.Departamentomanager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript">
        function validaLetras(e) {
            //Valida que solo se ingresen letras y algunos caracteres especiales
            key = e.keycode || e.which;
            tecla = String.fromCharCode(key).toLowerCase();
            letras = " áéíóúabcdefghijklmnñopqrstuvwxyz";
            especiales = "8-37-39-46";
            tecla_especial = false;
            for (var i in especiales) {
                if (key == especiales[i]) {
                    tecla_especial = true;
                    break;
                }
            }
            if (letras.indexOf(tecla) == -1 && !tecla_especial) {
                return false;
            }
        }
    </script>
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
        <div class="container-fluid justify-content-center" style="font-family: Arial; font-size: medium; font-weight: bold">
            <div class="row">
                <div class="col-md-7 ms-12 m-auto text-center">
                    <h1><strong>Departamento</strong></h1>
                </div>
            </div>
        </div>

        <div class="container-fluid">
            <div class="row">
                <div class="col-md-7 ms-12 m-auto text-center">
                    <asp:Label ID="lblAccion" runat="server" Text="Accion" Font-Bold="True"></asp:Label>
                </div>
            </div>
        </div>

        <div>
        </div>

        <div class="container-fluid">
            <div class="row">
                <div class="col-md-7 ms-12 m-auto text-center">
                    <div class="input-group">
                        <label class="input-group-text">Valor:</label>
                        <asp:TextBox CssClass="form-control w-100" ID="txtValor" runat="server"
                            Width="250px" ViewStateMode="Disabled" onkeypress="return validaLetras(event);"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvValor" runat="server" ControlToValidate="txtValor" ErrorMessage="*Valor es obligatorio" ValidationGroup="vgCatdepartamento"></asp:RequiredFieldValidator>

                        <asp:RegularExpressionValidator ID="revValor" runat="server" ControlToValidate="txtValor" ErrorMessage="*Formato invalido" ValidationExpression="^[a-zA-ZÀ-ÿ\u00f1\u00d1]+(\s*[a-zA-ZÀ-ÿ\u00f1\u00d1]*)*[a-zA-ZÀ-ÿ\u00f1\u00d1]+$" ValidationGroup="vgCatdepartamento"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="col-md-7 ms-12 m-auto text-center">
                    <div class="input-group">
                        <label class="input-group-text">Descripcion:</label>
                        <asp:TextBox CssClass="form-control w-100" ID="txtDescripcion" runat="server" Width="250px" ViewStateMode="Disabled"
                            onkeypress="return validaLetras(event);"></asp:TextBox>

                        <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ControlToValidate="txtDescripcion" ErrorMessage="*Descripcion es obligatoria" ValidationGroup="vgCatdepartamento"></asp:RequiredFieldValidator>

                        <asp:RegularExpressionValidator ID="revDescripcion" runat="server" ControlToValidate="txtDescripcion" ErrorMessage="*Formato invalido" ValidationExpression="^[a-zA-ZÀ-ÿ\u00f1\u00d1]+(\s*[a-zA-ZÀ-ÿ\u00f1\u00d1]*)*[a-zA-ZÀ-ÿ\u00f1\u00d1]+$" ValidationGroup="vgCatdepartamento"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="col-md-7 ms-12 m-auto text-center">
                    <asp:Button class="btn btn-info w-100 p-1" ID="btnAceptar" runat="server" Text="Aceptar"
                        OnClick="btnAceptar_Click" ViewStateMode="Disabled" />
                    <asp:Button class="btn btn-danger w-100 p-1" ID="btnCancelar" runat="server" Text="Cancelar"
                        OnClick="btnCancelar_Click" ViewStateMode="Disabled" />
                </div>

                <br />

            </div>
        </div>
        <div class="container-fluid">
            <div class="row">
                <div class="col">
                    <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                </div>
            </div>
        </div>
    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-MrcW6ZMFYlzcLA8Nl+NtUVF0sA7MsXsP1UyJoMp4YLEuNSfAP+JcXn/tWtIaxVXM" crossorigin="anonymous"></script>
</body>
</html>
