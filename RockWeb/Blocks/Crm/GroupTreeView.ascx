<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GroupTreeView.ascx.cs" Inherits="RockWeb.Blocks.Crm.GroupTreeView" %>

<asp:UpdatePanel ID="upGroupType" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
    <ContentTemplate>
        <asp:HiddenField ID="hfRootGroupId" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hfLimitToSecurityRoleGroups" runat="server" ClientIDMode="Static" />
        <div class="treeview-back">
            <h3>
                <asp:Literal ID="ltlTreeViewTitle" runat="server" /></h3>
            <div id="treeviewGroups" class="groupTreeview">
            </div>
        </div>
        <script>
            function onSelect(e) {
                var groupId = this.dataItem(e.node).id;
                showGroupDetails(groupId);
            }

            function getInitialGroupIdFromHash() {
                debugger 
                var hashValues = location.hash.match(new RegExp('groupId=([^&]*)'));
                if (hashValues) {
                    return hashValues[1];
                }
                return null;
            }

            function showGroupDetails(groupId) {
                __doPostBack('<%= upGroupType.ClientID %>', 'groupId=' + groupId);
            }

            function onDataBound(e) {
                // automatically select the first item or initial item in the treeview if there isn't one currently selected
                var treeViewData = $('.groupTreeview').data("kendoTreeView");
                var selectedNode = treeViewData.select();
                var nodeData = this.dataItem(selectedNode);
                if (!nodeData) {
                    var initialGroupId = getInitialGroupIdFromHash();
                    var initialGroupItem = this.dataSource.get(initialGroupId);
                    debugger
                    var firstItem = null;
                    if (initialGroupId) {
                        if (initialGroupItem) {
                            firstItem = treeViewData.findByUid(initialGroupItem.uid);
                        }
                    }
                    else {
                        firstItem = treeViewData.root[0].firstChild;
                    }
                    var firstDataItem = this.dataItem(firstItem);
                    if (firstDataItem) {
                        treeViewData.select(firstItem);
                        showGroupDetails(firstDataItem.id);
                    }

                }
            }

            var restUrl = "<%=ResolveUrl( "~/api/groups/getchildren/" ) %>";
            var rootGroupId = $('#hfRootGroupId').val();
            var limitToSecurityRoleGroups = $('#hfLimitToSecurityRoleGroups').val();

            var groupList = new kendo.data.HierarchicalDataSource({
                transport: {
                    read: {
                        url: function (options) {
                            var requestUrl = restUrl + (options.id || 0) + '/' + (rootGroupId || 0) + '/' + (limitToSecurityRoleGroups || false)
                            return requestUrl;
                        }
                    }
                },
                schema: {
                    model: {
                        id: 'Id',
                        hasChildren: 'HasChildren'
                    }
                }
            });

            $('.groupTreeview').kendoTreeView({
                template: "<i class='#= item.GroupTypeIconCssClass #'></i> #= item.Name #",
                dataSource: groupList,
                loadOnDemand: true,
                dataTextField: 'Name',
                dataImageUrlField: 'GroupTypeIconSmallUrl',
                select: onSelect,
                dataBound: onDataBound
            });

            if (getInitialGroupIdFromHash()) {
                var treeViewData = $('.groupTreeview').data("kendoTreeView");
                treeViewData.expand(".k-item");
            }
        </script>
    </ContentTemplate>
</asp:UpdatePanel>
