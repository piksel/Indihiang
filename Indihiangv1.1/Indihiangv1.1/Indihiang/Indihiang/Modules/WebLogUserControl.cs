using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

using Indihiang.Cores;
using Indihiang.Cores.Features;
namespace Indihiang.Modules
{
    public partial class WebLogUserControl : UserControl
    {
        public const int EM_REPLACESEL = 0xc2;
        public const int EM_SETMODIFY = 0xb9;

        private Guid _loadingId = Guid.NewGuid();

        

        public WebLogUserControl()
        {
            InitializeComponent();
        }        

        public void AddLogStatus(string logMessage)
        {
            string newLog = String.Format("\r\n{0}", logMessage);
            if (txtLog.IsHandleCreated)
            {
                txtLog.Select(txtLog.TextLength, txtLog.TextLength);
                SendMessage(txtLog.Handle, EM_REPLACESEL, new IntPtr(-1), newLog);
                SendMessage(txtLog.Handle, EM_SETMODIFY, new IntPtr(0), IntPtr.Zero);
            }
            else
                txtLog.AppendText(newLog);                      
        }
        public void Populate(LogParser parser)
        {
            if (parser.UseParallel)
                PopulateParalel(parser);
            else
                PopulateNonParallel(parser);
        }

        private void PopulateNonParallel(LogParser parser)
        {
            string id = "";
            for (int i = 0; i < parser.Features.Count; i++)
            {
                switch (parser.Features[i].FeatureName)
                {
                    case LogFeature.GENERAL:
                        id = LogFeature.GENERAL.ToString();
                        GeneralControl uc1 = new GeneralControl();
                        uc1.FileNames = IndihiangHelper.ParseFile(parser.FileName);
                        Attach(uc1, id, "General", parser.Features[i].Items);

                        break;
                    case LogFeature.USERAGENT:
                        id = LogFeature.USERAGENT.ToString();
                        UserAgentControl uc2 = new UserAgentControl();
                        Attach(uc2, id, "User Agent", parser.Features[i].Items);

                        break;
                    case LogFeature.HITS:
                        id = LogFeature.HITS.ToString();
                        HitsControl uc3 = new HitsControl();
                        Attach(uc3, id, "Hits", parser.Features[i].Items);

                        break;
                    case LogFeature.ACCESS:
                        id = LogFeature.ACCESS.ToString();
                        AccessPageControl uc4 = new AccessPageControl();
                        Attach(uc4, id, "Access Page", parser.Features[i].Items);

                        break;
                    case LogFeature.IPADDRESS:
                        id = LogFeature.IPADDRESS.ToString();
                        IPAddressControl uc5 = new IPAddressControl();
                        Attach(uc5, id, "IP Address", parser.Features[i].Items);

                        break;
                    case LogFeature.STATUS:
                        id = LogFeature.STATUS.ToString();
                        AccessStatusControl uc6 = new AccessStatusControl();
                        Attach(uc6, id, "HTTP Status", parser.Features[i].Items);

                        break;
                    case LogFeature.BANDWIDTH:
                        id = LogFeature.BANDWIDTH.ToString();
                        BandwidthControl uc7 = new BandwidthControl();
                        Attach(uc7, id, "Bandwidth", parser.Features[i].Items);

                        break;
                    case LogFeature.REQUEST:
                        id = LogFeature.REQUEST.ToString();
                        RequestProcessingControl uc8 = new RequestProcessingControl();
                        Attach(uc8, id, "Processing Request", parser.Features[i].Items);

                        break;
                }
            }
            tabMainLog.SelectedTab = tabMainLog.TabPages[LogFeature.GENERAL.ToString()];
        }

