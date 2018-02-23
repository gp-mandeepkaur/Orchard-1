using Orchard.UI.Resources;

namespace PI.Party
{
    public class ResourceManifest : IResourceManifestProvider
    {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();
            string basePath = "~/Modules/PI.Common/";
            //Javascript resources 
            manifest.DefineScript("jQuery").SetUrl("Scripts/jquery-2.1.1.min.js").SetBasePath(basePath);
            manifest.DefineScript("jQxCore").SetUrl("Scripts/jqwidgets/jqxcore.js").SetBasePath(basePath);
            manifest.DefineScript("jQxWindow").SetUrl("Scripts/jqwidgets/jqxwindow.js").SetBasePath(basePath);
            manifest.DefineScript("jQxLoader").SetUrl("Scripts/jqwidgets/jqxloader.js").SetBasePath(basePath);
            manifest.DefineScript("jQxDropDown").SetUrl("Scripts/jqwidgets/jqxdropdownlist.js").SetBasePath(basePath);
            manifest.DefineScript("jQxListBox").SetUrl("Scripts/jqwidgets/jqxlistbox.js").SetBasePath(basePath);
            manifest.DefineScript("jQxButtons").SetUrl("Scripts/jqwidgets/jqxbuttons.js").SetBasePath(basePath);
            manifest.DefineScript("jQxScrollBar").SetUrl("Scripts/jqwidgets/jqxscrollbar.js").SetBasePath(basePath);
            manifest.DefineScript("jQxDateTimeInput").SetUrl("Scripts/jqwidgets/jqxdatetimeinput.js").SetBasePath(basePath);
            manifest.DefineScript("jQxNumberInput").SetUrl("Scripts/jqwidgets/jqxnumberinput.js").SetBasePath(basePath);
            manifest.DefineScript("jQxInput").SetUrl("Scripts/jqwidgets/jqxinput.js").SetBasePath(basePath);
            manifest.DefineScript("jQxCalendar").SetUrl("Scripts/jqwidgets/jqxcalendar.js").SetBasePath(basePath);
            manifest.DefineScript("jQxToolTip").SetUrl("Scripts/jqwidgets/jqxtooltip.js").SetBasePath(basePath);
            manifest.DefineScript("jQxGrid").SetUrl("Scripts/jqwidgets/jqxgrid.js").SetBasePath(basePath);
            manifest.DefineScript("jQxGridEdit").SetUrl("Scripts/jqwidgets/jqxgrid.edit.js").SetBasePath(basePath);
            manifest.DefineScript("jqxGrid.Columnresize").SetUrl("Scripts/jqwidgets/jqxgrid.columnsresize.js").SetBasePath(basePath);
            manifest.DefineScript("jQxGrid.Pager").SetUrl("Scripts/jqwidgets/jqxgrid.pager.js").SetBasePath(basePath);
            manifest.DefineScript("jQxGrid.Sort").SetUrl("Scripts/jqwidgets/jqxgrid.sort.js").SetBasePath(basePath);
            manifest.DefineScript("jQxGrid.Filter").SetUrl("Scripts/jqwidgets/jqxgrid.filter.js").SetBasePath(basePath);
            manifest.DefineScript("jQxData").SetUrl("Scripts/jqwidgets/jqxdata.js").SetBasePath(basePath);
            manifest.DefineScript("jQxMenu").SetUrl("Scripts/jqwidgets/jqxmenu.js").SetBasePath(basePath);
            manifest.DefineScript("jQxSelection").SetUrl("Scripts/jqwidgets/jqxgrid.selection.js").SetBasePath(basePath);

            //view javascripts
            manifest.DefineScript("piPartySettings").SetUrl("pi.partysettings.js");
            manifest.DefineScript("CommonMethods").SetUrl("viewScripts/pi.partycommon.js");
            manifest.DefineScript("Home").SetUrl("viewScripts/pi.partyusers.js");

            //styles css
            manifest.DefineStyle("PIParty").SetUrl("PI.Party.css");
            manifest.DefineStyle("jQxBase").SetUrl("Styles/jqwidget/jqx.base-common.css").SetBasePath(basePath);
        }
    }
}