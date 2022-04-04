<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="UTTT.Ejemplo.Persona.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous"/>
     <script type="text/javascript">
        function soloLetras(e) {
            var key = e.keyCode || e.which,
                tecla = String.fromCharCode(key).toLowerCase(),
                letras = "áéíóúabcdefghijklmnñopqrstuvwxyz1234567890",
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
        function validaNumeros(evt) {
            //Valida que solo se ingresen numeros a la caja de texto
            var code = (evt.which) ? evt.which : evt.keycode;
            if (code == 8) {
                return true;
            }
            else if (code >= 48 && code <= 57) {
                return true;
            }
            else {
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
                  Login
              </div>
              <div class="col-md-7 ms-12 m-auto text-center">
                    <div class="input-group">
                        <label class="input-group-text">Usuario:</label>
                        <asp:TextBox CssClass="form-control w-100" ID="txtUsuario" runat="server" Width="250px" ViewStateMode="Disabled" onkeypress="return soloLetras(event);"></asp:TextBox>

                        <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" ControlToValidate="txtUsuario" ErrorMessage="*Usuario es obligatorio" ValidationGroup="vgLogin"></asp:RequiredFieldValidator>
          </div>
      </div>

              <div class="col-md-7 ms-12 m-auto text-center">
                    <div class="input-group">
                        <label class="input-group-text">Password:</label>
                        <input type="password" class="form-control w-100" id="txtPassword" runat="server" width="250px" viewstatemode="Disabled" onkeypress="return soloLetras(event);"/>

                        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="*Password es obligatorio" ValidationGroup="vgLogin"></asp:RequiredFieldValidator>

              </div>
          </div>
              <div class="col-md-7 ms-12 m-auto text-center">
            <asp:Button class="btn btn-success w-50 p-1" ID="btnLogin" runat="server" Text="Login"
                OnClick="btnLogin_Click" ViewStateMode="Disabled" />
                  </div>
              <div class="col-md-7 ms-12 m-auto text-center">
            <asp:Button class="btn btn-info w-25 p-1" ID="btnRecuperarPass" runat="server" Text="Recuperar contraseña"
                OnClick="btnRecuperar_Click" ViewStateMode="Disabled" />
                  <asp:Button class="btn btn-warning w-25 p-1" ID="btnRegistrar" runat="server" Text="Registrar"
                OnClick="btnRegistrar_Click" ViewStateMode="Disabled" />
        </div>
              </div>
          </div>
              <asp:Label ID="lblMensaje" runat="server" BorderColor="Red" Visible="False" ForeColor="Red" Text="color"></asp:Label>
    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-MrcW6ZMFYlzcLA8Nl+NtUVF0sA7MsXsP1UyJoMp4YLEuNSfAP+JcXn/tWtIaxVXM" crossorigin="anonymous"></script>
</body>
</html>