        private void PopulateParalel(LogParser parser)
        {
            string id = "";
            //foreach (var feature in parser.ParallelFeatures.GetConsumingEnumerable())
            string info = string.Empty;
            for (int i = 0; i < parser.ParallelFeatures.Count;i++)
            {
                BaseLogAnalyzeFeature feature = parser.ParallelFeatures[i];
                switch (feature.FeatureName)
                {
                    case LogFeature.GENERAL:
                        info = String.Format("{0:yyyy/MM/dd HH:mm:ss}[info]: {1}", DateTime.Now, "Render GENERAL");
                        AddLogStatus(info);

                        id = LogFeature.GENERAL.ToString();
                        GeneralControl uc1 = new GeneralControl();
                        uc1.FileNames = IndihiangHelper.ParseFile(parser.FileName);
                        Attach(uc1, id, "General", feature.Items);
                        break;
                    case LogFeature.USERAGENT:
                        info = String.Format("{0:yyyy/MM/dd HH:mm:ss}[info]: {1}", DateTime.Now, "Render USERAGENT");
                        AddLogStatus(info);

                        id = LogFeature.USERAGENT.ToString();
                        UserAgentControl uc2 = new UserAgentControl();
                        Attach(uc2, id, "User Agent", feature.Items);
                        break;
                    case LogFeature.HITS:
                        info = String.Format("{0:yyyy/MM/dd HH:mm:ss}[info]: {1}", DateTime.Now, "Render HITS");
                        AddLogStatus(info);

                        id = LogFeature.HITS.ToString();
                        HitsControl uc3 = new HitsControl();
                        Attach(uc3, id, "Hits", feature.Items);
                        break;
                    case LogFeature.ACCESS:
                        info = String.Format("{0:yyyy/MM/dd HH:mm:ss}[info]: {1}", DateTime.Now, "Render ACCESS");
                        AddLogStatus(info);

                        id = LogFeature.ACCESS.ToString();
                        AccessPageControl uc4 = new AccessPageControl();
                        Attach(uc4, id, "Access Page", feature.Items);
                        break;
                    case LogFeature.IPADDRESS:
                        info = String.Format("{0:yyyy/MM/dd HH:mm:ss}[info]: {1}", DateTime.Now, "Render IPADDRESS");
                        AddLogStatus(info);

                        id = LogFeature.IPADDRESS.ToString();
                        IPAddressControl uc5 = new IPAddressControl();
                        Attach(uc5, id, "IP Address", feature.Items);
                        break;
                    case LogFeature.STATUS:
                        info = String.Format("{0:yyyy/MM/dd HH:mm:ss}[info]: {1}", DateTime.Now, "Render STATUS");
                        AddLogStatus(info);

                        id = LogFeature.STATUS.ToString();
                        AccessStatusControl uc6 = new AccessStatusControl();
                        Attach(uc6, id, "HTTP Status", feature.Items);
                        break;
                    case LogFeature.BANDWIDTH:
                        info = String.Format("{0:yyyy/MM/dd HH:mm:ss}[info]: {1}", DateTime.Now, "Render BANDWIDTH");
                        AddLogStatus(info);

                        id = LogFeature.BANDWIDTH.ToString();
                        BandwidthControl uc7 = new BandwidthControl();
                        Attach(uc7, id, "Bandwidth", feature.Items);
                        info = String.Format("{0:yyyy/MM/dd HH:mm:ss}[info]: {1}", DateTime.Now, "Render BANDWIDTH was done");
                        AddLogStatus(info);

                        break;
                    case LogFeature.REQUEST:
                        info = String.Format("{0:yyyy/MM/dd HH:mm:ss}[info]: {1}", DateTime.Now, "Render REQUEST");
                        AddLogStatus(info);

                        id = LogFeature.REQUEST.ToString();
                        RequestProcessingControl uc8 = new RequestProcessingControl();
                        Attach(uc8, id, "Processing Request", feature.Items);

                        info = String.Format("{0:yyyy/MM/dd HH:mm:ss}[info]: {1}", DateTime.Now, "Render REQUEST was done");
                        AddLogStatus(info);
                        break;
                }
                System.Threading.Thread.Sleep(100);
            }
            tabMainLog.SelectedTab = tabMainLog.TabPages[LogFeature.GENERAL.ToString()];
        }

        private void Attach(UserControl control, string id,string name,Dictionary<string, LogCollection> dataSource)
        {
            tabMainLog.TabPages.Add(id, name, 0);
            tabMainLog.TabPages[id].Controls.Add(control);
            control.Dock = DockStyle.Fill;
            ((BaseControl)control).DataSource = dataSource;
            ((BaseControl)control).Populate();
            //((BaseControl)control).DataSource.Clear();
        }

        public void ShowLoadingControl()
        {
            LoadingControl uc = new LoadingControl();
            tabMainLog.TabPages.Add(_loadingId.ToString(), "Analyzing...", 1);
            tabMainLog.TabPages[_loadingId.ToString()].Controls.Add(uc);
            uc.Dock = DockStyle.Fill;
            uc.Start();

            tabMainLog.SelectedTab = tabMainLog.TabPages[_loadingId.ToString()];

        }
        public void HideLoadingControl()
        {
            for (int i = 0; i < tabMainLog.TabPages[_loadingId.ToString()].Controls.Count; i++)
                if (tabMainLog.TabPages[_loadingId.ToString()].Controls[i] is LoadingControl)
                {
                    ((LoadingControl)tabMainLog.TabPages[_loadingId.ToString()].Controls[i]).Stop();
                    break;
                }

            tabMainLog.TabPages.Clear();
        }

        private void WebLogUserControl_Load(object sender, EventArgs e)
        {
            Initialize();
        }

        private void Initialize()
        {
            cboStatus.SelectedIndex = 0;
        }


        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, Int32 Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, Int32 Msg, IntPtr wParam, String lParam);

    }
}