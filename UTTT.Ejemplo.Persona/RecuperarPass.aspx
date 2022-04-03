<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecuperarPass.aspx.cs" Inherits="UTTT.Ejemplo.Persona.RecuperarPass" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous"/>
    <script type="text/javascript">
        function soloLetras(e) {
            var key = e.keyCode || e.which,
                tecla = String.fromCharCode(key).toLowerCase(),
                letras = " áéíóúabcdefghijklmnñopqrstuvwxyz1234567890",
                especiales = [8, 37, 39, 46],
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
        <div class="container-fluid">
          <div class="row">
              <div class="col-md-7 ms-12 m-auto text-center">
                  Recuperar Contraseña
              </div>
              <div class="col-md-7 ms-12 m-auto text-center">
                    <div class="input-group">
                        <asp:label runat="server" class="input-group-text">Usuario:</asp:label>
                        <asp:TextBox CssClass="form-control w-100" ID="txtUsuario" runat="server" Width="250px" ViewStateMode="Disabled" ReadOnly="false"></asp:TextBox>

                        <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" ControlToValidate="txtUsuario" ErrorMessage="*Usuario es obligatorio" ValidationGroup="vgRecP"></asp:RequiredFieldValidator>
          </div>
      </div>

              <div class="col-md-7 ms-12 m-auto text-center">
                    <div class="input-group" id="igPass">
                        <label class="input-group-text" id="lblPass" visible="false">Password:</label>
                        <asp:TextBox type="password" class="form-control w-100" id="txtPassword" runat="server" width="250px" viewstatemode="Disabled" visible="false" onkeypress="return soloLetras(event);"/>

                        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="*Password es obligatorio" ValidationGroup="vgRecP"></asp:RequiredFieldValidator>

              </div>
                  <div class="input-group" id="igPass1">
                        <label class="input-group-text" id="lblPass1" visible="false">Confirmar Password:</label>
                        <asp:TextBox TextMode="password" class="form-control w-100" id="txtConfPassword" runat="server" width="250px" viewstatemode="Disabled" visible="false" onkeypress="return soloLetras(event);"/>

                        <asp:RequiredFieldValidator ID="rfvPassword1" runat="server" ControlToValidate="txtConfPassword" ErrorMessage="*Confirmar Password es obligatorio" ValidationGroup="vgRecP"></asp:RequiredFieldValidator>

              </div>
          </div>
              <div class="col-md-7 ms-12 m-auto text-center">
            <asp:Button class="btn btn-success w-50 p-1" ID="btnVerificar" runat="server" Text="Verificar"
                OnClick="btnVerificar_Click" ViewStateMode="Disabled" />
                  </div>
              <div class="col-md-7 ms-12 m-auto text-center">
                  <asp:Button class="btn btn-warning w-50 p-1" ID="btnCancelar" runat="server" Text="Cancelar"
                OnClick="btnCancelar_Click" ViewStateMode="Disabled" />
        </div>
              </div>
          </div>
        <asp:CompareValidator ID="comContras" Enabled="false" runat="server" ControlToCompare="txtPassword" ControlToValidate="txtConfPassword" ErrorMessage="No son iguales las contraseñas"></asp:CompareValidator>
              <asp:Label ID="lblMensaje" runat="server" BorderColor="Red" Visible="False" ForeColor="Red" Text="color"></asp:Label>
    </form>
    
        <div>
        </div>
   
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-MrcW6ZMFYlzcLA8Nl+NtUVF0sA7MsXsP1UyJoMp4YLEuNSfAP+JcXn/tWtIaxVXM" crossorigin="anonymous"></script>
</body>
</html>
