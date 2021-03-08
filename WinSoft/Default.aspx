<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WinSoft._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnSaveDepartment").click(function () {
                debugger;
                var deptid = $("#" + '<%= hdnDeptId.ClientID %>').val();
                var code = $("#" + '<%= txtCode.ClientID %>').val();
                var deptname = $("#" + '<%= txtDepartment.ClientID %>').val();

                jsonText = "{'DeptId':'" + deptid + "','Code':'" + code + "','Department':'" + deptname + "'}"

                $.ajax({
                    type: "POST",
                    url: "Default.aspx/SaveDepartment",
                    contentType: "application/json; charset=utf-8",
                    data: jsonText,
                    dataType: "json",
                    success: function (response) {
                        debugger;
                        $("#" + '<%= hdnDeptId.ClientID %>').val('0');
                        $("#" + '<%= txtCode.ClientID %>').val('');
                        $("#" + '<%= txtDepartment.ClientID %>').val('');
                        alert("Saved Successfully");
                        $("#" + '<%= divDeptSave.ClientID %>').css("display", "none");
                    },
                    error: function () {
                        console.log('There is some error');
                    }
                });
            });
        });
    </script>
    <div class="row">
        <div class="col-md-12">
            <h3>Departments</h3>
            <hr />
        </div>
        <div class="row">
            <div class="col-md-6">
                <asp:GridView ID="gvDepartments" runat="server" Width="100%" AutoGenerateColumns="false" DataKeyNames="DeptId">
                    <Columns>
                        <asp:BoundField HeaderText="Code" DataField="Code" ItemStyle-Width="20%">
                            <ItemStyle Width="20%"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Department">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbnDepartment" runat="server" Text='<%# Eval("Department") %>' OnClick="lbnDepartment_Click" />
                            </ItemTemplate>
                            <ItemStyle Width="80%" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:HiddenField ID="hdnDeptId" runat="server" Value="0" />
            </div>
            <div class="col-md-6">
                <asp:GridView ID="gvEmployees" runat="server" Width="100%" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField HeaderText="Code" DataField="Code" />
                        <asp:BoundField HeaderText="Names" DataField="Name" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="row" id="divDeptSave" runat="server" visible="false">
            <div class="col-md-6">
                <div class="row">
                    <div class="col-md-4">Code:</div>
                    <div class="col-md-8">
                        <asp:TextBox ID="txtCode" runat="server" Width="95%" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">Department:</div>
                    <div class="col-md-8">
                        <asp:TextBox ID="txtDepartment" runat="server" Width="95%" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-8">
                    </div>
                    <div class="col-md-4">
                        <%--<asp:Button ID="btnSaveDepartment" runat="server" Text="Save" OnClick="btnSaveDepartment_Click" />--%>
                        <input type="button" id="btnSaveDepartment" value="Save" />
                        <asp:Button ID="btnClear" runat="server" Text="Cancel" OnClick="btnClear_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
