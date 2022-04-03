<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registrar.aspx.cs" Inherits="UTTT.Ejemplo.Persona.Registrar" %>
<%@ Register Assembly ="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous"/>

    <title></title>
</head>
<body>
    <form id="form1" runat="server">
         <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
        <div class="container-fluid">
          <div class="row">
              <div class="col-md-7 ms-12 m-auto text-center">
                  Registar
              </div>
              <div class="col-md-7 ms-12 m-auto text-center">
                    <div class="input-group">
                        <label class="input-group-text">Id empleado:</label>
                        <asp:TextBox CssClass="form-control w-100" ID="txtIdEmpleado" runat="server" Width="250px" ViewStateMode="Disabled"></asp:TextBox>

                        <asp:RequiredFieldValidator ID="rfvIdEmp" runat="server" ControlToValidate="txtUsuario" ErrorMessage="*Id empleado es obligatorio" ValidationGroup="vgReg"></asp:RequiredFieldValidator>
          </div>
      </div>
              <div class="col-md-7 ms-12 m-auto text-center">
                    <div class="input-group">
                        <label class="input-group-text">Usuario:</label>
                        <asp:TextBox CssClass="form-control w-100" ID="txtUsuario" runat="server" Width="250px" ViewStateMode="Disabled"></asp:TextBox>

                        <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" ControlToValidate="txtUsuario" ErrorMessage="*Usuario es obligatorio" ValidationGroup="vgReg"></asp:RequiredFieldValidator>
          </div>
      </div>

              <div class="col-md-7 ms-12 m-auto text-center">
                    <div class="input-group">
                        <label class="input-group-text">Password:</label>
                        <input type="password" class="form-control w-100" id="txtPassword" runat="server" width="250px" viewstatemode="Disabled"/>

                        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="*Password es obligatorio" ValidationGroup="vgReg"></asp:RequiredFieldValidator>

              </div>
                  <div class="input-group">
                        <label class="input-group-text">Confirmar Password:</label>
                        <input type="password" class="form-control w-100" id="txtConfPassword" runat="server" width="250px" viewstatemode="Disabled"/>

                        <asp:RequiredFieldValidator ID="rfvPassword1" runat="server" ControlToValidate="txtConfPassword" ErrorMessage="*Confirmar Password es obligatorio" ValidationGroup="vgReg"></asp:RequiredFieldValidator>

              </div>
          </div>
              <div class="col-md-7 ms-12 m-auto text-center">
                                            <label class="col-form-label">Estado:</label>
                                            <br />
              <asp:UpdatePanel ID="UpdatePanel1" runat="server">

                    <ContentTemplate>
                        <asp:DropDownList CssClass="form-select w-100" ID="ddlEstado" runat="server"
                            Width="250px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvEstado" runat="server" ControlToValidate="ddlEstado" ErrorMessage="*Selecciona un estado " InitialValue="-1" ValidationGroup="vgReg"></asp:RequiredFieldValidator>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlEstado" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>

              <div class="col-md-7 ms-12 m-auto text-center">
            <asp:Button class="btn btn-success w-50 p-1" ID="btnConfirmar" runat="server" Text="Confirmar"
                OnClick="btnConfirmar_Click" ViewStateMode="Disabled" />
                  </div>
              <div class="col-md-7 ms-12 m-auto text-center">
                  <asp:Button class="btn btn-warning w-50 p-1" ID="btnCancelar" runat="server" Text="Cancelar"
                OnClick="btnCancelar_Click" ViewStateMode="Disabled" />
        </div>
              </div>
          </div>
        <asp:CompareValidator ID="comContras" Enabled="false" runat="server" ControlToCompare="txtPassword" ControlToValidate="txtConfPassword" ErrorMessage="No son iguales las contraseñas"></asp:CompareValidator>
              <asp:Label ID="lblMensaje" runat="server" BorderColor="Red" Visible="False" ForeColor="Red"></asp:Label>
        <div>
        </div>
    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-MrcW6ZMFYlzcLA8Nl+NtUVF0sA7MsXsP1UyJoMp4YLEuNSfAP+JcXn/tWtIaxVXM" crossorigin="anonymous"></script>
</body>
</html>
