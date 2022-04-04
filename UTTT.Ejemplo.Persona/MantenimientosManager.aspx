<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MantenimientosManager.aspx.cs" Inherits="UTTT.Ejemplo.Persona.MantenimientosManager" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous" />
    <script type="text/javascript">
        function validaLetras(e) {
            //Valida que solo se ingresen letras y algunos caracteres especiales
            key = e.keycode || e.which;
            tecla = String.fromCharCode(key).toLowerCase();
            letras = " áéíóúabcdefghijklmnñopqrstuvwxyz1234567890";
            especiales = "8-37-39-45-46";
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
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-7 ms-12 m-auto text-center" style="font-family: Arial; font-size: medium; font-weight: bold">
                    Dirección
                </div>
                <div class="col-md-7 ms-12 m-auto text-center">
                    <asp:Label ID="lblAccion" runat="server" Text="Accion" Font-Bold="True"></asp:Label>

                </div>


            </div>
            <div class="col-md-7 ms-12 m-auto text-center">
                <div class="input-group">
                    <label class="input-group-text">Fecha mantenimiento:</label>

                    <asp:TextBox CssClass="form-control" Width="90%" ID="txtFechaMantenimiento" runat="server"></asp:TextBox>
                    <asp:ImageButton CssClass="mt-1" ID="imgPopup" ImageUrl="~/Images/images.jpg" ImageAlign="Bottom" runat="server" CausesValidation="false" Height="33px" Width="42px" />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopup" runat="server" TargetControlID="txtFechaMantenimiento" Format="dd/MM/yyyy" />
                    <asp:RequiredFieldValidator ID="rfvFechaMantenimiento" runat="server" ControlToValidate="txtFechaMantenimiento" ErrorMessage="*Campo obligatorio" ValidationGroup="vgMant"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-md-7 ms-12 m-auto text-center">
                <div class="input-group">
                    <label class="input-group-text">Observaciones:</label>
                    <asp:TextBox CssClass="form-control w-100" ID="txtObservaciones" runat="server" Width="250px" ViewStateMode="Disabled"
                        onkeypress="return validaLetras(event);"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="rfvObservaciones" runat="server" ControlToValidate="txtObservaciones" ErrorMessage="*Observaciones es obligatorio" ValidationGroup="vgMant"></asp:RequiredFieldValidator>

                    <asp:RegularExpressionValidator ID="revObservaciones" runat="server" ControlToValidate="txtObservaciones" ErrorMessage="*Formato invalido" ValidationExpression="^[a-zA-Z0-9\-À-ÿ\u00f1\u00d1]+(\s*[a-zA-ZÀ-ÿ\u00f1\u00d1]*)*[a-zA-ZÀ-ÿ\u00f1\u00d1]+$" ValidationGroup="vgMant"></asp:RegularExpressionValidator>
                </div>
            </div>



            <div class="col-md-7 ms-12 m-auto text-center">
                <asp:Button class="btn btn-info w-100 p-1" ID="btnAceptar" runat="server" Text="Aceptar"
                    OnClick="btnAceptar_Click" ViewStateMode="Disabled" />
                <asp:Button class="btn btn-danger w-100 p-1" ID="btnCancelar" runat="server" Text="Cancelar"
                    OnClick="btnCancelar_Click" ViewStateMode="Disabled" />
            </div>

            <br />
            <div class="container-fluid">
                <div class="row">
                    <div class="col">
                        <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                    </div>
                </div>
            </div>
            <div>
            </div>
        </div>
        
    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-MrcW6ZMFYlzcLA8Nl+NtUVF0sA7MsXsP1UyJoMp4YLEuNSfAP+JcXn/tWtIaxVXM" crossorigin="anonymous"></script>
</body>
</html>
