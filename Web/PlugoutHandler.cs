using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml;

namespace lucidcode.LucidScribe.Plugout.Yocto.PowerRelay
{
    public class PlugoutHandler : Interface.LucidPlugoutBase
    {
        private Boolean Failed = false;

        public override string Name
        {
            get { return "Web"; }
        }

        public override bool Initialize()
        {
            return true;
        }

        public override void Dispose()
        {
            return;
        }

        public override void Trigger()
        {
            try
            {
                if (Failed) return;

                var settings = new XmlDocument();
                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\lucidcode\Lucid Scribe\Plugouts\Web.lsd";
                settings.Load(path);
                var url = settings.DocumentElement.SelectSingleNode("Plugout/Url").InnerText;
                var processStartInfo = new ProcessStartInfo("explorer.exe", url)
                {
                    UseShellExecute = true,
                    Verb = "open"
                };
                Process.Start(processStartInfo);
            }
            catch (Exception ex)
            {
                Failed = true;
                MessageBox.Show(ex.Message, "Web Plugout Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
